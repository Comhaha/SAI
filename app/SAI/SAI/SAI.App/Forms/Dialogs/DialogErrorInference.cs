using System;
using System.Drawing;
using System.Windows.Forms;

namespace SAI.SAI.App.Forms.Dialogs
{
    public partial class DialogErrorInference : Form
    {
        public DialogErrorInference()
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

            // btnOk
            btnOk.HoverState.FillColor = Color.Transparent;
            btnOk.PressedColor = Color.Transparent;
            btnOk.CheckedState.FillColor = Color.Transparent;
            btnOk.HoverState.FillColor = Color.Transparent;
            btnOk.BackColor = Color.Transparent;
            btnOk.Click += (s, e) => { this.DialogResult = DialogResult.OK; this.Close(); };
            // btnOk 마우스 입력 될 때
            btnOk.MouseEnter += (s, e) =>
            {
                btnOk.BackColor = Color.Transparent;
                btnOk.BackgroundImage = Properties.Resources.btn_red_ok_clicked_15258;
            };
            // btnOk 마우스 떠날때
            btnOk.MouseLeave += (s, e) =>
            {
                btnOk.BackColor = Color.Transparent;
                btnOk.BackgroundImage = Properties.Resources.btn_red_ok_15258;
            };
        }

        // 에러 메시지 설정 메서드 (생성자 밖으로 이동)
        public void SetErrorMessage(string errorMessage)
        {
            // errorMessage를 다이얼로그에 표시
            if (error != null)
            {
                error.Text = errorMessage;
            }
        }

        private void error_Load(object sender, EventArgs e)
        {

        }
    }
}
