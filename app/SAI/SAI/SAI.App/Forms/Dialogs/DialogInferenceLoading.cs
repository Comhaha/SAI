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
using SAI.SAI.App.Views.Common;


namespace SAI.SAI.App.Forms.Dialogs
{
    public partial class DialogInferenceLoading : Form
    {
        // 파이썬 스크립트 종료 감지를 위함
        private Process pythonProcess;
        
        // --- 폼 드래그를 위한 멤버 변수 추가 ---
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        // ------------------------------------
        
        // 버튼 원래 이미지 저장용 변수
        private Image originalButtonImage;
        
        public void SetProcess(Process process)
        {
            pythonProcess = process;
        }

        public DialogInferenceLoading()
        {
            InitializeComponent();

            DialogUtils.ApplyDefaultStyle(this, Color.Gray);

            //ButtonUtils.SetupButton(guna2Button1, "bg_yellow_btn_close_clicked", "bg_yellow_btn_close");
            guna2Button1.Click += guna2Button1_Click;  // 이벤트 핸들러 등록
            
            // 호버 이벤트 핸들러 등록
            guna2Button1.MouseEnter += Guna2Button1_MouseEnter;
            guna2Button1.MouseLeave += Guna2Button1_MouseLeave;
            
            // 원래 이미지 저장
            originalButtonImage = guna2Button1.BackgroundImage;

            // 폼 스타일 설정
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.TopMost = true;
            
            // --- 폼 드래그 이벤트 핸들러 등록 ---
            this.MouseDown += DialogInferenceLoading_MouseDown;
            this.MouseMove += DialogInferenceLoading_MouseMove;
            this.MouseUp += DialogInferenceLoading_MouseUp;
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
        
        // --- 폼 드래그 이벤트 핸들러 구현 ---
        private void DialogInferenceLoading_MouseDown(object sender, MouseEventArgs e)
        {
            // 왼쪽 마우스 버튼 클릭 시에만 드래그 시작
            if (e.Button == MouseButtons.Left)
            {
                dragging = true;
                dragCursorPoint = Cursor.Position; // 현재 마우스 커서의 화면상 위치
                dragFormPoint = this.Location;     // 현재 폼의 화면상 위치
            }
        }

        private void DialogInferenceLoading_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint)); // 마우스 이동량 계산
                this.Location = Point.Add(dragFormPoint, new Size(dif)); // 폼 위치 변경
            }
        }

        private void DialogInferenceLoading_MouseUp(object sender, MouseEventArgs e)
        {
            // 왼쪽 마우스 버튼을 뗄 때 드래그 종료
            if (e.Button == MouseButtons.Left)
            {
                dragging = false;
            }
        }
        // ------------------------------------

        // 마우스가 버튼 위에 올라갔을 때 이미지 변경
        private void Guna2Button1_MouseEnter(object sender, EventArgs e)
        {
            guna2Button1.BackgroundImage = global::SAI.Properties.Resources.btn_close_prepare_clicked;
        }

        // 마우스가 버튼에서 벗어났을 때 원래 이미지로 복원
        private void Guna2Button1_MouseLeave(object sender, EventArgs e)
        {
            guna2Button1.BackgroundImage = originalButtonImage;
        }
    }
}
