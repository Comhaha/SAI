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
using SAI.SAI.App.Presenters;
using SAI.SAI.App.Views.Interfaces;

namespace SAI.SAI.App.Views.Pages

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
            // 전체 튜토리얼 가이드 1
            prevButtonPositions.Add(new Point(210, 339)); // 첫 페이지 이전 버튼 위치
            nextButtonPositions.Add(new Point(1000, 339)); // 첫 페이지 다음 버튼 위치
            exitButtonPositions.Add(new Point(0, 0)); // 안 보이는 위치 (전체 튜토리얼)
            
            // 전체 튜토리얼 가이드 2
            prevButtonPositions.Add(new Point(210, 339));
            nextButtonPositions.Add(new Point(1000, 339));
            exitButtonPositions.Add(new Point(0, 0)); // 안 보이는 위치 (전체 튜토리얼)
            
            // 전체 튜토리얼 가이드 3
            prevButtonPositions.Add(new Point(210, 339));
            nextButtonPositions.Add(new Point(1000, 339));
            exitButtonPositions.Add(new Point(0, 0)); // 안 보이는 위치 (전체 튜토리얼)

            // 라벨링 가이드 1
            prevButtonPositions.Add(new Point(175, 387));
            nextButtonPositions.Add(new Point(831, 385));
            exitButtonPositions.Add(new Point(841, 260));

            // 라벨링 가이드 2
            prevButtonPositions.Add(new Point(784, 226));
            nextButtonPositions.Add(new Point(830, 226));
            exitButtonPositions.Add(new Point(840, 122));

            // 라벨링 가이드 3
            prevButtonPositions.Add(new Point(955, 262));
            nextButtonPositions.Add(new Point(1001, 262));
            exitButtonPositions.Add(new Point(1011, 134));

            // 라벨링 가이드 4
            prevButtonPositions.Add(new Point(762, 237));
            nextButtonPositions.Add(new Point(808, 237));
            exitButtonPositions.Add(new Point(817, 109));

            // 라벨링 가이드 5
            prevButtonPositions.Add(new Point(879, 399));
            nextButtonPositions.Add(new Point(925, 399));
            exitButtonPositions.Add(new Point(935, 267));

            // 라벨링 가이드 6
            prevButtonPositions.Add(new Point(801, 615));
            nextButtonPositions.Add(new Point(847, 615));
            exitButtonPositions.Add(new Point(857, 439));

            // 라벨링 가이드 7
            prevButtonPositions.Add(new Point(987, 525));
            nextButtonPositions.Add(new Point(1033, 525));
            exitButtonPositions.Add(new Point(1050, 181));

            // 라벨링 가이드 8
            prevButtonPositions.Add(new Point(1039, 219));
            nextButtonPositions.Add(new Point(1093, 219));
            exitButtonPositions.Add(new Point(1102, 117));
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
                    nextBtn.Size = new Size(30, 30);
                    preBtn.Size = new Size(30, 30);
                    nextBtn.ImageSize = new Size(30, 30);
                    preBtn.ImageSize = new Size(30, 30);
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
    }
}
