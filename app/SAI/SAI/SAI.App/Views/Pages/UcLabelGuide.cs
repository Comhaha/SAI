using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clipper2Lib;
using SAI.SAI.App.Views.Interfaces;
using SAI.SAI.App.Forms.Dialogs;
using SAI.SAI.App.Models.Events;
using SAI.SAI.App.Presenters;

namespace SAI.SAI.App.Views.Pages
{
    public partial class UcLabelGuide : UserControl, IUcShowDialogView
    {
        private readonly IMainView mainView;

		// 기본 도구 상태 및 이미지 관련
		private bool isDragging = false;
        private Point startPoint = new Point(0, 0);
        private bool isHandToolActive = false;
        private List<Image> images = new List<Image>();
        private int currentImageIndex = 0;
        private List<Image> levelImages = new List<Image>();
        private List<Image> classImages = new List<Image>();

        // 사각형 그리기 관련 변수
        private bool isSquareToolActive = false;
        private Rectangle currentRect = Rectangle.Empty;
        private List<Tuple<Rectangle, string>> boundingBoxes = new List<Tuple<Rectangle, string>>();
        private Point rectStartPoint;

        // 바운딩 박스 편집 관련 변수
        private bool isEditingBoundingBox = false;
        private int selectedBoxIndex = -1;
        private Rectangle editingRect = Rectangle.Empty;
        private bool isBoundingBoxDragging = false;
        private int dragHandleIndex = -1; // 0: 좌상, 1: 우상, 2: 좌하, 3: 우하, 4: 중앙(이동)
        private const int HandleSize = 10; // 핸들 크기

        // 이미지별 라벨링 데이터 저장
        private Dictionary<int, string> imageClassifications = new Dictionary<int, string>();

        // 이미지별 바운딩 박스 저장
        private Dictionary<int, List<Tuple<Rectangle, string>>> imageBoundingBoxes = new Dictionary<int, List<Tuple<Rectangle, string>>>();

        // 이미지별 세그멘테이션 데이터 저장
        private Dictionary<int, object> imageSegmentations = new Dictionary<int, object>();

        // 폴리곤 그리기 관련 변수
        private bool isPolygonToolActive = false;
        private List<Point> polygonPoints = new List<Point>();
        private Dictionary<int, List<Tuple<List<Point>, string>>> imagePolygons = new Dictionary<int, List<Tuple<List<Point>, string>>>();
        private Point? hoveredPoint = null;
        private bool isEditingPolygon = false;
        private int selectedPolygonIndex = -1;
        private int dragPointIndex = -1;
        private List<Point> editingPolygonPoints = new List<Point>();
        private bool isPolygonPointDragging = false;

        // 정답 데이터 저장용 변수
        private Dictionary<int, string> groundTruthClassifications = new Dictionary<int, string>();
        private Dictionary<int, Tuple<Rectangle, string>> groundTruthBoundingBoxes = new Dictionary<int, Tuple<Rectangle, string>>();
        private Dictionary<int, Tuple<List<Point>, string>> groundTruthPolygons = new Dictionary<int, Tuple<List<Point>, string>>();

        // 이미지별 정확도 저장을 위한 변수 
        private Dictionary<int, double> imageAccuracies = new Dictionary<int, double>();

        // 이미지 인덱스별 히스토리 스택 관리
        private Dictionary<int, List<ActionState>> imageHistoryStacks = new Dictionary<int, List<ActionState>>();
        private Dictionary<int, int> imageCurrentHistoryIndices = new Dictionary<int, int>();
        private const int MAX_HISTORY = 10; // 각 히스토리 스택당 최대 크기

        // 바운딩 박스와 세그멘테이션 가시성 관련 변수
        private bool isBoundingBoxVisible = true;
        private bool isSegmentationVisible = true;

        // 이미지 통과 여부 저장을 위한 변수
        private Dictionary<int, bool> imagePassedStatus = new Dictionary<int, bool>();

        // 이미지 상태 코드 저장을 위한 변수 (임시)
        // 상태 코드: 0 = 처음 진입(노란색), -1 = 틀림(빨간색), 1 = 맞음(녹색)
        private Dictionary<int, int> imageStatusCode = new Dictionary<int, int>();

        // 토스트 메시지 관련 변수
        private System.Windows.Forms.Timer toastTimer;
        private Guna.UI2.WinForms.Guna2Panel toastPanel;
        private Guna.UI2.WinForms.Guna2HtmlLabel toastLabel;

        // 줌 기능을 위한 변수
        private float zoomFactor = 1.0f;
        private Size originalImageSize;

        // 도구 설명 툴팁 관련 변수
        private Guna.UI2.WinForms.Guna2Panel tooltipPanel;
        private Guna.UI2.WinForms.Guna2HtmlLabel tooltipLabel;
        private System.Windows.Forms.Timer tooltipTimer;

        // 클래스 멤버 변수에 완료 다이얼로그 표시 여부 추가
        private bool isCompletionDialogShown = false;

        private string currentLevel = "0"; // 현재 레벨

        string baseDir = AppDomain.CurrentDomain.BaseDirectory;


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // 생성자 및 초기화 관련 ////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 초기 생성자
        /// </summary>
        /// 

        public event EventHandler<BlockEventArgs> AddBlockButtonClicked;
		public event EventHandler<BlockEventArgs> AddBlockButtonDoubleClicked;

		public UcLabelGuide(IMainView view)
        {
			InitializeComponent();
            this.mainView = view;

			LoadImages(); // 이미지 로드

            // 초기 설정
            SetupInitialConfig();
            RegisterExport();
            InitializeGroundTruthData(); // 정답 데이터 초기화
            InitializeProgressIndicators(); // 진행 상태 표시기 초기화
            //RegisterAccuracyButton(); // 정확도 계산 버튼 등록
            SetupZoomFunctionality(); // 줌 기능 설정
            InitializeToastPanel(); // 토스트 패널 초기화

            // 이벤트 핸들러 등록
            RegisterControlEvents();
            RegisterMouseEvents();
            RegisterToolEvents();
            
            // 초기 라운드 설정
            SetRoundedRegion();

            // 초기 이미지에 맞게 toolBox 가시성 설정
            UpdateCurrentLevel();

            // 다음/이전 버튼은 기본 비활성화
            UpdateNavigationButtonState();

            this.classBtn.CheckedState.FillColor = Color.Transparent;
            this.classBtn.PressedColor = Color.Transparent;
            classBtn.Visible = false; // 분류 버튼 숨김

            //// progress0 부터 8까지 호버 및 클릭 효과 삭제
            //for (int i = 0; i < 9; i++)
            //{
            //    var progressLabel = Controls.Find($"progress{i}", true).FirstOrDefault() as Guna.UI2.WinForms.Guna2CircleButton;
            //    if (progressLabel != null)
            //    {
            //        progressLabel.MouseEnter += (s, e) => { progressLabel.BackColor = Color.Transparent; };
            //        progressLabel.MouseLeave += (s, e) => { progressLabel.BackColor = Color.Transparent; };
            //        progressLabel.Click += (s, e) => { progressLabel.BackColor = Color.Transparent; };
            //        progressLabel.CheckedState.FillColor = Color.Transparent;
            //        progressLabel.PressedColor = Color.Transparent;
            //    }
            //}
            //progress0.CheckedState.FillColor = Color.Transparent;
            //progress0.PressedColor = Color.Transparent;
            //this.progress1.CheckedState.FillColor = Color.Transparent;
            //this.progress1.PressedColor = Color.Transparent;

            // class2 초기 상태에 따라 classBtn 가시성 설정
            UpdateClassButtonVisibility();
        }
        
        // 정답 데이터 초기화 메서드 
        private void InitializeGroundTruthData()
        {
            // 바운딩 박스 정답 데이터
            groundTruthBoundingBoxes[3] = ParseBoundingBoxFromJson(
                "{\"label\":\"cat\",\"x\":230,\"y\":79,\"width\":307,\"height\":305}");
            groundTruthBoundingBoxes[4] = ParseBoundingBoxFromJson(
                "{\"label\":\"dog\",\"x\":247,\"y\":58,\"width\":251,\"height\":265}");
            groundTruthBoundingBoxes[5] = ParseBoundingBoxFromJson(
                "{\"label\":\"dog\",\"x\":534,\"y\":211,\"width\":690,\"height\":470}");
            // 세그멘테이션 정답 데이터(폴리곤)
            groundTruthPolygons[6] = ParsePolygonFromJson(
                "{\"label\":\"apple\",\"points\":[{\"x\":362,\"y\":171},{\"x\":237,\"y\":293},{\"x\":204,\"y\":417},{\"x\":250,\"y\":591},{\"x\":329,\"y\":653},{\"x\":435,\"y\":687},{\"x\":547,\"y\":720},{\"x\":692,\"y\":683},{\"x\":782,\"y\":605},{\"x\":824,\"y\":536},{\"x\":829,\"y\":456},{\"x\":840,\"y\":394},{\"x\":804,\"y\":263},{\"x\":767,\"y\":206},{\"x\":657,\"y\":139},{\"x\":552,\"y\":123},{\"x\":457,\"y\":139},{\"x\":400,\"y\":153}]}");
            groundTruthPolygons[7] = ParsePolygonFromJson(
                "{\"label\":\"strawberry\",\"points\":[{\"x\":294,\"y\":424},{\"x\":160,\"y\":646},{\"x\":147,\"y\":703},{\"x\":127,\"y\":763},{\"x\":178,\"y\":795},{\"x\":224,\"y\":758},{\"x\":259,\"y\":742},{\"x\":448,\"y\":472},{\"x\":530,\"y\":431},{\"x\":424,\"y\":621},{\"x\":371,\"y\":804},{\"x\":446,\"y\":814},{\"x\":521,\"y\":749},{\"x\":591,\"y\":660},{\"x\":558,\"y\":747},{\"x\":604,\"y\":788},{\"x\":738,\"y\":724},{\"x\":822,\"y\":614},{\"x\":782,\"y\":699},{\"x\":840,\"y\":722},{\"x\":906,\"y\":667},{\"x\":945,\"y\":667},{\"x\":965,\"y\":628},{\"x\":998,\"y\":596},{\"x\":1053,\"y\":483},{\"x\":1066,\"y\":389},{\"x\":1035,\"y\":282},{\"x\":987,\"y\":222},{\"x\":939,\"y\":126},{\"x\":873,\"y\":75},{\"x\":785,\"y\":61},{\"x\":730,\"y\":16},{\"x\":655,\"y\":43},{\"x\":620,\"y\":89},{\"x\":650,\"y\":121},{\"x\":602,\"y\":183},{\"x\":582,\"y\":126},{\"x\":543,\"y\":105},{\"x\":501,\"y\":114},{\"x\":468,\"y\":137}]}");
            groundTruthPolygons[8] = ParsePolygonFromJson(
                "{\"label\":\"banana\",\"points\":[{\"x\":404,\"y\":399},{\"x\":404,\"y\":463},{\"x\":424,\"y\":528},{\"x\":448,\"y\":606},{\"x\":567,\"y\":642},{\"x\":653,\"y\":665},{\"x\":716,\"y\":647},{\"x\":771,\"y\":640},{\"x\":824,\"y\":661},{\"x\":956,\"y\":583},{\"x\":967,\"y\":534},{\"x\":945,\"y\":488},{\"x\":895,\"y\":424},{\"x\":853,\"y\":362},{\"x\":798,\"y\":291},{\"x\":736,\"y\":245},{\"x\":668,\"y\":213},{\"x\":620,\"y\":204},{\"x\":567,\"y\":199},{\"x\":479,\"y\":252},{\"x\":439,\"y\":286},{\"x\":413,\"y\":348}]}");
        }

        private void RegisterExport()
        {
            //// 좌표 내보내기 버튼 임시
            //this.exportBtn = new Guna.UI2.WinForms.Guna2Button();
            //this.exportBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            //this.exportBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            //this.exportBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            //this.exportBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            //this.exportBtn.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            //this.exportBtn.Font = new System.Drawing.Font("Noto Sans KR", 9F, System.Drawing.FontStyle.Bold);
            //this.exportBtn.ForeColor = System.Drawing.Color.White;
            //this.exportBtn.Location = new System.Drawing.Point(0, 0);
            //this.exportBtn.Name = "exportBtn";
            //this.exportBtn.Size = new System.Drawing.Size(138, 35);
            //this.exportBtn.TabIndex = 14;
            //this.exportBtn.Text = "좌표 내보내기";
            //this.imageContainer.Controls.Add(this.exportBtn);
        }

        /// <summary>
        /// 초기 설정
        /// </summary>
        private void SetupInitialConfig()
        {
            // 이미지 페인트 이벤트
            pictureBoxImage.Paint += PictureBoxImage_Paint;

            // 줌 관련 초기화
            originalImageSize = pictureBoxImage.Size;
            ZoomInBtn.Click += ZoomInBtn_Click;
            ZoomOutBtn.Click += ZoomOutBtn_Click;
            this.MouseWheel += UcLabelGuide_MouseWheel;
            
            // 툴팁 패널 초기화
            InitializeTooltipPanel();

        }

