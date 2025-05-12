using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SAI.SAI.App.Views.Interfaces;

namespace SAI.SAI.App.Views.Pages
{
    public partial class YoloTutorial : UserControl, IYoloTutorialView
    {
        public YoloTutorial()
        {
            InitializeComponent();
            YoloTutorialRun.Click += (s, e) => RunButtonClicked?.Invoke(this, e);
        }

        public event EventHandler RunButtonClicked;

        private void YoloTutorial_Load(object sender, EventArgs e)
        {
        }

        public void ShowErrorMessage(string message) {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => ShowErrorMessage(message)));
            }
            else
            {
                MessageBox.Show(message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void AppendLog(string text)
        {
            if (logOutput.InvokeRequired)
            {
                logOutput.Invoke(new MethodInvoker(() => AppendLog(text)));
            }
            else
            {
                logOutput.AppendText(text + Environment.NewLine);
                logOutput.SelectionStart = logOutput.Text.Length;
                logOutput.ScrollToCaret();
            }
        }

        public void ClearLog()
        {
            if (logOutput.InvokeRequired)
            {
                logOutput.Invoke(new MethodInvoker(ClearLog));
            }
            else
            {
                logOutput.Clear();
            }
        }

        public void SetLogVisible(bool visible)
        {
            if (logOutput.InvokeRequired)
            {
                logOutput.Invoke(new MethodInvoker(() => SetLogVisible(visible)));
            }
            else
            {
                logOutput.Visible = visible;
            }
        }
    }
}
