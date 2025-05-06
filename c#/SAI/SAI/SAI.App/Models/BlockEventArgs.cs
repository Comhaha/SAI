using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI.SAI.App.Models
{
	// 버튼을 누를 때, 블록타입(string)을 넣어서 이벤트 함수를 호출하기 위해 DTO 생성
	public class BlockEventArgs : EventArgs
	{
		public string BlockType { get; }

		public BlockEventArgs(string blockType)
		{
			BlockType = blockType;
		}
	}
}
