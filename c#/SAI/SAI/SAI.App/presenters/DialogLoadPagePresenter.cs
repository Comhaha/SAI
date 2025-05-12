using SAI.SAI.App.Views.Interfaces;
using SAI.SAI.App.Views.Pages;
using SAI.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAI.SAI.App.Presenters
{
	internal class DialogLoadPagePresenter
	{
		private readonly IMainView mainView;
		public DialogLoadPagePresenter(IMainView view)
		{
			this.mainView = view;
		}

		public void clickTutorial()
		{			
			mainView.LoadPage(new UcLabelGuide());			
		}

		public void clickTrain()
		{
			mainView.LoadPage(new UcPracticeBlockCode());
		}

		public void clickGoTutorialBlockCode()
		{
			mainView.LoadPage(new UcTutorialBlockCode());
		}
	}
}
