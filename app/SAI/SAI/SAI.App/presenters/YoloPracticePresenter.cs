using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using SAI.SAI.App.Forms.Dialogs;
using SAI.SAI.App.Models;
using SAI.SAI.App.Views.Interfaces;
using SAI.SAI.Application.Service;

namespace SAI.SAI.App.Presenters
{
    public class YoloPracticePresenter
    {
        private readonly IPracticeInferenceView _practiceInferenceView;
        private readonly IYoloPracticeView _yolopracticeview;
        private readonly PythonService _pythonService;
        private DialogModelProgress _progressDialog;

        public YoloPracticePresenter(IYoloPracticeView yolopracticeview)
        {
            _yolopracticeview = yolopracticeview;
            _practiceInferenceView = yolopracticeview as IPracticeInferenceView;
            _pythonService = new PythonService();

            _yolopracticeview.RunButtonClicked += OnRunButtonClicked;
        }

        private void OnRunButtonClicked(object sender, EventArgs e)
        {
            try
            {
                // 다이얼로그는 반드시 UI 스레드에서 실행되어야 함
                if (_yolopracticeview is Control viewControl && viewControl.InvokeRequired)
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
                _yolopracticeview.ShowErrorMessage($"실행 중 오류가 발생했습니다: {ex.Message}");
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

                    process = _pythonService.RunPythonScript(
                        PythonService.Mode.Practice, // 실습 모드
                        onOutput: text =>
                        {
                            try
                            {
                                if (string.IsNullOrEmpty(text)) return;

                                Console.WriteLine($"Python Output: {text}");

                                if (_yolopracticeview is Control logControl && logControl.InvokeRequired)
                                {
                                    logControl.Invoke(new Action(() => _yolopracticeview.AppendLog(text)));
                                }
                                else
                                {
                                    _yolopracticeview.AppendLog(text);
                                }

                                if (text.StartsWith("PROGRESS:"))
                                {
                                    var parts = text.Substring(9).Split(new[] { ':' }, 2);
                                    if (parts.Length == 2 && double.TryParse(parts[0], out double progress))
                                    {
                                        string message = parts[1];

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
                                                _progressDialog.UpdateProgress(0, $"{text}");
                                            }
                                        }));
                                    }
                                    else
                                    {
                                        _progressDialog.UpdateProgress(0, $"{text}");
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

                                if (_yolopracticeview is Control errorControl && errorControl.InvokeRequired)
                                {
                                    errorControl.Invoke(new Action(() => _yolopracticeview.ShowErrorMessage($"오류가 발생했습니다: {ex.Message}")));
                                }
                                else
                                {
                                    _yolopracticeview.ShowErrorMessage($"오류가 발생했습니다: {ex.Message}");
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
                    if (_yolopracticeview is Control errorView && errorView.InvokeRequired)
                    {
                        errorView.Invoke(new Action(() =>
                            _yolopracticeview.ShowErrorMessage($"스크립트 실행 중 오류가 발생했습니다: {ex.Message}")
                        ));
                    }
                    else
                    {
                        _yolopracticeview.ShowErrorMessage($"스크립트 실행 중 오류가 발생했습니다: {ex.Message}");
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
                                    _yolopracticeview.AppendLog("스크립트가 종료됐습니다!");
                                    _progressDialog.Close();
                                    _progressDialog.Dispose();


                                    var baseDir = AppDomain.CurrentDomain.BaseDirectory;
                                    var csvPath = Path.Combine(baseDir,
                                        @"..\..\SAI.Application\Python\runs\detect\train\results.csv");
                                    csvPath = Path.GetFullPath(csvPath);
                                    _yolopracticeview.ShowTrainingChart(csvPath);

                                    CheckAndShowInferenceResult();
                                }
                            }));
                        }
                        else
                        {
                            _yolopracticeview.AppendLog("스크립트가 종료됐습니다!");
                            _progressDialog.Close();
                            _progressDialog.Dispose();
                            CheckAndShowInferenceResult();
                        }
                    }
                }
            });
        }

        // YoloPracticePresenter.cs에 추가

        private void CheckAndShowInferenceResult()
        {
            try
            {
                var model = BlocklyModel.Instance;
                string imagePath = model?.imgPath;

                if (string.IsNullOrEmpty(imagePath))
                {
                    Console.WriteLine("[WARNING] 이미지 경로가 없습니다.");
                    return;
                }

                Console.WriteLine($"[DEBUG] 원본 이미지 경로: {imagePath}");

                // 🔥 결과 이미지 경로는 동일 (inference.py가 같으니까)
                string resultImagePath = null;
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

                    var result = new PythonService.InferenceResult
                    {
                        Success = true,
                        ResultImage = resultImagePath,
                        OriginalName = Path.GetFileName(imagePath)
                    };

                    // 🔥 Practice용 인터페이스 사용
                    if (_practiceInferenceView != null)
                    {
                        if (_yolopracticeview is Control viewControl && viewControl.InvokeRequired)
                        {
                            viewControl.Invoke(new Action(() => {
                                _practiceInferenceView.ShowPracticeInferResultImage(result);
                            }));
                        }
                        else
                        {
                            _practiceInferenceView.ShowPracticeInferResultImage(result);
                        }
                    }
                    else
                    {
                        Console.WriteLine("[ERROR] _practiceInferenceView가 null입니다.");
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
            _practiceInferenceView.ShowPracticeInferResultImage(result);
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
    }
}
