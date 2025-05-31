using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Http.Headers;
using SAI.SAI.App.Models;
using System.Net.WebSockets;
using System.Threading;

namespace SAI.SAI.Application.Service
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private ClientWebSocket _webSocket;

        public ApiService(string baseUrl)
        {
            _baseUrl = baseUrl;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(baseUrl);
            
            // 학습 프로세스를 위한 긴 타임아웃 설정 (10분)
            _httpClient.Timeout = TimeSpan.FromMinutes(10);
        }
        
        // 실시간 진행률 모니터링용 이벤트
        public event Action<string, float, string> ProgressUpdated;
        public event Action<string, object> TrainingCompleted;
        public event Action<string, string> TrainingFailed;
        public event Action<string, string> TrainingCancelled;
        
        // 웹소켓 연결 및 실시간 진행률 모니터링
        public async Task<bool> ConnectWebSocketAsync(string taskId, CancellationToken cancellationToken = default)
        {
            try
            {
                _webSocket = new ClientWebSocket();
                
                // WebSocket URL 구성 (HTTP를 WS로 변경)
                var wsUri = new Uri(_baseUrl.Replace("http://", "ws://").Replace("https://", "wss://") + $"/ws/training/{taskId}");
                Console.WriteLine($"[WEBSOCKET] 연결 시도: {wsUri}");
                
                await _webSocket.ConnectAsync(wsUri, cancellationToken);
                Console.WriteLine($"[WEBSOCKET] 연결 성공: task_id={taskId}");
                
                // 백그라운드에서 메시지 수신
                _ = Task.Run(async () => await ReceiveWebSocketMessages(taskId, cancellationToken));
                
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[WEBSOCKET] 연결 실패: {ex.Message}");
                return false;
            }
        }
        
        // 웹소켓 메시지 수신
        private async Task ReceiveWebSocketMessages(string taskId, CancellationToken cancellationToken)
        {
            var buffer = new byte[1024 * 4];
            
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
                        Console.WriteLine($"[WEBSOCKET] 메시지 수신이 취소됨 - 정상적인 종료");
                        break;
                    }
                    catch (WebSocketException wse)
                    {
                        Console.WriteLine($"[WEBSOCKET] 웹소켓 예외로 수신 종료: {wse.Message}");
                        break;
                    }
                    
                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                        Console.WriteLine($"[WEBSOCKET] 메시지 수신: {message}");
                        
                        try
                        {
                            var data = JsonSerializer.Deserialize<JsonElement>(message);
                            var messageType = data.GetProperty("type").GetString();
                            
                            switch (messageType)
                            {
                                case "connected":
                                    Console.WriteLine($"[WEBSOCKET] 연결 확인: {data.GetProperty("message").GetString()}");
                                    break;
                                    
                                case "progress":
                                    var progress = data.GetProperty("progress").GetSingle();
                                    var progressMessage = data.GetProperty("message").GetString();
                                    ProgressUpdated?.Invoke(taskId, progress, progressMessage);
                                    Console.WriteLine($"[WEBSOCKET] 진행률 업데이트: {progress}% - {progressMessage}");
                                    break;
                                    
                                case "completed":
                                    var results = data.GetProperty("results");
                                    TrainingCompleted?.Invoke(taskId, results);
                                    Console.WriteLine($"[WEBSOCKET] 학습 완료");
                                    return; // 완료되면 수신 종료
                                    
                                case "failed":
                                    var error = data.GetProperty("error").GetString();
                                    TrainingFailed?.Invoke(taskId, error);
                                    Console.WriteLine($"[WEBSOCKET] 학습 실패: {error}");
                                    return; // 실패하면 수신 종료
                                    
                                case "cancelled":
                                    var cancelReason = data.GetProperty("message").GetString();
                                    TrainingCancelled?.Invoke(taskId, cancelReason);
                                    Console.WriteLine($"[WEBSOCKET] 학습 취소: {cancelReason}");
                                    return; // 취소되면 수신 종료
                                    
                                case "ping":
                                    // Ping에 대한 응답으로 pong 전송
                                    try
                                    {
                                        await SendWebSocketMessage("pong");
                                    }
                                    catch (Exception pongEx)
                                    {
                                        Console.WriteLine($"[WEBSOCKET] Pong 전송 실패: {pongEx.Message}");
                                    }
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"[WEBSOCKET] 메시지 파싱 오류: {ex.Message}");
                        }
                    }
                    else if (result.MessageType == WebSocketMessageType.Close)
                    {
                        Console.WriteLine($"[WEBSOCKET] 서버에서 연결 종료 요청됨");
                        break;
                    }
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine($"[WEBSOCKET] 수신 작업이 취소됨 - 정상적인 종료");
            }
            catch (WebSocketException wse)
            {
                Console.WriteLine($"[WEBSOCKET] 웹소켓 연결 오류: {wse.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[WEBSOCKET] 수신 중 예상치 못한 오류: {ex.Message}");
            }
            finally
            {
                Console.WriteLine($"[WEBSOCKET] 메시지 수신 루프 종료");
                
                // 웹소켓 상태 확인 후 안전하게 정리
                try
                {
                    if (_webSocket != null && _webSocket.State == WebSocketState.Open)
                    {
                        await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "완료", CancellationToken.None);
                        Console.WriteLine($"[WEBSOCKET] 웹소켓 정상 종료 완료");
                    }
                }
                catch (Exception closeEx)
                {
                    Console.WriteLine($"[WEBSOCKET] 웹소켓 종료 중 예외 (무시됨): {closeEx.Message}");
                }
                finally
                {
                    _webSocket?.Dispose();
                    _webSocket = null;
                    Console.WriteLine($"[WEBSOCKET] 연결 정리 완료");
                }
            }
        }
        
        // 웹소켓 메시지 전송
        private async Task SendWebSocketMessage(string message)
        {
            try
            {
                if (_webSocket != null && _webSocket.State == WebSocketState.Open)
                {
                    var bytes = Encoding.UTF8.GetBytes(message);
                    await _webSocket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
                }
                else
                {
                    Console.WriteLine($"[WEBSOCKET] 메시지 전송 실패 - 웹소켓 상태: {_webSocket?.State}");
                }
            }
            catch (WebSocketException wse)
            {
                Console.WriteLine($"[WEBSOCKET] 메시지 전송 중 웹소켓 예외: {wse.Message}");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine($"[WEBSOCKET] 메시지 전송이 취소됨");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[WEBSOCKET] 메시지 전송 중 예외: {ex.Message}");
            }
        }
        
        // 웹소켓 연결 해제
        public async Task DisconnectWebSocketAsync()
        {
            try
            {
                if (_webSocket != null)
                {
                    Console.WriteLine($"[WEBSOCKET] 연결 해제 시도 - 현재 상태: {_webSocket.State}");
                    
                    // 웹소켓이 Open 상태일 때만 정상적으로 닫기
                    if (_webSocket.State == WebSocketState.Open)
                    {
                        try
                        {
                            await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "클라이언트 종료", CancellationToken.None);
                            Console.WriteLine($"[WEBSOCKET] 정상적으로 연결 해제 완료");
                        }
                        catch (OperationCanceledException)
                        {
                            Console.WriteLine($"[WEBSOCKET] 연결 해제 중 취소됨 - 정상적인 종료 과정");
                        }
                        catch (WebSocketException wse)
                        {
                            Console.WriteLine($"[WEBSOCKET] 연결 해제 중 웹소켓 예외 (정상): {wse.Message}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"[WEBSOCKET] 웹소켓이 이미 {_webSocket.State} 상태 - 강제 정리");
                    }
                    
                    // 웹소켓 리소스 해제
                    _webSocket.Dispose();
                    _webSocket = null;
                    Console.WriteLine($"[WEBSOCKET] 웹소켓 리소스 정리 완료");
                }
            }
            catch (Exception ex)
            {
                // 모든 예외를 잡아서 로그만 출력하고 계속 진행
                Console.WriteLine($"[WEBSOCKET] 연결 해제 중 예외 (무시됨): {ex.GetType().Name} - {ex.Message}");
            }
            finally
            {
                // 확실하게 null로 설정
                _webSocket = null;
                Console.WriteLine($"[WEBSOCKET] 연결 해제 프로세스 완료");
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

                // Base64 이미지를 파일로 저장
                string resultImagePath = Path.Combine(
                    Path.GetDirectoryName(imagePath),
                    $"{Path.GetFileNameWithoutExtension(imagePath)}_result{Path.GetExtension(imagePath)}"
                );

                File.WriteAllBytes(resultImagePath, Convert.FromBase64String(result.result_image_base64));

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
    }
} 