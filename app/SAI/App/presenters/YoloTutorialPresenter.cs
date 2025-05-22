using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SAI.App.Forms.Dialogs;
using SAI.App.Models;
using SAI.App.Views.Interfaces;
using SAI.Application.Service;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing;

namespace SAI.App.Presenters
{
    public class YoloTutorialPresenter
    {
        private readonly ITutorialInferenceView _itutorialInferenceView;
        private readonly IYoloTutorialView _yolotutorialview;
        private readonly PythonService _pythonService;
        private DialogModelProgress _progressDialog;
        private DateTime _scriptStartTime; // 스크립트 실행 시작 시간

        public YoloTutorialPresenter(IYoloTutorialView yolotutorialview)
        {
            _yolotutorialview = yolotutorialview;
            _pythonService = new PythonService();

            _itutorialInferenceView = yolotutorialview as ITutorialInferenceView;

            _yolotutorialview.RunButtonClicked += OnRunButtonClicked;
       
        }

        private void OnRunButtonClicked(object sender, EventArgs e)
        {
            try
            {
                // 스크립트 실행 시작 시간 기록
                _scriptStartTime = DateTime.Now;
                Console.WriteLine($"[INFO] 스크립트 실행 시작 시간: {_scriptStartTime}");
                
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
                                        @"..\\..\\Application\Python\runs\detect\train\results.csv");
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
                    @"..\\..\\Application\Python\runs\result",
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
        public void OnInferImageSelected(string imagePath, double conf)
        {
            var result = _pythonService.RunInference(imagePath, conf);
            _itutorialInferenceView.ShowInferenceResult(result);
        }

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

        // RunButtonClicked 이벤트 구독 해제 메서드 추가
        public void UnsubscribeFromRunButtonClicked(IYoloTutorialView view)
        {
            if (view != null)
            {
                view.RunButtonClicked -= OnRunButtonClicked;
            }
        }

       
    }
}
