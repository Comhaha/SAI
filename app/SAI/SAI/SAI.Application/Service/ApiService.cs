using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Http.Headers;
using System.Net.WebSockets;
using System.Threading;
using SAI.SAI.App.Models;

namespace SAI.SAI.Application.Service
{
    // WebSocket 메시지 클래스들
    public class WebSocketMessage
    {
        public string type { get; set; }
        public string task_id { get; set; }
        public string status { get; set; }
        public double progress { get; set; }
        public string message { get; set; }
        public string[] logs { get; set; }
        public object results { get; set; }
        public string error { get; set; }
    }

    public class TrainingProgressEventArgs : EventArgs
    {
        public string TaskId { get; set; }
        public string Status { get; set; }
        public double Progress { get; set; }
        public string Message { get; set; }
        public string[] Logs { get; set; }
        public object Results { get; set; }
        public string Error { get; set; }
    }

    public class ApiService : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private ClientWebSocket _webSocket;
        private CancellationTokenSource _webSocketCts;
        private bool _disposed = false;

        // WebSocket 이벤트 정의
        public event EventHandler<TrainingProgressEventArgs> TrainingProgressReceived;
        public event EventHandler<string> WebSocketError;
        public event EventHandler WebSocketConnected;
        public event EventHandler WebSocketDisconnected;

        public ApiService(string baseUrl)
        {
            _baseUrl = baseUrl;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(baseUrl);
            
            // 학습 프로세스를 위한 긴 타임아웃 설정 (10분)
            _httpClient.Timeout = TimeSpan.FromMinutes(10);
        }

        // WebSocket 연결
        public async Task<bool> ConnectWebSocket(string taskId)
        {
            try
            {
                // 기존 연결이 있다면 종료
                await DisconnectWebSocket();

                _webSocket = new ClientWebSocket();
                _webSocketCts = new CancellationTokenSource();

                // WebSocket URL 구성 (HTTP URL을 WebSocket URL로 변환)
                var wsUrl = _baseUrl.Replace("http://", "ws://").Replace("https://", "wss://");
                var uri = new Uri($"{wsUrl}/ws/training/{taskId}");

                Console.WriteLine($"[INFO] WebSocket 연결 시도: {uri}");
                Console.WriteLine($"[DEBUG] Base URL: {_baseUrl}");
                Console.WriteLine($"[DEBUG] WS URL: {wsUrl}");
                Console.WriteLine($"[DEBUG] Task ID: {taskId}");
                
                // 타임아웃을 10초로 설정
                _webSocketCts.CancelAfter(TimeSpan.FromSeconds(10));
                
                await _webSocket.ConnectAsync(uri, _webSocketCts.Token);
                
                Console.WriteLine($"[INFO] WebSocket 연결 성공 - State: {_webSocket.State}");
                
                // 연결 상태 재확인
                if (_webSocket.State != WebSocketState.Open)
                {
                    Console.WriteLine($"[ERROR] WebSocket 연결 후 상태가 Open이 아님: {_webSocket.State}");
                    return false;
                }
                
                WebSocketConnected?.Invoke(this, EventArgs.Empty);

                // 취소 토큰 재설정 (연결용 타임아웃 해제)
                _webSocketCts?.Cancel();
                _webSocketCts?.Dispose();
                _webSocketCts = new CancellationTokenSource();

                // 메시지 수신 루프 시작
                _ = Task.Run(async () => await ReceiveLoop(_webSocketCts.Token));

                // 연결 확인을 위한 초기 ping 전송
                await Task.Delay(100); // 잠시 대기 후 ping 전송
                await SendPing();
                
                Console.WriteLine($"[INFO] WebSocket 설정 완료, 수신 루프 시작됨");

                return true;
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine($"[ERROR] WebSocket 연결 타임아웃: {taskId}");
                return false;
            }
            catch (WebSocketException wsEx)
            {
                Console.WriteLine($"[ERROR] WebSocket 연결 실패 - WebSocketException: {wsEx.Message}");
                Console.WriteLine($"[ERROR] WebSocket Error Code: {wsEx.WebSocketErrorCode}");
                Console.WriteLine($"[ERROR] Native Error Code: {wsEx.NativeErrorCode}");
                WebSocketError?.Invoke(this, wsEx.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] WebSocket 연결 실패 - Exception: {ex.Message}");
                Console.WriteLine($"[ERROR] Exception Type: {ex.GetType().Name}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"[ERROR] Inner Exception: {ex.InnerException.Message}");
                }
                WebSocketError?.Invoke(this, ex.Message);
                return false;
            }
        }

