using System.Drawing;
using System.Windows.Forms;
using SAI.SAI.App.Views.Common;

namespace SAI.SAI.App.Forms.Dialogs
{
    public partial class DialogModelProgress : Form
    {
        public DialogModelProgress()
        {
            InitializeComponent();

            DialogUtils.ApplyDefaultStyle(this, Color.Gray);

            ButtonUtils.SetupButton(btnClose, "btn_yellow_close_train_clicked", "btn_yellow_close_train");
            btnClose.Click += (s, e) => { this.Close(); };

            progressBarModelLearning.ProgressColor = ColorTranslator.FromHtml("#55A605"); // 내부 진행 색상
        }
    }
}
