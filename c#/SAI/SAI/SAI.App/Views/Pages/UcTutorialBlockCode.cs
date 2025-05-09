using System;
using System.Drawing;
using System.Windows.Forms;
using SAI.SAI.App.Resources.Styles;
using Guna.UI2.WinForms;
using SAI.SAI.App.Views.Interfaces;

namespace SAI.SAI.App.Views.Pages
{
    public partial class UcTutorialBlockCode : UserControl, IHomeView
    {
        public event EventHandler HomeButtonClicked;
        public UcTutorialBlockCode()
        {
            InitializeComponent();
            ibtnHome.Click += (s, e) => HomeButtonClicked?.Invoke(this, EventArgs.Empty);

            ibtnHome.BackColor = Color.Transparent;
            ibtnDone.BackColor = Color.Transparent;
            ibtnInfer.BackColor = Color.Transparent;    
            ibtnMemo.BackColor = Color.Transparent;
        }
        private void UcTutorialBlockCode_Load(object sender, EventArgs e)
        {
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
            HomeButtonClicked?.Invoke(this, EventArgs.Empty); // Presenter에게 알림
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

        private void ibtnPlusBlock_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnTrashBlock_Click(object sender, EventArgs e)
        {

        }

        private void pTopCenter_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pTopRight_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pBlockList_Paint(object sender, PaintEventArgs e)
        {
        }

        private void ibtnCopy_Click(object sender, EventArgs e)
        {

        }

        private void pTopCode_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void webView21_Click(object sender, EventArgs e)
        {

        }

        private void pCode_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void webView21_Click_1(object sender, EventArgs e)
        {

        }
    }
}
