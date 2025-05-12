using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace SAI.SAI.App.Forms.Dialogs
{
    public partial class DialogModelSelect : Form
    {
        public DialogModelSelect()
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

            ibtnYolo.Click += ModelButton_Click;
            ibtnEfficientdet.Click += ModelButton_Click;
            ibtnFaster.Click += ModelButton_Click;
            ibtnClose.Click += ibtnClose_Click;
        }

        private void DialogModelSelect_Load(object sender, EventArgs e)
        {
            ResetModelSelection();
        }

        private void ResetModelSelection()
        {
            // 모든 버튼의 이미지를 원래 상태로 복원
            ibtnYolo.Image = global::SAI.Properties.Resources.btn_yolo_clicked;
            ibtnEfficientdet.Image = global::SAI.Properties.Resources.btn_efficientdet;
            ibtnFaster.Image = global::SAI.Properties.Resources.btn_faster;

            // 모든 패널 숨기기
            pYolo.Visible = true;
            pEfficientdet.Visible = false;
            pFaster.Visible = false;
        }

        private void ModelButton_Click(object sender, EventArgs e)
        {
            // 모든 버튼의 이미지를 원래 상태로 복원
            ibtnYolo.Image = global::SAI.Properties.Resources.btn_yolo;
            ibtnEfficientdet.Image = global::SAI.Properties.Resources.btn_efficientdet;
            ibtnFaster.Image = global::SAI.Properties.Resources.btn_faster;

            // 모든 패널 숨기기
            pYolo.Visible = false;
            pEfficientdet.Visible = false;
            pFaster.Visible = false;

            // 클릭된 버튼을 pressed 상태로 만들기
            Guna2ImageButton clickedButton = (Guna2ImageButton)sender;

            // 클릭된 버튼의 이미지와 패널을 변경
            if (clickedButton == ibtnYolo)
            {
                ibtnYolo.Image = global::SAI.Properties.Resources.btn_yolo_clicked;
                pYolo.Visible = true;
            }
            else if (clickedButton == ibtnEfficientdet)
            {
                ibtnEfficientdet.Image = global::SAI.Properties.Resources.btn_efficientdet_clicked;
                pEfficientdet.Visible = true;
            }
            else if (clickedButton == ibtnFaster)
            {
                ibtnFaster.Image = global::SAI.Properties.Resources.btn_faster_clicked;
                pFaster.Visible = true;
            }
        }

        private void ibtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
