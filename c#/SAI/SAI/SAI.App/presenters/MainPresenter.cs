using SAI.SAI.App.Models;
using SAI.SAI.App.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI.SAI.App.Presenters
{
	internal class MainPresenter
	{
		private readonly IMainView view;

		public MainPresenter(IMainView view)
		{
			this.view = view;
			this.view.AddBlockButtonClicked += OnAddBlockClicked;
		}

		private void OnAddBlockClicked(object sender, BlockEventArgs e)
		{
			// JS 함수로 이벤트 함수 파라미터 BlockEventArg 안 blockType으로 블록 생성
			view.InsertBlockToBlockly(e.BlockType);
		}
	}
}
