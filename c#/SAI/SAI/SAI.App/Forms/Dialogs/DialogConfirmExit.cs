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
	public partial class DialogConfirmExit : Form
	{
		public DialogConfirmExit()
		{
			InitializeComponent();
			this.StartPosition = FormStartPosition.CenterParent; // 부모 기준 중앙
			this.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.TopMost = true;
		}
	}
}
