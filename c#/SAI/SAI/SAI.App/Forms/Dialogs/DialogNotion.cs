using System;
using System.Drawing;
using System.Windows.Forms;

namespace SAI.SAI.App.Forms.Dialogs
{
    public partial class DialogNotion : Form
    {
        //webView2.Visible = false 되어 있어요. 로직 작성하실 때
        // secretkey 입력하고 맞으면 pInfo.Visible = false; webView2.Visible = true; 하고


        public DialogNotion()
        {
            InitializeComponent();

            pInfo.BackColor = ColorTranslator.FromHtml("#1D1D1D");
        }

        private void DialogNotion_Load(object sender, EventArgs e)
        {

        }

        private void ibtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
