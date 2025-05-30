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

        //public YoloTutorialPresenter(IYoloTutorialView yolotutorialview)
        //{
        //    _yolotutorialview = yolotutorialview;
        //    _pythonService = new PythonService();

        //    _itutorialInferenceView = yolotutorialview as ITutorialInferenceView;

        //    _yolotutorialview.RunButtonClicked += OnRunButtonClicked;
       
        //}

        public YoloTutorialPresenter(IYoloTutorialView yolotutorialview, string serverUrl = "http://127.0.0.1:8082")
        {
            _yolotutorialview = yolotutorialview;
            _pythonService = new PythonService();
            _apiService = new ApiService(serverUrl);
            _itutorialInferenceView = yolotutorialview as ITutorialInferenceView;
            _yolotutorialview.RunButtonClicked += OnRunButtonClicked;
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
                                        @"..\..\SAI.Application\Python\runs\detect\train\results.csv");
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
                    @"..\..\SAI.Application\Python\runs\result",
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
                bool isCompleted = false;
                int consecutiveErrors = 0;
                const int maxConsecutiveErrors = 5;

                while (!isCompleted && !_monitoringCancellationTokenSource.Token.IsCancellationRequested)
                {
                    try
                    {
                        // 진행 상황 요청
                        var response = await _apiService.GetAsync($"/api/training/progress/{taskId}");

                        if (response.IsSuccessStatusCode)
                        {
                            consecutiveErrors = 0; // 성공시 에러 카운트 리셋
                            
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

                                // 로그 추가
                                if (_yolotutorialview is Control logControl && logControl.InvokeRequired)
                                {
                                    logControl.Invoke(new Action(() => _yolotutorialview.AppendLog(message)));
                                }
                                else
                                {
                                    _yolotutorialview.AppendLog(message);
                                }

                                // 완료 확인
                                isCompleted = status == "completed" || status == "failed";

                                if (isCompleted)
                                {
                                    Console.WriteLine($"[YOLO Tutorial] 학습 완료: {status}");

                                    // 완료 처리
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

                                    // 결과 처리
                                    if (status == "completed")
                                    {
                                        // CSV 파일 경로 가져오기 및 차트 표시
                                        if (progressData.TryGetValue("results", out var resultsElement) &&
                                            resultsElement.ValueKind == JsonValueKind.Object)
                                        {
                                            var results = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(resultsElement.GetRawText());
                                            
                                            if (results.TryGetValue("csv_base64", out var csvElement) && 
                                                csvElement.ValueKind == JsonValueKind.String)
                                        {
                                            string csvBase64 = csvElement.GetString();
                                                if (!string.IsNullOrEmpty(csvBase64))
                                                {
                                            byte[] csvBytes = Convert.FromBase64String(csvBase64);

                                            string csvPath = Path.Combine(
                                                Path.GetTempPath(),
                                                $"training_results_{taskId}.csv"
                                            );

                                            File.WriteAllBytes(csvPath, csvBytes);

                                            if (_yolotutorialview is Control chartControl && chartControl.InvokeRequired)
                                            {
                                                chartControl.Invoke(new Action(() =>
                                                        {
                                                            _yolotutorialview.ShowTutorialTrainingChart(csvPath);
                                                            // 서버 모드에서 학습 완료 후 자동 추론 실행
                                                            CheckAndShowInferenceResultRemote();
                                                        }));
                                            }
                                            else
                                            {
                                                _yolotutorialview.ShowTutorialTrainingChart(csvPath);
                                                        // 서버 모드에서 학습 완료 후 자동 추론 실행
                                                        CheckAndShowInferenceResultRemote();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else if (status == "failed")
                                    {
                                        // 실패 메시지 표시
                                        string errorMessage = "학습이 실패했습니다.";
                                        if (progressData.TryGetValue("error", out var errorElement))
                                        {
                                            errorMessage = errorElement.GetString();
                                        }
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
                                }
                            }
                        }
                        else
                        {
                            consecutiveErrors++;
                            Console.WriteLine($"[WARNING] API 응답 실패: {response.StatusCode}, 연속 오류: {consecutiveErrors}");
                            
                            if (consecutiveErrors >= maxConsecutiveErrors)
                            {
                                throw new Exception($"연속 {maxConsecutiveErrors}회 API 호출 실패");
                            }
                        }
                    }
                    catch (TaskCanceledException tcex) when (tcex.CancellationToken.IsCancellationRequested)
                    {
                        Console.WriteLine("[INFO] 학습 모니터링이 취소되었습니다.");
                        break;
                    }
                    catch (Exception apiEx)
                    {
                        consecutiveErrors++;
                        Console.WriteLine($"[WARNING] API 호출 중 오류: {apiEx.Message}, 연속 오류: {consecutiveErrors}");
                        
                        if (consecutiveErrors >= maxConsecutiveErrors)
                        {
                            throw new Exception($"연속 {maxConsecutiveErrors}회 API 오류: {apiEx.Message}");
                        }
                    }

                    // 2초 대기 (이전보다 조금 더 길게)
                    if (!isCompleted && !_monitoringCancellationTokenSource.Token.IsCancellationRequested)
                    {
                        await Task.Delay(2000, _monitoringCancellationTokenSource.Token);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // 사용자가 직접 취소한 경우에는 에러 메시지를 표시하지 않음
                if (_userCancelled)
                {
                    Console.WriteLine("[INFO] 사용자에 의해 학습 모니터링이 취소되었습니다.");
                }
                else
                {
                    Console.WriteLine("[ERROR] 학습 모니터링이 타임아웃으로 취소되었습니다.");
                    if (_yolotutorialview is Control timeoutControl && timeoutControl.InvokeRequired)
                    {
                        timeoutControl.Invoke(new Action(() =>
                            _yolotutorialview.ShowErrorMessage("학습 모니터링이 타임아웃되었습니다. 서버에서 학습이 계속 진행될 수 있습니다.")
                        ));
                    }
                    else
                    {
                        _yolotutorialview.ShowErrorMessage("학습 모니터링이 타임아웃되었습니다. 서버에서 학습이 계속 진행될 수 있습니다.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] 학습 진행 상황 모니터링 중 오류 발생: {ex.Message}");

                if (_yolotutorialview is Control errorControl && errorControl.InvokeRequired)
                {
                    errorControl.Invoke(new Action(() =>
                        _yolotutorialview.ShowErrorMessage($"학습 진행 상황 모니터링 중 오류 발생: {ex.Message}")
                    ));
                }
                else
                {
                    _yolotutorialview.ShowErrorMessage($"학습 진행 상황 모니터링 중 오류 발생: {ex.Message}");
                }
            }
            finally
            {
                // 다이얼로그 정리
                if (_progressDialog != null && !_progressDialog.IsDisposed)
                {
                    if (_progressDialog.InvokeRequired)
                    {
                        _progressDialog.Invoke(new Action(() =>
                        {
                            if (!_progressDialog.IsDisposed)
                            {
                                _progressDialog.Close();
                                _progressDialog.Dispose();
                            }
                        }));
                    }
                    else
                    {
                        _progressDialog.Close();
                        _progressDialog.Dispose();
                    }
                }
                
                _monitoringCancellationTokenSource.Dispose();
                
                // 작업 완료 시 taskId 초기화
                _currentTaskId = null;
                
                // 사용자 취소 플래그 초기화
                _userCancelled = false;
            }
        }

        // 서버 모니터링 취소 메서드 추가
        public async void CancelServerMonitoring()
        {
            try
            {
                // 사용자가 직접 취소했다는 플래그 설정
                _userCancelled = true;
                
                // 모니터링 취소
                if (_monitoringCancellationTokenSource != null && !_monitoringCancellationTokenSource.Token.IsCancellationRequested)
                {
                    Console.WriteLine("[INFO] 서버 모니터링을 취소합니다.");
                    _monitoringCancellationTokenSource.Cancel();
                }

                // 실제 서버 학습 취소
                if (!string.IsNullOrEmpty(_currentTaskId))
                {
                    Console.WriteLine($"[INFO] 서버 학습 취소를 요청합니다. TaskId: {_currentTaskId}");
                    bool cancelResult = await _apiService.CancelTraining(_currentTaskId);
                    
                    if (cancelResult)
                    {
                        Console.WriteLine("[INFO] 서버 학습이 성공적으로 취소되었습니다.");
                        // UI에 취소 메시지 표시
                        if (_yolotutorialview is Control viewControl && viewControl.InvokeRequired)
                        {
                            viewControl.Invoke(new Action(() => 
                                _yolotutorialview.AppendLog("[INFO] 서버 학습이 취소되었습니다.")
                            ));
                        }
                        else
                        {
                            _yolotutorialview.AppendLog("[INFO] 서버 학습이 취소되었습니다.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("[WARNING] 서버 학습 취소 요청이 실패했습니다.");
                        // UI에 실패 메시지 표시
                        if (_yolotutorialview is Control viewControl && viewControl.InvokeRequired)
                        {
                            viewControl.Invoke(new Action(() => 
                                _yolotutorialview.AppendLog("[WARNING] 서버 학습 취소 요청이 실패했습니다.")
                            ));
                        }
                        else
                        {
                            _yolotutorialview.AppendLog("[WARNING] 서버 학습 취소 요청이 실패했습니다.");
                        }
                    }
                    
                    // taskId 초기화
                    _currentTaskId = null;
                }
                else
                {
                    Console.WriteLine("[INFO] 취소할 서버 학습 작업이 없습니다.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] 서버 학습 취소 중 오류 발생: {ex.Message}");
                // UI에 오류 메시지 표시
                if (_yolotutorialview is Control viewControl && viewControl.InvokeRequired)
                {
                    viewControl.Invoke(new Action(() => 
                        _yolotutorialview.AppendLog($"[ERROR] 서버 학습 취소 중 오류 발생: {ex.Message}")
                    ));
                }
                else
                {
                    _yolotutorialview.AppendLog($"[ERROR] 서버 학습 취소 중 오류 발생: {ex.Message}");
                }
            }
        }

        // 서버 모드에서 학습 완료 후 자동으로 추론 실행하는 메서드
        private async void CheckAndShowInferenceResultRemote()
        {
            try
            {
                // 블록 모델에서 이미지 경로 가져오기
                var model = BlocklyModel.Instance;
                string imagePath = model?.imgPath;
                double conf = model?.accuracy ?? 0.25;
                
                if (string.IsNullOrEmpty(imagePath))
                {
                    Console.WriteLine("[WARNING] 서버 추론: 이미지 경로가 없습니다.");
                    return;
                }
                
                Console.WriteLine($"[INFO] 서버에서 자동 추론을 시작합니다: {imagePath}, conf={conf}");
                
                // 서버에서 추론 실행
                var result = await RunInferenceDirectRemote(imagePath, conf);
                
                Console.WriteLine($"[DEBUG] 서버 추론 결과: success={result.Success}, error={result.Error}");
                
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
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] 서버 추론 실행 중 오류 발생: {ex.Message}");
                
                // 에러 결과 표시
                var errorResult = new PythonService.InferenceResult
                {
                    Success = false,
                    Error = $"서버 추론 실행 중 오류 발생: {ex.Message}"
                };
                
                if (_itutorialInferenceView != null)
                {
                    if (_yolotutorialview is Control viewControl && viewControl.InvokeRequired)
                    {
                        viewControl.Invoke(new Action(() => {
                            _itutorialInferenceView.ShowInferenceResult(errorResult);
                        }));
                    }
                    else
                    {
                        _itutorialInferenceView.ShowInferenceResult(errorResult);
                    }
                }
            }
        }
    }
}
