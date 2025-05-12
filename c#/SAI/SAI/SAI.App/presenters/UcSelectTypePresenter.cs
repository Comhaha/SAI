using SAI.SAI.App.Forms.Dialogs;
using SAI.SAI.App.Models;
using SAI.SAI.App.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI.SAI.App.Presenters
{
	internal class UcSelectTypePresenter
	{
		private readonly IUcSelectTypeView view;
		public UcSelectTypePresenter(IUcSelectTypeView view)
		{
			this.view = view;
		}
		public void clickType(string type)
		{
			var model = UcSelectTypeModel.Instance;
			model.selectType = type;
		}

		public void clickNext()
		{
			var model = UcSelectTypeModel.Instance;
			switch (model.selectType)
			{
				case "image":
					view.showDialog(new DialogModelSelect());
					break;
				case "audio":
					view.showDialog(new DialogPrepare());
					break;
				case "pose":
					view.showDialog(new DialogPrepare());
					break;
				default:
					break;
			}
		}
	}
}
