using System;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using SAI.SAI.App.Forms.Dialogs;
using SAI.SAI.App.Views.Common;
using SAI.SAI.App.Presenters;
using SAI.SAI.App.Views.Interfaces;

namespace SAI.SAI.App.Views.Pages
{
    public partial class UcTutorialBlockCode : UserControl, IUcShowDialogView
	{
		private UcShowDialogPresenter ucShowDialogPresenter;
		private readonly IMainView mainView;

		public event EventHandler HomeButtonClicked;
        private TodoManager todoManager;
        // todo 위에서부터 index 0~2번 입니다.
        // 로직 완료되면 todoManager.SetTodoStatus(1, false); 하면
        // 자동으로 pboxTodo1 이 Visible = false되고, pboxTodo1Done.Visible = true 됩니다.

        private bool isInferPanelVisible = false;
        private double currentThreshold = 0.5;
        private bool isMemoPanelVisible = false;
        public UcTutorialBlockCode(IMainView view)
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
            pboxInferAccuracy.Visible = false;
            pMemo.Visible = false;

            SetupThresholdControls();
            MemoUtils.ApplyStyle(tboxMemo);
        }
        private void UcTutorialBlockCode_Load(object sender, EventArgs e)
        {
            todoManager = new TodoManager(
                    (pboxTodo0, pboxTodo0Done),
                    (pboxTodo1, pboxTodo1Done),
                    (pboxTodo2, pboxTodo2Done)
                );

            // 초기 상태 모두 진행 중으로 설정
            todoManager.SetAllStatus(new bool[] { true, true, true });
        }

        private void SetupThresholdControls()
        {
            ThresholdUtils.Setup(tbarThreshold, tboxThreshold, (newValue) =>
            {
                currentThreshold = newValue;
            });
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

        // 다른 클래스에서도 사용 가능
        public void MarkTodoAsDone(int index)
        {
            todoManager.SetTodoStatus(index, false); // false면 완료 상태
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

        private void guna2ImageButton1_Click_1(object sender, EventArgs e)
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

        private void ibtnDone_Click(object sender, EventArgs e)
        {
			ucShowDialogPresenter.clickGoTrain();
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

        private void ibtnCloseMemo_Click(object sender, EventArgs e)
        {
            isMemoPanelVisible = !isMemoPanelVisible;
            pMemo.Visible = isMemoPanelVisible;
        }

        private void ibtnGoNotion_Click(object sender, EventArgs e)
        {
            using (var dialog = new DialogNotion())
            {
                dialog.ShowDialog();
            }
        }

		public void showDialog(Form dialog)
		{
			dialog.Owner = mainView as Form;
			dialog.ShowDialog();
		}
	}
}
