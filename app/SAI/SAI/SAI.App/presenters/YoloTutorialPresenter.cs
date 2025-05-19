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
using System.Drawing;

namespace SAI.SAI.App.Presenters
{
    public class YoloTutorialPresenter
    {
        private readonly ITutorialInferenceView _itutorialInferenceView;
        private readonly IYoloTutorialView _yolotutorialview;
        private readonly PythonService _pythonService;
        private DialogModelProgress _progressDialog;

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
                                                _progressDialog.UpdateProgress(0, $"오류: {text}");
                                            }
                                        }));
                                    }
                                    else
                                    {
                                        _progressDialog.UpdateProgress(0, $"오류: {text}");
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
                                }
                            }));
                        }
                        else
                        {
                            _yolotutorialview.AppendLog("스크립트가 종료됐습니다!");
                            _progressDialog.Close();
                            _progressDialog.Dispose();
                        }
                    }
                }
            });
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
            Console.WriteLine($"[DEBUG] RunInferenceDirect() 결과: success={result.Success}, image={result.ResultImage}");
            return result;
        }
    }
}
