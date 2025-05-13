using SAI.SAI.App.Views.Interfaces;
using SAI.SAI.App.Views.Pages;
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

            // 부모 기준 중앙
            this.StartPosition = FormStartPosition.CenterParent;
            // 기존 타이틀바 삭제
            this.FormBorderStyle = FormBorderStyle.None;
            // 떴을 때 이 다이얼로그가 가장 위에 있고 다이얼로그를 끄기 전에는 다른 건 못 누르게!
            this.TopMost = true;

            // 배경을 투명하게 하기 위해서
            this.BackColor = Color.Gray;           // 투명 처리할 색
            this.TransparencyKey = Color.Gray;

            // btnClose
            btnClose.BackColor = Color.Transparent;
            btnClose.PressedColor = Color.Transparent;
            btnClose.CheckedState.FillColor = Color.Transparent;
            btnClose.DisabledState.FillColor = Color.Transparent;
            btnClose.HoverState.FillColor = Color.Transparent;
            btnClose.Click += (s, e) => { this.Close(); };
            // btnClose 마우스 입력 될 때
            btnClose.MouseEnter += (s, e) =>
            {
                btnClose.BackColor = Color.Transparent;
                btnClose.BackgroundImage = Properties.Resources.bg_yellow_btn_close_clicked;
            };
            // btnClose 마우스 떠날때
            btnClose.MouseLeave += (s, e) =>
            {
                btnClose.BackgroundImage = Properties.Resources.bg_yellow_btn_close;
            };
            // btnCancel
            btnCancel.BackColor = Color.Transparent;
            btnCancel.PressedColor = Color.Transparent;
            btnCancel.CheckedState.FillColor = Color.Transparent;
            btnCancel.DisabledState.FillColor = Color.Transparent;
            btnCancel.HoverState.FillColor = Color.Transparent;
            btnCancel.Click += (s, e) => { this.Close(); };
            // btnCancel 마우스 입력 될 때
            btnCancel.MouseEnter += (s, e) =>
            {
                btnCancel.BackColor = Color.Transparent;
                btnCancel.BackgroundImage = Properties.Resources.btn_white_cancel_clicked;
            };
            // btnCancel 마우스 떠날때
            btnCancel.MouseLeave += (s, e) =>
            {
                btnCancel.BackgroundImage = Properties.Resources.btn_white_cancel;
            };
        }

        protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			var view = this.Owner as IMainView;
			this.mainView = view;
		}

		private void guna2Button1_Click(object sender, EventArgs e)
		{
			mainView.LoadPage(new UcTutorialBlockCode(mainView));
			this.Close();
		}
    }
}
