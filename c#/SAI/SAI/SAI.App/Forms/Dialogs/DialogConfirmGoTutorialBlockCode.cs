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
	public partial class DialogConfirmGoTutorialBlockCode : Form
	{
		private DialogLoadPagePresenter presenter;
		public DialogConfirmGoTutorialBlockCode()
		{
			InitializeComponent();
		}
		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);

			var view = this.Owner as IMainView;
			presenter = new DialogLoadPagePresenter(view);
		}

		private void guna2Button1_Click(object sender, EventArgs e)
		{
			presenter.clickGoTutorialBlockCode();
			this.Close();
		}
	}
}
