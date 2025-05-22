using SAI.App.Models;
using SAI.App.Views.Interfaces;
using SAI.App.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI.App.Presenters
{
	internal class DialogDeleteModelPresenter
	{
		private readonly IDialogDeleteModelView dialogDeleteModelView;

		public DialogDeleteModelPresenter(IDialogDeleteModelView view)
		{
			this.dialogDeleteModelView = view;
		}

		public void DialogDeleteModelEvent(bool check)
		{
			dialogDeleteModelView.CheckedDialogDeleteModel(check);
		}
	}
}
