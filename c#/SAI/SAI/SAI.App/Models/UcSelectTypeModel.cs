using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI.SAI.App.Models
{
	public class UcSelectTypeModel
	{
		private static UcSelectTypeModel instance;
		private string _selectType;	

		// private 생성자 -> 외부에서 생성 못하게.
		private UcSelectTypeModel()
		{
			_selectType = "";
		}

		// [싱글톤] 외부에서 BlocklyModel 인스턴스를 가져올 수 있게
		public static UcSelectTypeModel Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new UcSelectTypeModel();
				}
				return instance;
			}
		}

		public string selectType
		{
			get => _selectType;
			set
			{
				if (_selectType != value)
				{
					_selectType = value;
				}
			}
		}
	}
}
