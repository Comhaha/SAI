using System;
using System.Drawing;
using System.Windows.Forms;
using SAI.SAI.App.Views.Interfaces;

namespace SAI.SAI.App.Forms.Dialogs
{
    public partial class DialogPrepare : Form
    {
        private IMainView mainView;
        public DialogPrepare()
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

            // btnOk
            btnOk.HoverState.FillColor = Color.Transparent;
            btnOk.PressedColor = Color.Transparent;
            btnOk.CheckedState.FillColor = Color.Transparent;
            btnOk.HoverState.FillColor = Color.Transparent;
            btnOk.BackColor = Color.Transparent;
            btnOk.Click += (s, e) => { this.Close(); };
   
            // btnOk 마우스 입력 될 때
            btnOk.MouseEnter += (s, e) =>
            {
                btnOk.BackColor = Color.Transparent;
                btnOk.BackgroundImage = Properties.Resources.btn_yellow_ok_clicked;
            };
            // btnOk 마우스 떠날때
            btnOk.MouseLeave += (s, e) =>
            {
                btnOk.BackColor = Color.Transparent;
                btnOk.BackgroundImage = Properties.Resources.btn_yellow_ok;
            };
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            var view = this.Owner as IMainView;
            this.mainView = view;
        }
    }
}
