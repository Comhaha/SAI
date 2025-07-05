using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SAI.SAI.App.Forms.Dialogs;
using SAI.SAI.App.Models;
using SAI.SAI.App.Views.Interfaces;
using SAI.SAI.Application.Service;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Net.WebSockets;
using SAI.SAI.App.Views.Pages;

namespace SAI.SAI.App.Presenters
{
    public class YoloTutorialPresenter
    {
        private readonly ITutorialInferenceView _itutorialInferenceView;
        private readonly IYoloTutorialView _yolotutorialview;
        private readonly PythonService _pythonService;
        private readonly ApiService _apiService;
        private DialogModelProgress _progressDialog;
        private DateTime _scriptStartTime; // 스크립트 실행 시작 시간
        private CancellationTokenSource _monitoringCancellationTokenSource; // 서버 모니터링 취소용
        private string _currentTaskId; // 현재 실행 중인 서버 학습 작업 ID
        private bool _userCancelled = false; // 사용자가 직접 취소했는지 여부
        private DialogStartcampInput dialogStartcampInput;

        //public YoloTutorialPresenter(IYoloTutorialView yolotutorialview)
        //{
        //    _yolotutorialview = yolotutorialview;
        //    _pythonService = new PythonService();

        //    _itutorialInferenceView = yolotutorialview as ITutorialInferenceView;

        //    _yolotutorialview.RunButtonClicked += OnRunButtonClicked;

        //}
        public YoloTutorialPresenter(IYoloTutorialView yolotutorialview, string serverUrl = "http://3.39.207.72:9000")
        // public YoloTutorialPresenter(IYoloTutorialView yolotutorialview, string serverUrl = "http://localhost:9000")
        {
            _yolotutorialview = yolotutorialview;
            _pythonService = new PythonService();
            _apiService = new ApiService(serverUrl);
            _itutorialInferenceView = yolotutorialview as ITutorialInferenceView;
            _yolotutorialview.RunButtonClicked += OnRunButtonClicked;
        }

        public YoloTutorialPresenter(DialogStartcampInput dialogStartcampInput)
        {
            this.dialogStartcampInput = dialogStartcampInput;
        }

        //private void OnRunButtonClicked(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        // 스크립트 실행 시작 시간 기록
        //        _scriptStartTime = DateTime.Now;
        //        Console.WriteLine($"[INFO] 스크립트 실행 시작 시간: {_scriptStartTime}");

        //        // 다이얼로그는 반드시 UI 스레드에서 실행되어야 함
        //        if (_yolotutorialview is Control viewControl && viewControl.InvokeRequired)
        //        {
        //            viewControl.Invoke(new Action(() =>
        //            {
        //                _progressDialog = new DialogModelProgress();
        //                _progressDialog.FormClosing += (s, args) =>
        //                {
        //                    if (args.CloseReason == CloseReason.UserClosing)
        //                    {
        //                        args.Cancel = true;  // 사용자가 닫으려고 할 때 취소
        //                    }
        //                };
        //                _progressDialog.Show();  // 비모달로 표시
        //                StartPythonScript();
        //            }));
        //        }
        //        else
        //        {
        //            _progressDialog = new DialogModelProgress();
        //            _progressDialog.FormClosing += (s, args) =>
        //            {
        //                if (args.CloseReason == CloseReason.UserClosing)
        //                {
        //                    args.Cancel = true;  // 사용자가 닫으려고 할 때 취소
        //                }
        //            };
        //            _progressDialog.Show();  // 비모달로 표시
        //            StartPythonScript();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error in OnRunButtonClicked: {ex}");
        //        _yolotutorialview.ShowErrorMessage($"실행 중 오류가 발생했습니다: {ex.Message}");
        //    }
        //}
        private void OnRunButtonClicked(object sender, EventArgs e)
        {
            try
            {
                // 스크립트 실행 시작 시간 기록
                _scriptStartTime = DateTime.Now;
                Console.WriteLine($"[INFO] 스크립트 실행 시작 시간: {_scriptStartTime}");

                // BlocklyModel에서 GpuType 가져오기
                var model = BlocklyModel.Instance;
                var gpuType = model.gpuType;

                Console.WriteLine($"[INFO] 선택된 GPU 타입: {gpuType}");

                // GpuType에 따라 로컬 또는 원격 실행 결정
                if (gpuType == GpuType.Server)
                {
                    Console.WriteLine("[INFO] 서버에서 모델 학습을 시작합니다.");

                    // 다이얼로그 표시 (UI 스레드에서)
                    if (_yolotutorialview is Control viewControl && viewControl.InvokeRequired)
                    {
                        viewControl.Invoke(new Action(async () =>
                        {
                            _progressDialog = new DialogModelProgress();
                            _progressDialog.SetPresenter(this); // 서버 모니터링 취소를 위한 Presenter 참조 설정
                            _progressDialog.FormClosing += (s, args) =>
                            {
                                if (args.CloseReason == CloseReason.UserClosing)
                                {
                                    args.Cancel = true;  // 사용자가 닫으려고 할 때 취소
                                }
                            };
                            _progressDialog.Show();  // 비모달로 표시

                            // 서버에 학습 요청 시작
                            await Task.Run(async () => {
                                try
                                {
                                    string taskId = await StartTrainingRemote(model);
                                    if (string.IsNullOrEmpty(taskId))
                                    {
                                        throw new Exception("서버에서 작업 ID를 받지 못했습니다.");
                                    }

                                    Console.WriteLine($"[INFO] 서버 학습 작업 ID: {taskId}");
                                    // MonitorTrainingProgress는 이미 내부적으로 호출됨
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"[ERROR] 원격 학습 시작 중 오류: {ex.Message}");
                                    if (_progressDialog != null && !_progressDialog.IsDisposed)
                                    {
                                        _progressDialog.Invoke(new Action(() => {
                                            _progressDialog.Close();
                                            _progressDialog.Dispose();
                                            _yolotutorialview.ShowErrorMessage($"원격 학습 시작 중 오류가 발생했습니다: {ex.Message}");
                                        }));
                                    }
                                }
                            });
                        }));
                    }
                    else
                    {
                        _progressDialog = new DialogModelProgress();
                        _progressDialog.SetPresenter(this); // 서버 모니터링 취소를 위한 Presenter 참조 설정
                        _progressDialog.FormClosing += (s, args) =>
                        {
                            if (args.CloseReason == CloseReason.UserClosing)
                            {
                                args.Cancel = true;  // 사용자가 닫으려고 할 때 취소
                            }
                        };
                        _progressDialog.Show();  // 비모달로 표시

                        // 서버에 학습 요청 시작
                        Task.Run(async () => {
                            try
                            {
                                string taskId = await StartTrainingRemote(model);
                                if (string.IsNullOrEmpty(taskId))
                                {
                                    throw new Exception("서버에서 작업 ID를 받지 못했습니다.");
                                }

                                Console.WriteLine($"[INFO] 서버 학습 작업 ID: {taskId}");
                                // MonitorTrainingProgress는 이미 내부적으로 호출됨
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"[ERROR] 원격 학습 시작 중 오류: {ex.Message}");
                                if (_progressDialog != null && !_progressDialog.IsDisposed)
                                {
                                    _progressDialog.Close();
                                    _progressDialog.Dispose();
                                    _yolotutorialview.ShowErrorMessage($"원격 학습 시작 중 오류가 발생했습니다: {ex.Message}");
                                }
                            }
                        });
                    }
                }
                else // GpuType.Local
                {
                    Console.WriteLine("[INFO] 로컬에서 모델 학습을 시작합니다.");

                    // 다이얼로그는 반드시 UI 스레드에서 실행되어야 함
                    if (_yolotutorialview is Control viewControl && viewControl.InvokeRequired)
                    {
                        viewControl.Invoke(new Action(() =>
                        {
                            _progressDialog = new DialogModelProgress();
                            _progressDialog.FormClosing += (s, args) =>
                            {
                                if (args.CloseReason == CloseReason.UserClosing)
                                {
                                    args.Cancel = true;  // 사용자가 닫으려고 할 때 취소
                                }
                            };
                            _progressDialog.Show();  // 비모달로 표시
                            StartPythonScript();
                        }));
                    }
                    else
                    {
                        _progressDialog = new DialogModelProgress();
                        _progressDialog.FormClosing += (s, args) =>
                        {
                            if (args.CloseReason == CloseReason.UserClosing)
                            {
                                args.Cancel = true;  // 사용자가 닫으려고 할 때 취소
                            }
                        };
                        _progressDialog.Show();  // 비모달로 표시
                        StartPythonScript();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in OnRunButtonClicked: {ex}");
                _yolotutorialview.ShowErrorMessage($"실행 중 오류가 발생했습니다: {ex.Message}");
            }
        }

