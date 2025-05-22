using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using SAI.App.Views.Common;


namespace SAI.App.Forms.Dialogs
{
    public partial class DialogInferenceLoading : Form
    {
        // 파이썬 스크립트 종료 감지를 위함
        private Process pythonProcess;
        
        public DialogInferenceLoading()
        {
            InitializeComponent();

            DialogUtils.ApplyDefaultStyle(this, Color.Gray);

            //ButtonUtils.SetupButton(guna2Button1, "bg_yellow_btn_close_clicked", "bg_yellow_btn_close");
            guna2Button1.Click += guna2Button1_Click;  // 이벤트 핸들러 등록 

            // 폼 스타일 설정
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.TopMost = true;
        }

        public void UpdateProgress(double progress, string message)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateProgress(progress, message)));
                return;
            }
        }

        public void Reset()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(Reset));
                return;
            }
        }

        // 진행률 다이얼로그 닫기 버튼 누를시
        // 외부에서 실행되던 스크립트가 아예 중단됨
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                // DialogQuitInference의 인스턴스 생성
                using (DialogQuitInference quitDialog = new DialogQuitInference())
                {
                    // 다이얼로그 표시 및 결과 확인
                    if (quitDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        // 사용자가 확인을 선택한 경우
                        if (pythonProcess != null && !pythonProcess.HasExited)
                        {
                            pythonProcess.Kill();         // 프로세스 강제 종료
                            pythonProcess.Dispose();      // 리소스 정리
                        }

                        this.DialogResult = DialogResult.Cancel;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DialogInferenceLoading_Load(object sender, EventArgs e)
        {

        }
    }
}
