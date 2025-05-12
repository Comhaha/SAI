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
			this.view.AddBlockButtonDoubleClicked += OnAddBlockDoubleClicked;

			// 블록 하나의 코드가 변경되면 실행되는 이벤트
			blocklyModel.BlockCodeChanged += (newCode) =>
			{
				// 혜정언니 여기를 작성하면 돼!

				// 이 코드는 잘 출력되는지 확인용이라서 지워도 돼!
				MessageBox.Show(newCode);
			};

			// 전체 블록 코드가 변경되면 실행되는 이벤트
			blocklyModel.BlockAllCodeChanged += (newAllCode) =>
			{
				// 혜정언니 여기를 작성하면 돼!
			};
		}

		private void OnAddBlockDoubleClicked(object sender, BlockEventArgs e)
		{
			view.getPythonCodeByType(e.BlockType);
		}

		// 버튼 클릭시 호출되는 이벤트 메소드 -> view에게 전달
		private void OnAddBlockButtonClicked(object sender, BlockEventArgs e)
		{
			// View에게 JS로 블록 추가 명령
			view.addBlock(e.BlockType);
		}

		// presenter가 view와 service에게 전달해주기 위한 메소드
		public void HandleJsMessage(string code, string type)
		{
			if(type == "blockAllCode")
			{
				blocklyModel.blockAllCode = code;
				blocklyService.SaveCodeToFileInTutorial();
			}
			else if(type == "blockCode")
			{
				blocklyModel.blockCode = code;
			}
		}
	}
}
