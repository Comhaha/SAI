using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI.SAI.App.Models
{
	public class MainModel
	{
		private static MainModel instance;

		// private 생성자 -> 외부에서 생성 못하게.
		private MainModel()
		{
			DontShowDeleteModelDialog = false;
		}

		// [싱글톤] 외부에서 MainModel 인스턴스를 가져올 수 있게
		public static MainModel Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new MainModel();
				}
				return instance;
			}
		}

		public bool DontShowDeleteModelDialog { get; set; }
	}
}
