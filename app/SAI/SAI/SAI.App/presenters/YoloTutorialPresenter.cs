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
            Console.WriteLine("YoloTutorialRun 버튼이 클릭되었습니다."); // 디버그용 콘솔 출력
            _view.SetLogVisible(true);
            _view.ClearLog();
            _view.AppendLog("Python 스크립트 실행을 시작합니다...");
            Console.WriteLine("Python 스크립트 실행을 시작합니다..."); // 디버그용 콘솔 출력

            Task.Run(() =>
            {
                try
                {
                    Console.WriteLine("PythonService.RunPythonScript 호출 시작"); // 디버그용 콘솔 출력
                    _pythonService.RunPythonScript(
                        PythonService.Mode.Tutorial,
                        onOutput: text => 
                        {
                            Console.WriteLine($"Python Output: {text}"); // 디버그용 콘솔 출력  
                            _view.AppendLog(text);
                        },
                        onError: err => 
                        {
                            Console.WriteLine($"Python Error: {err}"); // 디버그용 콘솔 출력
                            _view.AppendLog("[Error] " + err);
                        },
                        onException: ex => 
                        {
                            Console.WriteLine($"Exception: {ex}"); // 디버그용 콘솔 출력
                            _view.ShowErrorMessage("❌ 파이썬 실행 중 예외 발생:\n" + ex.Message);
                        }
                    );
                    Console.WriteLine("PythonService.RunPythonScript 호출 완료"); // 디버그용 콘솔 출력
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected Exception: {ex}"); // 디버그용 콘솔 출력
                    _view.ShowErrorMessage("❌ 예상치 못한 오류 발생:\n" + ex.Message);
                }
            });
        }
    }
}
