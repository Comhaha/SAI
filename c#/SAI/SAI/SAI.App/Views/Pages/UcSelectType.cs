using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SAI.SAI.App.Forms.Dialogs;

namespace SAI.SAI.App.Views.Pages
{
    public partial class UcSelectType : UserControl

    {
        //public event EventHandler HomeButtonClicked;

        public UcSelectType()
        {
            InitializeComponent();
        }

        private void UcSelectType_Load(object sender, EventArgs e)
        {

        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {

        }

        private void ibtnNext_Click(object sender, EventArgs e)
        {
            using (var dialog = new DialogModelSelect())
            {
                dialog.ShowDialog();
            }
        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}
