using SAI.SAI.App.Models;
using SAI.SAI.App.Views.Interfaces;
using SAI.SAI.App.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAI.SAI.App.Presenters
{
	internal class MainPresenter
	{
		private readonly IMainView mainView;
		private readonly IYoloTutorialView yoloTutorialView;

		public MainPresenter(IMainView view)
		{
			this.mainView = view;
			
		}

		// 앱 실행 시 초기 페이지 로드
		public void Initialize()
		{
			var homePage = new YoloTutorial(); // View 생성
			//var homePage = new Blockly(); // View 생성
			mainView.LoadPage(homePage); // 메인 폼에 삽입
										 //mainView.LoadPage((UserControl)yoloTutorialView);
		}

		public void DialogDeleteModelEvent(bool check)
		{
			mainView.CheckedDialogDeleteModel(check);
		}
	}
}
