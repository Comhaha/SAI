using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SAI.SAI.App.Models;
using SAI.SAI.App.Views.Interfaces;
using static SAI.SAI.App.Models.BlocklyModel;

namespace SAI.SAI.App.Views.Pages
{
    public partial class DynamicUcPracticeBlockCode : UserControl
    {
        private readonly IMainView mainView;
        private BlocklyModel blocklyModel;
        public DynamicUcPracticeBlockCode(IMainView view)
        {
            this.mainView = view;
            InitializeComponent();

           
        }



        public void showDialog(Form dialog)
        {
            dialog.Owner = mainView as Form;
            dialog.ShowDialog();
        }
    }
}
