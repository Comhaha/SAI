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
	internal class UcShowDialogPresenter
	{
		private readonly IUcShowDialogView view;
		public UcShowDialogPresenter(IUcShowDialogView view)
		{
			this.view = view;
		}
		public void clickType(string type)
		{
			switch (type)
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

		public void clickGoTutorialBlockCode()
		{
			view.showDialog(new DialogConfirmGoTutorialBlockCode());
		}

		public void clickGoTrain()
		{
			view.showDialog(new DialogComfirmGoTrain());
		}

		public void clickFinish()
		{
			view.showDialog(new DialogFinishTrain());
		}
	}
}
