using System;
using SAI.SAI.App.Views.Interfaces;
using SAI.SAI.App.Views.Pages;

namespace SAI.SAI.App.Presenters
{
    internal class TrainsitionPresenter
    {

        private readonly IPageTransitionView view;

        public TrainsitionPresenter(IPageTransitionView view)
        {
            this.view = view;

        }
        public void Initialize()
        {
            var practicePage = new UcPracticeBlockCode();
            practicePage.HomeButtonClicked += OnSomeNavigationRequested;
            view.ShowPage(practicePage);
        }

        private void OnSomeNavigationRequested(object sender, EventArgs e)
        {
            view.ShowPage(new UcSelectType());
        }

    }
}
