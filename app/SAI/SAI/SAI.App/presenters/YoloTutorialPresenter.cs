using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAI.SAI.App.Forms.Dialogs;
using SAI.SAI.App.Views.Interfaces;
using SAI.SAI.Application.Service;

namespace SAI.SAI.App.Presenters
{
    public class YoloTutorialPresenter
    {
        private readonly IYoloTutorialView _view;
        private readonly PythonService _pythonService;
        private ProgressDialog _progressDialog;

        public YoloTutorialPresenter(IYoloTutorialView view)
        {
            _view = view;
            _pythonService = new PythonService();

            _view.RunButtonClicked += OnRunButtonClicked;
        }

        private void OnRunButtonClicked(object sender, EventArgs e)
        {
            _progressDialog = new ProgressDialog();
            _progressDialog.Show();

            Task.Run(() =>
            {
                try
                {
                    _pythonService.RunPythonScript(
                        PythonService.Mode.Tutorial,
                        onOutput: text =>
                        {
                            Console.WriteLine($"Python Output: {text}");
                            _view.AppendLog(text);

                            if (text.StartsWith("PROGRESS:"))
                            {
                                var parts = text.Substring(9).Split(new[] { ':' }, 2);
                                if (parts.Length == 2 && double.TryParse(parts[0], out double progress))
                                {
                                    string message = parts[1];

                                    if (_progressDialog != null)
                                    {
                                        if (_progressDialog.IsHandleCreated)
                                        {
                                            _progressDialog.Invoke((Action)(() =>
                                            {
                                                _progressDialog.UpdateProgress(progress, message);
                                            }));
                                        }
                                        else
                                        {
                                            _progressDialog.UpdateProgress(progress, message);
                                        }
                                    }

                                    if (progress >= 100 && message.Contains("학습 완료"))
                                    {
                                        if (_progressDialog != null && !_progressDialog.IsDisposed)
                                        {
                                            if (_progressDialog.IsHandleCreated)
                                            {
                                                _progressDialog.Invoke((Action)(() =>
                                                {
                                                    _progressDialog.Close();
                                                    _progressDialog = null;
                                                }));
                                            }
                                            else
                                            {
                                                _progressDialog.Close();
                                                _progressDialog = null;
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        onError: err => { _view.AppendLog("[Error] " + err); },
                        onException: ex => { _view.ShowErrorMessage("❌ 예외 발생:\n" + ex.Message); }
                    );
                }
                catch (Exception ex)
                {
                    _view.ShowErrorMessage("❌ 예상치 못한 오류 발생:\n" + ex.Message);
                }
            });
        }

        // 추론시 PythonService에 구현된 추론스크립트 함수를 실행
        // 사용자 지정 imagePath와 conf값을 스크립트에 던져주면, 스크립트에서 그 값으로 추론을 진행
        public void OnInferImageSelected(string imagePath, double conf)
        {
            var result = _pythonService.RunInference(imagePath, conf);
            _view.ShowInferenceResult(result);
        }
    }
}