        /// <summary>
        /// 이미지 로드
        /// </summary>
        private void LoadImages()
        {

            string folderPath = Path.Combine(baseDir, "Resources", "Images");

            // 디렉터리가 존재하는지 확인
            if (!Directory.Exists(folderPath))
            {
                // 디렉터리가 존재하지 않으면 개발환경용 경로 시도
                string devPath = Path.GetFullPath(Path.Combine(baseDir, @"..\..\SAI.APP\Resources\Images"));
                if (Directory.Exists(devPath))
                {
                    folderPath = devPath;
                }
                else
                {
                    MessageBox.Show($"이미지 폴더를 찾을 수 없습니다.\n시도한 경로:\n1. {folderPath}\n2. {devPath}",
                                    "경로 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // 또는 기본 이미지 사용 등의 대체 전략
                }
            }

            for (int i = 1; i <= 9; i++)
            {
                string filePath = Path.Combine(folderPath, $"{i}.jpg");
                if (File.Exists(filePath))
                {
                    images.Add(Image.FromFile(filePath));
                }
            }

            if (images.Count > 0)
            {
                currentImageIndex = 0;
                pictureBoxImage.BackgroundImage = images[currentImageIndex]; // 첫 번째 이미지 표시
                UpdateShowLevel(); // showLevel 업데이트
                UpdateCurrentLevel(); // currentLevel 업데이트
                LoadImageLabels(); // 현재 이미지의 라벨링 불러오기
            }
            else
            {
                MessageBox.Show("이미지를 불러올 수 없습니다. 경로를 확인하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        ///
        /// classification, bounding box, segmentation에 따라 이미지 라벨링 불러오기
        /// 
        private void LoadImageLabels()
        {
            // 기본값은 빈 문자열로 설정
            class3.Text = "";

            // Classification 라벨 불러오기
            if (imageClassifications.ContainsKey(currentImageIndex))
            {
                class3.Text = imageClassifications[currentImageIndex];
            }
            // Bounding Box 라벨 불러오기
            else if (imageBoundingBoxes.ContainsKey(currentImageIndex) &&
                     imageBoundingBoxes[currentImageIndex].Count > 0)
            {
                class3.Text = imageBoundingBoxes[currentImageIndex][0].Item2;
            }
            // Segmentation 라벨 불러오기 (구현되어 있다면)
            else if (imagePolygons.ContainsKey(currentImageIndex) &&
                     imagePolygons[currentImageIndex].Count > 0)
            {
                class3.Text = imagePolygons[currentImageIndex][0].Item2;
            }

            // classBtn 가시성 업데이트
            UpdateClassButtonVisibility();

            // 이미지 갱신하여 바운딩 박스 표시
            pictureBoxImage.Invalidate();
        }


        /// <summary>
        /// 컨트롤 이벤트 등록
        /// </summary>
        private void RegisterControlEvents()
        {
            // class2 라벨 클릭 시 주석 편집기 열기
            class3.Click += (s, e) => OpenAnnotationEditor(class3.Text);

            // 이미지 클릭 이벤트 추가
            pictureBoxImage.Click += PictureBoxImage_Click;

            // 버튼 이벤트 등록
            nextBtn.Click += (s, e) => ShowNextImage();
            preBtn.Click += (s, e) => ShowPreviousImage();
            
            // 이미지가 라운드 밖으로 나가지 않도록 설정
            imageContainer.Paint += ImageContainer_Paint;

            // SizeChanged 이벤트 추가
            imageContainer.SizeChanged += (s, e) => SetRoundedRegion();
            pictureBoxImage.SizeChanged += (s, e) => SetRoundedRegion();

            // popup 닫기
            popupCloseBtn.Click += (s, e) => toastPopupPanel.Visible = false;
            toastPopupPanel.Click += (s, e) => toastPopupPanel.Visible = false;

            // 홈으로 돌아가기
            homeBtn.Click += (s, e) =>
            {
                var dialog = new DialogHomeFromLabeling();
                dialog.ShowDialog(this);
            };
            //// 좌표 내보내기 버튼 클릭 이벤트 등록
            //exportBtn.Click += ExportBtn_Click;
        }


        /// <summary>
        /// 마우스 관련 이벤트 핸들러 등록
        /// </summary>
        private void RegisterMouseEvents()
        {
            // 이벤트 핸들러 등록
            pictureBoxImage.MouseDown += PictureBoxImage_MouseDown;
            pictureBoxImage.MouseMove += PictureBoxImage_MouseMove;
            pictureBoxImage.MouseUp += PictureBoxImage_MouseUp;
            pictureBoxImage.MouseMove += PictureBoxImage_MouseHover;
            pictureBoxImage.MouseClick += PictureBoxImage_MouseClick;

            // 이미지 컨테이너에도 마우스 이벤트 핸들러 추가
            imageContainer.MouseDown += ImageContainer_MouseDown;
            imageContainer.MouseMove += ImageContainer_MouseMove;
            imageContainer.MouseUp += ImageContainer_MouseUp;
        }

        /// <summary>
        /// 도구 관련 이벤트 핸들러 등록
        /// </summary>
        private void RegisterToolEvents()
        {
            // 도구 이벤트 등록
            RegisterHandToolEvent();
            RegisterSquareToolEvent();
            RegisterPolygonToolEvent();

            // Undo/Redo 버튼 이벤트 등록 추가
            toolUndo.Click += toolUndo_Click;
            toolRedo.Click += toolRedo_Click;

            // 라벨 지우기
            toolDelete.Click += toolDelete_Click;
            // 라벨 지우기 버튼 클릭 시 손 도구 비활성화
            toolDelete.Click += (s, e) => {
                if (isHandToolActive)
                {
                    isHandToolActive = false;
                    isEditingPolygon = false;
                    isEditingBoundingBox = false;
                    selectedPolygonIndex = -1;
                    selectedBoxIndex = -1;
                    UpdateToolVisualState();
                }
            };

            // 바운딩 박스 및 세그멘테이션 가시성 토글
            toolVisible.Click += toolVisible_Click;
            // 가시성 토글 버튼 클릭 시 손 도구 비활성화
            toolVisible.Click += (s, e) => {
                if (isHandToolActive)
                {
                    isHandToolActive = false;
                    isEditingPolygon = false;
                    isEditingBoundingBox = false;
                    selectedPolygonIndex = -1;
                    selectedBoxIndex = -1;
                    UpdateToolVisualState();
                }
            };
            
            // 툴팁 이벤트 등록
            RegisterTooltipEvents();
        }

        /// <summary>
        /// 이미지 컨테이너의 라운드 설정
        /// </summary>
        private void SetRoundedRegion()
        {
            int borderRadius = 50; // 원하는 borderRadius 값

            // imageContainer의 오른쪽 모서리만 둥글게 처리 (왼쪽은 직각)
            GraphicsPath containerPath = new GraphicsPath();
            // 왼쪽 위 (직각)
            containerPath.AddLine(0, 0, borderRadius, 0);
            // 오른쪽 위 (라운드)
            containerPath.AddArc(imageContainer.Width - borderRadius, 0, borderRadius, borderRadius, 270, 90);
            // 오른쪽 아래 (라운드)
            containerPath.AddArc(imageContainer.Width - borderRadius, imageContainer.Height - borderRadius, borderRadius, borderRadius, 0, 90);
            // 왼쪽 아래 (직각)
            containerPath.AddLine(borderRadius, imageContainer.Height, 0, imageContainer.Height);
            containerPath.AddLine(0, imageContainer.Height, 0, 0);
            containerPath.CloseAllFigures();
            imageContainer.Region = new Region(containerPath);

            // PictureBox 자체에는 Region을 설정하지 않음 (null)
            // 컨테이너의 Region에 의해 잘려 보이게 됨
            if (pictureBoxImage.Region != null) // 이미 null이 아니면 null로 설정
            {
                pictureBoxImage.Region = null;
            }
        }

        private void UpdateNavigationButtonState()
        {
            bool isCurrentImageLabeled = false;
            double currentAccuracy = 0;
            nextBtn.Enabled = false;
            preBtn.Enabled = false;

            // 현재 이미지에 대한 라벨링 및 정확도 확인
            if (imageAccuracies.ContainsKey(currentImageIndex))
            {
                currentAccuracy = imageAccuracies[currentImageIndex];
                isCurrentImageLabeled = currentAccuracy >= 50.0;
            }
            else
            {
                // 이미지 타입에 따라 라벨링 되었는지 확인
                if (currentLevel == "Classification")
                {
                    isCurrentImageLabeled = imageClassifications.ContainsKey(currentImageIndex);
                }
                else if (currentLevel == "Bounding Box")
                {
                    isCurrentImageLabeled = imageBoundingBoxes.ContainsKey(currentImageIndex) && imageBoundingBoxes[currentImageIndex].Count > 0;
                }
                else if (currentLevel == "Segmentation")
                {
                    isCurrentImageLabeled = imagePolygons.ContainsKey(currentImageIndex) && imagePolygons[currentImageIndex].Count > 0;
                }
            }

            // 이미지가 classification일 경우 라벨링만으로 통과
            if (currentLevel == "Classification" && currentAccuracy >= 1)
            {
                nextBtn.Enabled = true;
                if (currentImageIndex >1)
                {
                    preBtn.Enabled = true;
                }
                imageStatusCode[currentImageIndex] = 1;
                UpdateProgressIndicator(currentImageIndex, 1);
            }
            else if (currentAccuracy >= 90 && currentImageIndex != 8)
            {
                //임시 50
                nextBtn.Enabled = true;
                imageStatusCode[currentImageIndex] = 1;
                UpdateProgressIndicator(currentImageIndex, 1);
            }
            else if (currentAccuracy >= 50 && currentImageIndex == 8)
            {
                imageStatusCode[currentImageIndex] = 1;
                UpdateProgressIndicator(currentImageIndex, 1);
            }
            else if (0 < currentAccuracy && currentAccuracy < 50)
            {
                imageStatusCode[currentImageIndex] = -1;
                UpdateProgressIndicator(currentImageIndex, -1);
            }
            else
            {
                imageStatusCode[currentImageIndex] = 0;
                UpdateProgressIndicator(currentImageIndex, 0);
            }

            // 첫 번째 이미지일 경우 이전 버튼 비활성
            if (currentImageIndex == 0)
            {
                preBtn.Visible = false;
            }
            else
            {
                preBtn.Enabled = true;
                preBtn.Visible = true;
            }

            // 도움 버튼 시각적 업데이트
            UpdateQuestionButton();
            // 모든 이미지가 완료되었는지 확인
            CheckAllImagesCompleted();
        }

        /// <summary>
        /// 모든 이미지의 라벨링이 완료되었는지 확인하고, 완료되었으면 완료 다이얼로그를 표시
        /// </summary>
        private void CheckAllImagesCompleted()
        {
            // 모든 이미지가 통과 상태인지 확인
            bool allCompleted = true;
            
            for (int i = 0; i < images.Count; i++)
            {
                if (!imageStatusCode.ContainsKey(i) || imageStatusCode[i] == -1 || imageStatusCode[i] == 0)
                {
                    allCompleted = false;
                    break;
                }
            }
            
            // 모든 이미지가 완료된 경우 완료 다이얼로그 표시
            if (allCompleted)
            {
                // 이미 대화상자가 표시된 경우 중복 표시 방지
                if (!isCompletionDialogShown)
                {
                    isCompletionDialogShown = true;
                    
                    // 완료 다이얼로그 표시
                    DialogCompleteLabeling completeDialog = new DialogCompleteLabeling();
                    completeDialog.Owner = this.FindForm();
                    completeDialog.ShowDialog();
                }
            }
        }

        // 진행 상태 표시기를 업데이트하는 메서드
        private void UpdateProgressIndicator(int imageIndex, int passed)
        {
            // 이미지 인덱스에 맞는 progress 버튼 찾기
            var progressControl = GetProgressControl(imageIndex);

            if (progressControl != null)
            {
                // 이미지를 통과했으면 통과 상태 기록
                if (passed == 1)
                {
                    progressControl.FillColor = Color.Green;
                    ShowToast(true); // 통과 시 초록색 토스트 메시지 표시
                }
                else if (passed == -1)
                {
                    progressControl.FillColor = Color.Red;
                    ShowToast(false); // 실패 시 빨간색 토스트 메시지 표시
                }
                else
                {
                    progressControl.FillColor = Color.Yellow;
                }
            }
        }

        // 진행 상태 표시기 초기화 메서드 추가
        private void InitializeProgressIndicators()
        {
            // 모든 progress 버튼 초기화
            for (int i = 0; i < 9; i++)
            {
                var progressControl = GetProgressControl(i);
                if (progressControl != null)
                {
                    // 초기 상태는 테두리만 있는 투명 원
                    progressControl.FillColor = Color.Transparent;
                    progressControl.BorderColor = Color.Black;
                    progressControl.BorderThickness = 1;
                }
            }
            
            // 완료 다이얼로그 표시 여부 초기화
            isCompletionDialogShown = false;
        }

        // 이미지 인덱스에 맞는 progress 버튼을 반환하는 헬퍼 메서드
        private Guna.UI2.WinForms.Guna2Panel GetProgressControl(int imageIndex)
        {
            switch (imageIndex)
            {
                case 0:
                    return progress0;
                case 1:
                    return progress1;
                case 2:
                    return progress2;
                case 3:
                    return progress3;
                case 4:
                    return progress4;
                case 5:
                    return progress5;
                case 6:
                    return progress6;
                case 7:
                    return progress7;
                case 8:
                    return progress8;
                default:
                    return null;
            }
        }

        //////////////////////////////////////////////////////////////////////////////
        ////// 줌 기능 관련 이벤트 핸들러
        //////////////////////////////////////////////////////////////////////////////
        
        private void SetupZoomFunctionality()
        {
            // 원본 이미지 크기 저장
            originalImageSize = pictureBoxImage.Size;

            // 줌 버튼 이벤트 연결
            ZoomInBtn.Click += ZoomInBtn_Click;
            ZoomOutBtn.Click += ZoomOutBtn_Click;

            // 마우스 휠 이벤트 등록
            this.MouseWheel += UcLabelGuide_MouseWheel;

            // ZoomCurrent 초기값 설정
            UpdateZoomCurrentLabel();
        }

        // 마우스 휠 이벤트 핸들러
        private void UcLabelGuide_MouseWheel(object sender, MouseEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control)
            {
                // 확대/축소 계수 조정 (휠을 위로 올리면 확대, 아래로 내리면 축소)
                float delta = e.Delta > 0 ? 0.1f : -0.1f;

                // 현재 마우스 위치를 기준으로 줌 적용
                Point mousePos = pictureBoxImage.PointToClient(Cursor.Position);
                ApplyZoom(delta, mousePos);
            }
        }

        // 줌 적용 메서드
        private void ApplyZoom(float delta, Point? mousePosition = null)
        {
            // 이전 줌 계수 저장
            float prevZoom = zoomFactor;

            // 줌 계수 업데이트
            zoomFactor += delta;

            // 최소/최대 확대 비율 제한
            zoomFactor = Math.Max(0.1f, Math.Min(zoomFactor, 5.0f));

            // 크기 조정 전 이미지 중심점 계산
            Point? zoomCenter = mousePosition;
            if (!zoomCenter.HasValue)
            {
                // 마우스 위치가 없으면 이미지 중앙 사용
                zoomCenter = new Point(
                    pictureBoxImage.Width / 2,
                    pictureBoxImage.Height / 2
                );
            }

            // 마우스 위치가 이미지 내에서 어디에 있는지 상대적인 위치 계산 (0.0 ~ 1.0)
            float relativeX = (zoomCenter.Value.X - pictureBoxImage.Left) / (float)pictureBoxImage.Width;
            float relativeY = (zoomCenter.Value.Y - pictureBoxImage.Top) / (float)pictureBoxImage.Height;

            // 새 크기 계산
            int newWidth = (int)(originalImageSize.Width * zoomFactor);
            int newHeight = (int)(originalImageSize.Height * zoomFactor);

            // 이미지 크기 업데이트
            pictureBoxImage.Size = new Size(newWidth, newHeight);

            // 마우스 포인터 위치가 같은 상대적 위치를 유지하도록 이미지 위치 조정
            if (delta != 0)
            {
                pictureBoxImage.Left = (int)(zoomCenter.Value.X - (newWidth * relativeX));
                pictureBoxImage.Top = (int)(zoomCenter.Value.Y - (newHeight * relativeY));
            }

            // ZoomCurrent 라벨 업데이트
            UpdateZoomCurrentLabel();

            // 화면 갱신
            pictureBoxImage.Invalidate();
        }

        // ZoomCurrent 라벨 업데이트 메서드
        private void UpdateZoomCurrentLabel()
        {
            // 백분율로 표시 (100% = 1.0)
            int zoomPercentage = (int)(zoomFactor * 100);
            ZoomCurrent.Text = $"{zoomPercentage}%";
        }

        /// <summary>
        /// 줌 상태를 초기화하는 메서드
        /// </summary>
        private void ResetZoom()
        {
            // 줌 팩터를 기본값(1.0)으로 재설정
            zoomFactor = 1.0f;

            // 이미지 크기를 원래 크기로 복원
            pictureBoxImage.Size = originalImageSize;

            // 이미지 위치 중앙으로 조정
            CenterImage();

            // ZoomCurrent 라벨 업데이트
            UpdateZoomCurrentLabel();
        }

        /// <summary>
        /// 이미지를 컨테이너 중앙에 배치하는 메서드
        /// </summary>
        private void CenterImage()
        {
            // 이미지를 컨테이너의 중앙에 배치
            int x = (imageContainer.Width - pictureBoxImage.Width) / 2;
            int y = (imageContainer.Height - pictureBoxImage.Height) / 2;

            // 위치가 음수가 되지 않도록 보정
            pictureBoxImage.Left = Math.Max(0, x);
            pictureBoxImage.Top = Math.Max(0, y);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // 도구 관련 이벤트 처리 ////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///
        /// <summary>
        /// 손 도구 관련 이벤트 등록
        /// </summary>
        private void RegisterHandToolEvent()
        {
            toolHand.Click += (s, e) =>
            {
                // 폴리곤 작업 중이었다면 초기화
                if (isPolygonToolActive && polygonPoints.Count > 0)
                {
                    polygonPoints.Clear();
                    pictureBoxImage.Invalidate();
                }

                isHandToolActive = !isHandToolActive;
                isSquareToolActive = false;
                isPolygonToolActive = false; // 폴리곤 도구 비활성화

                // 손 도구가 활성화되면 현재 모드에 따라 적절한 편집 모드 설정
                if (isHandToolActive) 
                {
                    if (currentLevel == "Segmentation" &&
                        imagePolygons.ContainsKey(currentImageIndex) &&
                        imagePolygons[currentImageIndex].Count > 0)
                    {
                        // 세그멘테이션 모드에서는 폴리곤 편집 모드 활성화
                        isEditingPolygon = true;
                        isEditingBoundingBox = false;

                        // 기존에 저장된 폴리곤의 점들을 편집용으로 복사
                        var existingPolygon = imagePolygons[currentImageIndex][0]; // 첫 번째 폴리곤만 편집 가능
                        selectedPolygonIndex = 0;

                        // 이미지 좌표를 화면 좌표로 변환
                        editingPolygonPoints = ConvertPointsToDisplayCoordinates(existingPolygon.Item1);
                    }
                    else if (currentLevel == "Bounding Box" &&
                             imageBoundingBoxes.ContainsKey(currentImageIndex) &&
                             imageBoundingBoxes[currentImageIndex].Count > 0)
                    {
                        // 바운딩 박스 모드에서는 바운딩 박스 편집 모드 활성화
                        isEditingBoundingBox = true;
                        isEditingPolygon = false;

                        // 기존에 저장된 바운딩 박스를 편집용으로 복사
                        var existingBox = imageBoundingBoxes[currentImageIndex][0]; // 첫 번째 바운딩 박스만 편집 가능
                        selectedBoxIndex = 0;

                        // 이미지 좌표를 화면 좌표로 변환
                        float scaleX = pictureBoxImage.ClientSize.Width / (float)pictureBoxImage.BackgroundImage.Width;
                        float scaleY = pictureBoxImage.ClientSize.Height / (float)pictureBoxImage.BackgroundImage.Height;

                        editingRect = new Rectangle(
                            (int)(existingBox.Item1.X * scaleX),
                            (int)(existingBox.Item1.Y * scaleY),
                            (int)(existingBox.Item1.Width * scaleX),
                            (int)(existingBox.Item1.Height * scaleY)
                        );
                    }
                    else
                    {
                        // 그 외의 경우는 편집 모드 비활성화
                        isEditingPolygon = false;
                        isEditingBoundingBox = false;
                        selectedPolygonIndex = -1;
                        selectedBoxIndex = -1;
                        editingPolygonPoints.Clear();
                    }
                }
                else
                {
                    // 손 도구가 비활성화되면 모든 편집 모드 비활성화
                    isEditingPolygon = false;
                    isEditingBoundingBox = false;
                    selectedPolygonIndex = -1;
                    selectedBoxIndex = -1;
                    editingPolygonPoints.Clear();
                }

                UpdateToolVisualState();
                pictureBoxImage.Invalidate();
            };
        }

        /// <summary>
        /// 사각형 도구 관련 이벤트 등록
        /// </summary>
        private void RegisterSquareToolEvent()
        {
            toolLabelingSquare.Click += (s, e) =>
            {
                if (currentLevel == "Bounding Box")
                {
                    // 폴리곤 작업 중이었다면 초기화
                    if (isPolygonToolActive && polygonPoints.Count > 0)
                    {
                        polygonPoints.Clear();
                        pictureBoxImage.Invalidate();
                    }

                    // 도구 상태 변경
                    isHandToolActive = false; // 손 도구 비활성화
                    isSquareToolActive = true;
                    isPolygonToolActive = false;
                    
                    // 바운딩 박스 편집 모드 해제
                    isEditingBoundingBox = false;
                    selectedBoxIndex = -1;
                    editingRect = Rectangle.Empty;

                    UpdateToolVisualState();
                    pictureBoxImage.Cursor = Cursors.Cross;
                }
            };
        }

        /// <summary>
        /// 폴리곤 도구 관련 이벤트 등록
        /// </summary>
        private void RegisterPolygonToolEvent()
        {
            toolLabelingPolygon.Click += (s, e) =>
            {
                if (currentLevel == "Segmentation")
                {
                    // 현재 편집 모드라면 편집 내용을 저장
                    if (isEditingPolygon)
                    {
                        SaveEditingPolygon();
                    }

                    // 이미 폴리곤 도구가 활성화된 상태에서 다시 클릭하면 작업 중인 폴리곤 초기화
                    if (isPolygonToolActive && polygonPoints.Count > 0)
                    {
                        polygonPoints.Clear();
                    }

                    // 모든 도구 상태 비활성화
                    isHandToolActive = false; // 손 도구 비활성화
                    isSquareToolActive = false;

                    // 폴리곤 도구 활성화 상태 토글
                    isPolygonToolActive = !isPolygonToolActive;
                    
                    // 폴리곤 편집 모드 해제
                    isEditingPolygon = false;
                    selectedPolygonIndex = -1;
                    editingPolygonPoints.Clear();

                    UpdateToolVisualState();
                    pictureBoxImage.Cursor = isPolygonToolActive ? Cursors.Cross : Cursors.Default;

                    // 화면 갱신
                    pictureBoxImage.Invalidate();
                }
            };
        }

        /// <summary>
        /// 손 도구 클릭 시 상태를 변경하고 폴리곤 편집 모드를 성정 / 해제
        /// </summary>
        private void toolHand_Click(object sender, EventArgs e)
        {
            isHandToolActive = !isHandToolActive; // 활성화 상태 토글
            toolHand.BackColor = isHandToolActive ? Color.LightGray : Color.Transparent; // 시각적 피드백
        }

        /// <summary>
        /// 현재 활성화된 도구에 따라 시각적 상태를 업데이트
        /// </summary>
        private void UpdateToolVisualState()
        {
            // BackColor 대신 Image 속성 사용하여 활성화 상태 표시
            toolHand.Image = isHandToolActive ? Properties.Resources.toolHandClick : Properties.Resources.tool_hand_basic;
            toolLabelingSquare.Image = isSquareToolActive ? Properties.Resources.toolLabelClick : Properties.Resources.tool_square_basic;
            toolLabelingPolygon.Image = isPolygonToolActive ? Properties.Resources.toolPolClick : Properties.Resources.tool_pol_basic;
                
            // 마우스 커서 설정
            pictureBoxImage.Cursor = isHandToolActive ? Cursors.Hand : Cursors.Default;
        }

        /// <summary>
        /// 모든 도구의 상태를 초기화
        /// </summary>
        private void ResetToolState()
        {
            // 드래깅 상태 초기화
            isDragging = false;
            isBoundingBoxDragging = false;
            isPolygonPointDragging = false;
            
            // 편집 모드 초기화
            isEditingBoundingBox = false;
            isEditingPolygon = false;
            selectedBoxIndex = -1;
            selectedPolygonIndex = -1;
            
            // 그리기 상태 초기화
            currentRect = Rectangle.Empty;
            editingRect = Rectangle.Empty;
            polygonPoints.Clear();
            editingPolygonPoints.Clear();
            
            // 호버링 상태 초기화
            hoveredPoint = null;

            // 이미지 인덱스에 따라 적절한 도구 활성화
            int imageIndex = currentImageIndex + 1; // 1부터 시작하는 인덱스
            
            if (imageIndex >= 1 && imageIndex <= 3) {
                // Classification 모드 (이미지 1-3)
                isHandToolActive = true;
                isSquareToolActive = false;
                isPolygonToolActive = false;
                pictureBoxImage.Cursor = Cursors.Hand;
            } 
            else if (imageIndex >= 4 && imageIndex <= 6) {
                // Bounding Box 모드 (이미지 4-6)
                isHandToolActive = false;
                isSquareToolActive = true;
                isPolygonToolActive = false;
                pictureBoxImage.Cursor = Cursors.Cross;
            }
            else if (imageIndex >= 7 && imageIndex <= 9) {
                // Segmentation 모드 (이미지 7-9)
                isHandToolActive = false;
                isSquareToolActive = false;
                isPolygonToolActive = true;
                pictureBoxImage.Cursor = Cursors.Cross;
            }
            
            // 도구 버튼 시각적 상태 업데이트
            toolHand.Image = isHandToolActive ? Properties.Resources.toolHandClick : Properties.Resources.tool_hand_basic;
            toolLabelingSquare.Image = isSquareToolActive ? Properties.Resources.toolLabelClick : Properties.Resources.tool_square_basic;
            toolLabelingPolygon.Image = isPolygonToolActive ? Properties.Resources.toolPolClick : Properties.Resources.tool_pol_basic;
        }

        // ActionState 클래스 추가
        public class ActionState
        {
            // 작업 종류
            public enum ActionType
            {
                BoundingBox,
                Polygon,
                PolygonEdit
            }

            // 작업 종류
            public ActionType Type { get; set; }

            // 작업이 적용된 이미지 인덱스
            public int ImageIndex { get; set; }

            // 작업 전 상태
            public object BeforeState { get; set; }

            // 작업 후 상태
            public object AfterState { get; set; }
        }

        /// <summary>
        /// 액션 히스토리에 새 작업 추가
        /// </summary>
        private void AddToHistory(ActionState action)
        {
            int imageIndex = action.ImageIndex;

            // 해당 이미지의 히스토리 스택이 없으면 생성
            if (!imageHistoryStacks.ContainsKey(imageIndex))
            {
                imageHistoryStacks[imageIndex] = new List<ActionState>();
                imageCurrentHistoryIndices[imageIndex] = -1;
            }

            // 현재 인덱스 이후의 모든 히스토리 제거
            int currentIndex = imageCurrentHistoryIndices[imageIndex];
            if (currentIndex < imageHistoryStacks[imageIndex].Count - 1)
            {
                imageHistoryStacks[imageIndex].RemoveRange(currentIndex + 1,
                    imageHistoryStacks[imageIndex].Count - currentIndex - 1);
            }

            // 히스토리 최대 크기 유지
            if (imageHistoryStacks[imageIndex].Count >= MAX_HISTORY)
            {
                imageHistoryStacks[imageIndex].RemoveAt(0);
                imageCurrentHistoryIndices[imageIndex]--;
            }

            // 히스토리에 추가
            imageHistoryStacks[imageIndex].Add(action);
            imageCurrentHistoryIndices[imageIndex]++;
        }

        /// <summary>
        /// Undo - 작업 취소
        /// </summary>
        private void Undo()
        {
            try
            {
                // 현재 이미지에 대한 히스토리 확인
                if (!imageHistoryStacks.ContainsKey(currentImageIndex) ||
                    imageCurrentHistoryIndices[currentImageIndex] < 0)
                {
                    // 히스토리가 없으면 아무것도 하지 않음
                    return;
                }

                // 현재 작업 가져오기
                int currentIndex = imageCurrentHistoryIndices[currentImageIndex];
                var action = imageHistoryStacks[currentImageIndex][currentIndex];

                // 작업 타입에 따라 이전 상태로 복원
                switch (action.Type)
                {
                    case ActionState.ActionType.BoundingBox:
                        if (action.BeforeState == null)
                            imageBoundingBoxes.Remove(action.ImageIndex);
                        else
                            imageBoundingBoxes[action.ImageIndex] = (List<Tuple<Rectangle, string>>)action.BeforeState;
                        break;

                    case ActionState.ActionType.Polygon:
                    case ActionState.ActionType.PolygonEdit:
                        if (action.BeforeState == null)
                            imagePolygons.Remove(action.ImageIndex);
                        else
                            imagePolygons[action.ImageIndex] = (List<Tuple<List<Point>, string>>)action.BeforeState;
                        break;
                }

                // 현재 히스토리 인덱스 감소
                imageCurrentHistoryIndices[currentImageIndex]--;

                // 화면 갱신
                LoadImageLabels();
                pictureBoxImage.Invalidate();
                CalculateAndDisplayAccuracy();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"실행 취소 중 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Redo - 작업 재실행
        /// </summary>
        private void Redo()
        {
            try
            {
                // 현재 이미지에 대한 히스토리 확인
                if (!imageHistoryStacks.ContainsKey(currentImageIndex) ||
                    imageCurrentHistoryIndices[currentImageIndex] >= imageHistoryStacks[currentImageIndex].Count - 1)
                {
                    // 더 이상 앞으로 갈 히스토리가 없음
                    return;
                }

                // 다음 작업 인덱스로 이동
                imageCurrentHistoryIndices[currentImageIndex]++;
                int currentIndex = imageCurrentHistoryIndices[currentImageIndex];
                var action = imageHistoryStacks[currentImageIndex][currentIndex];

                // 작업 타입에 따라 작업 상태 복원
                switch (action.Type)
                {
                    case ActionState.ActionType.BoundingBox:
                        if (action.AfterState == null)
                            imageBoundingBoxes.Remove(action.ImageIndex);
                        else
                            imageBoundingBoxes[action.ImageIndex] = (List<Tuple<Rectangle, string>>)action.AfterState;
                        break;

                    case ActionState.ActionType.Polygon:
                    case ActionState.ActionType.PolygonEdit:
                        if (action.AfterState == null)
                            imagePolygons.Remove(action.ImageIndex);
                        else
                            imagePolygons[action.ImageIndex] = (List<Tuple<List<Point>, string>>)action.AfterState;
                        break;
                }

                // 화면 갱신
                LoadImageLabels();
                pictureBoxImage.Invalidate();
                CalculateAndDisplayAccuracy();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"다시 실행 중 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// 이미지 탐색 관련 /////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 현재 이미지 인덱스에 따라 라벨링 단계 업데이트
        /// </summary>
        private void UpdateCurrentLevel()
        {
            int imageIndex = currentImageIndex + 1; // 1부터 시작하는 인덱스

            // 도구 상태 초기화
            isHandToolActive = false;
            isSquareToolActive = false;
            isPolygonToolActive = false;

            if (imageIndex >= 1 && imageIndex <= 3)
            {
                currentLevel = "Classification"; // 이미지 1,2,3
                toolBox.Visible = false; // Classification 단계에서는 도구창 숨김
                accuracyPanel.Visible = false; // 정확도 패널 숨김
                //accuracyLabel.Visible = false; // 정확도 숨김

                // Classification 단계에서는 손 도구를 기본으로 활성화
                isHandToolActive = true;
                pictureBoxImage.Cursor = Cursors.Hand;

                // Classification 단계에서는 라벨이 항상 보이도록 설정
                isBoundingBoxVisible = true;
                isSegmentationVisible = true;

                // 버튼 비활성화 또는 상태 업데이트
                if (toolVisible != null)
                {
                    toolVisible.Enabled = false;  // 버튼 비활성화
                    toolVisible.Image = Properties.Resources.tool_visible_basic;
                    toolVisible.Text = "라벨 숨기기";
                }
            }
            else if (imageIndex >= 4 && imageIndex <= 6)
            {
                currentLevel = "Bounding Box"; // 이미지 4,5,6
                toolBox.Visible = true; // Bounding Box 단계에서는 도구창 표시
                //accuracyLabel.Visible = true; // Bounding Box 단계에서는 정확도 라벨 표시
                accuracyPanel.Visible = true; // 정확도 패널 숨김

                // Bounding Box 단계에서는 사각형 도구를 기본으로 활성화
                isSquareToolActive = true;
                pictureBoxImage.Cursor = Cursors.Cross;

                // Bounding Box 단계에서는 폴리곤을 비활성 및 squre 활성
                toolLabelingPolygon.Visible = false;
                toolLabelingSquare.Visible = true;

                // 버튼 활성화 및 상태 업데이트
                if (toolVisible != null)
                {
                    toolVisible.Enabled = true;  // 버튼 활성화
                    toolVisible.Image = isBoundingBoxVisible ? Properties.Resources.tool_visible_basic : Properties.Resources.toolVClick;
                    toolVisible.Text = isBoundingBoxVisible ? "라벨 숨기기" : "라벨 표시하기";
                }
            }
            else if (imageIndex >= 7 && imageIndex <= 9)
            {
                currentLevel = "Segmentation"; // 이미지 7,8,9
                toolBox.Visible = true; // Segmentation 단계에서는 도구창 표시
                //accuracyLabel.Visible = true; // Bounding Box 단계에서는 정확도 라벨 표시
                accuracyPanel.Visible = true; // 정확도 패널 숨김

                // Segmentation 단계에서는 폴리곤 도구를 기본으로 활성화
                isPolygonToolActive = true;
                pictureBoxImage.Cursor = Cursors.Cross;

                // Segmentation 단계에서는 사각형 도구를 비활성 및 polygon 활성
                toolLabelingSquare.Visible = false;
                toolLabelingPolygon.Visible = true;

                // 버튼 활성화 및 상태 업데이트
                if (toolVisible != null)
                {
                    toolVisible.Enabled = true;  // 버튼 활성화
                    toolVisible.Image = isSegmentationVisible ? Properties.Resources.tool_visible_basic : Properties.Resources.toolVClick;
                    toolVisible.Text = isSegmentationVisible ? "라벨 숨기기" : "라벨 표시하기";
                }
            }

            // 도구 버튼 상태 업데이트
            toolHand.Image = isHandToolActive ? Properties.Resources.toolHandClick : Properties.Resources.tool_hand_basic;
            toolLabelingSquare.Image = isSquareToolActive ? Properties.Resources.toolLabelClick : Properties.Resources.tool_square_basic;
            toolLabelingPolygon.Image = isPolygonToolActive ? Properties.Resources.toolPolClick : Properties.Resources.tool_pol_basic;

            LoadclassImage(); // 이미지 라벨링 불러오기
        }

        /// <summary>
        /// 현재 이미지 번호를 업데이트하여 showLevel에 표시
        /// </summary>
        private void UpdateShowLevel()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string folderPath = Path.Combine(baseDir, "Resources", "Images");

            // 디렉터리가 존재하는지 확인
            if (!Directory.Exists(folderPath))
            {
                // 디렉터리가 존재하지 않으면 개발환경용 경로 시도
                string devPath = Path.GetFullPath(Path.Combine(baseDir, @"..\..\Resources"));

                if (Directory.Exists(devPath))
                {
                    folderPath = devPath;
                }
                else
                {
                    MessageBox.Show($"이미지 폴더를 찾을 수 없습니다.\n시도한 경로:\n1. {folderPath}\n2. {devPath}",
                                   "경로 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // 또는 기본 이미지 사용 등의 대체 전략
                }
            }

            // 레벨 이미지가 비어있는 경우에만 로드
            if (levelImages.Count == 0)
            {
                for (int i = 1; i <= 9; i++)
                {
                    string filePath = Path.Combine(folderPath, $"level {i}-9.png");
                    if (File.Exists(filePath))
                    {
                        levelImages.Add(Image.FromFile(filePath));
                    }
                }
            }

            // 이미지가 로드되었고 유효한 인덱스인 경우에만 배경 이미지 설정
            if (levelImages.Count > 0)
            {
                // 현재 이미지 인덱스를 사용하여 적절한 레벨 이미지 인덱스 계산
                int levelIndex = currentImageIndex;
                
                // 인덱스가 범위를 벗어나지 않도록 확인
                if (levelIndex >= 0 && levelIndex < levelImages.Count)
                {
                    showLevel1.BackgroundImage = levelImages[levelIndex];
                }
            }
        }

        /// <summary>
        /// 다음 이미지로 이동
        /// </summary>
        private void ShowNextImage()
        {
            if (images.Count > 0)
            {   
                currentImageIndex = currentImageIndex + 1; // 다음 이미지로 이동

                if (currentImageIndex == 8)
                {
                    nextBtn.Enabled = false;
                    nextBtn.Visible = false; // 마지막 이미지일 경우 다음 버튼 비활성화
                }
                else
                {
                    nextBtn.Enabled = true;
                    nextBtn.Visible = true; // 마지막 이미지가 아니므로 다음 버튼 활성화
                }
                pictureBoxImage.BackgroundImage = images[currentImageIndex];
                ResetZoom(); // 줌 초기화
                UpdateShowLevel(); // showLevel 업데이트
                ResetToolState(); // 도구 상태 초기화 (UpdateCurrentLevel 전에 실행)
                UpdateCurrentLevel(); // currentLevel 업데이트
                LoadImageLabels(); // 현재 이미지의 라벨링 불러오기

                // 정확도 표시 업데이트
                if (imageAccuracies.ContainsKey(currentImageIndex))
                {
                    accuracyLabel1.Text = $"Accuracy: {imageAccuracies[currentImageIndex]:F0}%";

                }
                else
                {
                    accuracyLabel1.Text = "Accuracy: 0%";
                }   

                // // 이전에 통과한 이미지인 경우 다음 버튼 활성화
                // if (imagePassedStatus.ContainsKey(currentImageIndex) && imagePassedStatus[currentImageIndex])
                // {
                //     //nextBtn.Enabled = true;
                //     UpdateProgressIndicator(currentImageIndex, true);
                // }
                // else
                // {
                // 통과 여부에 따라 진행 상태 표시
                UpdateNavigationButtonState();
                // }
            }
        }

        /// <summary>
        /// 이전 이미지로 이동
        /// </summary>
        private void ShowPreviousImage()
        {
            if (images.Count > 0)
            {
                
                currentImageIndex = currentImageIndex - 1; // 이전 이미지로 이동

                //// 첫 번째 이미지로 이동한 경우 이전 버튼 비활성화
                //if (currentImageIndex == 0)
                //{
                //    preBtn.Visible = true;
                //}
                //else
                //{
                //    preBtn.Visible = true;
                //}

                // 다음 버튼은 항상 활성화 (마지막 이미지가 아니므로)
                nextBtn.Enabled = true;
                nextBtn.Visible = true; // 마지막 이미지가 아니므로 다음 버튼 활성화

                pictureBoxImage.BackgroundImage = images[currentImageIndex];
                ResetZoom(); // 줌 초기화
                UpdateShowLevel(); // showLevel 업데이트
                ResetToolState(); // 도구 상태 초기화 (UpdateCurrentLevel 전에 실행)
                UpdateCurrentLevel(); // currentLevel 업데이트
                LoadImageLabels(); // 현재 이미지의 라벨링 불러오기

                // 정확도 표시 업데이트
                if (imageAccuracies.ContainsKey(currentImageIndex))
                {
                    accuracyLabel1.Text = $"Accuracy: {imageAccuracies[currentImageIndex]:F0}%";
                }
                else
                {
                    accuracyLabel1.Text = "Accuracy: 0%";
                }

                // // 이전에 통과한 이미지인 경우 다음 버튼 활성화
                // if (imagePassedStatus.ContainsKey(currentImageIndex) && imagePassedStatus[currentImageIndex])
                // {
                //     //nextBtn.Enabled = true;
                //     UpdateProgressIndicator(currentImageIndex, true);
                // }
                // else
                // {
                // 통과 여부에 따라 진행 상태 표시
                UpdateNavigationButtonState();
                // }
            }
        }
        

        /// <summary>
        /// 현재 이미지에 대한 라벨 정보를 불러옴
        /// </summary>
        private void LoadclassImage()
        {
            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string devPath = Path.GetFullPath(Path.Combine(baseDir, @"..\..\Resources"));

                // classImages가 비어있는 경우에만 로드
                if (classImages.Count == 0)
                {
                    for (int i = 1; i <= 3; i++)
                    {
                        string filePath = Path.Combine(devPath, $"class{i}.png");
                        if (File.Exists(filePath))
                        {
                            classImages.Add(Image.FromFile(filePath));
                        }
                        else
                        {
                            // 파일이 없는 경우 로그 출력
                            Console.WriteLine($"파일을 찾을 수 없음: {filePath}");
                        }
                    }
                }

                // 이미지가 로드되었고 유효한 배열인지 확인
                if (classImages.Count >= 3)
                {
                    // currentLevel에 따라 배경 이미지 설정
                    if (currentLevel == "Classification")
                    {
                        levelPanel.BackgroundImage = classImages[0]; // class1.png
                    }
                    else if (currentLevel == "Bounding Box")
                    {
                        levelPanel.BackgroundImage = classImages[1]; // class2.png
                    }
                    else if (currentLevel == "Segmentation")
                    {
                        levelPanel.BackgroundImage = classImages[2]; // class3.png
                    }
                }
                else
                {
                    Console.WriteLine($"classImages 배열이 비어있거나 크기가 충분하지 않음. 현재 크기: {classImages.Count}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"LoadclassImage 에러: {ex.Message}");
            }

        }

        /// <summary>
        /// 단계에 따른 도움버튼 가시성 변경
        /// </summary>
        private void UpdateQuestionButton()
        {
            if (currentLevel == "Classification")
            {
                questBoxPanel.Visible = false; // BoundingBox 도움말 버튼 숨김
                questSegPanel.Visible = false; // Segmentation 도움말 버튼 숨김
                questClassificationPanel.Visible = true; // Classification 도움말 버튼 표시
            }
            else if (currentLevel == "Bounding Box")
            {
                questBoxPanel.Visible = true;
                questSegPanel.Visible = false; // Segmentation 도움말 버튼 숨김
                questClassificationPanel.Visible = false; // Classification 도움말 버튼 숨김
            }
            else if (currentLevel == "Segmentation")
            {
                questBoxPanel.Visible = false; // BoundingBox 도움말 버튼 숨김
                questSegPanel.Visible = true; // Segmentation 도움말 버튼 표시
                questClassificationPanel.Visible = false; // Classification 도움말 버튼 숨김
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// 마우스이벤트 처리 /////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 이미지 컨테이너에서 마우스 다운 이벤트 핸들러 
        /// </summary>
        private void ImageContainer_MouseDown(object sender, MouseEventArgs e)
        {
            if (isHandToolActive && e.Button == MouseButtons.Left)
            {
                // 이미지 영역 밖에서도 드래그 시작
                isDragging = true;
                startPoint = e.Location;
            }
        }

        /// <summary>
        /// 이미지 컨테이너에서 마우스 이동 이벤트 핸들러
        /// </summary>
        private void ImageContainer_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // 이미지 이동
                pictureBoxImage.Left += e.X - startPoint.X;
                pictureBoxImage.Top += e.Y - startPoint.Y;
                startPoint = e.Location;
            }
        }

        /// <summary>
        /// 이미지 컨테이너에서 마우스 업 이벤트 핸들러
        /// </summary>
        private void ImageContainer_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false; // 드래그 종료
            }
        }

        /// <summary>
        /// pictureBoxImage에서 마우스 다운 이벤트 핸들러
        /// </summary>
        private void PictureBoxImage_MouseDown(object sender, MouseEventArgs e)
        {
            if (isHandToolActive && e.Button == MouseButtons.Left)
            {
                if (isEditingPolygon)
                {
                    // 폴리곤 꼭지점 클릭 확인
                    int pointIndex = GetPointAtPosition(e.Location, 10, editingPolygonPoints);
                    if (pointIndex >= 0)
                    {
                        // 꼭지점을 클릭했으면 드래그 모드 시작
                        isPolygonPointDragging = true;
                        dragPointIndex = pointIndex;
                        pictureBoxImage.Cursor = Cursors.SizeAll;
                    }
                    else
                    {
                        // 선 위를 클릭했는지 확인
                        int segmentIndex = GetLineSegmentIndex(e.Location, 10);
                        if (segmentIndex >= 0)
                        {
                            // 선 위를 클릭한 경우, 새로운 점을 추가
                            Point clickPoint = e.Location;
                            // segmentIndex와 segmentIndex+1 사이에 새로운 점 삽입
                            editingPolygonPoints.Insert(segmentIndex + 1, clickPoint);

                            // 새로 추가된 점을 즉시 드래그 모드로 설정
                            isPolygonPointDragging = true;
                            dragPointIndex = segmentIndex + 1;
                            pictureBoxImage.Cursor = Cursors.SizeAll;

                            pictureBoxImage.Invalidate();
                        }
                        else
                        {
                            // 일반 드래그 모드 시작
                            isDragging = true;
                            startPoint = pictureBoxImage.PointToScreen(e.Location);
                        }
                    }
                }
                else if (isEditingBoundingBox)
                {
                    // 바운딩 박스 핸들 클릭 확인
                    dragHandleIndex = GetHandleAtPosition(e.Location);
                    if (dragHandleIndex >= 0)
                    {
                        // 핸들을 클릭했으면 크기 조정 모드 시작
                        isBoundingBoxDragging = true;
                        pictureBoxImage.Cursor = GetResizeCursor(dragHandleIndex);
                    }
                    else if (editingRect.Contains(e.Location))
                    {
                        // 바운딩 박스 내부를 클릭했으면 이동 모드 시작
                        isBoundingBoxDragging = true;
                        dragHandleIndex = 4; // 중앙(이동) 모드
                        pictureBoxImage.Cursor = Cursors.SizeAll;
                    }
                    else
                    {
                        // 일반 드래그 모드 시작
                        isDragging = true;
                        startPoint = pictureBoxImage.PointToScreen(e.Location);
                    }
                }
                else
                {
                    // 일반 드래그 모드 시작
                    isDragging = true;
                    startPoint = pictureBoxImage.PointToScreen(e.Location);
                }
            }
            else if (isSquareToolActive && e.Button == MouseButtons.Left)
            {
                // 사각형 그리기 시작점 저장
                rectStartPoint = e.Location;
                currentRect = Rectangle.Empty;
                pictureBoxImage.Invalidate();
            }
        }

        /// <summary>
        /// pictureBoxImage에서 마우스 이동 이벤트 핸들러
        /// </summary>
        private void PictureBoxImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (isPolygonPointDragging && dragPointIndex >= 0 && isEditingPolygon)
            {
                // 폴리곤 꼭지점 드래그 중
                editingPolygonPoints[dragPointIndex] = e.Location;
                pictureBoxImage.Invalidate();
            }
            else if (isBoundingBoxDragging && dragHandleIndex >= 0 && isEditingBoundingBox)
            {
                // 바운딩 박스 편집 모드
                UpdateEditingRect(e.Location, dragHandleIndex);
                pictureBoxImage.Invalidate();
            }
            else if (isDragging)
            {
                // 현재 마우스 위치를 스크린 좌표로 변환
                Point currentPoint = pictureBoxImage.PointToScreen(e.Location);

                // 이전 위치와 현재 위치의 차이만큼 이미지 이동
                int deltaX = currentPoint.X - startPoint.X;
                int deltaY = currentPoint.Y - startPoint.Y;
                pictureBoxImage.Left += deltaX;
                pictureBoxImage.Top += deltaY;

                // 현재 위치를 새로운 시작 위치로 업데이트
                startPoint = currentPoint;
            }
            else if (isSquareToolActive && e.Button == MouseButtons.Left)
            {
                // 마우스 움직임에 따라 사각형 크기 조정
                int width = Math.Abs(e.X - rectStartPoint.X);
                int height = Math.Abs(e.Y - rectStartPoint.Y);
                int x = Math.Min(rectStartPoint.X, e.X);
                int y = Math.Min(rectStartPoint.Y, e.Y);

                currentRect = new Rectangle(x, y, width, height);
                pictureBoxImage.Invalidate();
            }
            else if (isEditingBoundingBox)
            {
                // 바운딩 박스 편집 모드에서 마우스 호버링 처리
                int handleIndex = GetHandleAtPosition(e.Location);
                if (handleIndex >= 0)
                {
                    pictureBoxImage.Cursor = GetResizeCursor(handleIndex);
                }
                else if (editingRect.Contains(e.Location))
                {
                    pictureBoxImage.Cursor = Cursors.SizeAll;
                }
                else
                {
                    pictureBoxImage.Cursor = Cursors.Hand;
                }
            }
        }

        /// <summary>
        /// pictureBoxImage에서 마우스 업 이벤트 핸들러
        /// </summary>
        private void PictureBoxImage_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (isPolygonPointDragging)
                {
                    // 폴리곤 꼭지점 드래그 종료
                    isPolygonPointDragging = false;
                    dragPointIndex = -1;
                    pictureBoxImage.Cursor = Cursors.Hand;

                    // 수정된 폴리곤 저장
                    if (isEditingPolygon && selectedPolygonIndex >= 0 &&
                        imagePolygons.ContainsKey(currentImageIndex) &&
                        selectedPolygonIndex < imagePolygons[currentImageIndex].Count)
                    {
                        // 화면 좌표를 이미지 좌표로 변환
                        List<Point> imagePoints = ConvertPointsToImageCoordinates(editingPolygonPoints);

                        // 기존 라벨 유지하면서 좌표만 업데이트
                        string label = imagePolygons[currentImageIndex][selectedPolygonIndex].Item2;
                        imagePolygons[currentImageIndex][selectedPolygonIndex] =
                            new Tuple<List<Point>, string>(imagePoints, label);

                        // 폴리곤 수정 후 정확도 즉시 계산 및 표시
                        if (currentLevel == "Segmentation")
                        {
                            CalculateAndDisplayAccuracy();
                        }
                    }
                }
                else if (isBoundingBoxDragging)
                {
                    // 바운딩 박스 드래그 종료
                    isBoundingBoxDragging = false;
                    dragHandleIndex = -1;
                    pictureBoxImage.Cursor = Cursors.Hand;

                    // 수정된 바운딩 박스 저장
                    if (isEditingBoundingBox && selectedBoxIndex >= 0 &&
                        imageBoundingBoxes.ContainsKey(currentImageIndex) &&
                        selectedBoxIndex < imageBoundingBoxes[currentImageIndex].Count)
                    {
                        // 화면 좌표를 이미지 좌표로 변환
                        Rectangle imageRect = ConvertToImageCoordinates(editingRect);

                        // 이전 상태 저장 (히스토리용)
                        List<Tuple<Rectangle, string>> previousBoxes = null;
                        if (imageBoundingBoxes.ContainsKey(currentImageIndex))
                        {
                            previousBoxes = new List<Tuple<Rectangle, string>>(imageBoundingBoxes[currentImageIndex]);
                        }

                        // 기존 라벨 유지하면서 좌표만 업데이트
                        string label = imageBoundingBoxes[currentImageIndex][selectedBoxIndex].Item2;
                        imageBoundingBoxes[currentImageIndex][selectedBoxIndex] =
                            new Tuple<Rectangle, string>(imageRect, label);

                        // 히스토리에 작업 추가
                        var action = new ActionState
                        {
                            Type = ActionState.ActionType.BoundingBox,
                            ImageIndex = currentImageIndex,
                            BeforeState = previousBoxes,
                            AfterState = new List<Tuple<Rectangle, string>>(imageBoundingBoxes[currentImageIndex])
                        };
                        AddToHistory(action);

                        // 바운딩 박스 수정 후 정확도 즉시 계산 및 표시
                        if (currentLevel == "Bounding Box")
                        {
                            CalculateAndDisplayAccuracy();
                        }
                    }
                }
                else if (isDragging)
                {
                    isDragging = false; // 일반 드래그 종료
                }
                else if (isSquareToolActive && !currentRect.IsEmpty)
                {
                    // 최소 크기 이상인 경우에만 처리
                    if (currentRect.Width > 5 && currentRect.Height > 5)
                    {
                        // 화면 좌표에서 이미지 좌표로 변환
                        Rectangle imageRect = ConvertToImageCoordinates(currentRect);

                        // 변환된 이미지 좌표를 사용하여 바운딩 박스 편집기 열기
                        OpenAnnotationEditorForBoundingBox(imageRect);
                    }
                    else
                    {
                        // 너무 작은 박스는 취소
                        currentRect = Rectangle.Empty;
                        pictureBoxImage.Invalidate();
                    }
                }
            }
        }


        /// <summary>
        /// pictureBoxImage에서 마우스 호버 이벤트 핸들러
        /// </summary>
        private void PictureBoxImage_MouseHover(object sender, MouseEventArgs e)
        {
            Point? previousHoveredPoint = hoveredPoint;
            hoveredPoint = null;

            if (isPolygonToolActive && polygonPoints.Count > 0)
            {
                // 폴리곤 생성 중일 때의 호버링 로직
                if (polygonPoints.Count >= 3)
                {
                    Point firstPoint = polygonPoints[0];
                    int hoverThreshold = 15;

                    if (Math.Abs(e.X - firstPoint.X) <= hoverThreshold &&
                        Math.Abs(e.Y - firstPoint.Y) <= hoverThreshold)
                    {
                        hoveredPoint = firstPoint;
                        pictureBoxImage.Cursor = Cursors.Hand;
                    }
                    else
                    {
                        pictureBoxImage.Cursor = isPolygonToolActive ? Cursors.Cross : Cursors.Default;
                    }
                }
            }
            else if (isEditingPolygon && editingPolygonPoints.Count > 0)
            {
                // 폴리곤 편집 중일 때의 호버링 로직
                int pointIndex = GetPointAtPosition(e.Location, 10, editingPolygonPoints);
                if (pointIndex >= 0)
                {
                    hoveredPoint = editingPolygonPoints[pointIndex];
                    pictureBoxImage.Cursor = Cursors.SizeAll;
                }
                else
                {
                    // 선 위에 있는지 확인
                    int lineIndex = GetLineSegmentIndex(e.Location, 5);
                    if (lineIndex >= 0)
                    {
                        pictureBoxImage.Cursor = Cursors.Cross; // 선 위에서는 Cross 커서 표시
                    }
                    else
                    {
                        pictureBoxImage.Cursor = Cursors.Hand;
                    }
                }
            }

            // 호버링 상태가 변경되었으면 화면 갱신
            if (previousHoveredPoint != hoveredPoint)
            {
                pictureBoxImage.Invalidate();
            }
        }

        /// <summary>
        /// pictureBoxImage에서 마우스 클릭 이벤트 핸들러
        /// </summary>
        private void PictureBoxImage_Click(object sender, EventArgs e)
        {
            MouseEventArgs mouseEvent = e as MouseEventArgs;
            if (mouseEvent == null)
                return;

            // 현재 마우스 포인터 위치 가져오기
            Point clickPoint = mouseEvent.Location;

            if (currentLevel == "Classification")
            {
                // Classification 단계에서는 이미지 클릭으로 주석 편집기 열기
                OpenAnnotationEditor(class3.Text);
            }
            else if ((currentLevel == "Bounding Box" || currentLevel == "Segmentation") &&
                     !isSquareToolActive && !isEditingBoundingBox) // 바운딩 박스 편집 모드에서는 annotation editor 열지 않음
            {
                // 클릭한 위치가 기존 바운딩 박스 내부인지 확인
                if (IsClickInsideBoundingBox(clickPoint, out Tuple<Rectangle, string> box))
                {
                    // 기존 라벨로 주석 편집기 열기
                    OpenAnnotationEditor(box.Item2);
                }
            }
        }

        /// <summary>
        /// pictureBoxImage에서 마우스 클릭 이벤트 핸들러(폴리곤 관련)
        /// </summary>
        private void PictureBoxImage_MouseClick(object sender, MouseEventArgs e)
        {
            // 폴리곤 도구가 활성화된 상태에서만 처리
            if (isPolygonToolActive && currentLevel == "Segmentation")
            {
                Point clickPoint = e.Location;

                // 클릭한 위치가 이미 있는 점과 근접한지 확인
                int pointIndex = GetPointAtPosition(clickPoint, hoveredPoint.HasValue ? 15 : 10);

                if (pointIndex == 0 && polygonPoints.Count >= 3)
                {
                    // 첫 번째 점을 클릭하면 폴리곤 완성
                    CompletePolygon();

                    // 호버링 상태 초기화
                    hoveredPoint = null;
                    pictureBoxImage.Cursor = isPolygonToolActive ? Cursors.Cross : Cursors.Default;
                }

                else if (pointIndex > 0)
                {
                    // 클릭한 점과 그 이전 점들만 유지
                    polygonPoints = polygonPoints.Take(pointIndex).ToList();
                    pictureBoxImage.Invalidate();

                }
                else if (pointIndex > 0 && pointIndex < polygonPoints.Count)
                {
                    // 클릭한 점을 삭제
                    polygonPoints.RemoveAt(pointIndex);
                    pictureBoxImage.Invalidate();
                }
                else if (pointIndex == 0 && polygonPoints.Count < 3)
                {
                    polygonPoints.Clear();
                    pictureBoxImage.Invalidate();
                }
                else
                {
                    // 새 점 추가
                    polygonPoints.Add(clickPoint);
                    pictureBoxImage.Invalidate();
                }
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // 그리기 관련 ////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// pictureBox를 다시 그림 
        /// </summary>
        private void PictureBoxImage_Paint(object sender, PaintEventArgs e)
        {
            // 현재 그리는 중인 사각형 그리기
            if (isSquareToolActive && currentRect != Rectangle.Empty)
            {
                using (Pen pen = new Pen(Color.Red, 2))
                {
                    e.Graphics.DrawRectangle(pen, currentRect);
                }
            }

            // 저장된 바운딩 박스들 그리기 - 현재 이미지에 해당하는 바운딩 박스만 표시
            // 편집 모드일 때는 기존 박스를 표시하지 않음
            if (isBoundingBoxVisible && imageBoundingBoxes.ContainsKey(currentImageIndex) && !isEditingBoundingBox)
            {
                foreach (var box in imageBoundingBoxes[currentImageIndex])
                {
                    // 이미지와 PictureBox 간의 비율 계산
                    float scaleX = pictureBoxImage.ClientSize.Width / (float)pictureBoxImage.BackgroundImage.Width;
                    float scaleY = pictureBoxImage.ClientSize.Height / (float)pictureBoxImage.BackgroundImage.Height;

                    // 좌표 변환
                    Rectangle displayRect = new Rectangle(
                        (int)(box.Item1.X * scaleX),
                        (int)(box.Item1.Y * scaleY),
                        (int)(box.Item1.Width * scaleX),
                        (int)(box.Item1.Height * scaleY)
                    );

                    // 테두리가 잘리지 않도록 안쪽으로 2픽셀 조정
                    displayRect.Inflate(-2, -2);

                    using (Pen pen = new Pen(Color.Red, 2))
                    {
                        e.Graphics.DrawRectangle(pen, displayRect);

                        // 라벨 텍스트 그리기
                        if (!string.IsNullOrEmpty(box.Item2))
                        {
                            using (Font font = new Font("Arial", 10))
                            using (SolidBrush brush = new SolidBrush(Color.Red))
                            using (SolidBrush bgBrush = new SolidBrush(Color.FromArgb(180, Color.White)))
                            {
                                SizeF textSize = e.Graphics.MeasureString(box.Item2, font);

                                // 텍스트 배경
                                e.Graphics.FillRectangle(bgBrush,
                                    displayRect.X,
                                    displayRect.Y > textSize.Height ? displayRect.Y - textSize.Height : displayRect.Y,
                                    textSize.Width, textSize.Height);

                                // 텍스트
                                e.Graphics.DrawString(box.Item2, font, brush,
                                    displayRect.X,
                                    displayRect.Y > textSize.Height ? displayRect.Y - textSize.Height : displayRect.Y);
                            }
                        }
                    }
                }
            }

            // 편집 중인 바운딩 박스 그리기
            if (isEditingBoundingBox && !editingRect.IsEmpty)
            {
                using (Pen pen = new Pen(Color.Orange, 2))
                {
                    e.Graphics.DrawRectangle(pen, editingRect);

                    // 핸들 그리기 (4개 코너)
                    using (SolidBrush handleBrush = new SolidBrush(Color.Orange))
                    {
                        // 좌상단
                        e.Graphics.FillRectangle(handleBrush, 
                            editingRect.X - HandleSize / 2, 
                            editingRect.Y - HandleSize / 2, 
                            HandleSize, HandleSize);

                        // 우상단
                        e.Graphics.FillRectangle(handleBrush, 
                            editingRect.Right - HandleSize / 2, 
                            editingRect.Y - HandleSize / 2, 
                            HandleSize, HandleSize);

                        // 좌하단
                        e.Graphics.FillRectangle(handleBrush, 
                            editingRect.X - HandleSize / 2, 
                            editingRect.Bottom - HandleSize / 2, 
                            HandleSize, HandleSize);

                        // 우하단
                        e.Graphics.FillRectangle(handleBrush, 
                            editingRect.Right - HandleSize / 2, 
                            editingRect.Bottom - HandleSize / 2, 
                            HandleSize, HandleSize);
                    }

                    // 라벨 텍스트 그리기
                    if (imageBoundingBoxes.ContainsKey(currentImageIndex) &&
                        selectedBoxIndex >= 0 &&
                        selectedBoxIndex < imageBoundingBoxes[currentImageIndex].Count)
                    {
                        string label = imageBoundingBoxes[currentImageIndex][selectedBoxIndex].Item2;
                        if (!string.IsNullOrEmpty(label))
                        {
                            using (Font font = new Font("Arial", 10))
                            using (SolidBrush brush = new SolidBrush(Color.Orange))
                            using (SolidBrush bgBrush = new SolidBrush(Color.FromArgb(180, Color.White)))
                            {
                                SizeF textSize = e.Graphics.MeasureString(label, font);

                                // 텍스트 배경
                                e.Graphics.FillRectangle(bgBrush,
                                    editingRect.X,
                                    editingRect.Y > textSize.Height ? editingRect.Y - textSize.Height : editingRect.Y,
                                    textSize.Width, textSize.Height);

                                // 텍스트
                                e.Graphics.DrawString(label, font, brush,
                                    editingRect.X,
                                    editingRect.Y > textSize.Height ? editingRect.Y - textSize.Height : editingRect.Y);
                            }
                        }
                    }
                }
            }

            // 폴리곤 그리기 (현재 작업 중인 폴리곤)
            if (isPolygonToolActive && polygonPoints.Count > 0)
            {
                using (Pen pen = new Pen(Color.Blue, 2))
                {
                    // 연결된 점들 그리기
                    for (int i = 0; i < polygonPoints.Count - 1; i++)
                    {
                        e.Graphics.DrawLine(pen, polygonPoints[i], polygonPoints[i + 1]);
                    }

                    // 마지막 점에서 첫 점까지 선 그리기 (3개 이상 점이 있을 때)
                    if (polygonPoints.Count >= 3)
                    {
                        using (Pen dashedPen = new Pen(Color.Blue, 2))
                        {
                            dashedPen.DashStyle = DashStyle.Dash;
                            e.Graphics.DrawLine(dashedPen, polygonPoints[polygonPoints.Count - 1], polygonPoints[0]);
                        }
                    }
                }

                // 점 그리기
                int normalPointSize = 10;
                int hoveredPointSize = 16; // 호버링 시 점 크기

                using (SolidBrush blueBrush = new SolidBrush(Color.Blue))
                using (SolidBrush redBrush = new SolidBrush(Color.Red))
                using (SolidBrush orangeBrush = new SolidBrush(Color.Orange)) // 호버링 시 색상
                {
                    // 첫 번째 점은 빨간색 또는 오렌지색(호버링 시)으로 표시
                    if (polygonPoints.Count > 0)
                    {
                        Point firstPoint = polygonPoints[0];
                        bool isHovered = (hoveredPoint.HasValue && hoveredPoint.Value == firstPoint);

                        // 호버링 상태에 따라 크기와 색상 변경
                        int pointSize = isHovered ? hoveredPointSize : normalPointSize;
                        SolidBrush brush = isHovered ? orangeBrush : redBrush;

                        e.Graphics.FillEllipse(brush,
                            firstPoint.X - pointSize / 2,
                            firstPoint.Y - pointSize / 2,
                            pointSize, pointSize);

                        // 호버링 시 테두리 추가
                        if (isHovered)
                        {
                            using (Pen outlinePen = new Pen(Color.White, 2))
                            {
                                e.Graphics.DrawEllipse(outlinePen,
                                    firstPoint.X - pointSize / 2,
                                    firstPoint.Y - pointSize / 2,
                                    pointSize, pointSize);
                            }
                        }
                    }

                    // 나머지 점들은 파란색으로 표시
                    for (int i = 1; i < polygonPoints.Count; i++)
                    {
                        e.Graphics.FillEllipse(blueBrush,
                            polygonPoints[i].X - normalPointSize / 2,
                            polygonPoints[i].Y - normalPointSize / 2,
                            normalPointSize, normalPointSize);
                    }
                }
            }

            // 폴리곤 편집 모드일 때 편집 중인 폴리곤 그리기
            if (isEditingPolygon && editingPolygonPoints.Count >= 3)
            {
                using (Pen pen = new Pen(Color.Orange, 2))
                {
                    // 편집 중인 폴리곤 그리기
                    e.Graphics.DrawPolygon(pen, editingPolygonPoints.ToArray());

                    // 꼭지점 그리기
                    int pointSize = 10;
                    using (SolidBrush pointBrush = new SolidBrush(Color.Orange))
                    {
                        foreach (var point in editingPolygonPoints)
                        {
                            // 호버링 중인 점은 더 크게 표시
                            bool isHovered = (hoveredPoint.HasValue &&
                                             Math.Abs(point.X - hoveredPoint.Value.X) <= 5 &&
                                             Math.Abs(point.Y - hoveredPoint.Value.Y) <= 5);

                            int size = isHovered ? 16 : pointSize;

                            e.Graphics.FillEllipse(pointBrush,
                                point.X - size / 2,
                                point.Y - size / 2,
                                size, size);

                            // 호버링 시 테두리 추가
                            if (isHovered)
                            {
                                using (Pen outlinePen = new Pen(Color.White, 2))
                                {
                                    e.Graphics.DrawEllipse(outlinePen,
                                        point.X - size / 2,
                                        point.Y - size / 2,
                                        size, size);
                                }
                            }
                        }
                    }

                    // 라벨 텍스트 그리기
                    if (imagePolygons.ContainsKey(currentImageIndex) &&
                        selectedPolygonIndex >= 0 &&
                        selectedPolygonIndex < imagePolygons[currentImageIndex].Count)
                    {
                        string label = imagePolygons[currentImageIndex][selectedPolygonIndex].Item2;
                        if (!string.IsNullOrEmpty(label))
                        {
                            using (Font font = new Font("Arial", 10))
                            using (SolidBrush brush = new SolidBrush(Color.Orange))
                            using (SolidBrush bgBrush = new SolidBrush(Color.FromArgb(180, Color.White)))
                            {
                                // 폴리곤의 중심 계산
                                int sumX = 0, sumY = 0;
                                foreach (var point in editingPolygonPoints)
                                {
                                    sumX += point.X;
                                    sumY += point.Y;
                                }
                                Point center = new Point(sumX / editingPolygonPoints.Count,
                                                       sumY / editingPolygonPoints.Count);

                                SizeF textSize = e.Graphics.MeasureString(label, font);

                                // 텍스트 배경
                                e.Graphics.FillRectangle(bgBrush,
                                    center.X - textSize.Width / 2,
                                    center.Y - textSize.Height / 2,
                                    textSize.Width, textSize.Height);

                                // 텍스트
                                e.Graphics.DrawString(label, font, brush,
                                    center.X - textSize.Width / 2,
                                    center.Y - textSize.Height / 2);
                            }
                        }
                    }
                }
            }

            // 저장된 폴리곤들 그리기 - 편집 모드가 아닐 때만
            if (isSegmentationVisible && !isEditingPolygon && imagePolygons.ContainsKey(currentImageIndex))
            {
                foreach (var polygon in imagePolygons[currentImageIndex])
                {
                    // 이미지 좌표를 화면 좌표로 변환
                    List<Point> displayPoints = ConvertPointsToDisplayCoordinates(polygon.Item1);

                    if (displayPoints.Count >= 3) // 최소 3개 점 필요
                    {
                        using (Pen pen = new Pen(Color.Green, 2))
                        {
                            // 폴리곤 그리기
                            e.Graphics.DrawPolygon(pen, displayPoints.ToArray());

                            // 라벨 텍스트 그리기
                            if (!string.IsNullOrEmpty(polygon.Item2))
                            {
                                using (Font font = new Font("Arial", 10))
                                using (SolidBrush brush = new SolidBrush(Color.Green))
                                using (SolidBrush bgBrush = new SolidBrush(Color.FromArgb(180, Color.White)))
                                {
                                    // 폴리곤의 중심 계산
                                    int sumX = 0, sumY = 0;
                                    foreach (var point in displayPoints)
                                    {
                                        sumX += point.X;
                                        sumY += point.Y;
                                    }
                                    Point center = new Point(sumX / displayPoints.Count, sumY / displayPoints.Count);

                                    SizeF textSize = e.Graphics.MeasureString(polygon.Item2, font);

                                    // 텍스트 배경
                                    e.Graphics.FillRectangle(bgBrush,
                                        center.X - textSize.Width / 2,
                                        center.Y - textSize.Height / 2,
                                        textSize.Width, textSize.Height);

                                    // 텍스트
                                    e.Graphics.DrawString(polygon.Item2, font, brush,
                                        center.X - textSize.Width / 2,
                                        center.Y - textSize.Height / 2);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ImageContainer를 다시 그림
        /// </summary>
        private void ImageContainer_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // 바운딩 박스 관련 ////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // 바운딩 박스 좌표를 JSON 형태로 내보내기

        //private string ExportBoundingBoxCoordinates(int imageIndex)
        //{
        //    if (imageBoundingBoxes.ContainsKey(imageIndex) &&
        //        imageBoundingBoxes[imageIndex].Count > 0)
        //    {
        //        var box = imageBoundingBoxes[imageIndex][0];
        //        return $"{{\"label\":\"{box.Item2}\",\"x\":{box.Item1.X},\"y\":{box.Item1.Y},\"width\":{box.Item1.Width},\"height\":{box.Item1.Height}}}";
        //    }
        //    return "{}";
        //}

        //// 폴리곤 좌표를 JSON 형태로 내보내기
        //private string ExportPolygonCoordinates(int imageIndex)
        //{
        //    if (imagePolygons.ContainsKey(imageIndex) &&
        //        imagePolygons[imageIndex].Count > 0)
        //    {
        //        var polygon = imagePolygons[imageIndex][0];

        //        // 점들의 좌표를 배열로 변환
        //        var points = polygon.Item1.Select(p => $"{{\"x\":{p.X},\"y\":{p.Y}}}").ToArray();
        //        string pointsJson = string.Join(",", points);

        //        return $"{{\"label\":\"{polygon.Item2}\",\"points\":[{pointsJson}]}}";
        //    }
        //    return "{}";
        //}

        /// <summary>
        /// 현재 이미지 인덱스에 해당하는 바운딩 박스 목록을 가져옴
        /// </summary>
        private List<Tuple<Rectangle, string>> CurrentBoundingBoxes
        {
            get
            {
                if (!imageBoundingBoxes.ContainsKey(currentImageIndex))
                {
                    imageBoundingBoxes[currentImageIndex] = new List<Tuple<Rectangle, string>>();
                }
                return imageBoundingBoxes[currentImageIndex];
            }
        }

        /// <summary>
        /// 화면 좌표를 이미지 좌표로 변환
        /// </summary>
        private Rectangle ConvertToImageCoordinates(Rectangle displayRect)
        {
            // 이미지와 PictureBox 간의 비율 계산
            float scaleX = pictureBoxImage.BackgroundImage.Width / (float)pictureBoxImage.ClientSize.Width;
            float scaleY = pictureBoxImage.BackgroundImage.Height / (float)pictureBoxImage.ClientSize.Height;

            // 화면 좌표를 이미지 좌표로 변환
            return new Rectangle(
                (int)(displayRect.X * scaleX),
                (int)(displayRect.Y * scaleY),
                (int)(displayRect.Width * scaleX),
                (int)(displayRect.Height * scaleY)
            );
        }

        /// <summary>
        /// 바운딩 박스 생성을 위한 주석 편집기 열기
        /// </summary>

        private void OpenAnnotationEditorForBoundingBox(Rectangle imageRect)
        {
            using (var editorForm = new AnnotationEditorForm(class3.Text))
            {
                if (editorForm.ShowDialog() == DialogResult.OK || editorForm.IsSaved)
                {
                    // 저장된 주석 텍스트 가져오기
                    string annotationText = editorForm.AnnotationText;

                    // 기존 바운딩 박스 상태 저장 (히스토리용)
                    List<Tuple<Rectangle, string>> previousBoxes = null;
                    if (imageBoundingBoxes.ContainsKey(currentImageIndex))
                    {
                        previousBoxes = new List<Tuple<Rectangle, string>>(imageBoundingBoxes[currentImageIndex]);
                    }

                    // class2 라벨 업데이트 (Bounding Box 단계에서도 라벨 표시)
                    class3.Text = annotationText;
                    
                    // classBtn 가시성 업데이트
                    UpdateClassButtonVisibility();

                    // 기존 바운딩 박스 모두 제거 (라벨링은 항상 하나만)
                    CurrentBoundingBoxes.Clear();

                    // 새 바운딩 박스 추가
                    CurrentBoundingBoxes.Add(new Tuple<Rectangle, string>(imageRect, annotationText));

                    // 히스토리에 작업 추가
                    var action = new ActionState
                    {
                        Type = ActionState.ActionType.BoundingBox,
                        ImageIndex = currentImageIndex,
                        BeforeState = previousBoxes,
                        AfterState = new List<Tuple<Rectangle, string>>(imageBoundingBoxes[currentImageIndex])
                    };
                    AddToHistory(action);

                    // 그리기 상태 초기화
                    currentRect = Rectangle.Empty;
                    pictureBoxImage.Invalidate();

                    // 네비게이션 버튼 상태 업데이트
                    UpdateNavigationButtonState();

                    // 정확도 계산 버튼 활성화
                    CalculateAndDisplayAccuracy();
                }
                else
                {
                    // 취소된 경우 현재 그리던 사각형 취소
                    currentRect = Rectangle.Empty;
                    pictureBoxImage.Invalidate();
                }
            }
        }

        // JSON 문자열에서 바운딩 박스 파싱
        private Tuple<Rectangle, string> ParseBoundingBoxFromJson(string json)
        {
            // 간단한 JSON 파싱 (완전한 파서는 Newtonsoft.Json 사용 권장)
            int xStart = json.IndexOf("\"x\":") + 4;
            int yStart = json.IndexOf("\"y\":") + 4;
            int widthStart = json.IndexOf("\"width\":") + 8;
            int heightStart = json.IndexOf("\"height\":") + 9;
            int labelStart = json.IndexOf("\"label\":\"") + 9;
            int labelEnd = json.IndexOf("\"", labelStart);

            string label = json.Substring(labelStart, labelEnd - labelStart);
            int x = int.Parse(json.Substring(xStart, json.IndexOf(",", xStart) - xStart));
            int y = int.Parse(json.Substring(yStart, json.IndexOf(",", yStart) - yStart));
            int width = int.Parse(json.Substring(widthStart, json.IndexOf(",", widthStart) - widthStart));
            int height = int.Parse(json.Substring(heightStart, json.IndexOf("}", heightStart) - heightStart));

            return new Tuple<Rectangle, string>(new Rectangle(x, y, width, height), label);
        }

        // JSON 문자열에서 폴리곤 파싱
        private Tuple<List<Point>, string> ParsePolygonFromJson(string json)
        {
            // 라벨 추출
            int labelStart = json.IndexOf("\"label\":\"") + 9;
            int labelEnd = json.IndexOf("\"", labelStart);
            string label = json.Substring(labelStart, labelEnd - labelStart);

            // 포인트 추출
            List<Point> points = new List<Point>();
            int pointsStart = json.IndexOf("\"points\":[") + 10;
            int pointsEnd = json.IndexOf("]", pointsStart);
            string pointsJson = json.Substring(pointsStart, pointsEnd - pointsStart);

            // 각 점 파싱
            int index = 0;
            while (index < pointsJson.Length)
            {
                int coordStart = pointsJson.IndexOf("{", index);
                if (coordStart == -1) break;

                int coordEnd = pointsJson.IndexOf("}", coordStart);
                string coordJson = pointsJson.Substring(coordStart, coordEnd - coordStart + 1);

                int xStart = coordJson.IndexOf("\"x\":") + 4;
                int yStart = coordJson.IndexOf("\"y\":") + 4;

                int x = int.Parse(coordJson.Substring(xStart, coordJson.IndexOf(",", xStart) - xStart));
                int y = int.Parse(coordJson.Substring(yStart, coordJson.IndexOf("}", yStart) - yStart));

                points.Add(new Point(x, y));
                index = coordEnd + 1;
            }

            return new Tuple<List<Point>, string>(points, label);
        }

        // IoU (Intersection over Union) 계산 메서드 (바운딩 박스용)
        private double CalculateIoU(Rectangle boxA, Rectangle boxB)
        {
            // 교집합 영역 계산
            Rectangle intersectionRect = Rectangle.Intersect(boxA, boxB);
            int intersectionArea = intersectionRect.Width * intersectionRect.Height;

            // 합집합 영역 계산 (A + B - 교집합)
            int boxAArea = boxA.Width * boxA.Height;
            int boxBArea = boxB.Width * boxB.Height;
            int unionArea = boxAArea + boxBArea - intersectionArea;

            // IoU = 교집합 / 합집합
            return (double)intersectionArea / unionArea;
        }

        // IoA (Intersection over Area) 계산 메서드 (세그멘테이션용)
        private double CalculateIoA(List<Point> userPolygon, List<Point> groundTruthPolygon)
        {
            try
            {
                if (userPolygon.Count < 3 || groundTruthPolygon.Count < 3)
                {
                    MessageBox.Show("폴리곤은 최소 3개 이상의 점이 필요합니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return 0; // 유효하지 않은 폴리곤
                }

                // Clipper2에서 사용되는 Path64 객체 생성
                Clipper2Lib.Path64 userPath = new Clipper2Lib.Path64();
                Clipper2Lib.Path64 groundTruthPath = new Clipper2Lib.Path64();

                // 정밀도를 위해 좌표 스케일링 (Clipper2는 정수 좌표만 사용)
                const double scale = 1000.0;

                // 사용자 폴리곤 변환
                foreach (var point in userPolygon)
                {
                    userPath.Add(new Clipper2Lib.Point64(
                        (long)(point.X * scale),
                        (long)(point.Y * scale)));
                }

                // 정답 폴리곤 변환
                foreach (var point in groundTruthPolygon)
                {
                    groundTruthPath.Add(new Clipper2Lib.Point64(
                        (long)(point.X * scale),
                        (long)(point.Y * scale)));
                }

                // 폴리곤 경로 생성
                Clipper2Lib.Paths64 userPaths = new Clipper2Lib.Paths64();
                Clipper2Lib.Paths64 groundTruthPaths = new Clipper2Lib.Paths64();
                userPaths.Add(userPath);
                groundTruthPaths.Add(groundTruthPath);

                // 사용자 폴리곤과 정답 폴리곤 모두 시계 방향으로 정렬
                if (Clipper2Lib.Clipper.Area(userPath) < 0)
                    Clipper2Lib.Clipper.ReversePath(userPath);
                if (Clipper2Lib.Clipper.Area(groundTruthPath) < 0)
                    Clipper2Lib.Clipper.ReversePath(groundTruthPath);

                // 교집합 계산
                Clipper2Lib.Paths64 intersectionPaths = Clipper2Lib.Clipper.Intersect(
                    userPaths,
                    groundTruthPaths,
                    Clipper2Lib.FillRule.NonZero);

                // 면적 계산 (절대값 사용)
                double intersectionArea = 0;
                foreach (var path in intersectionPaths)
                {
                    intersectionArea += Math.Abs(Clipper2Lib.Clipper.Area(path)) / (scale * scale);
                }

                // 사용자 폴리곤과 정답 폴리곤의 면적 계산 (절대값 사용)
                double groundTruthArea = Math.Abs(Clipper2Lib.Clipper.Area(groundTruthPath)) / (scale * scale);
                double userArea = Math.Abs(Clipper2Lib.Clipper.Area(userPath)) / (scale * scale);

                // 교집합 영역이 전체 영역에서 차지하는 비율 계산 (IoU 형태)
                double unionArea = userArea + groundTruthArea - intersectionArea;
                if (unionArea <= 0) return 0;

                // 정확도 계산: 교집합 / 합집합 (IoU)
                double accuracy = intersectionArea / unionArea;

                // 기존 방식: 교집합 / 정답 영역 (IoA)
                // double accuracy = groundTruthArea > 0 ? intersectionArea / groundTruthArea : 0;

                // 디버그 정보 출력
                Console.WriteLine($"사용자 폴리곤 면적: {userArea:.2f}, 정답 폴리곤 면적: {groundTruthArea:.2f}, 교집합 면적: {intersectionArea:.2f}");
                Console.WriteLine($"합집합 면적: {unionArea:.2f}, 정확도: {accuracy:.4f}");

                // 부동소수점 오류 보정
                if (accuracy > 1.0) accuracy = 1.0;

                return accuracy;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"IoA 계산 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // 비트맵 기반의 대체 방식으로 계산
                return CalculateIoUWithBitmap(userPolygon, groundTruthPolygon);
            }
        }

        // 비트맵 기반의 IoU 계산 대체 메서드 (IoA 대신 IoU를 계산)
        private double CalculateIoUWithBitmap(List<Point> userPolygon, List<Point> groundTruthPolygon)
        {
            try
            {
                // 모든 좌표의 경계 구하기
                int minX = int.MaxValue, minY = int.MaxValue;
                int maxX = int.MinValue, maxY = int.MinValue;

                foreach (var point in userPolygon.Concat(groundTruthPolygon))
                {
                    minX = Math.Min(minX, point.X);
                    minY = Math.Min(minY, point.Y);
                    maxX = Math.Max(maxX, point.X);
                    maxY = Math.Max(maxY, point.Y);
                }

                // 비트맵 크기 결정 (최대 2000x2000)
                int width = Math.Min(2000, maxX - minX + 100);
                int height = Math.Min(2000, maxY - minY + 100);

                // 좌표를 비트맵 영역으로 변환하는 오프셋
                int offsetX = minX - 50;
                int offsetY = minY - 50;

                using (Bitmap bmp = new Bitmap(width, height))
                {
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                        g.Clear(Color.Black); // 배경은 검은색

                        // 변환된 폴리곤 좌표 생성
                        Point[] gtPoints = groundTruthPolygon
                            .Select(p => new Point(p.X - offsetX, p.Y - offsetY))
                            .ToArray();

                        Point[] userPoints = userPolygon
                            .Select(p => new Point(p.X - offsetX, p.Y - offsetY))
                            .ToArray();

                        // 첫 번째 비트맵: 정답 폴리곤 (흰색으로 그림)
                        using (Bitmap gtBitmap = new Bitmap(width, height))
                        using (Graphics gtGraphics = Graphics.FromImage(gtBitmap))
                        {
                            gtGraphics.Clear(Color.Black);
                            using (SolidBrush whiteBrush = new SolidBrush(Color.White))
                            {
                                gtGraphics.FillPolygon(whiteBrush, gtPoints);
                            }
                            int gtArea = CountWhitePixels(gtBitmap);

                            // 두 번째 비트맵: 사용자 폴리곤 (흰색으로 그림)
                            using (Bitmap userBitmap = new Bitmap(width, height))
                            using (Graphics userGraphics = Graphics.FromImage(userBitmap))
                            {
                                userGraphics.Clear(Color.Black);
                                using (SolidBrush whiteBrush = new SolidBrush(Color.White))
                                {
                                    userGraphics.FillPolygon(whiteBrush, userPoints);
                                }
                                int userArea = CountWhitePixels(userBitmap);

                                // 세 번째 비트맵: 교집합 계산 (두 영역이 겹치는 부분)
                                g.Clear(Color.Black);

                                // 첫 번째 폴리곤을 빨간색으로 그림
                                using (SolidBrush redBrush = new SolidBrush(Color.Red))
                                {
                                    g.FillPolygon(redBrush, gtPoints);
                                }

                                // 두 번째 폴리곤을 녹색으로 그림 (겹치는 부분은 노란색이 됨)
                                using (SolidBrush greenBrush = new SolidBrush(Color.Green))
                                {
                                    g.FillPolygon(greenBrush, userPoints);
                                }

                                // 노란색 픽셀 수 카운트 (교집합)
                                int intersectionArea = CountYellowPixels(bmp);

                                // 합집합 = 사용자 영역 + 정답 영역 - 교집합
                                int unionArea = userArea + gtArea - intersectionArea;

                                // IoU = 교집합 / 합집합
                                double iou = unionArea > 0 ? (double)intersectionArea / unionArea : 0;

                                Console.WriteLine($"비트맵 계산: 정답 면적: {gtArea}, 사용자 면적: {userArea}, 교집합: {intersectionArea}, 합집합: {unionArea}, IoU: {iou}");

                                return Math.Min(iou, 1.0); // 부동소수점 오류 방지
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"비트맵 IoU 계산 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        // 노란색(교집합) 픽셀 수 계산
        private int CountYellowPixels(Bitmap bmp)
        {
            int count = 0;
            // 성능 최적화를 위해 샘플링
            int stride = Math.Max(1, bmp.Width > 500 ? 2 : 1);

            for (int y = 0; y < bmp.Height; y += stride)
            {
                for (int x = 0; x < bmp.Width; x += stride)
                {
                    Color pixelColor = bmp.GetPixel(x, y);
                    // 노란색 감지 (R과 G 성분이 높고, B 성분이 낮음)
                    if (pixelColor.R > 200 && pixelColor.G > 200 && pixelColor.B < 100)
                    {
                        count += (stride * stride); // 샘플링된 영역 보정
                    }
                }
            }
            return count;
        }


        // 흰색 픽셀 수 계산
        private int CountWhitePixels(Bitmap bmp)
        {
            int count = 0;
            // 성능 최적화를 위해 샘플링
            int stride = Math.Max(1, bmp.Width > 500 ? 2 : 1);

            for (int y = 0; y < bmp.Height; y += stride)
            {
                for (int x = 0; x < bmp.Width; x += stride)
                {
                    Color pixelColor = bmp.GetPixel(x, y);
                    if (pixelColor.R > 240 && pixelColor.G > 240 && pixelColor.B > 240) // 흰색
                    {
                        count += (stride * stride); // 샘플링된 영역 보정
                    }
                }
            }
            return count;
        }

        /// <summary>
        /// 클릭한 위치가 바운딩 박스 영역 내부인지 확인
        /// </summary>
        // 클릭한 위치가 바운딩 박스 영역 내부인지 확인하는 메서드
        private bool IsClickInsideBoundingBox(Point clickPoint, out Tuple<Rectangle, string> clickedBox)
        {
            clickedBox = null;

            if (!imageBoundingBoxes.ContainsKey(currentImageIndex) ||
                imageBoundingBoxes[currentImageIndex].Count == 0)
                return false;

            // 이미지와 PictureBox 간의 비율 계산
            float scaleX = pictureBoxImage.ClientSize.Width / (float)pictureBoxImage.BackgroundImage.Width;
            float scaleY = pictureBoxImage.ClientSize.Height / (float)pictureBoxImage.BackgroundImage.Height;

            foreach (var box in imageBoundingBoxes[currentImageIndex])
            {
                // 이미지 좌표를 화면 좌표로 변환
                Rectangle displayRect = new Rectangle(
                    (int)(box.Item1.X * scaleX),
                    (int)(box.Item1.Y * scaleY),
                    (int)(box.Item1.Width * scaleX),
                    (int)(box.Item1.Height * scaleY)
                );

                // 클릭 좌표가 바운딩 박스 내부인지 확인
                if (displayRect.Contains(clickPoint))
                {
                    clickedBox = box;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 전체 이미지 영역에 대한 바운딩 박스 추가
        /// </summary>
        private void AddFullImageBoundingBox(string label)
        {
            if (pictureBoxImage.BackgroundImage != null)
            {
                // 원본 이미지 크기에 맞는 직사각형 생성
                Rectangle fullImageRect = new Rectangle(
                    0, 0,
                    pictureBoxImage.BackgroundImage.Width,
                    pictureBoxImage.BackgroundImage.Height
                );

                // 현재 이미지의 바운딩 박스가 있는지 확인하고, 있으면 제거 (라벨링은 하나만 허용)
                if (CurrentBoundingBoxes.Count > 0)
                {
                    // 첫 번째 아이템만 새 라벨로 교체
                    var existingBox = CurrentBoundingBoxes[0];
                    CurrentBoundingBoxes.Clear(); // 목록 초기화
                    CurrentBoundingBoxes.Add(new Tuple<Rectangle, string>(existingBox.Item1, label));
                }
                else
                {
                    // 바운딩 박스가 없는 경우 새로 추가
                    CurrentBoundingBoxes.Add(new Tuple<Rectangle, string>(fullImageRect, label));
                }

                // PictureBox 다시 그리기
                pictureBoxImage.Invalidate();
            }
        }

        /// <summary>
        /// 현재 그리고 있는 폴리곤을 완성하고 저장
        /// </summary>
        private void CompletePolygon()
        {
            try
            {
                using (var editorForm = new AnnotationEditorForm(class3.Text))
                {
                    if (editorForm.ShowDialog() == DialogResult.OK || editorForm.IsSaved)
                    {
                        string annotationText = editorForm.AnnotationText;
                        string previousLabel = class3.Text;

                        // 이전 폴리곤 상태 저장
                        List<Tuple<List<Point>, string>> previousPolygons = new List<Tuple<List<Point>, string>>();
                        if (imagePolygons.ContainsKey(currentImageIndex))
                        {
                            previousPolygons = new List<Tuple<List<Point>, string>>(imagePolygons[currentImageIndex]);
                        }

                        // 화면 좌표를 이미지 좌표로 변환
                        List<Point> imagePoints = ConvertPointsToImageCoordinates(polygonPoints);
                        List<Point> savedPolygonPoints = new List<Point>(polygonPoints);

                        class3.Text = annotationText;
                        
                        // classBtn 가시성 업데이트
                        UpdateClassButtonVisibility();

                        // 기존 폴리곤 데이터 삭제
                        if (imagePolygons.ContainsKey(currentImageIndex))
                        {
                            imagePolygons.Remove(currentImageIndex);
                        }

                        // 폴리곤 저장
                        if (!imagePolygons.ContainsKey(currentImageIndex))
                        {
                            imagePolygons[currentImageIndex] = new List<Tuple<List<Point>, string>>();
                        }

                        // 새 폴리곤 추가
                        imagePolygons[currentImageIndex].Add(new Tuple<List<Point>, string>(
                            new List<Point>(imagePoints), annotationText));

                        // 폴리곤 점 초기화 및 화면 갱신
                        polygonPoints.Clear();
                        pictureBoxImage.Invalidate();

                        var action = new ActionState
                        {
                            Type = ActionState.ActionType.Polygon,
                            ImageIndex = currentImageIndex,
                            BeforeState = previousPolygons,
                            AfterState = new List<Tuple<List<Point>, string>>(imagePolygons[currentImageIndex])
                        };

                        AddToHistory(action);

                        // 네비게이션 버튼 상태 업데이트
                        UpdateNavigationButtonState();

                        // 정확도 즉시 계산 및 표시
                        CalculateAndDisplayAccuracy();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"폴리곤을 저장하는 중 오류가 발생했습니다.\n\n오류 메시지: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 편집 중인 폴리곤의 변경 사항을 저장
        /// </summary>
        private void SaveEditingPolygon()
        {
            try
            {
                // 편집 중인 폴리곤 저장
                if (selectedPolygonIndex >= 0 &&
                    imagePolygons.ContainsKey(currentImageIndex) &&
                    selectedPolygonIndex < imagePolygons[currentImageIndex].Count)
                {
                    // 이전 상태 저장
                    List<Tuple<List<Point>, string>> previousPolygons = null;
                    if (imagePolygons.ContainsKey(currentImageIndex))
                    {
                        previousPolygons = new List<Tuple<List<Point>, string>>(imagePolygons[currentImageIndex]);
                    }

                    // 화면 좌표를 이미지 좌표로 변환
                    List<Point> imagePoints = ConvertPointsToImageCoordinates(editingPolygonPoints);
                    List<Point> savedEditingPoints = new List<Point>(editingPolygonPoints);

                    // 기존 라벨 유지하면서 좌표만 업데이트
                    string label = imagePolygons[currentImageIndex][selectedPolygonIndex].Item2;
                    imagePolygons[currentImageIndex][selectedPolygonIndex] =
                        new Tuple<List<Point>, string>(imagePoints, label);

                    // 히스토리에 작업 추가 (세그멘테이션 데이터도 포함)
                    var action = new ActionState
                    {
                        Type = ActionState.ActionType.PolygonEdit,
                        ImageIndex = currentImageIndex,
                        BeforeState = previousPolygons,
                        AfterState = new List<Tuple<List<Point>, string>>(imagePolygons[currentImageIndex]),
                    };
                    AddToHistory(action);
                }

                // 편집 모드 종료
                isEditingPolygon = false;
                selectedPolygonIndex = -1;
                editingPolygonPoints.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"폴리곤 편집 내용을 저장하는 중 오류가 발생했습니다.\n\n오류 메시지: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// 현재 이미지의 폴리곤 라벨을 업데이트
        /// </summary>
        private void UpdatePolygonLabel(string newLabel)
        {
            // Segmentation 모드이고, 현재 이미지에 폴리곤이 있는 경우
            if (currentLevel == "Segmentation" &&
                imagePolygons.ContainsKey(currentImageIndex) &&
                imagePolygons[currentImageIndex].Count > 0)
            {
                // 기존 폴리곤의 좌표와 새 라벨로 교체
                var existingPolygon = imagePolygons[currentImageIndex][0];
                List<Point> polygonCoords = existingPolygon.Item1;

                // 폴리곤 목록 초기화 후 새 라벨로 다시 추가
                imagePolygons[currentImageIndex].Clear();
                imagePolygons[currentImageIndex].Add(new Tuple<List<Point>, string>(
                    polygonCoords, newLabel));

                // 화면 갱신
                pictureBoxImage.Invalidate();
            }
        }

        /// <summary>
        /// 현재 좌표 목록을 이미지 좌표 목록으로 변환
        /// </summary>
        private List<Point> ConvertPointsToImageCoordinates(List<Point> displayPoints)
        {
            // 이미지와 PictureBox 간의 비율 계산
            float scaleX = pictureBoxImage.BackgroundImage.Width / (float)pictureBoxImage.ClientSize.Width;
            float scaleY = pictureBoxImage.BackgroundImage.Height / (float)pictureBoxImage.ClientSize.Height;

            // 화면 좌표를 이미지 좌표로 변환
            List<Point> imagePoints = new List<Point>();
            foreach (var point in displayPoints)
            {
                imagePoints.Add(new Point(
                    (int)(point.X * scaleX),
                    (int)(point.Y * scaleY)
                ));
            }

            return imagePoints;
        }

        /// <summary>
        /// 이미지 좌표 목록을 화면 좌표 목록으로 변환
        /// </summary>
        private List<Point> ConvertPointsToDisplayCoordinates(List<Point> imagePoints)
        {
            // 이미지와 PictureBox 간의 비율 계산
            float scaleX = pictureBoxImage.ClientSize.Width / (float)pictureBoxImage.BackgroundImage.Width;
            float scaleY = pictureBoxImage.ClientSize.Height / (float)pictureBoxImage.BackgroundImage.Height;

            // 이미지 좌표를 화면 좌표로 변환
            List<Point> displayPoints = new List<Point>();
            foreach (var point in imagePoints)
            {
                displayPoints.Add(new Point(
                    (int)(point.X * scaleX),
                    (int)(point.Y * scaleY)
                ));
            }

            return displayPoints;
        }

        /// <summary>
        /// 특정 위치에 있는 주어진 점의 인덱스를 반환
        /// </summary>
        private int GetPointAtPosition(Point position, int threshold, List<Point> points)
        {
            for (int i = 0; i < points.Count; i++)
            {
                if (Math.Abs(position.X - points[i].X) <= threshold &&
                    Math.Abs(position.Y - points[i].Y) <= threshold)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// 특정 위치에 있는 폴리곤 점의 인덱스를 반환
        /// </summary>
        private int GetPointAtPosition(Point position, int threshold)
        {
            for (int i = 0; i < polygonPoints.Count; i++)
            {
                if (Math.Abs(position.X - polygonPoints[i].X) <= threshold &&
                    Math.Abs(position.Y - polygonPoints[i].Y) <= threshold)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// 클릭한 위치가 선분 위에 있는지 확인하고, 해당 선분의 인덱스를 반환
        /// </summary>
        private int GetLineSegmentIndex(Point clickPoint, int threshold)
        {
            if (editingPolygonPoints.Count < 2)
                return -1;

            for (int i = 0; i < editingPolygonPoints.Count; i++)
            {
                Point start = editingPolygonPoints[i];
                Point end = editingPolygonPoints[(i + 1) % editingPolygonPoints.Count]; // 마지막 점과 첫 번째 점을 연결

                // 클릭한 위치가 두 점 사이의 선 위에 있는지 확인
                if (IsPointNearLine(clickPoint, start, end, threshold))
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// 점이 선분 근처에 있는지 확인
        /// </summary>
        private bool IsPointNearLine(Point point, Point lineStart, Point lineEnd, int threshold)
        {
            // 선분의 길이가 매우 작으면 선분이 아닌 점으로 처리
            if (Math.Abs(lineStart.X - lineEnd.X) <= threshold &&
                Math.Abs(lineStart.Y - lineEnd.Y) <= threshold)
            {
                return false;
            }

            // 선분의 길이를 계산
            double lineLength = Math.Sqrt(
                Math.Pow(lineEnd.X - lineStart.X, 2) +
                Math.Pow(lineEnd.Y - lineStart.Y, 2)
            );

            // 점에서 선분까지의 거리 계산
            double distance = Math.Abs(
                (lineEnd.Y - lineStart.Y) * point.X -
                (lineEnd.X - lineStart.X) * point.Y +
                lineEnd.X * lineStart.Y -
                lineEnd.Y * lineStart.X
            ) / lineLength;

            // 점이 선분의 끝점 밖에 있는지 확인 (선분 연장선 위에 있는지)
            double dot1 = (point.X - lineStart.X) * (lineEnd.X - lineStart.X) +
                          (point.Y - lineStart.Y) * (lineEnd.Y - lineStart.Y);
            double dot2 = (point.X - lineEnd.X) * (lineStart.X - lineEnd.X) +
                          (point.Y - lineEnd.Y) * (lineStart.Y - lineEnd.Y);

            return distance <= threshold && dot1 >= 0 && dot2 >= 0;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // 주석 편집기 관련 ////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void OpenAnnotationEditor(string initialText = "")
        {
            using (var editorForm = new AnnotationEditorForm(initialText))
            {
                // SaveClicked 이벤트 구독 - 저장 시 라벨 업데이트
                editorForm.SaveClicked += (sender, annotationText) =>
                {
                    // 이전 상태 저장
                    string previousLabel = class3.Text;

                    // class2 라벨 업데이트
                    class3.Text = annotationText;
                    
                    // classBtn 가시성 업데이트
                    UpdateClassButtonVisibility();

                    // 라벨링 단계에 따라 다른 작업 수행
                    if (currentLevel == "Segmentation")
                    {
                        // 이전 폴리곤 상태 저장
                        List<Tuple<List<Point>, string>> previousState = null;
                        if (imagePolygons.ContainsKey(currentImageIndex))
                        {
                            previousState = new List<Tuple<List<Point>, string>>(imagePolygons[currentImageIndex]);
                        }

                        // 세그멘테이션 모드에서 폴리곤 라벨 업데이트
                        UpdatePolygonLabel(annotationText);
                        CalculateAndDisplayAccuracy(); // 정확도 계산

                        // 히스토리 추가
                        var action = new ActionState
                        {
                            Type = ActionState.ActionType.Polygon,
                            ImageIndex = currentImageIndex,
                            BeforeState = previousState,
                            AfterState = imagePolygons.ContainsKey(currentImageIndex) ?
                                new List<Tuple<List<Point>, string>>(imagePolygons[currentImageIndex]) : null
                        };
                        AddToHistory(action);
                    }
                    else if (currentLevel == "Classification")
                    {
                        // 현재 이미지의 분류 정보 저장
                        imageClassifications[currentImageIndex] = annotationText;

                        // 사진 전체에 네모 박스 추가
                        AddFullImageBoundingBox(annotationText);

                        imageAccuracies[currentImageIndex] = 100.0;

                    }
                        
                    else if (currentLevel == "Bounding Box")
                    {
                        // 이전 상태 저장
                        List<Tuple<Rectangle, string>> previousBoxes = null;
                        if (imageBoundingBoxes.ContainsKey(currentImageIndex))
                        {
                            previousBoxes = new List<Tuple<Rectangle, string>>(imageBoundingBoxes[currentImageIndex]);
                        }

                        var existingBox = CurrentBoundingBoxes[0];
                        Rectangle rect = existingBox.Item1;
                        CurrentBoundingBoxes.Clear();
                        CurrentBoundingBoxes.Add(new Tuple<Rectangle, string>(rect, annotationText));
                        pictureBoxImage.Invalidate();
                        CalculateAndDisplayAccuracy(); // 정확도 계산

                        // 히스토리 추가
                        var action = new ActionState
                        {
                            Type = ActionState.ActionType.BoundingBox,
                            ImageIndex = currentImageIndex,
                            BeforeState = previousBoxes,
                            AfterState = new List<Tuple<Rectangle, string>>(imageBoundingBoxes[currentImageIndex])
                        };
                        AddToHistory(action);
                    }
                    UpdateNavigationButtonState();
                };

                if (editorForm.ShowDialog() == DialogResult.OK || editorForm.IsSaved)
                {
                    // 저장된 주석 텍스트 가져오기 (이벤트 핸들러에서 처리되므로 여기서는 추가 작업 필요 없음)
                }
            }
        }

        // 정확도 계산 및 표시 메서드
        private void CalculateAndDisplayAccuracy()
        {
            try
            {
                if (currentLevel == "Bounding Box")
                {
                    if (imageBoundingBoxes.ContainsKey(currentImageIndex) &&
                        imageBoundingBoxes[currentImageIndex] != null &&
                        imageBoundingBoxes[currentImageIndex].Count > 0 &&
                        groundTruthBoundingBoxes.ContainsKey(currentImageIndex))
                    {
                        var userBox = imageBoundingBoxes[currentImageIndex][0];
                        var groundTruthBox = groundTruthBoundingBoxes[currentImageIndex];

                        double iou = CalculateIoU(userBox.Item1, groundTruthBox.Item1);
                        double accuracy = iou * 100;

                        accuracyLabel1.Text = $"Accuracy: {accuracy:F0}%";
                        imageAccuracies[currentImageIndex] = accuracy;
                    }
                    else
                    {
                        accuracyLabel1.Text = "Accuracy: 0%";
                        imageAccuracies[currentImageIndex] = 0;
                    }
                }
                else if (currentLevel == "Segmentation")
                {
                    if (imagePolygons.ContainsKey(currentImageIndex) &&
                        imagePolygons[currentImageIndex] != null &&
                        imagePolygons[currentImageIndex].Count > 0 &&
                        groundTruthPolygons.ContainsKey(currentImageIndex))
                    {
                        var userPolygon = imagePolygons[currentImageIndex][0];
                        var groundTruthPolygon = groundTruthPolygons[currentImageIndex];

                        double ioa = CalculateIoA(userPolygon.Item1, groundTruthPolygon.Item1);
                        double accuracy = ioa * 100;

                        accuracyLabel1.Text = $"Accuracy: {accuracy:F0}%";
                        imageAccuracies[currentImageIndex] = accuracy;
                    }
                    else
                    {
                        accuracyLabel1.Text = "Accuracy: 0%";
                        imageAccuracies[currentImageIndex] = 0;
                    }
                }

                // 네비게이션 버튼 상태 업데이트
                UpdateNavigationButtonState();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"정확도 계산 중 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                accuracyLabel1.Text = "Accuracy: 0%";
                imageAccuracies[currentImageIndex] = 0;
            }
        }



        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // 기존 이벤트 핸들러들 ////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void toolUndo_Click(object sender, EventArgs e)
        {
            Undo();
            
            // 손 도구가 활성화된 상태였다면 비활성화
            if (isHandToolActive)
            {
                isHandToolActive = false;
                isEditingPolygon = false;
                isEditingBoundingBox = false;
                selectedPolygonIndex = -1;
                selectedBoxIndex = -1;
                UpdateToolVisualState();
            }
        }

        private void toolRedo_Click(object sender, EventArgs e)
        {
            Redo();
            
            // 손 도구가 활성화된 상태였다면 비활성화
            if (isHandToolActive)
            {
                isHandToolActive = false;
                isEditingPolygon = false;
                isEditingBoundingBox = false;
                selectedPolygonIndex = -1;
                selectedBoxIndex = -1;
                UpdateToolVisualState();
            }
        }

        private void toolDelete_Click(object sender, EventArgs e)
        {
            try
            {

                if (currentLevel == "Bounding Box")
                {
                    // 기존 바운딩 박스가 있는지 확인
                    if (imageBoundingBoxes.ContainsKey(currentImageIndex) &&
                        imageBoundingBoxes[currentImageIndex].Count > 0)
                    {
                        // 이전 상태 저장 (히스토리용)
                        List<Tuple<Rectangle, string>> previousBoxes =
                            new List<Tuple<Rectangle, string>>(imageBoundingBoxes[currentImageIndex]);

                        // 바운딩 박스 데이터 삭제
                        imageBoundingBoxes[currentImageIndex].Clear();
                        // class2 라벨 지우기
                        class3.Text = "";
                        
                        // classBtn 가시성 업데이트
                        UpdateClassButtonVisibility();

                        // 히스토리에 작업 추가
                        var action = new ActionState
                        {
                            Type = ActionState.ActionType.BoundingBox,
                            ImageIndex = currentImageIndex,
                            BeforeState = previousBoxes,
                            AfterState = null
                        };
                        AddToHistory(action);

                        // 정확도 초기화
                        if (imageAccuracies.ContainsKey(currentImageIndex))
                            imageAccuracies[currentImageIndex] = 0;

                        accuracyLabel1.Text = "Accuracy: 0%";

                        // 화면 갱신
                        pictureBoxImage.Invalidate();

                        // 네비게이션 버튼 상태 업데이트
                        UpdateNavigationButtonState();
                    }
                }
                else if (currentLevel == "Segmentation")
                {
                    // 기존 폴리곤이 있는지 확인
                    if (imagePolygons.ContainsKey(currentImageIndex) &&
                        imagePolygons[currentImageIndex].Count > 0)
                    {
                        // 이전 상태 저장 (히스토리용)
                        List<Tuple<List<Point>, string>> previousPolygons =
                            new List<Tuple<List<Point>, string>>(imagePolygons[currentImageIndex]);

                        // 폴리곤 데이터 삭제
                        imagePolygons[currentImageIndex].Clear();

                        // 편집 모드일 경우 편집 상태 초기화
                        if (isEditingPolygon)
                        {
                            isEditingPolygon = false;
                            selectedPolygonIndex = -1;
                            editingPolygonPoints.Clear();
                        }

                        // 폴리곤 그리기 중이었다면 초기화
                        polygonPoints.Clear();

                        // class2 라벨 지우기
                        class3.Text = "";
                        
                        // classBtn 가시성 업데이트
                        UpdateClassButtonVisibility();

                        // 히스토리에 작업 추가
                        var action = new ActionState
                        {
                            Type = ActionState.ActionType.Polygon,
                            ImageIndex = currentImageIndex,
                            BeforeState = previousPolygons,
                            AfterState = null
                        };
                        AddToHistory(action);

                        // 정확도 초기화
                        if (imageAccuracies.ContainsKey(currentImageIndex))
                            imageAccuracies[currentImageIndex] = 0;

                        accuracyLabel1.Text = "Accuracy: 0%";

                        // 화면 갱신
                        pictureBoxImage.Invalidate();

                        // 네비게이션 버튼 상태 업데이트
                        UpdateNavigationButtonState();
                    }
                }
                else if (currentLevel == "Classification") 
                {
                    // 분류 라벨 삭제
                    if (imageClassifications.ContainsKey(currentImageIndex))
                    {
                        imageClassifications.Remove(currentImageIndex);
                        class3.Text = "";
                        
                        // classBtn 가시성 업데이트
                        UpdateClassButtonVisibility();
                        
                        // 정확도 초기화
                        if (imageAccuracies.ContainsKey(currentImageIndex))
                            imageAccuracies[currentImageIndex] = 0;
                        
                        // 화면 갱신
                        pictureBoxImage.Invalidate();
                        
                        // 네비게이션 버튼 상태 업데이트
                        UpdateNavigationButtonState();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"라벨을 지우는 중 오류가 발생했습니다: {ex.Message}",
                               "오류",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
            }
        }
        private void toolVisible_Click(object sender, EventArgs e)
        {
            try
            {
                // 바운딩 박스 또는 세그멘테이션 가시성 전환
                if (currentLevel == "Bounding Box")
                {
                    // 바운딩 박스 가시성 토글
                    isBoundingBoxVisible = !isBoundingBoxVisible;
                    // 시각적 피드백 제공
                    toolVisible.Image = isBoundingBoxVisible ? Properties.Resources.tool_visible_basic : Properties.Resources.toolVClick;
                    toolVisible.Text = isBoundingBoxVisible ? "라벨 숨기기" : "라벨 표시하기";
                }
                else if (currentLevel == "Segmentation")
                {
                    // 세그멘테이션 가시성 토글
                    isSegmentationVisible = !isSegmentationVisible;
                    // 시각적 피드백 제공
                    toolVisible.Image = isSegmentationVisible ? Properties.Resources.tool_visible_basic : Properties.Resources.toolVClick;
                    toolVisible.Text = isSegmentationVisible ? "라벨 숨기기" : "라벨 표시하기";
                }
                else if (currentLevel == "Classification")
                {
                    // Classification 모드에서는 항상 라벨이 보이도록 설정
                    isBoundingBoxVisible = true;
                    isSegmentationVisible = true;

                    // 버튼을 활성화된 상태로 유지
                    toolVisible.Image = Properties.Resources.tool_visible_basic;
                    toolVisible.Text = "라벨 숨기기";

                    // 메시지 표시 (선택 사항)
                    MessageBox.Show("분류 모드에서는 라벨이 항상 표시됩니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // 이미지 갱신
                pictureBoxImage.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"라벨 가시성을 변경하는 중 오류가 발생했습니다: {ex.Message}",
                               "오류",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
            }
        }

        private void ZoomOutBtn_Click(object sender, EventArgs e)
        {
            ApplyZoom(-0.1f);
        }

        private void ZoomInBtn_Click(object sender, EventArgs e)
        {
            ApplyZoom(0.1f);
        }

        // 툴팁 패널 초기화 메서드
        private void InitializeTooltipPanel()
        {
            // 기존 텍스트 툴팁 패널 생성
            tooltipPanel = new Guna.UI2.WinForms.Guna2Panel();
            tooltipPanel.BackColor = System.Drawing.Color.FromArgb(50, 50, 50);
            tooltipPanel.FillColor = System.Drawing.Color.FromArgb(50, 50, 50);
            tooltipPanel.BorderRadius = 10;
            tooltipPanel.Padding = new Padding(10);
            tooltipPanel.Visible = false;
            tooltipPanel.AutoSize = true;
            tooltipPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tooltipPanel.MinimumSize = new Size(150, 0);
            tooltipPanel.MaximumSize = new Size(500, 0);
            
            // 툴팁 레이블 생성
            tooltipLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            tooltipLabel.ForeColor = System.Drawing.Color.White;
            tooltipLabel.Font = new System.Drawing.Font("Noto Sans KR", 13F);
            tooltipLabel.AutoSize = true;
            tooltipLabel.MaximumSize = new Size(2000, 0);
            tooltipLabel.TextAlignment = ContentAlignment.MiddleLeft;
            
            // 패널에 레이블 추가
            tooltipPanel.Controls.Add(tooltipLabel);
            
            // 부모 컨트롤에 툴팁 패널 추가
            this.Controls.Add(tooltipPanel);
            tooltipPanel.BringToFront();
            
            // 툴팁 타이머 초기화
            tooltipTimer = new System.Windows.Forms.Timer();
            tooltipTimer.Interval = 50;
            tooltipTimer.Tick += TooltipTimer_Tick;

            // 부모 컨트롤의 MouseMove 이벤트 캡처하여 툴팁 위치 업데이트
            this.MouseMove += (s, e) => 
            {
                if (tooltipPanel.Visible)
                {
                    Point mousePos = this.PointToClient(Cursor.Position);
                    int xOffset = -tooltipPanel.Width - 10;
                    int yOffset = -(tooltipPanel.Height / 2);
                    
                    if (mousePos.X + xOffset < 0)
                    {
                        xOffset = 10;
                    }
                    
                    tooltipPanel.Location = new Point(mousePos.X + xOffset, mousePos.Y + yOffset);
                }
            };
        }
        
        // 툴팁 타이머 이벤트 핸들러
        private void TooltipTimer_Tick(object sender, EventArgs e)
        {
            tooltipTimer.Stop();
            tooltipPanel.Visible = true;
        }
        private void InitializeToastPanel()
        {
            // 토스트 패널 생성
            toastPanel = toastPopupPanel;

            // 토스트 타이머 초기화
            toastTimer = new System.Windows.Forms.Timer();
            toastTimer.Interval = 3000;
            toastTimer.Tick += (s,e) => {
                toastTimer.Stop();
                toastPanel.Visible = false;
            };
            
        }

        // 토스트 메시지 표시 메서드
        private void ShowToast(bool isSuccess)
        {
            // 기존 타이머 중지
            toastTimer.Stop();
            // 메시지 설정
            toastPanel.BackgroundImage = isSuccess ? Properties.Resources.toastPopupS : Properties.Resources.toastPopupF;
            // 패널 표시
            toastPanel.Visible = true;
            // 타이머 시작
            toastTimer.Start();
        }
        
        // 툴팁 이벤트 등록
        private void RegisterTooltipEvents()
        {
            // 각 도구 버튼에 툴팁 이벤트 등록
            RegisterTooltip(toolHand, "손 도구를 활성화하여 이미지를 드래그하거나 폴리곤 꼭지점을 편집할 수 있습니다.");
            RegisterTooltip(toolLabelingSquare, "사각형 도구를 활성화하여 객체 주위에 바운딩 박스를 그릴 수 있습니다.");
            RegisterTooltip(toolLabelingPolygon, "폴리곤 도구를 활성화하여 객체의 세밀한 윤곽을 그릴 수 있습니다.");
            RegisterTooltip(toolDelete, "현재 이미지의 모든 라벨을 지웁니다.");
            RegisterTooltip(toolUndo, "마지막 작업을 취소합니다.");
            RegisterTooltip(toolRedo, "취소된 작업을 다시 실행합니다.");
            RegisterTooltip(toolVisible, "라벨을 표시하거나 숨깁니다.");
            RegisterTooltip(ZoomInBtn, "이미지를 확대합니다.");
            RegisterTooltip(ZoomOutBtn, "이미지를 축소합니다.");
            RegisterTooltip(nextBtn, "다음 이미지로 이동합니다.");
            RegisterTooltip(preBtn, "이전 이미지로 이동합니다.");
            RegisterTooltip(questClassificationPanel, "이미지를 클릭하여 class를 분류합니다.");
            RegisterTooltip(questBoxPanel, "바운딩 박스를 통해 객체를 라벨링합니다.");
            RegisterTooltip(questSegPanel, "폴리곤을 통해 객체의 세밀한 윤곽을 라벨링합니다.");
            RegisterTooltip(nextBtnVisible, "라벨링 정확도가 90% 이상인 경우 활성화됩니다");
        }
        
        // 컨트롤에 툴팁 이벤트 등록
        private void RegisterTooltip(Control control, string tooltipText)
        {
            if (control != null)
            {
                control.MouseEnter += (s, e) => 
                {
                    // 일반 툴팁을 보여줄 컨트롤인지 특수 패널을 보여줄 컨트롤인지 확인
                    bool isSpecialControl = (control == toolHand || control == toolLabelingPolygon || control == toolLabelingSquare);
                    
                    if (!isSpecialControl)
                    {
                        tooltipLabel.Text = tooltipText;
                        Point mousePos = this.PointToClient(Cursor.Position);
                        int xOffset = 0;
                        int yOffset = 0;
                        
                        // 툴팁 위치 계산
                        if (control == questClassificationPanel || control == questBoxPanel || control == questSegPanel)
                        {
                            xOffset = 20; // 마우스 우측에 표시
                            yOffset = -(tooltipPanel.Height / 2);
                        }
                        else
                        {
                            xOffset = -tooltipPanel.Width - 10; // 마우스 좌측에 표시
                            yOffset = -(tooltipPanel.Height / 2);
                        }
                        
                        // 화면 경계 확인
                        if (mousePos.X + xOffset < 0)
                        {
                            xOffset = 10;
                        }
                        
                        // 위치 설정 및 타이머 시작
                        tooltipPanel.Location = new Point(mousePos.X + xOffset, mousePos.Y + yOffset);
                        tooltipTimer.Start();
                    }
                    else
                    {
                        // 특수 컨트롤에 대한 처리
                        if (control == toolHand)
                        {
                            toolHandPanel.Visible = true;
                        }
                        else if (control == toolLabelingPolygon)
                        {
                            toolLabelingPolygonPanel.Visible = true;
                        }
                        else if (control == toolLabelingSquare)
                        {
                            toolLabelingSquarePanel.Visible = true;
                        }
                    }
                };
                
                // MouseLeave 이벤트는 그대로 유지
                control.MouseLeave += (s, e) => 
                {
                    tooltipTimer.Stop();
                    tooltipPanel.Visible = false;
                    toolHandPanel.Visible = false;
                    toolLabelingPolygonPanel.Visible = false;
                    toolLabelingSquarePanel.Visible = false;
                };
            }
        }

        /// <summary>
        /// 특정 위치에 있는 바운딩 박스 핸들의 인덱스 반환
        /// </summary>
        private int GetHandleAtPosition(Point point)
        {
            if (editingRect.IsEmpty) return -1;

            // 좌상단 핸들 (0)
            Rectangle handleRect = new Rectangle(
                editingRect.X - HandleSize / 2,
                editingRect.Y - HandleSize / 2,
                HandleSize, HandleSize);
            if (handleRect.Contains(point)) return 0;

            // 우상단 핸들 (1)
            handleRect = new Rectangle(
                editingRect.Right - HandleSize / 2,
                editingRect.Y - HandleSize / 2,
                HandleSize, HandleSize);
            if (handleRect.Contains(point)) return 1;

            // 좌하단 핸들 (2)
            handleRect = new Rectangle(
                editingRect.X - HandleSize / 2,
                editingRect.Bottom - HandleSize / 2,
                HandleSize, HandleSize);
            if (handleRect.Contains(point)) return 2;

            // 우하단 핸들 (3)
            handleRect = new Rectangle(
                editingRect.Right - HandleSize / 2,
                editingRect.Bottom - HandleSize / 2,
                HandleSize, HandleSize);
            if (handleRect.Contains(point)) return 3;

            return -1;
        }

        /// <summary>
        /// 핸들 인덱스에 따른 커서 반환
        /// </summary>
        private Cursor GetResizeCursor(int handleIndex)
        {
            switch (handleIndex)
            {
                case 0: // 좌상단
                    return Cursors.SizeNWSE;
                case 1: // 우상단
                    return Cursors.SizeNESW;
                case 2: // 좌하단
                    return Cursors.SizeNESW;
                case 3: // 우하단
                    return Cursors.SizeNWSE;
                case 4: // 중앙(이동)
                    return Cursors.SizeAll;
                default:
                    return Cursors.Default;
            }
        }

        /// <summary>
        /// 마우스 위치에 따라 바운딩 박스 업데이트
        /// </summary>
        private void UpdateEditingRect(Point mousePos, int handleIndex)
        {
            switch (handleIndex)
            {
                case 0: // 좌상단
                    editingRect = new Rectangle(
                        mousePos.X,
                        mousePos.Y,
                        editingRect.Right - mousePos.X,
                        editingRect.Bottom - mousePos.Y);
                    break;
                case 1: // 우상단
                    editingRect = new Rectangle(
                        editingRect.X,
                        mousePos.Y,
                        mousePos.X - editingRect.X,
                        editingRect.Bottom - mousePos.Y);
                    break;
                case 2: // 좌하단
                    editingRect = new Rectangle(
                        mousePos.X,
                        editingRect.Y,
                        editingRect.Right - mousePos.X,
                        mousePos.Y - editingRect.Y);
                    break;
                case 3: // 우하단
                    editingRect = new Rectangle(
                        editingRect.X,
                        editingRect.Y,
                        mousePos.X - editingRect.X,
                        mousePos.Y - editingRect.Y);
                    break;
                case 4: // 중앙(이동)
                    int width = editingRect.Width;
                    int height = editingRect.Height;
                    editingRect = new Rectangle(
                        mousePos.X - width / 2,
                        mousePos.Y - height / 2,
                        width,
                        height);
                    break;
            }

            // 음수 크기 처리
            if (editingRect.Width < 0)
            {
                editingRect = new Rectangle(
                    editingRect.Right,
                    editingRect.Y,
                    -editingRect.Width,
                    editingRect.Height);
            }
            if (editingRect.Height < 0)
            {
                editingRect = new Rectangle(
                    editingRect.X,
                    editingRect.Bottom,
                    editingRect.Width,
                    -editingRect.Height);
            }

            // 최소 크기 제한
            if (editingRect.Width < 10) editingRect.Width = 10;
            if (editingRect.Height < 10) editingRect.Height = 10;
        }

        // 버튼 클릭 이벤트 - 튜토리얼 블록 코딩으로 넘어가기 전 다이얼로그를 띄움
		private void guna2Panel1_Click(object sender, EventArgs e)
		{
            showDialog(new DialogConfirmGoTutorialBlockCode());
		}

		// 블록 코딩으로 넘어가는 다이얼로그를 띄우는 메서드
		public void showDialog(Form dialog)
		{
			dialog.Owner = mainView as Form;
            dialog.ShowDialog();
		}

        /// <summary>
        /// class2.Text 값에 따라 classBtn 가시성을 업데이트하는 메서드
        /// </summary>
        private void UpdateClassButtonVisibility()
        {
            // class2에 텍스트가 있으면 버튼 표시, 없으면 숨김
            classBtn.Visible = !string.IsNullOrEmpty(class3.Text);
        }
    }
}
