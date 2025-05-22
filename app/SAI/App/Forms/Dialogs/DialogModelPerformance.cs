using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SAI.App.Views.Common;

namespace SAI.App.Forms.Dialogs
{
    public partial class DialogModelPerformance : Form
    {
        public DialogModelPerformance()
        {
            InitializeComponent();

            DialogUtils.ApplyDefaultStyle(this, Color.Gray);

            ButtonUtils.SetupButton(btnClose, "btn_close_white_clicked", "btn_close_white");
            btnClose.Click += (s, e) => { this.Close(); };

        }
    }
}
