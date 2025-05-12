using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using SAI.SAI.App.Presenters;
using SAI.SAI.App.Views.Interfaces;

namespace SAI.SAI.App.Views.Pages
{
    public partial class UcPracticeBlockCode : UserControl, IUcShowDialogView
	{
		private UcShowDialogPresenter ucShowDialogPresenter;
		private readonly IMainView mainView;

		public event EventHandler HomeButtonClicked;

        private bool isInferPanelVisible = false;
        private int inferPanelWidth = 420; 
        private int originalCodePanelWidth;
        private int originalCodePanelLeft;
        private bool isMemoPanelVisible = false;
        public UcPracticeBlockCode(IMainView view)
        {
            InitializeComponent();

			this.mainView = view;
			ucShowDialogPresenter = new UcShowDialogPresenter(this);

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

		private void ibtnDone_Click(object sender, EventArgs e)
		{
			ucShowDialogPresenter.clickFinish();
		}

		public void showDialog(Form dialog)
		{
			dialog.Owner = mainView as Form;
			dialog.ShowDialog();
		}
	}
}
