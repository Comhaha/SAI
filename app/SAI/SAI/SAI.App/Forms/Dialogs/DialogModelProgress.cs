using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using SAI.SAI.App.Views.Common;

namespace SAI.SAI.App.Forms.Dialogs
{
    public partial class DialogModelProgress : Form

    {
        // 파이썬 스크립트 종료 감지를 위함
        private Process pythonProcess;
        
        // --- 폼 드래그를 위한 멤버 변수 추가 ---
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        // ------------------------------------
        
        public void SetProcess(Process process)
        {
            pythonProcess = process;
        }

        public DialogModelProgress()
        {
            InitializeComponent();

            DialogUtils.ApplyDefaultStyle(this, Color.Gray);

            ButtonUtils.SetupButton(btnClose, "btn_close_modelProgress_clicked", "btn_close_modelProgress");
            btnClose.Click += btnClose_Click;  // 이벤트 핸들러 등록

            progressBarModelLearning.ProgressColor = ColorTranslator.FromHtml("#55A605"); // 내부 진행 색상
            
            // --- 폼 드래그 이벤트 핸들러 등록 ---
            this.MouseDown += DialogModelProgress_MouseDown;
            this.MouseMove += DialogModelProgress_MouseMove;
            this.MouseUp += DialogModelProgress_MouseUp;
        }

        public void UpdateProgress(double progress, string message)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateProgress(progress, message)));
                return;
            }

            // 좌측 라운드 안먹는 이슈 해결
            int adjustedValue = Math.Max((int)progress, 3);
            progressBarModelLearning.Value = (int)adjustedValue; //(int)progress
            lblStatus.Text = message;
        }

        public void Reset()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(Reset));
                return;
            }

			progressBarModelLearning.Value = 0;
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

        // --- 폼 드래그 이벤트 핸들러 구현 ---
        private void DialogModelProgress_MouseDown(object sender, MouseEventArgs e)
        {
            // 왼쪽 마우스 버튼 클릭 시에만 드래그 시작
            if (e.Button == MouseButtons.Left)
            {
                dragging = true;
                dragCursorPoint = Cursor.Position; // 현재 마우스 커서의 화면상 위치
                dragFormPoint = this.Location;     // 현재 폼의 화면상 위치
            }
        }

        private void DialogModelProgress_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint)); // 마우스 이동량 계산
                this.Location = Point.Add(dragFormPoint, new Size(dif)); // 폼 위치 변경
            }
        }

        private void DialogModelProgress_MouseUp(object sender, MouseEventArgs e)
        {
            // 왼쪽 마우스 버튼을 뗄 때 드래그 종료
            if (e.Button == MouseButtons.Left)
            {
                dragging = false;
            }
        }

    }
}
