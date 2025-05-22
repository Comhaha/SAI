using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAI.App.Views.Interfaces
{
	internal interface IDialogDeleteModelView
	{
		// 프로그램 종료까지 해당 다이얼로그가 안 뜨게 체크한 이벤트 처리
		void CheckedDialogDeleteModel(Boolean check);
	}
}
