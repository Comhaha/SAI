using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using SAI.App.Presenters;
using SAI.App.Views.Interfaces;

namespace SAI.App.Views.Pages

{
    public partial class UcTutorialGuide : UserControl
	{
		private readonly IMainView mainView;

		private int currentPage = 0;
        private int totalPages = 11; // 전체 튜토리얼 3장 + 라벨링 튜토리얼 8장
        
        // 각 페이지별 버튼 위치 저장용 리스트
        private List<Point> prevButtonPositions = new List<Point>();
        private List<Point> nextButtonPositions = new List<Point>();
        private List<Point> exitButtonPositions = new List<Point>(); // exit 버튼 위치 리스트 추가
        
        public UcTutorialGuide(IMainView view)
        {
            InitializeComponent();
            SetupButtonPositions();
            SetupButtonEvents();
            InitializeProgressIndicators();

			this.mainView = view;
		}

        private void UcTutorialGuide_Load(object sender, EventArgs e)
        {
            // 초기 페이지 설정
            UpdatePage(0);
        }
        
        private void SetupButtonEvents()
        {
            // 버튼 이벤트 핸들러 연결
            preBtn.Click += PreBtn_Click;
            nextBtn.Click += NextBtn_Click;
            goLabelingBtn.Click += goLabelingBtn_Click;
            goToLabeling.Click += goToLabeling_Click;
            exit.Click += exit_Click;
        }
        private void InitializeProgressIndicators()
        { 
            this.goLabelingBtn.CheckedState.FillColor = Color.Transparent;
            this.goLabelingBtn.PressedColor = Color.Transparent;
            this.goLabelingBtn.HoverState.FillColor = Color.Transparent;
            goLabelingBtn.Visible = false;
        }


        private void SetupButtonPositions()
        {
            // 전체 튜토리얼 가이드 1 (1.5배 확대)
            prevButtonPositions.Add(new Point(326, 540)); // 첫 페이지 이전 버튼 위치
            nextButtonPositions.Add(new Point(1539, 540)); // 첫 페이지 다음 버튼 위치
            exitButtonPositions.Add(new Point(0, 0)); // 안 보이는 위치 (전체 튜토리얼)
            
            // 전체 튜토리얼 가이드 2 (1.5배 확대)
            prevButtonPositions.Add(new Point(326, 540));
            nextButtonPositions.Add(new Point(1539, 540));
            exitButtonPositions.Add(new Point(0, 0)); // 안 보이는 위치 (전체 튜토리얼)
            
            // 전체 튜토리얼 가이드 3 (1.5배 확대)
            prevButtonPositions.Add(new Point(326, 540));
            nextButtonPositions.Add(new Point(1539, 540));
            exitButtonPositions.Add(new Point(0, 0)); // 안 보이는 위치 (전체 튜토리얼)

            // 라벨링 가이드 1 (1.5배 확대)
            prevButtonPositions.Add(new Point(263, 581));
            nextButtonPositions.Add(new Point(1245, 612));
            exitButtonPositions.Add(new Point(1270, 430));

            // 라벨링 가이드 2 (1.5배 확대)
            prevButtonPositions.Add(new Point(1176, 316));
            nextButtonPositions.Add(new Point(1245, 316));
            exitButtonPositions.Add(new Point(1269, 166));

            // 라벨링 가이드 3 (1.5배 확대)
            prevButtonPositions.Add(new Point(1432, 342));
            nextButtonPositions.Add(new Point(1501, 342));
            exitButtonPositions.Add(new Point(1526, 155));

            // 라벨링 가이드 4 (1.5배 확대)
            prevButtonPositions.Add(new Point(1143, 312));
            nextButtonPositions.Add(new Point(1212, 312));
            exitButtonPositions.Add(new Point(1237, 126));

            // 라벨링 가이드 5 (1.5배 확대)
            prevButtonPositions.Add(new Point(1318, 556));
            nextButtonPositions.Add(new Point(1387, 556));
            exitButtonPositions.Add(new Point(1412, 369));

            // 라벨링 가이드 6 (1.5배 확대)
            prevButtonPositions.Add(new Point(1200, 879));
            nextButtonPositions.Add(new Point(1269, 879));
            exitButtonPositions.Add(new Point(1294, 620));

            // 라벨링 가이드 7 (1.5배 확대)
            prevButtonPositions.Add(new Point(1484, 725));
            nextButtonPositions.Add(new Point(1553, 725));
            exitButtonPositions.Add(new Point(1588, 214));

            // 라벨링 가이드 8 (1.5배 확대)
            prevButtonPositions.Add(new Point(1560, 288));
            nextButtonPositions.Add(new Point(1640, 288));
            exitButtonPositions.Add(new Point(1667, 140));
        }

