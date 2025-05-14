using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAI.SAI.App.Forms.Dialogs
{
    public partial class TutorialGuideForm : Form
    {
        private int currentStep = 0;
        private List<string> tutorialContents = new List<string>();
        private List<Point> tutorialPositions = new List<Point>();
        private List<Size> tutorialSizes = new List<Size>();
        private Control targetControl; // 튜토리얼이 가리킬 컨트롤

        public TutorialGuideForm(Control targetControl)
        {
            InitializeComponent();
            this.targetControl = targetControl;
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            
            // 배경 투명하게 설정
            this.BackColor = Color.White;
            this.TransparencyKey = Color.White;
            
            SetupTutorialSteps();
            ShowCurrentStep();
        }

        private void SetupTutorialSteps()
        {
            // 단계별 내용, 위치, 크기 설정
            tutorialContents.Add("0단계: 라벨링\n이미지, 텍스트, 음성 등 다양한 데이터에 정답을 달아주는 작업입니다.");
            tutorialPositions.Add(new Point(50, 50));
            tutorialSizes.Add(new Size(400, 200));

            tutorialContents.Add("1단계: 모델 추론\n학습된 AI 모델이 새로운 데이터를 받아서 어떻게 처리할지 판단을 내립니다.");
            tutorialPositions.Add(new Point(150, 150));
            tutorialSizes.Add(new Size(400, 200));

            // 추가 단계들...
        }

        private void ShowCurrentStep()
        {
            // 현재 단계에 맞게 폼 위치, 크기, 내용 업데이트
            this.Location = CalculatePosition(tutorialPositions[currentStep]);
            this.Size = tutorialSizes[currentStep];
            //lblContent.Text = tutorialContents[currentStep];
            //lblPageNumber.Text = $"{currentStep + 1}/{tutorialContents.Count}";
            
            //btnPrevious.Visible = currentStep > 0;
            //btnNext.Text = currentStep < tutorialContents.Count - 1 ? "다음" : "완료";
        }

        private Point CalculatePosition(Point basePosition)
        {
            // 타겟 컨트롤 기준으로 위치 계산
            Point targetPoint = targetControl.PointToScreen(basePosition);
            return targetPoint;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentStep < tutorialContents.Count - 1)
            {
                currentStep++;
                ShowCurrentStep();
            }
            else
            {
                this.Close();
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (currentStep > 0)
            {
                currentStep--;
                ShowCurrentStep();
            }
        }
    }
}
