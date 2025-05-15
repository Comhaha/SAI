using System;
using System.Drawing;
using System.Windows.Forms;
using SAI.SAI.App.Views.Interfaces;
using SAI.SAI.App.Views.Common;

namespace SAI.SAI.App.Views.Pages
{
    public partial class UcBlockGuide : UserControl
    {
        private readonly IMainView mainView;
        // 현재 이미지 인덱스
        private int currentIndex = 0;

        // 이미지 리소스 이름 배열
        private string[] imageNames = new string[]
        {
            "bg_blockGuide1",
            "bg_blockGuide2",
            "bg_blockGuide3",
            "bg_blockGuide4",
            "bg_blockGuide5",
            "bg_blockGuide6",
            "bg_blockGuide7"
        };

        // 버튼 위치 배열 (각 이미지별 위치)
        private Point[] nextButtonPositions = new Point[]
        {
            new Point(1201, 640), // 1번 next 버튼 위치
            new Point(812, 278), // 2번 
            new Point(1181, 451), // 3번 
            new Point(855, 400), // 4번 
            new Point(700, 352), // 5번 
            new Point(750, 599), // 6번 
            new Point(869, 191)  // 7번 
        };

        private Point[] preButtonPositions = new Point[]
        {
            new Point(1155, 640), // 1번 pre 버튼 위치
            new Point(766, 278), // 2번
            new Point(1135, 451), // 3번 
            new Point(749, 346), // 4번
            new Point(746, 352), // 5번 
            new Point(796, 599), // 6번 
            new Point(915, 191)  // 7번 
        };

        private Point[] closeButtonPositions = new Point[]
        {
            new Point(1194, 370), // 1번 close 버튼 위치
            new Point(805, 151), // 2번
            new Point(1167, 151), // 3번
            new Point(742, 151), // 4번
            new Point(739, 152), // 5번
            new Point(789, 472), // 6번
            new Point(908, 67)  // 7번
        };

        public UcBlockGuide(IMainView view)
        {
            InitializeComponent();
            this.mainView = view;

            ButtonUtils.SetupButton(btnPre, "btn_pre_guide_clicked", "btn_pre_guide");
            ButtonUtils.SetupButton(btnNext, "btn_next_guide_clicked", "btn_next_guide");
            ButtonUtils.SetupButton(btnClose, "btn_close_guide_clicked", "btn_close_guide");

            SetupControl();
            SetupButtonEvents();
            DisplayCurrentImage(); // 1번 이미지 표시
        }

        private void SetupControl()
        {
            // PictureBox 설정 수정
            pictureBox.Size = this.Size;
            pictureBox.Location = new Point(0, 0);  
            pictureBox.Dock = DockStyle.Fill; 
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.BackColor = Color.Transparent;

            // 버튼들을 pictureBox 위로
            btnNext.Parent = pictureBox;
            btnPre.Parent = pictureBox;
            btnClose.Parent = pictureBox;

            btnNext.BringToFront();
            btnPre.BringToFront();
            btnClose.BringToFront();
        }

        private void SetupButtonEvents()
        {
            btnNext.Click += BtnNext_Click;
            btnPre.Click += BtnPre_Click;
            btnClose.Click += BtnClose_Click;
        }

        private void DisplayCurrentImage()
        {
            try
            {
                string imageName = imageNames[currentIndex];
                var image = (Image)Properties.Resources.ResourceManager.GetObject(imageName);
                
                if (image == null)
                {
                    MessageBox.Show($"이미지 리소스를 찾을 수 없습니다: {imageName}");
                    return;
                }

                pictureBox.Image = image;
                pictureBox.Invalidate();  // pictureBox 강제 다시 그리기

                btnNext.Location = nextButtonPositions[currentIndex];
                btnPre.Location = preButtonPositions[currentIndex];
                btnClose.Location = closeButtonPositions[currentIndex];

                btnPre.Visible = (currentIndex > 0);
                btnNext.Visible = (currentIndex < imageNames.Length - 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"이미지 로드 오류: {ex.Message}");
            }
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (currentIndex < imageNames.Length - 1)
            {
                currentIndex++;
                DisplayCurrentImage();
            }
        }

        private void BtnPre_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                DisplayCurrentImage();
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            mainView.LoadPage(new UcTutorialBlockCode(mainView));
        }

        public void showDialog(Form dialog)
        {
            dialog.Owner = mainView as Form;
            dialog.ShowDialog();
        }
    }
}