        private void UpdatePage(int pageIndex)
        {
            if (pageIndex < 0 || pageIndex >= totalPages)
                return;
        
            currentPage = pageIndex;
            
            // 1. 배경 이미지 설정
            if (pageIndex < 3) // 전체 튜토리얼 (1~3)
                this.BackgroundImage = Properties.Resources.ResourceManager.GetObject($"전체튜토리얼가이드{pageIndex + 1}") as System.Drawing.Image;
            else // 라벨링 가이드 (1~8)
                this.BackgroundImage = Properties.Resources.ResourceManager.GetObject($"라벨링가이드{pageIndex - 2}") as System.Drawing.Image;
            
            // 2. 기본 버튼 상태 초기화
            preBtn.Visible = false;
            nextBtn.Visible = false;
            goLabelingBtn.Visible = false;
            
            // 3. 페이지 구분에 따른 버튼 처리
            bool isWholeTutorial = (currentPage < 3);
            
            // goToLabeling 버튼은 전체 튜토리얼에서만 표시
            goToLabeling.Visible = isWholeTutorial;
            
            // exit 버튼은 라벨링 가이드에서만 표시하고 페이지별 위치 설정
            exit.Visible = !isWholeTutorial;
            if (!isWholeTutorial && pageIndex < exitButtonPositions.Count)
            {
                exit.Location = exitButtonPositions[pageIndex];
            }
            
            // 4. 페이지별 버튼 설정
            switch (currentPage)
            {
                case 0: // 첫 페이지
                    nextBtn.Location = nextButtonPositions[0];
                    nextBtn.Visible = true;
                    break;
                
                case 2: // 전체 튜토리얼 마지막
                    preBtn.Location = prevButtonPositions[2];
                    preBtn.Visible = true;
                    goLabelingBtn.Visible = true;
                    break;
                
                case 3: // 라벨링 첫 페이지
                    nextBtn.Location = nextButtonPositions[3];
                    nextBtn.Visible = true;
                    nextBtn.Size = new Size(45, 45);
                    preBtn.Size = new Size(45, 45);
                    nextBtn.ImageSize = new Size(45, 45);
                    preBtn.ImageSize = new Size(45, 45);
                    break;
                
                case 10: // 마지막 페이지
                    preBtn.Location = prevButtonPositions[10];
                    preBtn.Visible = true;
                    break;
                
                default: // 중간 페이지들
                    preBtn.Location = prevButtonPositions[currentPage];
                    nextBtn.Location = nextButtonPositions[currentPage];
                    preBtn.Visible = true;
                    nextBtn.Visible = true;
                    break;
            }
        }

        private void PreBtn_Click(object sender, EventArgs e)
        {
            if (currentPage > 0)
            {
                UpdatePage(currentPage - 1);
            }
        }

        private void NextBtn_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages - 1)
            {
                UpdatePage(currentPage + 1);
            }
        }

        private void goLabelingBtn_Click(object sender, EventArgs e)
        {
            UpdatePage(3); // 라벨링 튜토리얼 첫 페이지(4번째)로 이동
        }

        private void goToLabeling_Click(object sender, EventArgs e)
        {
            UpdatePage(3); // 라벨링 가이드 첫 페이지로 이동
        }

        private void exit_Click(object sender, EventArgs e)
        {
            mainView.LoadPage(new UcLabelGuide(mainView));
		}

        private void LabelingTutorialPanel0_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void giudeOpacityPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {

        }

		public void showDialog(Form dialog)
		{
			dialog.Owner = mainView as Form;
			dialog.ShowDialog();
		}

        private void goToLabeling_Click_1(object sender, EventArgs e)
        {

        }

        private void nextBtn_Click_1(object sender, EventArgs e)
        {

        }

        private void exit_Click_1(object sender, EventArgs e)
        {

        }

        private void goToLabeling_Click_2(object sender, EventArgs e)
        {

        }

        private void goLabelingBtn_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }
    }
}
