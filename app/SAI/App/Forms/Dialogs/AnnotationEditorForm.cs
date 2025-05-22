using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms; // Guna UI를 사용하고 있으니 그대로 둡니다.

namespace SAI.App.Forms.Dialogs
{
    public partial class AnnotationEditorForm : Form
    {
        // 저장 이벤트 추가 - 외부에서 저장 이벤트를 구독할 수 있음
        public event EventHandler<string> SaveClicked;

        public string AnnotationText { get; private set; }
        public bool IsSaved { get; private set; } = false;

        // 정확도 속성 추가
        public double Accuracy { get; set; }

        // --- 폼 드래그를 위한 멤버 변수 추가 ---
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        // ------------------------------------

        public AnnotationEditorForm(string initialText = "", double accuracy = 0.0)
        {
            InitializeComponent();

            // 이벤트 핸들러 등록
            saveBtn.Click += SaveButton_Click;
            cancelBtn.Click += (sender, e) => this.Close();
            xBtn.Click += (sender, e) => this.Close();

            // 초기값 설정
            Accuracy = accuracy;
            annotationText1.Text = initialText;
            pleaseNamePanel.Visible = false; // 초기에는 경고 패널 숨김

            // 초기 상태 설정
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterParent;

            // --- 폼 드래그 이벤트 핸들러 등록 ---
            this.MouseDown += AnnotationEditorForm_MouseDown;
            this.MouseMove += AnnotationEditorForm_MouseMove;
            this.MouseUp += AnnotationEditorForm_MouseUp;

            // 폼이 화면에 표시된 후 포커스 설정
            this.Shown += (s, args) => 
            {
                annotationText1.Focus();
                annotationText1.SelectAll();
            };
        }

        // 정확도 설정 메서드
        public void SetAccuracy(double accuracy)
        {
            Accuracy = accuracy;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {   
            if (!annotationText1.Text.Trim().Any())
            {
                pleaseNamePanel.Visible = true; // 텍스트가 비어있으면 경고 패널 표시
                return;
            }
            AnnotationText = annotationText1.Text;
            IsSaved = true;

            // 저장 이벤트 발생 - 텍스트 전달
            SaveClicked?.Invoke(this, AnnotationText);

            this.Close();
        }

        // --- 폼 드래그 이벤트 핸들러 구현 ---
        private void AnnotationEditorForm_MouseDown(object sender, MouseEventArgs e)
        {
            // 왼쪽 마우스 버튼 클릭 시에만 드래그 시작
            if (e.Button == MouseButtons.Left)
            {
                dragging = true;
                dragCursorPoint = Cursor.Position; // 현재 마우스 커서의 화면상 위치
                dragFormPoint = this.Location;     // 현재 폼의 화면상 위치
            }
        }

        private void AnnotationEditorForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint)); // 마우스 이동량 계산
                this.Location = Point.Add(dragFormPoint, new Size(dif)); // 폼 위치 변경
            }
        }

        private void AnnotationEditorForm_MouseUp(object sender, MouseEventArgs e)
        {
            // 왼쪽 마우스 버튼을 뗄 때 드래그 종료
            if (e.Button == MouseButtons.Left)
            {
                dragging = false;
            }
        }

        // ------------------------------------
    }
}