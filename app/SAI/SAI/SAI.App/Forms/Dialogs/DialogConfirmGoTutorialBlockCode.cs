using SAI.SAI.App.Views.Interfaces;
using SAI.SAI.App.Views.Pages;
using SAI.SAI.App.Views.Common;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SAI.SAI.App.Forms.Dialogs
{
	public partial class DialogConfirmGoTutorialBlockCode : Form
	{
		private IMainView mainView;
		public DialogConfirmGoTutorialBlockCode()
		{
			InitializeComponent();

			DialogUtils.ApplyDefaultStyle(this, Color.Gray);

            ButtonUtils.SetupButton(btnClose, "bg_yellow_btn_close_clicked", "bg_yellow_btn_close");
            btnClose.Click += (s, e) => { this.Close(); };

            ButtonUtils.SetupButton(btnCancel, "btn_white_cancel_clicked", "btn_white_cancel");
            btnCancel.Click += (s, e) => { this.Close(); };

            ButtonUtils.SetupButton(btnOk, "btn_yellow_ok_clicked", "btn_yellow_ok");
        }

        protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			var view = this.Owner as IMainView;
			this.mainView = view;
		}

		private void guna2Button1_Click(object sender, EventArgs e)
		{
			mainView.LoadPage(new UcBlockGuide(mainView));
			this.Close();
		}
    }
}
