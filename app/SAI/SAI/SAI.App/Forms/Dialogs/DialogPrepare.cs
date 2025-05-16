using System;
using System.Drawing;
using System.Windows.Forms;
using SAI.SAI.App.Views.Interfaces;
using SAI.SAI.App.Views.Common;

namespace SAI.SAI.App.Forms.Dialogs
{
    public partial class DialogPrepare : Form
    {
        private IMainView mainView;
        public DialogPrepare()
        {
            InitializeComponent();

            DialogUtils.ApplyDefaultStyle(this, Color.Gray);

            ButtonUtils.SetupButton(btnClose, "bg_yellow_btn_close_clicked", "bg_yellow_btn_close");
            btnClose.Click += (s, e) => { this.Close(); };
            ButtonUtils.SetupButton(btnOk, "btn_yellow_ok_clicked", "btn_yellow_ok");
            btnOk.Click += (s, e) => { this.Close(); };
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            var view = this.Owner as IMainView;
            this.mainView = view;
        }
    }
}
