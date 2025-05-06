using SAI.SAI.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI.SAI.App.Views.Interfaces
{
	public interface IMainView
	{
		event EventHandler<BlockEventArgs> AddBlockButtonClicked;
		void InsertBlockToBlockly(string blockType);  // 웹뷰에 블록 추가
	}
}
