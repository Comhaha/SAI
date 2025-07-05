using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SAI.SAI.App.Views.Common;

namespace SAI.SAI.App.Forms.Dialogs
{
    public partial class DialogStartcampInput : Form
    {
        public DialogStartcampInput()
        {
            InitializeComponent();

            DialogUtils.ApplyDefaultStyle(this, Color.Gray);

            ButtonUtils.SetupButton(btnClose2, "bg_yellow_btn_close_clicked", "bg_yellow_btn_close");
            btnClose2.Click += (s, e) => { this.Close(); };

            //pbox 호버 -> seletimage 보임
            // 새 이미지 불러오기 버튼 설정
            btnStartcampInput.Size = new Size(494, 278);  // pInferAccuracy와 동일한 크기
            pboxStartcampInput.Controls.Add(btnStartcampInput);
            btnStartcampInput.Location = new Point(0, 0);
            btnStartcampInput.Enabled = true;
            btnStartcampInput.Cursor = Cursors.Hand;

            //pboxStartcampInput.MouseEnter += (s, e) =>
            //{
            //    if (pSideInfer.Visible)
            //    {
            //        btnStartcampInput.Visible = true;
            //        btnStartcampInput.BackgroundImage = Properties.Resources.btn_selectinferimage_hover;
            //    }
            //};

            pboxStartcampInput.MouseLeave += (s, e) =>
            {
                if (!btnStartcampInput.ClientRectangle.Contains(btnStartcampInput.PointToClient(Control.MousePosition)))
                {
                    btnStartcampInput.Visible = false;
                    btnStartcampInput.BackgroundImage = Properties.Resources.btn_selectinferimage;
                }
            };

            // 버튼에도 MouseEnter/Leave 이벤트 추가
            btnStartcampInput.MouseEnter += (s, e) =>
            {
                btnStartcampInput.Visible = true;
                btnStartcampInput.BackgroundImage = Properties.Resources.btn_selectinferimage_hover;
            };

            btnStartcampInput.MouseLeave += (s, e) =>
            {
                if (!pboxStartcampInput.ClientRectangle.Contains(pboxStartcampInput.PointToClient(Control.MousePosition)))
                {
                    btnStartcampInput.Visible = false;
                    btnStartcampInput.BackgroundImage = Properties.Resources.btn_selectinferimage;
                }
            };



        }
        

        private void panelTitleBar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tbarThreshold_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void lblThreshold_Load(object sender, EventArgs e)
        {

        }

        private void pboxInferAccuracy_Click(object sender, EventArgs e)
        {

        }

        private void ibtnSizeup_Click(object sender, EventArgs e)
        {

        }

        private void btnStartcampInput_Click(object sender, EventArgs e)
        {

        }

        private void pboxStartcampInput_Click(object sender, EventArgs e)
        {

        }
    }
}
