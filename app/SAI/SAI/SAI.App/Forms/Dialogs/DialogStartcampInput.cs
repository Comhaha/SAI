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

            ButtonUtils.SetupButton(btnClose, "btn_close_white_clicked", "btn_close_white");
            btnClose.Click += (s, e) => { this.Close(); };

            

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

        private void ibtnGoNotion_Click(object sender, EventArgs e)
        {

        }

        private void pboxInferAccuracy_Click(object sender, EventArgs e)
        {

        }
    }
}
