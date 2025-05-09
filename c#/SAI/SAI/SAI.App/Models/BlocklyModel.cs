using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI.SAI.App.Models
{
	public class BlocklyModel
	{
		private static BlocklyModel instance;

		// private 생성자 -> 외부에서 생성 못하게.
		private BlocklyModel()
		{
			// 초기화
		}

		// [싱글톤] 외부에서 BlocklyModel 인스턴스를 가져올 수 있게
		public static BlocklyModel Instance
		{
			get { 
				if (instance == null)
				{
					instance = new BlocklyModel();
				}
				return instance;
			}
		}

		// 블록에서 반환되는 Python 코드
		public string code { get; set; }
	}
}
