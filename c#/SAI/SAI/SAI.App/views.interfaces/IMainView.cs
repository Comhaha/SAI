using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAI.SAI.App.Views.Interfaces
{
	internal interface IMainView
	{
		// 페이지 불러오는 함수
		void LoadPage(UserControl page);
	}
}
