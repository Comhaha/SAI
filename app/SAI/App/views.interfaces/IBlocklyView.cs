using SAI.App.Models;
using SAI.App.Models.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI.App.Views.Interfaces
{
	internal interface IBlocklyView
	{
		// 버튼이 클릭되었다는 이벤트
		event EventHandler<BlockEventArgs> AddBlockButtonClicked;

		// 블록을 추가하는 JS 함수를 실행
		void addBlock(string blockType);

		void getPythonCodeByType(string blockType);
	}
}
