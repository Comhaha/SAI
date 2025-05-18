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

namespace SAI.SAI.App.Presenters
{
    public class YoloTutorialPresenter
    {
        private readonly ITutorialInferenceView _itutorialInferenceView;
        private readonly IYoloTutorialView _yolotutorialview;
        private readonly PythonService _pythonService;
        private ProgressDialog _progressDialog;

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
                _progressDialog = new ProgressDialog();
                _progressDialog.Show();

                Task.Run(() =>
                {
                    try
                    {
                        // 표준 출력 스트림 설정
                        Console.WriteLine("[DEBUG] 표준 출력 스트림 설정 시도");
                        Console.WriteLine("[DEBUG] 표준 출력 스트림 설정 생략");

                        _pythonService.RunPythonScript(
                            PythonService.Mode.Tutorial,
                            onOutput: text =>
                            {
                                try
                                {
                                    if (string.IsNullOrEmpty(text)) return;

                                    Console.WriteLine($"Python Output: {text}");
                                    
                                    // UI 스레드에서 로그 추가
                                    if (_yolotutorialview != null)
                                    {
                                        if (_yolotutorialview is Control control && control.InvokeRequired)
                                        {
                                            control.Invoke(new Action(() => _yolotutorialview.AppendLog(text)));
                                        }
                                        else
                                        {
                                            _yolotutorialview.AppendLog(text);
                                        }
                                    }

                                    // 진행률 업데이트
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
                                    
                                    // UI 스레드에서 예외 메시지 표시
                                    if (_yolotutorialview != null)
                                    {
                                        if (_yolotutorialview is Control control && control.InvokeRequired)
                                        {
                                            control.Invoke(new Action(() => _yolotutorialview.ShowErrorMessage($"오류가 발생했습니다: {ex.Message}")));
                                        }
                                        else
                                        {
                                            _yolotutorialview.ShowErrorMessage($"오류가 발생했습니다: {ex.Message}");
                                        }
                                    }
                                }
                                catch (Exception innerEx)
                                {
                                    Console.WriteLine($"Error in exception handler: {innerEx}");
                                }
                            },
                            blocklyModel: BlocklyModel.Instance
                        );
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error in Python script execution: {ex}");
                        if (_yolotutorialview != null)
                        {
                            if (_yolotutorialview is Control control && control.InvokeRequired)
                            {
                                control.Invoke(new Action(() => _yolotutorialview.ShowErrorMessage($"스크립트 실행 중 오류가 발생했습니다: {ex.Message}")));
                            }
                            else
                            {
                                _yolotutorialview.ShowErrorMessage($"스크립트 실행 중 오류가 발생했습니다: {ex.Message}");
                            }
                        }
                    }
                    finally
                    {
                        // 진행 대화상자 정리
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
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in OnRunButtonClicked: {ex}");
                if (_yolotutorialview != null)
                {
                    _yolotutorialview.ShowErrorMessage($"실행 중 오류가 발생했습니다: {ex.Message}");
                }
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
            Console.WriteLine($"[DEBUG] RunInferenceDirect() 결과: success={result.Success}, image={result.ResultImage}");
            return result;
        }
    }
}
