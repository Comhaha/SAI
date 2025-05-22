using SAI.App.Views.Interfaces;
using SAI.App.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAI.App.Presenters
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
			mainView.LoadPage(new UcTutorialGuide(mainView));			
		}

		public void clickTrainAtModelSelect()
		{
			mainView.LoadPage(new UcPracticeGuide(mainView));
		}

		//public void clickGoTutorialBlockCode()
		//{
		//	mainView.LoadPage(new UcTutorialBlockCode(mainView));
		//}

		public void clickGoTrain()
		{
			mainView.LoadPage(new UcPracticeBlockCode(mainView));
		}

		public void clickFinish()
		{
			mainView.LoadPage(new UcSelectType(mainView));
		}

        public void clickGoPracticeGuide()
        {
            mainView.LoadPage(new UcPracticeGuide(mainView));
        }
    }
}
