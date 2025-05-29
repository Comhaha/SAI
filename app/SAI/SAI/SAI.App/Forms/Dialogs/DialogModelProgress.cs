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
        // 서버 모니터링 취소를 위한 Presenter 참조
        private object serverPresenter; // YoloTutorialPresenter 또는 다른 Presenter

        
        public void SetProcess(Process process)
        {
            pythonProcess = process;
        }

        // 서버 모니터링을 취소할 수 있는 Presenter 설정
        public void SetPresenter(object presenter)
        {
            serverPresenter = presenter;
        }

        public DialogModelProgress()
        {
            InitializeComponent();

            DialogUtils.ApplyDefaultStyle(this, Color.Gray);

            ButtonUtils.SetupButton(btnClose, "btn_close_modelProgress_clicked", "btn_close_modelProgress");
            btnClose.Click += btnClose_Click;  // 이벤트 핸들러 등록

			// btnMinimize
			btnMinimize.BackColor = Color.Transparent;
			btnMinimize.PressedColor = Color.Transparent;
			btnMinimize.CheckedState.FillColor = Color.Transparent;
			btnMinimize.DisabledState.FillColor = Color.Transparent;
		    btnMinimize.Click += (s, e) => {
                this.WindowState = FormWindowState.Minimized; // 최소화
			};
			// btnMinimize 마우스 입력 될 때
			btnMinimize.MouseEnter += (s, e) =>
			{
				btnMinimize.BackColor = Color.Transparent;
				btnMinimize.BackgroundImage = Properties.Resources.btn_yellow_minimize_clicked;
			};
			// btnMinimize 마우스 떠날때
			btnMinimize.MouseLeave += (s, e) =>
			{
				btnMinimize.BackgroundImage = Properties.Resources.btn_yellow_minimize;
			};

			progressBarModelLearning.ProgressColor = ColorTranslator.FromHtml("#55A605"); // 내부 진행 색상

			guna2DragControl1.TargetControl = panelTitleBar;
			guna2DragControl1.TransparentWhileDrag = false;
			guna2DragControl1.UseTransparentDrag = false;
		}

		private void BtnMinimize_Click(object sender, EventArgs e)
		{
			throw new NotImplementedException();
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
                // DialogQuitInference의 인스턴스 생성
                using (DialogQuitInference quitDialog = new DialogQuitInference())
                {
                    // 다이얼로그 표시 및 결과 확인
                    if (quitDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        // 사용자가 확인을 선택한 경우에만 프로세스 종료
                        if (pythonProcess != null)
                        {
                            if (!pythonProcess.HasExited)
                            {
                                pythonProcess.Kill();         // 프로세스 강제 종료
                            }
                            pythonProcess.Dispose();      // 리소스 정리
                        }

                        // 서버 모니터링 취소 (YoloTutorialPresenter인 경우)
                        if (serverPresenter != null)
                        {
                            // Reflection을 사용하여 CancelServerMonitoring 메서드 호출
                            var cancelMethod = serverPresenter.GetType().GetMethod("CancelServerMonitoring");
                            if (cancelMethod != null)
                            {
                                cancelMethod.Invoke(serverPresenter, null);
                            }
                        }

                        this.DialogResult = DialogResult.Cancel;
                        this.Close();
                    }
                    // 사용자가 취소를 선택한 경우 아무것도 하지 않음
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
        }
    }
}