        private void StartPythonScript()
        {
            Task.Run(() =>
            {
                Process process = null;
                try
                {
                    Console.WriteLine("[DEBUG] 표준 출력 스트림 설정 생략");

                    // 스크립트 진행률 파싱
                    process = _pythonService.RunPythonScript(
                        PythonService.Mode.Tutorial,
                        onOutput: text =>
                        {
                            try
                            {
                                if (string.IsNullOrEmpty(text)) return;

                                Console.WriteLine($"Python Output: {text}");

                                if (_yolotutorialview is Control logControl && logControl.InvokeRequired)
                                {
                                    logControl.Invoke(new Action(() => _yolotutorialview.AppendLog(text)));
                                }
                                else
                                {
                                    _yolotutorialview.AppendLog(text);
                                }

                                // PROGRESS: 로 시작하는 로그 찾아서 : 기준으로 끊고, 왼쪽은 progress, 오른쪽은 message로 파싱
                                // updateprogress 호출
                                if (text.StartsWith("PROGRESS:"))
                                {
                                    var parts = text.Substring(9).Split(new[] { ':' }, 2);
                                    if (parts.Length == 2 && double.TryParse(parts[0], out double progress))
                                    {
                                        string message = parts[1];

                                        // 태그 추출
                                        // "TRAIN" "DATASET" "ERROR" "INFO"
                                        string tag = "";
                                        var tagMatch = Regex.Match(message, @"\[(\w+)\]");
                                        if (tagMatch.Success)
                                        {
                                            tag = tagMatch.Groups[1].Value; 
                                        }

                                        //if (!string.IsNullOrEmpty(tag))
                                        //{
                                        //    // 태그 부분([TAG] )만 제거
                                        //    message = message.Substring(tagMatch.Length).Trim();
                                        //}

                                        if (_progressDialog != null && !_progressDialog.IsDisposed)
                                        {
                                            if (_progressDialog.InvokeRequired)
                                            {
                                                _progressDialog.Invoke(new Action(() =>
                                                {
                                                    if (!_progressDialog.IsDisposed)

                                                    {
                                                        _progressDialog.UpdateProgress(progress, message);
                                                    }
                                                }));
                                            }
                                            else
                                            {
                                                _progressDialog.UpdateProgress(progress, message);
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error in output handler: {ex}");
                            }
                        },
                        onError: text =>
                        {
                            try
                            {
                                if (string.IsNullOrEmpty(text)) return;

                                if (_progressDialog != null && !_progressDialog.IsDisposed)
                                {
                                    if (_progressDialog.InvokeRequired)
                                    {
                                        _progressDialog.Invoke(new Action(() =>
                                        {
                                            if (!_progressDialog.IsDisposed)
                                            {
                                                //_progressDialog.UpdateProgress(0, $"오류: {text}");
                                            }
                                        }));
                                    }
                                    else
                                    {
                                        //_progressDialog.UpdateProgress(0, $"오류: {text}");
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error in error handler: {ex}");
                            }
                        },
                        onException: ex =>
                        {
                            try
                            {
                                Console.WriteLine($"Python Exception: {ex}");

                                if (_yolotutorialview is Control errorControl && errorControl.InvokeRequired)
                                {
                                    errorControl.Invoke(new Action(() => _yolotutorialview.ShowErrorMessage($"오류가 발생했습니다: {ex.Message}")));
                                }
                                else
                                {
                                    _yolotutorialview.ShowErrorMessage($"오류가 발생했습니다: {ex.Message}");
                                }
                            }
                            catch (Exception innerEx)
                            {
                                Console.WriteLine($"Error in exception handler: {innerEx}");
                            }
                        },
                        blocklyModel: BlocklyModel.Instance
                    );

                    _progressDialog.SetProcess(process);

                    // 여기서 프로세스가 끝날 때까지 대기
                    process?.WaitForExit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in Python script execution: {ex}");
                    if (_yolotutorialview is Control errorView && errorView.InvokeRequired)
                    {
                        errorView.Invoke(new Action(() =>
                            _yolotutorialview.ShowErrorMessage($"스크립트 실행 중 오류가 발생했습니다: {ex.Message}")
                        ));
                    }
                    else
                    {
                        _yolotutorialview.ShowErrorMessage($"스크립트 실행 중 오류가 발생했습니다: {ex.Message}");
                    }
                }
                finally
                {
                    if (_progressDialog != null && !_progressDialog.IsDisposed)
                    {
                        if (_progressDialog.InvokeRequired)
                        {
                            _progressDialog.Invoke(new Action(() =>
                            {
                                if (!_progressDialog.IsDisposed)
                                {
                                    _yolotutorialview.AppendLog("스크립트가 종료됐습니다!");
                                    _progressDialog.Close();
                                    _progressDialog.Dispose();

                                    var baseDir   = AppDomain.CurrentDomain.BaseDirectory;
                                    var csvPath   = Path.Combine(baseDir,
                                        "SAI.Application","Python", "runs", "detect", "train", "results.csv");
                                    csvPath       = Path.GetFullPath(csvPath);
                                    _yolotutorialview.ShowTutorialTrainingChart(csvPath);
                                    
                            // 스크립트 종료시 추론 결과 이미지 확인 및 표시
                                    CheckAndShowInferenceResult();
                                }
                            }));
                        }
                        else
                        {
                            _yolotutorialview.AppendLog("스크립트가 종료됐습니다!");
                            _progressDialog.Close();
                            _progressDialog.Dispose();
                            
                            // 스크립트 종료시 추론 결과 이미지 확인 및 표시
                            CheckAndShowInferenceResult();
                        }
                    }
                }
            });
        }

        // 추론 결과를 확인하고 UI에 표시하는 메서드
        private void CheckAndShowInferenceResult()
        {
            try
            {
                // 블록 모델에서 이미지 경로 가져오기
                var model = BlocklyModel.Instance;
                string imagePath = model?.imgPath;
                
                if (string.IsNullOrEmpty(imagePath))
                {
                    Console.WriteLine("[WARNING] 이미지 경로가 없습니다.");
                    return;
                }
                
                Console.WriteLine($"[DEBUG] 원본 이미지 경로: {imagePath}");
                
                // 결과 이미지 경로 생성 (inference.py 스크립트와 동일한 방식으로)
                string resultImagePath = null;
                string directory = Path.GetDirectoryName(imagePath);
                string filename = Path.GetFileNameWithoutExtension(imagePath);
                string extension = Path.GetExtension(imagePath);
                resultImagePath = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "SAI.Application", "Python", "runs", "result",
                    $"{filename}_result{extension}");
                resultImagePath = Path.GetFullPath(resultImagePath);
                
                Console.WriteLine($"[DEBUG] 결과 이미지 경로: {resultImagePath}");
                
                if (File.Exists(resultImagePath))
                {
                    Console.WriteLine($"[INFO] 결과 이미지 파일 발견: {resultImagePath}");
                    
                    // 추론 결과 객체 생성
                    var result = new PythonService.InferenceResult
                    {
                        Success = true,
                        ResultImage = resultImagePath,
                        OriginalName = Path.GetFileName(imagePath)
                    };
                    
                    // UI 스레드에서 결과 표시
                    if (_itutorialInferenceView != null)
                    {
                        if (_yolotutorialview is Control viewControl && viewControl.InvokeRequired)
                        {
                            viewControl.Invoke(new Action(() => {
                                _itutorialInferenceView.ShowInferenceResult(result);
                            }));
                        }
                        else
                        {
                            _itutorialInferenceView.ShowInferenceResult(result);
                        }
                    }
                    else
                    {
                        Console.WriteLine("[ERROR] _itutorialInferenceView가 null입니다.");
                    }
                }
                else
                {
                    Console.WriteLine($"[WARNING] 결과 이미지 파일을 찾을 수 없습니다: {resultImagePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] 추론 결과 확인 중 오류 발생: {ex.Message}");
            }
        }

        // 추론시 PythonService에 구현된 추론스크립트 함수를 실행
        // 사용자 지정 imagePath와 conf값을 파이썬에 던져주면, 스크립트에서 그 값으로 추론을 진행
        public async void OnInferImageSelected(string imagePath, double conf)
        {
            try
            {
                var model = BlocklyModel.Instance;
                var gpuType = model.gpuType;

                PythonService.InferenceResult result;

                if (gpuType == GpuType.Server)
                {
                    Console.WriteLine("[INFO] 서버에서 추론을 수행합니다.");
                    result = await RunInferenceDirectRemote(imagePath, conf);
                }
                else
                {
                    Console.WriteLine("[INFO] 로컬에서 추론을 수행합니다.");
                    result = _pythonService.RunInference(imagePath, conf);
                }

                _itutorialInferenceView.ShowInferenceResult(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] 추론 중 오류 발생: {ex.Message}");
                var errorResult = new PythonService.InferenceResult
                {
                    Success = false,
                    Error = $"추론 중 오류 발생: {ex.Message}"
                };
                _itutorialInferenceView.ShowInferenceResult(errorResult);
            }
        }

        /*
        public PythonService.InferenceResult RunInferenceDirect(string imagePath, double conf)
        {
            Console.WriteLine($"[DEBUG] RunInferenceDirect() 실행: {imagePath}, conf={conf}");
            var result = _pythonService.RunInference(imagePath, conf);
            Console.WriteLine($"[DEBUG] RunInferenceDirect() 결과: success={result.Success}, image={result.ResultImage}, error={result.Error}");
            Console.WriteLine($"[LOG] RunInferenceDirect 결과: success={result.Success}, image={result.ResultImage}, error={result.Error}");
            Console.WriteLine($"[LOG] ResultImage 파일 존재 여부: {File.Exists(result.ResultImage)}");
            Console.WriteLine($"[INFO] 원본 이미지 파일명: {result.OriginalName}");
            return result;
        }
        */
        
        // RunButtonClicked 이벤트 구독 해제 메서드 추가
        public void UnsubscribeFromRunButtonClicked(IYoloTutorialView view)
        {
            if (view != null)
            {
                view.RunButtonClicked -= OnRunButtonClicked;
            }
        }

        public async Task<PythonService.InferenceResult> RunInferenceDirect(string imagePath, double conf)
        {
            try
            {
                Console.WriteLine($"[DEBUG] RunInferenceDirect() 실행: {imagePath}, conf={conf}");
                
                var model = BlocklyModel.Instance;
                var gpuType = model.gpuType;

                PythonService.InferenceResult result;

                if (gpuType == GpuType.Server)
                {
                    Console.WriteLine("[INFO] 서버에서 추론을 수행합니다.");
                    result = await RunInferenceDirectRemote(imagePath, conf);
                }
                else
                {
                    Console.WriteLine("[INFO] 로컬에서 추론을 수행합니다.");
                    result = _pythonService.RunInference(imagePath, conf);
                }

                Console.WriteLine($"[DEBUG] RunInferenceDirect() 결과: success={result.Success}, image={result.ResultImage}, error={result.Error}");
                Console.WriteLine($"[LOG] RunInferenceDirect 결과: success={result.Success}, image={result.ResultImage}, error={result.Error}");
                
                if (!string.IsNullOrEmpty(result.ResultImage))
                {
                    Console.WriteLine($"[LOG] ResultImage 파일 존재 여부: {File.Exists(result.ResultImage)}");
                }
                
                Console.WriteLine($"[INFO] 원본 이미지 파일명: {result.OriginalName}");
                
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] RunInferenceDirect 중 오류 발생: {ex.Message}");
                return new PythonService.InferenceResult
                {
                    Success = false,
                    Error = $"추론 중 오류 발생: {ex.Message}"
                };
            }
        }

        public async Task<PythonService.InferenceResult> RunInferenceDirectRemote(string imagePath, double conf)
        {
            try
            {
                var apiResult = await _apiService.RunInference(imagePath, conf);

                // ApiService.InferenceResult를 PythonService.InferenceResult로 변환
                return new PythonService.InferenceResult
                {
                    Success = apiResult.Success,
                    ResultImage = apiResult.ResultImage,
                    OriginalName = apiResult.OriginalName,
                    InferenceTime = apiResult.InferenceTime,
                    Error = apiResult.Error
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] 원격 추론 중 오류 발생: {ex.Message}");
                return new PythonService.InferenceResult
                {
                    Success = false,
                    Error = $"원격 추론 중 오류 발생: {ex.Message}"
                };
            }
        }
        public async Task<string> StartTrainingRemote(BlocklyModel model)
        {
            try
            {
                // 모델 파라미터를 JSON으로 직렬화
                var requestData = new
                {
                    name = model.model,
                    epochs = model.epoch,
                    image_size = model.imgsz,
                    accuracy = model.accuracy,
                    blocks = model.blockTypes.Select(b => b.type).ToArray()
                };

                // API 호출
                var content = new StringContent(
                    JsonSerializer.Serialize(requestData),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await _apiService.PostAsync("/api/training/start", content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"API 호출 실패: {response.StatusCode}");
                }

                // 응답 파싱
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"[DEBUG] 서버 응답: {responseContent}");
                
                var result = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(responseContent);
                
                if (result.TryGetValue("task_id", out var taskIdElement) && 
                    taskIdElement.ValueKind == JsonValueKind.String)
                {
                    string taskId = taskIdElement.GetString();
                    if (!string.IsNullOrEmpty(taskId))
                    {
                        // 현재 실행 중인 taskId 저장
                        _currentTaskId = taskId;
                        
                        // 학습 진행 상황을 모니터링하는 메서드 호출
                        MonitorTrainingProgress(taskId);
                        return taskId;
                    }
                }
                
                throw new Exception("응답에서 유효한 task_id를 찾을 수 없습니다.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] 원격 학습 시작 중 오류 발생: {ex.Message}");
                _yolotutorialview.ShowErrorMessage($"원격 학습 시작 중 오류가 발생했습니다: {ex.Message}");
                return null;
            }
        }
        private async void MonitorTrainingProgress(string taskId)
        {
            _monitoringCancellationTokenSource = new CancellationTokenSource();
            // 전체 모니터링을 30분으로 제한
            _monitoringCancellationTokenSource.CancelAfter(TimeSpan.FromMinutes(30));

            try
            {
                Console.WriteLine($"[INFO] WebSocket을 통한 학습 진행률 모니터링 시작: taskId={taskId}");
                
                // WebSocket 이벤트 핸들러 등록
                _apiService.TrainingProgressReceived += OnTrainingProgressReceived;
                _apiService.WebSocketError += OnWebSocketError;
                _apiService.WebSocketDisconnected += OnWebSocketDisconnected;
                
                // WebSocket 연결 시도
                bool connected = await _apiService.ConnectWebSocket(taskId);
                
                if (!connected)
                {
                    Console.WriteLine($"[ERROR] WebSocket 연결 실패, HTTP 폴링으로 대체");
                    // WebSocket 연결 실패시 기존 HTTP 폴링 방식으로 폴백
                    await MonitorTrainingProgressHttp(taskId);
                    return;
                }
                
                Console.WriteLine($"[INFO] WebSocket 연결 성공, 실시간 모니터링 시작");
                
                // Ping 전송 루프 (연결 유지용)
                _ = Task.Run(async () =>
                {
                    while (!_monitoringCancellationTokenSource.Token.IsCancellationRequested)
                    {
                        try
                        {
                            await Task.Delay(30000, _monitoringCancellationTokenSource.Token); // 30초마다
                            await _apiService.SendPing();
                        }
                        catch (OperationCanceledException)
                        {
                            break;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"[WARNING] Ping 전송 실패: {ex.Message}");
                        }
                    }
                });
                
                // 모니터링 완료까지 대기
                while (!_monitoringCancellationTokenSource.Token.IsCancellationRequested)
                {
                    await Task.Delay(1000, _monitoringCancellationTokenSource.Token);
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine($"[INFO] 학습 모니터링이 취소되었습니다.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] 학습 모니터링 중 오류: {ex.Message}");
                
                // 오류 발생시 HTTP 폴링으로 대체
                try
                {
                    await MonitorTrainingProgressHttp(taskId);
                }
                catch (Exception fallbackEx)
                {
                    Console.WriteLine($"[ERROR] HTTP 폴링 대체도 실패: {fallbackEx.Message}");
                }
            }
            finally
            {
                // WebSocket 이벤트 핸들러 해제
                _apiService.TrainingProgressReceived -= OnTrainingProgressReceived;
                _apiService.WebSocketError -= OnWebSocketError;
                _apiService.WebSocketDisconnected -= OnWebSocketDisconnected;
                
                // WebSocket 연결 종료
                await _apiService.DisconnectWebSocket();
                
                Console.WriteLine($"[INFO] 학습 모니터링 종료");
            }
        }

        // WebSocket 진행률 수신 이벤트 핸들러
        private void OnTrainingProgressReceived(object sender, TrainingProgressEventArgs e)
        {
            try
            {
                // 진행률 메시지를 AppendLog를 통해 출력 (중복 방지를 위해 Console.WriteLine 제거)
                if (!string.IsNullOrEmpty(e.Message))
                {
                    var logMessage = $"{e.Progress}:{e.Message}";
                    if (_yolotutorialview is Control logControl && logControl.InvokeRequired)
                    {
                        logControl.Invoke(new Action(() => _yolotutorialview.AppendLog($"[YOLO Tutorial] {logMessage}")));
                    }
                    else
                    {
                        _yolotutorialview.AppendLog($"[YOLO Tutorial] {logMessage}");
                    }
                }

                // 새로운 로그 처리 (서버에서 받은 추가 로그들)
                if (e.Logs != null && e.Logs.Length > 0)
                {
                    foreach (var log in e.Logs)
                    {
                        if (!string.IsNullOrEmpty(log))
                        {
                            // 이미 "[YOLO Tutorial]" 접두사가 있는지 확인하여 중복 방지
                            var logToDisplay = log.StartsWith("[YOLO Tutorial]") ? log : $"[YOLO Tutorial] {log}";
                            
                            // 로그 추가 (UI 스레드에서)
                            if (_yolotutorialview is Control logControl && logControl.InvokeRequired)
                            {
                                logControl.Invoke(new Action(() => _yolotutorialview.AppendLog(logToDisplay)));
                            }
                            else
                            {
                                _yolotutorialview.AppendLog(logToDisplay);
                            }
                        }
                    }
                }

                // UI 업데이트
                if (_progressDialog != null && !_progressDialog.IsDisposed)
                {
                    if (_progressDialog.InvokeRequired)
                    {
                        _progressDialog.Invoke(new Action(() =>
                        {
                            if (!_progressDialog.IsDisposed)
                            {
                                _progressDialog.UpdateProgress(e.Progress, e.Message);
                            }
                        }));
                    }
                    else
                    {
                        _progressDialog.UpdateProgress(e.Progress, e.Message);
                    }
                }

                // 완료 처리
                if (e.Status == "completed" || e.Status == "failed")
                {
                    Console.WriteLine($"[YOLO Tutorial] 학습 완료: {e.Status}");

                    if (e.Status == "completed")
                    {
                        HandleTrainingCompleted(e);
                    }
                    else if (e.Status == "failed")
                    {
                        HandleTrainingFailed(e);
                    }
                    
                    // 모니터링 종료
                    _monitoringCancellationTokenSource?.Cancel();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] 진행률 수신 처리 중 오류: {ex.Message}");
            }
        }

        // WebSocket 오류 이벤트 핸들러
        private void OnWebSocketError(object sender, string error)
        {
            Console.WriteLine($"[ERROR] WebSocket 오류: {error}");
            
            // 오류 발생시 HTTP 폴링으로 대체할 수 있지만, 여기서는 로그만 출력
        }

        // WebSocket 연결 해제 이벤트 핸들러
        private void OnWebSocketDisconnected(object sender, EventArgs e)
        {
            Console.WriteLine($"[INFO] WebSocket 연결이 해제되었습니다.");
        }

        // 학습 완료 처리
        private async void HandleTrainingCompleted(TrainingProgressEventArgs e)
        {
            // 학습 완료 - 추론 시작 알림
            if (_progressDialog != null && !_progressDialog.IsDisposed)
            {
                if (_progressDialog.InvokeRequired)
                {
                    _progressDialog.Invoke(new Action(() =>
                    {
                        if (!_progressDialog.IsDisposed)
                        {
                            _progressDialog.UpdateProgress(100, "학습 완료 - 결과 데이터 처리 중...");
                        }
                    }));
                }
                else
                {
                    _progressDialog.UpdateProgress(100, "학습 완료 - 결과 데이터 처리 중...");
                }
            }

            // 결과 데이터 처리
            object finalResults = e.Results;
            
            // 결과 데이터가 요약 정보만 있는 경우 전체 데이터 조회
            if (e.Results != null)
            {
                try
                {
                    var resultsJson = JsonSerializer.Serialize(e.Results);
                    var resultsSummary = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(resultsJson);
                    
                    // data_available 플래그 확인
                    if (resultsSummary.ContainsKey("data_available") && 
                        resultsSummary["data_available"].GetBoolean())
                    {
                        Console.WriteLine($"[INFO] 큰 결과 데이터 감지, HTTP API로 전체 데이터 조회 중...");
                        
                        // 진행률 업데이트
                        if (_progressDialog != null && !_progressDialog.IsDisposed)
                        {
                            if (_progressDialog.InvokeRequired)
                            {
                                _progressDialog.Invoke(new Action(() =>
                                {
                                    if (!_progressDialog.IsDisposed)
                                    {
                                        _progressDialog.UpdateProgress(100, "큰 결과 데이터 다운로드 중...");
                                    }
                                }));
                            }
                            else
                            {
                                _progressDialog.UpdateProgress(100, "큰 결과 데이터 다운로드 중...");
                            }
                        }
                        
                        // HTTP API로 전체 결과 조회
                        var fullResultsResponse = await _apiService.GetAsync($"/api/training/results/{_currentTaskId}");
                        
                        if (fullResultsResponse.IsSuccessStatusCode)
                        {
                            var fullResultsContent = await fullResultsResponse.Content.ReadAsStringAsync();
                            var fullResultsData = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(fullResultsContent);
                            
                            if (fullResultsData.ContainsKey("results"))
                            {
                                finalResults = JsonSerializer.Deserialize<object>(fullResultsData["results"].GetRawText());
                                Console.WriteLine($"[INFO] 전체 결과 데이터 조회 완료");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"[WARNING] 전체 결과 데이터 조회 실패: {fullResultsResponse.StatusCode}");
                            // 요약 데이터라도 사용
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] 결과 데이터 처리 중 오류: {ex.Message}");
                    // 기존 결과 데이터 사용
                }
            }

            // CSV 파일 처리 및 차트 표시
            if (finalResults != null)
            {
                try
                {
                    var resultsJson = JsonSerializer.Serialize(finalResults);
                    var results = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(resultsJson);
                    
                    if (results.TryGetValue("csv_base64", out var csvElement) && 
                        csvElement.ValueKind == JsonValueKind.String)
                    {
                        string csvBase64 = csvElement.GetString();
                        if (!string.IsNullOrEmpty(csvBase64))
                        {
                            // 진행률 업데이트
                            if (_progressDialog != null && !_progressDialog.IsDisposed)
                            {
                                if (_progressDialog.InvokeRequired)
                                {
                                    _progressDialog.Invoke(new Action(() =>
                                    {
                                        if (!_progressDialog.IsDisposed)
                                        {
                                            _progressDialog.UpdateProgress(100, "학습 차트 생성 중...");
                                        }
                                    }));
                                }
                                else
                                {
                                    _progressDialog.UpdateProgress(100, "학습 차트 생성 중...");
                                }
                            }
                            
                            byte[] csvBytes = Convert.FromBase64String(csvBase64);
                            
                            // 로컬과 동일한 경로 구조로 CSV 파일 저장
                            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
                            var localCsvPath = Path.Combine(baseDir,
                                "SAI.Application","Python","runs","detect", "train", "results.csv");
                            localCsvPath = Path.GetFullPath(localCsvPath);
                            
                            // 디렉토리가 없으면 생성
                            var csvDir = Path.GetDirectoryName(localCsvPath);
                            if (!Directory.Exists(csvDir))
                            {
                                Directory.CreateDirectory(csvDir);
                                Console.WriteLine($"[INFO] 디렉토리 생성: {csvDir}");
                            }
                            
                            // CSV 파일을 로컬 경로에 저장
                            File.WriteAllBytes(localCsvPath, csvBytes);
                            Console.WriteLine($"[INFO] 서버 학습 결과 CSV 파일 저장: {localCsvPath}");
                            
                            // 추가로 임시 파일에도 저장 (기존 코드 유지)
                            string tempCsvPath = Path.Combine(
                                Path.GetTempPath(),
                                $"training_results_{_currentTaskId}.csv"
                            );
                            File.WriteAllBytes(tempCsvPath, csvBytes);

                            if (_yolotutorialview is Control chartControl && chartControl.InvokeRequired)
                            {
                                chartControl.Invoke(new Action(() =>
                                {
                                    // 로컬과 동일한 경로의 파일을 사용하여 차트 표시
                                    _yolotutorialview.ShowTutorialTrainingChart(localCsvPath);
                                }));
                            }
                            else
                            {
                                _yolotutorialview.ShowTutorialTrainingChart(localCsvPath);
                            }
                            
                            Console.WriteLine($"[INFO] 학습 차트 생성 완료 (경로: {localCsvPath})");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] 결과 처리 중 오류: {ex.Message}");
                }
            }
            
            // 최종 진행률 업데이트
            if (_progressDialog != null && !_progressDialog.IsDisposed)
            {
                if (_progressDialog.InvokeRequired)
                {
                    _progressDialog.Invoke(new Action(() =>
                    {
                        if (!_progressDialog.IsDisposed)
                        {
                            _progressDialog.UpdateProgress(100, "학습 완료 - 추론을 시작합니다...");
                        }
                    }));
                }
                else
                {
                    _progressDialog.UpdateProgress(100, "학습 완료 - 추론을 시작합니다...");
                }
            }
            
            // 추론 실행 (다이얼로그는 유지)
            _ = Task.Run(async () =>
            {
                await CheckAndShowInferenceResultRemoteWithDialog();
            });
        }

        // 학습 실패 처리
        private void HandleTrainingFailed(TrainingProgressEventArgs e)
        {
            // 실패 시에만 다이얼로그 닫기
            if (_progressDialog != null && !_progressDialog.IsDisposed)
            {
                if (_progressDialog.InvokeRequired)
                {
                    _progressDialog.Invoke(new Action(() =>
                    {
                        _progressDialog.Close();
                        _progressDialog.Dispose();
                    }));
                }
                else
                {
                    _progressDialog.Close();
                    _progressDialog.Dispose();
                }
            }
            
            // Run 버튼 다시 활성화
            if (_yolotutorialview is UcTutorialBlockCode tutorialView)
            {
                tutorialView.EnableRunButton();
                Console.WriteLine("[INFO] 학습 실패로 인한 Run 버튼 상태 복원 완료");
            }
            
            // 실패 메시지 표시
            string errorMessage = e.Error ?? "학습이 실패했습니다.";
            Console.WriteLine($"[YOLO Tutorial] 학습 실패: {errorMessage}");
            
            if (_yolotutorialview is Control errorControl && errorControl.InvokeRequired)
            {
                errorControl.Invoke(new Action(() =>
                    _yolotutorialview.ShowErrorMessage(errorMessage)
                ));
            }
            else
            {
                _yolotutorialview.ShowErrorMessage(errorMessage);
            }
        }

        // HTTP 폴링 방식 (WebSocket 실패시 대체용)
        private async Task MonitorTrainingProgressHttp(string taskId)
        {
            Console.WriteLine($"[INFO] HTTP 폴링 방식으로 학습 진행률 모니터링 시작");
            
            bool isCompleted = false;
            int consecutiveErrors = 0;
            const int maxConsecutiveErrors = 5;
            int lastLogCount = 0;

            while (!isCompleted && !_monitoringCancellationTokenSource.Token.IsCancellationRequested)
            {
                try
                {
                    // 진행 상황 요청
                    var response = await _apiService.GetAsync($"/api/training/progress/{taskId}");

                    if (response.IsSuccessStatusCode)
                    {
                        consecutiveErrors = 0;
                        
                        var content = await response.Content.ReadAsStringAsync();
                        var progressData = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(content);

                        if (progressData.TryGetValue("progress", out var progressElement) &&
                            progressData.TryGetValue("status", out var statusElement) &&
                            progressData.TryGetValue("message", out var messageElement))
                        {
                            double progress = progressElement.GetDouble();
                            string status = statusElement.GetString();
                            string message = messageElement.GetString();

                            Console.WriteLine($"[YOLO Tutorial] {progress}:{message}");

                            // 새로운 로그 처리
                            if (progressData.TryGetValue("logs", out var logsElement) && 
                                logsElement.ValueKind == JsonValueKind.Array)
                            {
                                var logs = logsElement.EnumerateArray().Select(x => x.GetString()).ToList();
                                
                                if (logs.Count > lastLogCount)
                                {
                                    var newLogs = logs.Skip(lastLogCount).ToList();
                                    foreach (var log in newLogs)
                                    {
                                        if (!string.IsNullOrEmpty(log))
                                        {
                                            if (_yolotutorialview is Control logControl && logControl.InvokeRequired)
                                            {
                                                logControl.Invoke(new Action(() => _yolotutorialview.AppendLog(log)));
                                            }
                                            else
                                            {
                                                _yolotutorialview.AppendLog(log);
                                            }
                                        }
                                    }
                                    lastLogCount = logs.Count;
                                }
                            }

                            // UI 업데이트
                            if (_progressDialog != null && !_progressDialog.IsDisposed)
                            {
                                if (_progressDialog.InvokeRequired)
                                {
                                    _progressDialog.Invoke(new Action(() =>
                                    {
                                        if (!_progressDialog.IsDisposed)
                                        {
                                            _progressDialog.UpdateProgress(progress, message);
                                        }
                                    }));
                                }
                                else
                                {
                                    _progressDialog.UpdateProgress(progress, message);
                                }
                            }

                            // 완료 확인
                            isCompleted = status == "completed" || status == "failed";

                            if (isCompleted)
                            {
                                Console.WriteLine($"[YOLO Tutorial] 학습 완료: {status}");

                                if (status == "completed")
                                {
                                    var fakeArgs = new TrainingProgressEventArgs
                                    {
                                        Status = status,
                                        Progress = progress,
                                        Message = message,
                                        Results = progressData.ContainsKey("results") ? 
                                            JsonSerializer.Deserialize<object>(progressData["results"].GetRawText()) : null
                                    };
                                    HandleTrainingCompleted(fakeArgs);
                                }
                                else if (status == "failed")
                                {
                                    var fakeArgs = new TrainingProgressEventArgs
                                    {
                                        Status = status,
                                        Error = progressData.ContainsKey("error") ? progressData["error"].GetString() : "알 수 없는 오류"
                                    };
                                    HandleTrainingFailed(fakeArgs);
                                }
                            }
                        }
                    }
                    else
                    {
                        consecutiveErrors++;
                        Console.WriteLine($"[WARNING] 진행률 조회 실패: {response.StatusCode} (연속 오류: {consecutiveErrors}/{maxConsecutiveErrors})");

                        if (consecutiveErrors >= maxConsecutiveErrors)
                        {
                            Console.WriteLine($"[ERROR] 연속 {maxConsecutiveErrors}회 실패로 모니터링 중단");
                            break;
                        }
                    }

                    // 2초 대기
                    await Task.Delay(2000, _monitoringCancellationTokenSource.Token);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    consecutiveErrors++;
                    Console.WriteLine($"[ERROR] 진행률 조회 중 오류: {ex.Message} (연속 오류: {consecutiveErrors}/{maxConsecutiveErrors})");

                    if (consecutiveErrors >= maxConsecutiveErrors)
                    {
                        Console.WriteLine($"[ERROR] 연속 {maxConsecutiveErrors}회 실패로 모니터링 중단");
                        break;
                    }

                    await Task.Delay(5000, _monitoringCancellationTokenSource.Token);
                }
            }
        }

        // 서버 모니터링 취소 메서드 추가
        public async void CancelServerMonitoring()
        {
            try
            {
                Console.WriteLine("[INFO] 서버 모니터링을 취소합니다.");
                _userCancelled = true;

                // 1단계: 모니터링 취소 신호 전송
                if (_monitoringCancellationTokenSource != null && !_monitoringCancellationTokenSource.Token.IsCancellationRequested)
                {
                    _monitoringCancellationTokenSource.Cancel();
                }

                // 2단계: 서버에 학습 취소 요청 (WebSocket이 살아있을 때)
                if (!string.IsNullOrEmpty(_currentTaskId))
                {
                    Console.WriteLine($"[INFO] 서버 학습 취소를 요청합니다. TaskId: {_currentTaskId}");
                    
                    try
                    {
                        // 타임아웃을 짧게 설정하여 빠른 응답
                        using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5)))
                        {
                            var success = await _apiService.CancelTraining(_currentTaskId);
                            if (success)
                            {
                                Console.WriteLine($"[INFO] 서버 학습 취소 성공: taskId={_currentTaskId}");
                            }
                            else
                            {
                                Console.WriteLine($"[WARNING] 서버 학습 취소 실패: taskId={_currentTaskId}");
                            }
                        }
                    }
                    catch (TaskCanceledException)
                    {
                        Console.WriteLine("[INFO] 취소 요청 중 타임아웃 발생 (정상적인 취소 과정)");
                    }
                    catch (OperationCanceledException)
                    {
                        Console.WriteLine("[INFO] 취소 요청이 취소됨 (정상적인 취소 과정)");
                    }
                    catch (HttpRequestException ex)
                    {
                        Console.WriteLine($"[WARNING] HTTP 요청 실패: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[WARNING] 서버 학습 취소 요청 중 오류: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("[INFO] 취소할 서버 학습 작업이 없습니다.");
                }

                // 3단계: WebSocket 연결 정리 (잠시 대기 후)
                try
                {
                    await Task.Delay(500); // 서버 응답 대기
                    await _apiService.DisconnectWebSocket();
                    Console.WriteLine("[INFO] WebSocket 연결 정리 완료");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[INFO] WebSocket 정리 중 예외 (정상적인 취소 과정): {ex.Message}");
                }

                // 4단계: 다이얼로그 정리
                await CleanupProgressDialog("사용자 취소");

                // 5단계: 상태 초기화
                _currentTaskId = null;
                
                // 6단계: UI 상태 복원 - Run 버튼 다시 활성화
                if (_yolotutorialview is UcTutorialBlockCode tutorialView)
                {
                    tutorialView.EnableRunButton();
                    Console.WriteLine("[INFO] Run 버튼 상태 복원 완료");
                }

                Console.WriteLine("[INFO] 서버 학습이 성공적으로 취소되었습니다.");
                
                // UI에 취소 메시지 표시
                if (_yolotutorialview is Control messageControl && messageControl.InvokeRequired)
                {
                    messageControl.Invoke(new Action(() =>
                        _yolotutorialview.AppendLog("[INFO] 학습이 취소되었습니다.")
                    ));
                }
                else
                {
                    _yolotutorialview.AppendLog("[INFO] 학습이 취소되었습니다.");
                }

                Console.WriteLine("[YOLO Tutorial] [INFO] 서버 학습이 취소되었습니다.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] 취소 처리 중 예상치 못한 오류: {ex.Message}");
                
                // 강제로 다이얼로그 정리
                try
                {
                    await CleanupProgressDialog("오류로 인한 강제 취소");
                }
                catch (Exception cleanupEx)
                {
                    Console.WriteLine($"[ERROR] 강제 정리 중 오류: {cleanupEx.Message}");
                }
            }
        }

        // 다이얼로그 정리를 위한 별도 메서드
        private async Task CleanupProgressDialog(string reason)
        {
            Console.WriteLine($"[INFO] 다이얼로그 정리 시작: {reason}");
            
            try
            {
                if (_progressDialog != null && !_progressDialog.IsDisposed)
                {
                    if (_progressDialog.InvokeRequired)
                    {
                        await Task.Run(() =>
                        {
                            try
                            {
                                _progressDialog.Invoke(new Action(() =>
                                {
                                    try
                                    {
                                        if (!_progressDialog.IsDisposed)
                                        {
                                            _progressDialog.Close();
                                            _progressDialog.Dispose();
                                            Console.WriteLine("[INFO] 다이얼로그 정리 완료 (Invoke)");
                                        }
                                    }
                                    catch (ObjectDisposedException)
                                    {
                                        Console.WriteLine("[INFO] 다이얼로그 이미 정리됨 (Invoke)");
                                    }
                                }));
                            }
                            catch (InvalidOperationException ex)
                            {
                                Console.WriteLine($"[WARNING] 다이얼로그 Invoke 실패: {ex.Message}");
                                // UI 스레드가 종료된 경우 강제 정리
                                try
                                {
                                    _progressDialog.Close();
                                    _progressDialog.Dispose();
                                }
                                catch (Exception disposeEx)
                                {
                                    Console.WriteLine($"[WARNING] 강제 정리 실패: {disposeEx.Message}");
                                }
                            }
                        });
                    }
                    else
                    {
                        try
                        {
                            _progressDialog.Close();
                            _progressDialog.Dispose();
                            Console.WriteLine("[INFO] 다이얼로그 정리 완료 (Direct)");
                        }
                        catch (ObjectDisposedException)
                        {
                            Console.WriteLine("[INFO] 다이얼로그 이미 정리됨 (Direct)");
                        }
                    }
                    
                    _progressDialog = null;
                }
                else
                {
                    Console.WriteLine("[INFO] 정리할 다이얼로그가 없음");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] 다이얼로그 정리 중 오류: {ex.Message}");
                
                // 마지막 수단: 강제 null 할당
                _progressDialog = null;
            }
            
            // 다이얼로그 정리 완료 후 Run 버튼 활성화
            if (_yolotutorialview is UcTutorialBlockCode tutorialView)
            {
                tutorialView.EnableRunButton();
                Console.WriteLine($"[INFO] 다이얼로그 정리 완료 후 Run 버튼 활성화: {reason}");
            }
        }

        // 서버 모드에서 학습 완료 후 자동으로 추론 실행하면서 다이얼로그를 유지하는 메서드
        private async Task CheckAndShowInferenceResultRemoteWithDialog()
        {
            try
            {
                // 추론 진행 상황 업데이트
                if (_progressDialog != null && !_progressDialog.IsDisposed)
                {
                    if (_progressDialog.InvokeRequired)
                    {
                        _progressDialog.Invoke(new Action(() =>
                        {
                            if (!_progressDialog.IsDisposed)
                            {
                                _progressDialog.UpdateProgress(100, "추론을 진행하고 있습니다...");
                            }
                        }));
                    }
                    else
                    {
                        _progressDialog.UpdateProgress(100, "추론을 진행하고 있습니다...");
                    }
                }
                
                // 블록 모델에서 이미지 경로 가져오기
                var model = BlocklyModel.Instance;
                string imagePath = model?.imgPath;
                double conf = model?.accuracy ?? 0.25;
                
                if (string.IsNullOrEmpty(imagePath))
                {
                    Console.WriteLine("[WARNING] 서버 추론: 이미지 경로가 없습니다.");
                    
                    // 다이얼로그 닫기
                    if (_progressDialog != null && !_progressDialog.IsDisposed)
                    {
                        if (_progressDialog.InvokeRequired)
                        {
                            _progressDialog.Invoke(new Action(() =>
                            {
                                _progressDialog.Close();
                                _progressDialog.Dispose();
                            }));
                        }
                        else
                        {
                            _progressDialog.Close();
                            _progressDialog.Dispose();
                        }
                    }
                    return;
                }
                
                Console.WriteLine($"[INFO] 서버에서 자동 추론을 시작합니다: {imagePath}, conf={conf}");
                
                // 서버에서 추론 실행
                var result = await RunInferenceDirectRemote(imagePath, conf);
                
                Console.WriteLine($"[DEBUG] 서버 추론 결과: success={result.Success}, error={result.Error}");
                
                // 추론 완료 상태 업데이트
                if (_progressDialog != null && !_progressDialog.IsDisposed)
                {
                    if (_progressDialog.InvokeRequired)
                    {
                        _progressDialog.Invoke(new Action(() =>
                        {
                            if (!_progressDialog.IsDisposed)
                            {
                                _progressDialog.UpdateProgress(100, "추론이 완료되었습니다!");
                            }
                        }));
                    }
                    else
                    {
                        _progressDialog.UpdateProgress(100, "추론이 완료되었습니다!");
                    }
                }
                
                // 잠시 대기 후 다이얼로그 닫기
                await Task.Delay(1000);
                
                // UI 스레드에서 결과 표시 및 다이얼로그 닫기
                if (_yolotutorialview is Control viewControl && viewControl.InvokeRequired)
                {
                    viewControl.Invoke(new Action(() => {
                        // 다이얼로그 닫기
                        if (_progressDialog != null && !_progressDialog.IsDisposed)
                        {
                            _progressDialog.Close();
                            _progressDialog.Dispose();
                        }
                        
                        // 추론 결과 표시
                        if (_itutorialInferenceView != null)
                        {
                            _itutorialInferenceView.ShowInferenceResult(result);
                        }
                        else
                        {
                            Console.WriteLine("[ERROR] _itutorialInferenceView가 null입니다.");
                        }
                    }));
                }
                else
                {
                    // 다이얼로그 닫기
                    if (_progressDialog != null && !_progressDialog.IsDisposed)
                    {
                        _progressDialog.Close();
                        _progressDialog.Dispose();
                    }
                    
                    // 추론 결과 표시
                    if (_itutorialInferenceView != null)
                    {
                        _itutorialInferenceView.ShowInferenceResult(result);
                    }
                    else
                    {
                        Console.WriteLine("[ERROR] _itutorialInferenceView가 null입니다.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] 서버 추론 실행 중 오류 발생: {ex.Message}");
                
                // 에러 결과 표시 및 다이얼로그 닫기
                var errorResult = new PythonService.InferenceResult
                {
                    Success = false,
                    Error = $"서버 추론 실행 중 오류 발생: {ex.Message}"
                };
                
                if (_yolotutorialview is Control viewControl && viewControl.InvokeRequired)
                {
                    viewControl.Invoke(new Action(() => {
                        // 다이얼로그 닫기
                        if (_progressDialog != null && !_progressDialog.IsDisposed)
                        {
                            _progressDialog.Close();
                            _progressDialog.Dispose();
                        }
                        
                        // 에러 결과 표시
                        if (_itutorialInferenceView != null)
                        {
                            _itutorialInferenceView.ShowInferenceResult(errorResult);
                        }
                    }));
                }
                else
                {
                    // 다이얼로그 닫기
                    if (_progressDialog != null && !_progressDialog.IsDisposed)
                    {
                        _progressDialog.Close();
                        _progressDialog.Dispose();
                    }
                    
                    // 에러 결과 표시
                    if (_itutorialInferenceView != null)
                    {
                        _itutorialInferenceView.ShowInferenceResult(errorResult);
                    }
                }
            }
        }
    }
}