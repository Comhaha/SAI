using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace SAI.SAI.App.Views.Pages
{
    public partial class AnnotationEditorForm : Form
    {
        private Guna2TextBox annotationTextBox;
        private Guna2Button cancelButton;
        private Guna2Button saveButton;
        private Guna2HtmlLabel accuiredLabel; // 정확도 표시 라벨 추가

        // 저장 이벤트 추가 - 외부에서 저장 이벤트를 구독할 수 있음
        public event EventHandler<string> SaveClicked;

        public string AnnotationText { get; private set; }
        public bool IsSaved { get; private set; } = false;

        // 정확도 속성 추가
        public double Accuracy { get; set; }

        public AnnotationEditorForm(string initialText = "", double accuracy = 0.0)
        {
            Accuracy = accuracy;
            InitializeComponents();
            annotationTextBox.Text = initialText;
            UpdateAccuracyLabel(); // 정확도 라벨 초기화
        }

        private void InitializeComponents()
        {
            // 폼 설정
            this.Text = "Annotation Editor";
            this.Size = new Size(300, 180); // 폼 높이를 정확도 라벨 위한 공간 추가
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(255, 242, 242);

            // 텍스트 상자 설정
            annotationTextBox = new Guna2TextBox
            {
                Location = new Point(20, 20),
                Size = new Size(260, 30),
                PlaceholderText = "Enter annotation text"
            };

            // 정확도 라벨 설정
            accuiredLabel = new Guna2HtmlLabel
            {
                Location = new Point(20, 55),
                Size = new Size(260, 18),
                ForeColor = Color.FromArgb(0, 120, 0),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Text = "정확도: 0.0%"
            };

            // 취소 버튼 설정
            cancelButton = new Guna2Button
            {
                Text = "CANCEL",
                Size = new Size(80, 30),
                Location = new Point(100, 100), // 버튼 위치 조정
                FillColor = Color.White,
                ForeColor = Color.Black,
                BorderColor = Color.Gray,
                BorderThickness = 1
            };
            cancelButton.Click += (sender, e) => this.Close();

            // 저장 버튼 설정
            saveButton = new Guna2Button
            {
                Text = "SAVE",
                Size = new Size(80, 30),
                Location = new Point(200, 100), // 버튼 위치 조정
                FillColor = Color.FromArgb(94, 148, 255)
            };
            saveButton.Click += SaveButton_Click;

            // 컨트롤 추가
            this.Controls.Add(annotationTextBox);
            this.Controls.Add(accuiredLabel);
            this.Controls.Add(cancelButton);
            this.Controls.Add(saveButton);
        }

        // 정확도 라벨 업데이트 메서드
        public void UpdateAccuracyLabel()
        {
            // 정확도에 따라 색상 설정
            if (Accuracy >= 80)
                accuiredLabel.ForeColor = Color.FromArgb(0, 150, 0); // 진한 녹색
            else if (Accuracy >= 50)
                accuiredLabel.ForeColor = Color.FromArgb(180, 150, 0); // 노란색
            else
                accuiredLabel.ForeColor = Color.FromArgb(180, 0, 0); // 빨간색

            // 정확도 표시
            accuiredLabel.Text = $"정확도: {Accuracy:F1}%";
        }

        // 정확도 설정 메서드
        public void SetAccuracy(double accuracy)
        {
            Accuracy = accuracy;
            UpdateAccuracyLabel();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            AnnotationText = annotationTextBox.Text;
            IsSaved = true;

            // 저장 이벤트 발생 - 텍스트 전달
            SaveClicked?.Invoke(this, AnnotationText);

            this.Close();
        }
    }
}

