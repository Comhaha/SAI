using System;
using System.Windows.Forms;
using SAI.App.Views.Common;
using SAI.App.Views.Interfaces;

namespace SAI.App.Views.Pages
{
    public partial class UcPracticeGuide : UserControl
    {
        private IMainView mainView;
        public UcPracticeGuide(IMainView mainView)
        {
            InitializeComponent();
            this.mainView = mainView;
            ButtonUtils.SetupButton(btnClose, "btn_close_guide_clicked", "btn_close_guide");
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            mainView.LoadPage(new UcPracticeBlockCode(mainView));
        }
    }
}
