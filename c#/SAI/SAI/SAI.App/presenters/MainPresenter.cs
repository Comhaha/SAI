using SAI.SAI.App.Models;
using SAI.SAI.App.Views.Interfaces;
using SAI.SAI.App.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
			//var blocklyPresenter = new BlocklyPresenter(blockly); // Presenter 연결
			//mainView.LoadPage(blockly); // 메인 폼에 삽입
		}
	}
}