        // WebSocket 연결 종료
        public async Task DisconnectWebSocket()
        {
            try
            {
                if (_webSocketCts != null && !_webSocketCts.Token.IsCancellationRequested)
                {
                    _webSocketCts.Cancel();
                    Console.WriteLine($"[INFO] WebSocket 취소 토큰 설정됨");
                }

                if (_webSocket != null)
                {
                    try
                    {
                        if (_webSocket.State == WebSocketState.Open)
                        {
                            Console.WriteLine($"[INFO] WebSocket 정상 종료 시도");
                            await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                            Console.WriteLine($"[INFO] WebSocket 정상 종료 완료");
                        }
                        else if (_webSocket.State == WebSocketState.Connecting)
                        {
                            Console.WriteLine($"[INFO] WebSocket 연결 중 - 강제 종료");
                            _webSocket.Abort();
                        }
                        else
                        {
                            Console.WriteLine($"[INFO] WebSocket 상태: {_webSocket.State} - 종료 불필요");
                        }
                    }
                    catch (WebSocketException ex) when (ex.WebSocketErrorCode == System.Net.WebSockets.WebSocketError.InvalidState)
                    {
                        Console.WriteLine($"[INFO] WebSocket이 이미 종료된 상태입니다: {ex.Message}");
                    }
                    catch (ObjectDisposedException)
                    {
                        Console.WriteLine($"[INFO] WebSocket이 이미 정리되었습니다");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[WARNING] WebSocket 종료 중 오류: {ex.Message}");
                    }
                    finally
                    {
                        try
                        {
                            _webSocket.Dispose();
                            Console.WriteLine($"[INFO] WebSocket 리소스 정리 완료");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"[WARNING] WebSocket 리소스 정리 중 오류: {ex.Message}");
                        }
                    }
                }

                try
                {
                    _webSocketCts?.Dispose();
                    Console.WriteLine($"[INFO] WebSocket 취소 토큰 정리 완료");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[WARNING] 취소 토큰 정리 중 오류: {ex.Message}");
                }

                _webSocket = null;
                _webSocketCts = null;

                WebSocketDisconnected?.Invoke(this, EventArgs.Empty);
                Console.WriteLine($"[INFO] WebSocket 연결 해제 완료");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] WebSocket 종료 중 예상치 못한 오류: {ex.Message}");
            }
        }

        // WebSocket 메시지 수신 루프
        private async Task ReceiveLoop(CancellationToken cancellationToken)
        {
            var buffer = new byte[1024 * 16]; // 버퍼 크기를 16KB로 증가
            var messageBuffer = new MemoryStream(); // 완전한 메시지를 위한 버퍼
            
            try
            {
                while (_webSocket.State == WebSocketState.Open && !cancellationToken.IsCancellationRequested)
                {
                    WebSocketReceiveResult result;
                    
                    try
                    {
                        result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);
                    }
                    catch (OperationCanceledException)
                    {
                        Console.WriteLine($"[INFO] WebSocket 수신이 취소되었습니다.");
                        break;
                    }
                    catch (WebSocketException ex) when (ex.WebSocketErrorCode == System.Net.WebSockets.WebSocketError.ConnectionClosedPrematurely)
                    {
                        Console.WriteLine($"[INFO] WebSocket 연결이 미리 종료되었습니다.");
                        break;
                    }
                    catch (WebSocketException ex)
                    {
                        Console.WriteLine($"[WARNING] WebSocket 오류: {ex.Message}");
                        break;
                    }
                    
                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        Console.WriteLine($"[INFO] WebSocket 종료 메시지 수신됨");
                        break;
                    }
                    
                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        // 수신된 데이터를 메시지 버퍼에 추가
                        messageBuffer.Write(buffer, 0, result.Count);
                        
                        // 메시지가 완전히 수신되었는지 확인
                        if (result.EndOfMessage)
                        {
                            try
                            {
                                // 완전한 메시지 추출
                                var completeMessage = Encoding.UTF8.GetString(messageBuffer.ToArray());
                                Console.WriteLine($"[DEBUG] 완전한 WebSocket 메시지 수신 (길이: {completeMessage.Length})");
                                
                                // 너무 긴 메시지는 일부만 로그에 출력
                                if (completeMessage.Length > 500)
                                {
                                    Console.WriteLine($"[DEBUG] 메시지 미리보기: {completeMessage.Substring(0, 200)}...(생략)");
                                }
                                else
                                {
                                    Console.WriteLine($"[DEBUG] WebSocket 메시지: {completeMessage}");
                                }
                                
                                // JSON 파싱 시도
                                var progressData = JsonSerializer.Deserialize<WebSocketMessage>(completeMessage);
                                
                                if (progressData.type == "progress")
                                {
                                    var args = new TrainingProgressEventArgs
                                    {
                                        TaskId = progressData.task_id,
                                        Status = progressData.status,
                                        Progress = progressData.progress,
                                        Message = progressData.message,
                                        Logs = progressData.logs,
                                        Results = progressData.results,
                                        Error = progressData.error
                                    };
                                    
                                    TrainingProgressReceived?.Invoke(this, args);
                                }
                                else if (progressData.type == "pong")
                                {
                                    Console.WriteLine("[DEBUG] Pong 메시지 수신됨");
                                }
                            }
                            catch (JsonException ex)
                            {
                                Console.WriteLine($"[ERROR] WebSocket 메시지 JSON 파싱 실패: {ex.Message}");
                                
                                // 디버깅을 위해 파싱 실패한 메시지의 시작 부분만 출력
                                var failedMessage = Encoding.UTF8.GetString(messageBuffer.ToArray());
                                var preview = failedMessage.Length > 200 ? failedMessage.Substring(0, 200) : failedMessage;
                                Console.WriteLine($"[DEBUG] 파싱 실패한 메시지 미리보기: {preview}");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"[ERROR] WebSocket 메시지 처리 중 예상치 못한 오류: {ex.Message}");
                            }
                            finally
                            {
                                // 메시지 버퍼 초기화 (다음 메시지를 위해)
                                messageBuffer.SetLength(0);
                                messageBuffer.Position = 0;
                            }
                        }
                        else
                        {
                            // 메시지가 아직 완전하지 않음 - 계속 수신
                            Console.WriteLine($"[DEBUG] 부분 메시지 수신됨 (현재 버퍼 크기: {messageBuffer.Length} bytes)");
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine($"[INFO] WebSocket 수신 루프 취소됨");
            }
            catch (WebSocketException ex) when (ex.WebSocketErrorCode == System.Net.WebSockets.WebSocketError.InvalidState)
            {
                Console.WriteLine($"[INFO] WebSocket이 이미 Aborted 상태입니다. 정상적인 취소 과정입니다.");
            }
            catch (WebSocketException ex)
            {
                Console.WriteLine($"[WARNING] WebSocket 수신 중 오류: {ex.Message} (오류 코드: {ex.WebSocketErrorCode})");
                WebSocketError?.Invoke(this, ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] WebSocket 수신 중 예상치 못한 오류: {ex.Message}");
                WebSocketError?.Invoke(this, ex.Message);
            }
            finally
            {
                messageBuffer?.Dispose();
                Console.WriteLine($"[INFO] WebSocket 수신 루프 종료됨");
            }
        }

        // Ping 메시지 전송 (연결 유지용)
        public async Task SendPing()
        {
            try
            {
                if (_webSocket != null && _webSocket.State == WebSocketState.Open)
                {
                    var pingMessage = JsonSerializer.Serialize(new { type = "ping" });
                    var bytes = Encoding.UTF8.GetBytes(pingMessage);
                    await _webSocket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Ping 전송 실패: {ex.Message}");
            }
        }

        public async Task<HttpResponseMessage> PostAsync(string endpoint, HttpContent content)
        {
            return await _httpClient.PostAsync(endpoint, content);
        }

        public async Task<HttpResponseMessage> GetAsync(string endpoint)
        {
            return await _httpClient.GetAsync(endpoint);
        }

        // 서버 학습 취소 메서드 추가
        public async Task<bool> CancelTraining(string taskId)
        {
            try
            {
                Console.WriteLine($"[INFO] 서버 학습 취소 요청: taskId={taskId}");
                
                var response = await _httpClient.PostAsync(
                    $"/api/training/cancel/{taskId}",
                    new StringContent(string.Empty, Encoding.UTF8, "application/json")
                );

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"[INFO] 서버 학습 취소 성공: taskId={taskId}");
                    return true;
                }
                else
                {
                    Console.WriteLine($"[WARNING] 서버 학습 취소 실패: taskId={taskId}, StatusCode={response.StatusCode}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] 서버 학습 취소 중 오류 발생: taskId={taskId}, Error={ex.Message}");
                return false;
            }
        }

        public async Task<InferenceResult> RunInference(string imagePath, double conf)
        {
            try
            {
                // 이미지를 Base64로 인코딩
                string imageBase64 = Convert.ToBase64String(File.ReadAllBytes(imagePath));
                string imageName = Path.GetFileName(imagePath);

                // API 요청 데이터 구성
                var requestData = new
                {
                    image_base64 = imageBase64,
                    confidence = conf,
                    image_name = imageName
                };

                // API 호출
                var response = await _httpClient.PostAsync(
                    "/api/inference",
                    new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json")
                );

                if (!response.IsSuccessStatusCode)
                {
                    return new InferenceResult
                    {
                        Success = false,
                        Error = $"API 호출 실패: {response.StatusCode}"
                    };
                }

                // 응답 파싱
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ApiInferenceResponse>(responseContent);

                if (!result.success)
                {
                    return new InferenceResult
                    {
                        Success = false,
                        Error = result.error
                    };
                }

                // Local 환경과 동일한 경로에 Base64 이미지를 파일로 저장
                string filename = Path.GetFileNameWithoutExtension(imagePath);
                string extension = Path.GetExtension(imagePath);
                string resultImagePath = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "SAI.Application","Python","runs","result",
                    $"{filename}_result{extension}");
                resultImagePath = Path.GetFullPath(resultImagePath);
                
                // 디렉토리가 없으면 생성
                string resultDir = Path.GetDirectoryName(resultImagePath);
                if (!Directory.Exists(resultDir))
                {
                    Directory.CreateDirectory(resultDir);
                    Console.WriteLine($"[INFO] 결과 디렉토리 생성: {resultDir}");
                }

                File.WriteAllBytes(resultImagePath, Convert.FromBase64String(result.result_image_base64));
                Console.WriteLine($"[INFO] 서버 추론 결과를 Local과 동일한 경로에 저장: {resultImagePath}");

                return new InferenceResult
                {
                    Success = true,
                    ResultImage = resultImagePath,
                    OriginalName = result.original_name
                };
            }
            catch (Exception ex)
            {
                return new InferenceResult
                {
                    Success = false,
                    Error = $"API 호출 중 오류 발생: {ex.Message}"
                };
            }
        }

        private class ApiInferenceResponse
        {
            public bool success { get; set; }
            public string result_image_base64 { get; set; }
            public string original_name { get; set; }
            public string error { get; set; }
        }
        public class InferenceResult
        {
            public bool Success { get; set; }
            public string ResultImage { get; set; }
            public double InferenceTime { get; set; }
            public string Error { get; set; }
            public string OriginalName { get; set; } // 추가된 속성
        }

        // IDisposable 구현
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    try
                    {
                        // WebSocket 정리
                        if (_webSocketCts != null && !_webSocketCts.Token.IsCancellationRequested)
                        {
                            _webSocketCts.Cancel();
                        }
                        
                        _webSocket?.Dispose();
                        _webSocketCts?.Dispose();
                        
                        // HttpClient 정리
                        _httpClient?.Dispose();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[ERROR] ApiService Dispose 중 오류: {ex.Message}");
                    }
                }
                
                _disposed = true;
            }
        }

        ~ApiService()
        {
            Dispose(false);
        }
    }
} 