using System;
using System.Drawing;
using System.Windows.Forms;
using SAI.SAI.App.Styles;

namespace SAI.SAI.App.Views.Pages
{
    public partial class UcPraticeBlockCode : UserControl
    {
        private PictureBox img_background;
        public UcPraticeBlockCode()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
            InitializeBackgroundImage();
            pRight.BackColor = Color.Transparent;
            pCenter.BackColor = Color.Transparent; 
        }
        private void UcPraticeBlockCode_Load(object sender, EventArgs e)
        {
           pboxBaseFrame.BackColor = Color.Transparent;
        }

        private void InitializeBackgroundImage()
        {
            img_background = new PictureBox
            {
                Dock = DockStyle.Fill,
                Image = Properties.Resources.img_background,
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            this.Controls.Add(img_background);
            img_background.SendToBack();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (Parent != null)
            {
                using (SolidBrush brush = new SolidBrush(Parent.BackColor))
                {
                    e.Graphics.FillRectangle(brush, this.ClientRectangle);
                }
            }
            else
            {
                base.OnPaintBackground(e);
            }
        }


        private void leftPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void baseFramePanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void leftPanel_Paint_1(object sender, PaintEventArgs e)
        {
            
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void pnl_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2ImageButton1_Click_1(object sender, EventArgs e)
        {

        }

        private void ibtnInfer_Click(object sender, EventArgs e)
        {

        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void pLeft_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2ShadowPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2HtmlToolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void pCenter_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
