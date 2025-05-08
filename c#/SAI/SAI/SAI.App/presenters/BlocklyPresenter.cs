using SAI.SAI.App.Models;
using SAI.SAI.App.Models.Events;
using SAI.SAI.App.Views.Interfaces;
using SAI.SAI.Application.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAI.SAI.App.Presenters
{
	internal class BlocklyPresenter
	{
		private readonly IBlocklyView view;
		private BlocklyService blocklyService;
		private BlocklyModel blocklyModel;

		public BlocklyPresenter(IBlocklyView view)
		{
			this.view = view;
			this.blocklyService = new BlocklyService();
			this.blocklyModel = BlocklyModel.Instance;
			this.view.AddBlockButtonClicked += OnAddBlockButtonClicked;			
		}

		// 버튼 클릭시 호출되는 이벤트 메소드 -> view에게 전달
		private void OnAddBlockButtonClicked(object sender, BlockEventArgs e)
		{
			// View에게 JS로 블록 추가 명령
			view.addBlock(e.BlockType);
		}

		// JS에서 receiveFromJs() 호출해서 코드 전달했을 때 presenter가 view와 service에게 전달해주기 위한 메소드
		public void HandleJsMessage(string code)
		{
			// 메시지를 View로 전달해서 RichTextBox에 출력
			view.ShowGeneratedCode(code);
			blocklyModel.code = code;
			blocklyService.SaveCodeToFileInTutorial();
		}
	}
}
