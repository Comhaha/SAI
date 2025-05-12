using SAI.SAI.App.Presenters;
using SAI.SAI.App.Views.Interfaces;
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
    public partial class DialogModelSelect : Form
    {
        private DialogModelSelectPresenter presenter;
		public DialogModelSelect()
        {
            InitializeComponent();
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);

			var view = this.Owner as IMainView;
			presenter = new DialogModelSelectPresenter(view);
		}

		private void ibtnGoTutorial_Click(object sender, EventArgs e)
		{
            presenter.clickTutorial();
			this.Close();
		}

		private void ibtnGoPractice_Click(object sender, EventArgs e)
		{
			presenter.clickTrain();
			this.Close();
		}
	}
}
