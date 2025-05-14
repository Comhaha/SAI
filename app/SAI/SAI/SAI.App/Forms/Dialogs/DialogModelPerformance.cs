using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAI.SAI.App.Forms.Dialogs
{
    public partial class DialogModelPerformance : Form
    {
        public DialogModelPerformance()
        {
            InitializeComponent();

            // 부모 기준 중앙
            this.StartPosition = FormStartPosition.CenterParent;
            // 기존 타이틀바 삭제
            this.FormBorderStyle = FormBorderStyle.None;
            // 떴을 때 이 다이얼로그가 가장 위에 있고 다이얼로그를 끄기 전에는 다른 건 못 누르게!
            this.TopMost = true;

            // 배경을 투명하게 하기 위해서
            this.BackColor = Color.Gray;          
            this.TransparencyKey = Color.Gray;

            // btnClose
            btnClose.BackColor = Color.Transparent;
            btnClose.PressedColor = Color.Transparent;
            btnClose.CheckedState.FillColor = Color.Transparent;
            btnClose.DisabledState.FillColor = Color.Transparent;
            btnClose.HoverState.FillColor = Color.Transparent;
            btnClose.Click += (s, e) => { this.Close(); };
   
            btnClose.MouseEnter += (s, e) =>
            {
                btnClose.BackColor = Color.Transparent;
                btnClose.BackgroundImage = Properties.Resources.btn_close_white_clicked;
            };

            btnClose.MouseLeave += (s, e) =>
            {
                btnClose.BackgroundImage = Properties.Resources.btn_close_white;
            };


        }
    }
}
