using SAI.SAI.App.Models;
using SAI.SAI.App.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI.SAI.App.Presenters
{
	internal class BlocklyPresenter
	{
		private readonly IBlocklyView view;

		public BlocklyPresenter(IBlocklyView view)
		{
			this.view = view;
			this.view.AddBlockButtonClicked += OnAddBlockButtonClicked;	
		}

		// 버튼 클릭시 호출되는 이벤트 메소드
		private void OnAddBlockButtonClicked(object sender, BlockEventArgs e)
		{
			// View에게 JS로 블록 추가 명령
			view.addBlock(e.BlockType);
		}
	}
}
