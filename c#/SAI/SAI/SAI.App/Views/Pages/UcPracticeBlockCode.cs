using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using SAI.SAI.App.Views.Interfaces;
using SAI.SAI.App.Views.Common;


namespace SAI.SAI.App.Views.Pages
{
    public partial class UcPracticeBlockCode : UserControl
    {
        public event EventHandler HomeButtonClicked;

        private bool isInferPanelVisible = false;
        private int inferPanelWidth = 420; 
        private int originalCodePanelWidth;
        private int originalCodePanelLeft;
        private bool isMemoPanelVisible = false;

        private double currentThreshold = 0.5; // 기본값 0.5
        public UcPracticeBlockCode()
        {
            InitializeComponent();

            ibtnHome.Click += (s, e) => HomeButtonClicked?.Invoke(this, EventArgs.Empty);

            ibtnHome.BackColor = Color.Transparent;
            ibtnDone.BackColor = Color.Transparent;
            ibtnInfer.BackColor = Color.Transparent;    
            ibtnMemo.BackColor = Color.Transparent;

            // 초기에는 숨기길 패널들
            pSideInfer.Visible = false;
            ibtnCloseInfer.Visible = false;
            pMemo.Visible = false;
            pboxInferAccuracy.Visible = false;
            // 추론사이드패널에서 '이미지 불러오기' 버튼 누르고 'pboxInferAccuracy'에 이미지 띄우고
            // pboxInferAccuracy.Visible = true 해주시면 됩니다.

            // 코드 패널의 초기 위치와 크기 저장
            originalCodePanelWidth = pCode.Width;
            originalCodePanelLeft = pCode.Left;

            tboxMemo.FillColor = ColorTranslator.FromHtml("#F7FFB8");
            tboxMemo.BorderColor = ColorTranslator.FromHtml("#F7FFB8");
            tboxMemo.FocusedState.BorderColor = ColorTranslator.FromHtml("#F7FFB8");
            tboxMemo.HoverState.BorderColor = ColorTranslator.FromHtml("#F7FFB8");
            tboxMemo.DisabledState.BorderColor = ColorTranslator.FromHtml("#F7FFB8");
            SetupThresholdControls();
            ScrollUtils.AdjustPanelScroll(pSideInfer);

        }
        private void UcPraticeBlockCode_Load(object sender, EventArgs e)
        {
            // 추론 패널 초기화
            pSideInfer.Width = inferPanelWidth;
            pSideInfer.Left = pCode.Right - inferPanelWidth;
            pSideInfer.Top = pCode.Top;
            pSideInfer.Height = pCode.Height;
        }
        private void ShowpSIdeInfer()
        {
            pSideInfer.Visible = true;
            ibtnCloseInfer.Visible = true;
            isInferPanelVisible = true;
        }

        private void HidepSideInfer()
        {
            pSideInfer.Visible = false;
            ibtnCloseInfer.Visible = false;
            isInferPanelVisible = false;
        }

        private void SetupThresholdControls()
        {
            // TrackBar 설정 (0.01 ~ 1.00, 100단계로 나누어 0.01 단위로 조절)
            tbarThreshold.Minimum = 1;  // 0.01
            tbarThreshold.Maximum = 100; // 1.00
            tbarThreshold.Value = 50;   // 0.50 (기본값)

            // TrackBar 값 변경 이벤트 처리
            tbarThreshold.ValueChanged += TbarThreshold_ValueChanged;

            // 텍스트박스 설정
            tboxThreshold.Text = "0.50";
            tboxThreshold.TextAlign = HorizontalAlignment.Center;
            tboxThreshold.KeyPress += TboxThreshold_KeyPress;
            tboxThreshold.Leave += TboxThreshold_Leave;

            // 현재 임계값 설정
            currentThreshold = 0.5;
        }

