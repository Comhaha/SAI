using SAI.SAI.App.Views.Interfaces;
using SAI.SAI.App.Views.Pages;
using SAI.SAI.App.Presenters;

namespace SAI.SAI.App.Presenters
{
    internal class MainPresenter
    {
        private readonly IMainView mainView;

        public MainPresenter(IMainView view)
        {
            this.mainView = view;
        }

        // 앱 실행 시 초기 페이지 로드
        public void Initialize()
        {
            //var blockly = new Blockly(); // View 생성
            //mainView.LoadPage(blockly); // 메인 폼에 삽입
            var yolorun = new YoloTutorial();
            var yoloPresenter = new YoloTutorialPresenter(yolorun);
            mainView.LoadPage(yolorun);
        }
    }
}
