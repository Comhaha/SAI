using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using SAI.SAI.App.Views.Common;

namespace SAI.SAI.App.Forms.Dialogs
{
    public partial class DialogModelProgress : Form

    {
        private Process pythonProcess;
        public void SetProcess(Process process)
        {
            pythonProcess = process;
        }

        public DialogModelProgress()
        {
            InitializeComponent();

            DialogUtils.ApplyDefaultStyle(this, Color.Gray);

            ButtonUtils.SetupButton(btnClose, "btn_yellow_close_train_clicked", "btn_yellow_close_train");
            btnClose.Click += btnClose_Click;  // 이벤트 핸들러 등록

            guna2ProgressBar1.ProgressColor = ColorTranslator.FromHtml("#55A605"); // 내부 진행 색상
        }

        public void UpdateProgress(double progress, string message)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateProgress(progress, message)));
                return;
            }

            guna2ProgressBar1.Value = (int)progress;
            lblStatus.Text = message;
        }

        public void Reset()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(Reset));
                return;
            }

            guna2ProgressBar1.Value = 0;
            lblStatus.Text = "준비 중...";
        }

        // 진행률 다이얼로그 닫기 버튼 누를시
        // 외부에서 실행되던 스크립트가 아예 중단됨
        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (pythonProcess != null)
                {
                    if (!pythonProcess.HasExited)
                    {
                        pythonProcess.Kill();         // 프로세스 강제 종료
                    }
                    pythonProcess.Dispose();      // 리소스 정리
                }
            }
            catch (InvalidOperationException)
            {
                // 이미 종료된 프로세스이거나 연결이 끊긴 경우 무시
            }
            catch (Exception ex)
            {
                MessageBox.Show($"프로세스를 종료하는 중 오류가 발생했습니다:\n{ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.DialogResult = DialogResult.Cancel;
            this.Close();

        }
    }
}
