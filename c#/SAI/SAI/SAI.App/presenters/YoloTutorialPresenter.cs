using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAI.SAI.App.Views.Interfaces;
using SAI.SAI.Application.Service;

namespace SAI.SAI.App.Presenters
{
    public class YoloTutorialPresenter
    {
        private readonly IYoloTutorialView _view;
        private readonly PythonService _pythonService;

        public YoloTutorialPresenter(IYoloTutorialView view)
        {
            _view = view;
            _pythonService = new PythonService();

            _view.RunButtonClicked += OnRunButtonClicked;
        }

        private void OnRunButtonClicked(object sender, EventArgs e)
        {
            _view.SetLogVisible(true);
            _view.ClearLog();

            Task.Run(() =>
            {
                _pythonService.RunPythonScript(
                    PythonService.Mode.Tutorial,
                    onOutput: text => _view.AppendLog(text),
                    onError: err => _view.AppendLog("[Error] " + err),
                    onException: ex => _view.ShowErrorMessage("❌ 파이썬 실행 중 예외 발생:\n" + ex.Message)
                );
            });
        }
    }
}