        private void TbarThreshold_ValueChanged(object sender, EventArgs e)
        {
            // TrackBar 값을 0.01 ~ 1.00 범위로 변환
            currentThreshold = tbarThreshold.Value / 100.0;

            // 텍스트박스에 값 표시 (소수점 둘째자리까지)
            tboxThreshold.Text = currentThreshold.ToString("0.00");

            // 여기에 임계값이 변경되었을 때 수행할 작업 추가
            // 예: UpdateModelThreshold(currentThreshold);
        }

        private void TboxThreshold_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 숫자, 백스페이스, 소수점만 입력 허용
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
                return;
            }

            // 소수점은 한 번만 입력 가능
            if (e.KeyChar == '.' && tboxThreshold.Text.Contains("."))
            {
                e.Handled = true;
                return;
            }

            // Enter 키 처리
            if (e.KeyChar == (char)Keys.Enter)
            {
                UpdateThresholdFromTextBox();
                e.Handled = true;
            }
        }

        private void TboxThreshold_Leave(object sender, EventArgs e)
        {
            UpdateThresholdFromTextBox();
        }

        private void UpdateThresholdFromTextBox()
        {
            if (double.TryParse(tboxThreshold.Text, out double value))
            {
                // 값의 범위 제한 (0.01 ~ 1.00)
                value = Math.Max(0.01, Math.Min(1.00, value));

                // 값 설정
                currentThreshold = value;
                tboxThreshold.Text = value.ToString("0.00");

                // TrackBar 값 업데이트 (중복 이벤트 방지를 위해 이벤트 핸들러 일시 제거)
                tbarThreshold.ValueChanged -= TbarThreshold_ValueChanged;
                tbarThreshold.Value = (int)(value * 100);
                tbarThreshold.ValueChanged += TbarThreshold_ValueChanged;

                // 여기에 임계값이 변경되었을 때 수행할 작업 추가
                // 예: UpdateModelThreshold(currentThreshold);
            }
            else
            {
                // 잘못된 입력일 경우 현재 값으로 복원
                tboxThreshold.Text = currentThreshold.ToString("0.00");
            }
        }

        private void leftPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void baseFramePanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void leftPanel_Paint_1(object sender, PaintEventArgs e)
        {
            
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void pnl_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {

        }

        private void ibtnHome_Click(object sender, EventArgs e)
        {
            HomeButtonClicked?.Invoke(this, EventArgs.Empty); // Presenter에게 알림
        }

        private void ibtnInfer_Click(object sender, EventArgs e)
        {
            if (!isInferPanelVisible)
            {
                ShowpSIdeInfer();
            }
            else
            {
                HidepSideInfer();
            }
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void pLeft_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2ShadowPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2HtmlToolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void pCenter_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ibtnPlusBlock_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnTrashBlock_Click(object sender, EventArgs e)
        {

        }

        private void pTopCenter_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pTopRight_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pBlockList_Paint(object sender, PaintEventArgs e)
        {
        }

        private void ibtnCopy_Click(object sender, EventArgs e)
        {

        }

        private void pTopCode_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void webView21_Click(object sender, EventArgs e)
        {

        }

        private void pCode_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void webView21_Click_1(object sender, EventArgs e)
        {

        }

        private void webBrowserBlock_LoadingStateChanged(object sender, CefSharp.LoadingStateChangedEventArgs e)
        {

        }

        private void pInferContent_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ibtnCloseInfer_Click(object sender, EventArgs e)
        {
            HidepSideInfer();
        }

        private void ibtnMemo_Click(object sender, EventArgs e)
        {
            isMemoPanelVisible = !isMemoPanelVisible;
            pMemo.Visible = isMemoPanelVisible;
        }

        private void lblThreshold_Click(object sender, EventArgs e)
        {

        }

        private void ibtnCloseMemo_Click(object sender, EventArgs e)
        {
            isMemoPanelVisible = !isMemoPanelVisible;
            pMemo.Visible = isMemoPanelVisible;
        }
    }
}
