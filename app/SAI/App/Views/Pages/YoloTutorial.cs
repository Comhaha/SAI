using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SAI.SAI.App.Presenters;
using SAI.SAI.App.Views.Interfaces;
using SAI.SAI.Application.Service;

namespace SAI.SAI.App.Views.Pages
{
    public partial class YoloTutorial : UserControl, IYoloTutorialView
    {
        private YoloTutorialPresenter presenter;

        public YoloTutorial()
        {
            presenter = new YoloTutorialPresenter(this);
            InitializeComponent();
            YoloTutorialRun.Click += (s, e) => RunButtonClicked?.Invoke(this, e);
            logOutput.Visible = false; // 초기에는 로그 숨기기
        }

        public event EventHandler RunButtonClicked;

        private void YoloTutorial_Load(object sender, EventArgs e)
        {
            // 초기화 로직이 필요한 경우 여기에 추가
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

        public void ShowInferenceResult(PythonService.InferenceResult result)
        {
            throw new NotImplementedException();
        }

        public void ShowTrainingChart(string csvPath)
        {
            return;
        }

        public void ShowTutorialTrainingChart(string csvPath)
        {
            return;
        }
    }
}
