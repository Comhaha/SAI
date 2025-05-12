
namespace SAI.SAI.App.Views.Pages
{
    partial class UcPracticeBlockCode
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcPracticeBlockCode));
            this.pMain = new Guna.UI2.WinForms.Guna2Panel();
            this.pCode = new Guna.UI2.WinForms.Guna2Panel();
            this.ibtnCloseInfer = new Guna.UI2.WinForms.Guna2ImageButton();
            this.webViewCode = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.ibtnCopy = new Guna.UI2.WinForms.Guna2ImageButton();
            this.pTopCode = new System.Windows.Forms.Panel();
            this.pZoomCode = new Guna.UI2.WinForms.Guna2Panel();
            this.tboxZoomCode = new Guna.UI2.WinForms.Guna2TextBox();
            this.ibtnMinusCode = new Guna.UI2.WinForms.Guna2ImageButton();
            this.ibtnPlusCode = new Guna.UI2.WinForms.Guna2ImageButton();
            this.pBlock = new Guna.UI2.WinForms.Guna2Panel();
            this.webViewBlock = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.pTopBlock = new System.Windows.Forms.Panel();
            this.btnRunModel = new Guna.UI2.WinForms.Guna2ImageButton();
            this.btnPreBlock = new Guna.UI2.WinForms.Guna2ImageButton();
            this.btnTrashBlock = new Guna.UI2.WinForms.Guna2ImageButton();
            this.btnNextBlock = new Guna.UI2.WinForms.Guna2ImageButton();
            this.pZoomBlock = new Guna.UI2.WinForms.Guna2Panel();
            this.tboxZoomBlock = new Guna.UI2.WinForms.Guna2TextBox();
            this.ibtnMinusBlock = new Guna.UI2.WinForms.Guna2ImageButton();
            this.ibtnPlusBlock = new Guna.UI2.WinForms.Guna2ImageButton();
            this.pBlockList = new Guna.UI2.WinForms.Guna2Panel();
            this.ibtnMemo = new Guna.UI2.WinForms.Guna2ImageButton();
            this.ibtnDone = new Guna.UI2.WinForms.Guna2ImageButton();
            this.ibtnInfer = new Guna.UI2.WinForms.Guna2ImageButton();
            this.ibtnHome = new Guna.UI2.WinForms.Guna2ImageButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.pSideInfer = new Guna.UI2.WinForms.Guna2Panel();
            this.pThreshold = new Guna.UI2.WinForms.Guna2Panel();
            this.tboxThreshold = new Guna.UI2.WinForms.Guna2TextBox();
            this.tbarThreshold = new Guna.UI2.WinForms.Guna2TrackBar();
            this.ibtnTest = new Guna.UI2.WinForms.Guna2ImageButton();
            this.ibtnDownloadAIModel = new Guna.UI2.WinForms.Guna2ImageButton();
            this.ibtnGoNotion = new Guna.UI2.WinForms.Guna2ImageButton();
            this.ibtnAiFeedback = new Guna.UI2.WinForms.Guna2ImageButton();
            this.ibtnInfoGraph = new Guna.UI2.WinForms.Guna2ImageButton();
            this.ibtnInfoThreshold = new Guna.UI2.WinForms.Guna2ImageButton();
            this.pInferAccuracy = new Guna.UI2.WinForms.Guna2Panel();
            this.ibtnSelectInferImage = new Guna.UI2.WinForms.Guna2ImageButton();
            this.pboxInferAccuracy = new Guna.UI2.WinForms.Guna2PictureBox();
            this.lblInferGraph = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblThreshold = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblInfer = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.pMemo = new Guna.UI2.WinForms.Guna2Panel();
            this.tboxMemo = new Guna.UI2.WinForms.Guna2TextBox();
            this.ibtnCloseMemo = new Guna.UI2.WinForms.Guna2ImageButton();
            this.ibtnQuestionMemo = new Guna.UI2.WinForms.Guna2ImageButton();
            this.pMain.SuspendLayout();
            this.pCode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webViewCode)).BeginInit();
            this.pZoomCode.SuspendLayout();
            this.pBlock.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webViewBlock)).BeginInit();
            this.pTopBlock.SuspendLayout();
            this.pZoomBlock.SuspendLayout();
            this.pSideInfer.SuspendLayout();
            this.pThreshold.SuspendLayout();
            this.pInferAccuracy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxInferAccuracy)).BeginInit();
            this.pMemo.SuspendLayout();
            this.SuspendLayout();
            // 
            // pMain
            // 
            this.pMain.BackColor = System.Drawing.Color.Transparent;
            this.pMain.BackgroundImage = global::SAI.Properties.Resources.img_frame_shadow;
            resources.ApplyResources(this.pMain, "pMain");
            this.pMain.BorderRadius = 32;
            this.pMain.BorderThickness = 1;
            this.pMain.Controls.Add(this.pCode);
            this.pMain.Controls.Add(this.pBlock);
            this.pMain.Controls.Add(this.pBlockList);
            this.pMain.FillColor = System.Drawing.Color.Transparent;
            this.pMain.ForeColor = System.Drawing.Color.Transparent;
            this.pMain.Name = "pMain";
            this.pMain.ShadowDecoration.BorderRadius = 32;
            this.pMain.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 0, 6, 6);
            this.pMain.Paint += new System.Windows.Forms.PaintEventHandler(this.pMain_Paint);
            // 
            // pCode
            // 
            this.pCode.BackgroundImage = global::SAI.Properties.Resources.p_block;
            resources.ApplyResources(this.pCode, "pCode");
            this.pCode.Controls.Add(this.ibtnCloseInfer);
            this.pCode.Controls.Add(this.webViewCode);
            this.pCode.Controls.Add(this.ibtnCopy);
            this.pCode.Controls.Add(this.pTopCode);
            this.pCode.Controls.Add(this.pZoomCode);
            this.pCode.Name = "pCode";
            // 
            // ibtnCloseInfer
            // 
            this.ibtnCloseInfer.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnCloseInfer.HoverState.Image = global::SAI.Properties.Resources.btn_close_infer_hover;
            this.ibtnCloseInfer.HoverState.ImageSize = new System.Drawing.Size(38, 153);
            this.ibtnCloseInfer.Image = global::SAI.Properties.Resources.btn_close_infer;
            this.ibtnCloseInfer.ImageOffset = new System.Drawing.Point(0, 0);
            this.ibtnCloseInfer.ImageRotate = 0F;
            this.ibtnCloseInfer.ImageSize = new System.Drawing.Size(38, 153);
            resources.ApplyResources(this.ibtnCloseInfer, "ibtnCloseInfer");
            this.ibtnCloseInfer.Name = "ibtnCloseInfer";
            this.ibtnCloseInfer.PressedState.ImageSize = new System.Drawing.Size(38, 153);
            this.ibtnCloseInfer.Click += new System.EventHandler(this.ibtnCloseInfer_Click);
            // 
            // webViewCode
            // 
            this.webViewCode.AllowExternalDrop = true;
            this.webViewCode.CreationProperties = null;
            this.webViewCode.DefaultBackgroundColor = System.Drawing.Color.White;
            resources.ApplyResources(this.webViewCode, "webViewCode");
            this.webViewCode.Name = "webViewCode";
            this.webViewCode.ZoomFactor = 1D;
            this.webViewCode.Click += new System.EventHandler(this.webView21_Click_1);
            // 
            // ibtnCopy
            // 
            this.ibtnCopy.BackgroundImage = global::SAI.Properties.Resources.btn_copy;
            resources.ApplyResources(this.ibtnCopy, "ibtnCopy");
            this.ibtnCopy.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnCopy.HoverState.Image = global::SAI.Properties.Resources.btn_copy_hover;
            this.ibtnCopy.HoverState.ImageSize = new System.Drawing.Size(29, 29);
            this.ibtnCopy.Image = global::SAI.Properties.Resources.btn_copy;
            this.ibtnCopy.ImageOffset = new System.Drawing.Point(0, 0);
            this.ibtnCopy.ImageRotate = 0F;
            this.ibtnCopy.ImageSize = new System.Drawing.Size(29, 29);
            this.ibtnCopy.Name = "ibtnCopy";
            this.ibtnCopy.PressedState.ImageSize = new System.Drawing.Size(29, 29);
            this.ibtnCopy.Click += new System.EventHandler(this.ibtnCopy_Click);
            // 
            // pTopCode
            // 
            this.pTopCode.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.pTopCode, "pTopCode");
            this.pTopCode.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pTopCode.Name = "pTopCode";
            this.pTopCode.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // pZoomCode
            // 
            this.pZoomCode.BackgroundImage = global::SAI.Properties.Resources.btn_zoom;
            this.pZoomCode.Controls.Add(this.tboxZoomCode);
            this.pZoomCode.Controls.Add(this.ibtnMinusCode);
            this.pZoomCode.Controls.Add(this.ibtnPlusCode);
            resources.ApplyResources(this.pZoomCode, "pZoomCode");
            this.pZoomCode.Name = "pZoomCode";
            // 
            // tboxZoomCode
            // 
            this.tboxZoomCode.BorderColor = System.Drawing.Color.Black;
            this.tboxZoomCode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tboxZoomCode.DefaultText = "";
            this.tboxZoomCode.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.tboxZoomCode.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.tboxZoomCode.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tboxZoomCode.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tboxZoomCode.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            resources.ApplyResources(this.tboxZoomCode, "tboxZoomCode");
            this.tboxZoomCode.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tboxZoomCode.Name = "tboxZoomCode";
            this.tboxZoomCode.PlaceholderText = "";
            this.tboxZoomCode.SelectedText = "";
            // 
            // ibtnMinusCode
            // 
            this.ibtnMinusCode.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnMinusCode.HoverState.Image = global::SAI.Properties.Resources.btn_minus;
            this.ibtnMinusCode.HoverState.ImageSize = new System.Drawing.Size(9, 9);
            this.ibtnMinusCode.Image = global::SAI.Properties.Resources.btn_minus;
            this.ibtnMinusCode.ImageOffset = new System.Drawing.Point(0, 0);
            this.ibtnMinusCode.ImageRotate = 0F;
            this.ibtnMinusCode.ImageSize = new System.Drawing.Size(9, 9);
            resources.ApplyResources(this.ibtnMinusCode, "ibtnMinusCode");
            this.ibtnMinusCode.Name = "ibtnMinusCode";
            this.ibtnMinusCode.PressedState.ImageSize = new System.Drawing.Size(9, 9);
            this.ibtnMinusCode.UseTransparentBackground = true;
            // 
            // ibtnPlusCode
            // 
            this.ibtnPlusCode.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnPlusCode.HoverState.ImageSize = new System.Drawing.Size(9, 9);
            this.ibtnPlusCode.Image = global::SAI.Properties.Resources.btn_plus;
            this.ibtnPlusCode.ImageOffset = new System.Drawing.Point(0, 0);
            this.ibtnPlusCode.ImageRotate = 0F;
            this.ibtnPlusCode.ImageSize = new System.Drawing.Size(9, 9);
            resources.ApplyResources(this.ibtnPlusCode, "ibtnPlusCode");
            this.ibtnPlusCode.Name = "ibtnPlusCode";
            this.ibtnPlusCode.PressedState.ImageSize = new System.Drawing.Size(9, 9);
            // 
            // pBlock
            // 
            this.pBlock.BackgroundImage = global::SAI.Properties.Resources.p_block;
            resources.ApplyResources(this.pBlock, "pBlock");
            this.pBlock.Controls.Add(this.webViewBlock);
            this.pBlock.Controls.Add(this.pTopBlock);
            this.pBlock.Controls.Add(this.pZoomBlock);
            this.pBlock.Name = "pBlock";
            this.pBlock.Paint += new System.Windows.Forms.PaintEventHandler(this.guna2Panel1_Paint);
            // 
            // webViewBlock
            // 
            this.webViewBlock.AllowExternalDrop = true;
            this.webViewBlock.CreationProperties = null;
            this.webViewBlock.DefaultBackgroundColor = System.Drawing.Color.White;
            resources.ApplyResources(this.webViewBlock, "webViewBlock");
            this.webViewBlock.Name = "webViewBlock";
            this.webViewBlock.ZoomFactor = 1D;
            // 
            // pTopBlock
            // 
            this.pTopBlock.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.pTopBlock, "pTopBlock");
            this.pTopBlock.Controls.Add(this.btnRunModel);
            this.pTopBlock.Controls.Add(this.btnPreBlock);
            this.pTopBlock.Controls.Add(this.btnTrashBlock);
            this.pTopBlock.Controls.Add(this.btnNextBlock);
            this.pTopBlock.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pTopBlock.Name = "pTopBlock";
            this.pTopBlock.Paint += new System.Windows.Forms.PaintEventHandler(this.pTopCenter_Paint);
            // 
            // btnRunModel
            // 
            this.btnRunModel.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnRunModel.HoverState.ImageSize = new System.Drawing.Size(12, 14);
            this.btnRunModel.Image = global::SAI.Properties.Resources.btn_run_model;
            this.btnRunModel.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnRunModel.ImageRotate = 0F;
            this.btnRunModel.ImageSize = new System.Drawing.Size(10, 12);
            resources.ApplyResources(this.btnRunModel, "btnRunModel");
            this.btnRunModel.Name = "btnRunModel";
            this.btnRunModel.PressedState.ImageSize = new System.Drawing.Size(10, 12);
            // 
            // btnPreBlock
            // 
            this.btnPreBlock.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnPreBlock.HoverState.ImageSize = new System.Drawing.Size(17, 12);
            this.btnPreBlock.Image = global::SAI.Properties.Resources.btn_pre_block;
            this.btnPreBlock.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnPreBlock.ImageRotate = 0F;
            this.btnPreBlock.ImageSize = new System.Drawing.Size(15, 10);
            resources.ApplyResources(this.btnPreBlock, "btnPreBlock");
            this.btnPreBlock.Name = "btnPreBlock";
            this.btnPreBlock.PressedState.ImageSize = new System.Drawing.Size(15, 10);
            // 
            // btnTrashBlock
            // 
            this.btnTrashBlock.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnTrashBlock.HoverState.ImageSize = new System.Drawing.Size(15, 16);
            this.btnTrashBlock.Image = global::SAI.Properties.Resources.btn_trash_block;
            this.btnTrashBlock.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnTrashBlock.ImageRotate = 0F;
            this.btnTrashBlock.ImageSize = new System.Drawing.Size(13, 14);
            resources.ApplyResources(this.btnTrashBlock, "btnTrashBlock");
            this.btnTrashBlock.Name = "btnTrashBlock";
            this.btnTrashBlock.PressedState.ImageSize = new System.Drawing.Size(13, 14);
            this.btnTrashBlock.Click += new System.EventHandler(this.btnTrashBlock_Click);
            // 
            // btnNextBlock
            // 
            this.btnNextBlock.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnNextBlock.HoverState.ImageSize = new System.Drawing.Size(17, 12);
            this.btnNextBlock.Image = global::SAI.Properties.Resources.btn_next_block;
            this.btnNextBlock.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnNextBlock.ImageRotate = 0F;
            this.btnNextBlock.ImageSize = new System.Drawing.Size(15, 10);
            resources.ApplyResources(this.btnNextBlock, "btnNextBlock");
            this.btnNextBlock.Name = "btnNextBlock";
            this.btnNextBlock.PressedState.ImageSize = new System.Drawing.Size(15, 10);
            // 
            // pZoomBlock
            // 
            this.pZoomBlock.BackgroundImage = global::SAI.Properties.Resources.btn_zoom;
            this.pZoomBlock.Controls.Add(this.tboxZoomBlock);
            this.pZoomBlock.Controls.Add(this.ibtnMinusBlock);
            this.pZoomBlock.Controls.Add(this.ibtnPlusBlock);
            resources.ApplyResources(this.pZoomBlock, "pZoomBlock");
            this.pZoomBlock.Name = "pZoomBlock";
            this.pZoomBlock.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_Paint);
            // 
            // tboxZoomBlock
            // 
            this.tboxZoomBlock.BorderColor = System.Drawing.Color.Black;
            this.tboxZoomBlock.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tboxZoomBlock.DefaultText = "";
            this.tboxZoomBlock.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.tboxZoomBlock.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.tboxZoomBlock.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tboxZoomBlock.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tboxZoomBlock.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            resources.ApplyResources(this.tboxZoomBlock, "tboxZoomBlock");
            this.tboxZoomBlock.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tboxZoomBlock.Name = "tboxZoomBlock";
            this.tboxZoomBlock.PlaceholderText = "";
            this.tboxZoomBlock.SelectedText = "";
            // 
            // ibtnMinusBlock
            // 
            this.ibtnMinusBlock.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnMinusBlock.HoverState.Image = global::SAI.Properties.Resources.btn_minus;
            this.ibtnMinusBlock.HoverState.ImageSize = new System.Drawing.Size(11, 11);
            this.ibtnMinusBlock.Image = global::SAI.Properties.Resources.btn_minus;
            this.ibtnMinusBlock.ImageOffset = new System.Drawing.Point(0, 0);
            this.ibtnMinusBlock.ImageRotate = 0F;
            this.ibtnMinusBlock.ImageSize = new System.Drawing.Size(9, 9);
            resources.ApplyResources(this.ibtnMinusBlock, "ibtnMinusBlock");
            this.ibtnMinusBlock.Name = "ibtnMinusBlock";
            this.ibtnMinusBlock.PressedState.ImageSize = new System.Drawing.Size(9, 9);
            this.ibtnMinusBlock.UseTransparentBackground = true;
            // 
            // ibtnPlusBlock
            // 
            this.ibtnPlusBlock.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnPlusBlock.HoverState.ImageSize = new System.Drawing.Size(11, 11);
            this.ibtnPlusBlock.Image = global::SAI.Properties.Resources.btn_plus;
            this.ibtnPlusBlock.ImageOffset = new System.Drawing.Point(0, 0);
            this.ibtnPlusBlock.ImageRotate = 0F;
            this.ibtnPlusBlock.ImageSize = new System.Drawing.Size(9, 9);
            resources.ApplyResources(this.ibtnPlusBlock, "ibtnPlusBlock");
            this.ibtnPlusBlock.Name = "ibtnPlusBlock";
            this.ibtnPlusBlock.PressedState.ImageSize = new System.Drawing.Size(9, 9);
            this.ibtnPlusBlock.Click += new System.EventHandler(this.ibtnPlusBlock_Click);
            // 
            // pBlockList
            // 
            this.pBlockList.BackColor = System.Drawing.Color.Transparent;
            this.pBlockList.BackgroundImage = global::SAI.Properties.Resources.p_block;
            resources.ApplyResources(this.pBlockList, "pBlockList");
            this.pBlockList.BorderColor = System.Drawing.Color.Transparent;
            this.pBlockList.FillColor = System.Drawing.Color.Transparent;
            this.pBlockList.Name = "pBlockList";
            this.pBlockList.ShadowDecoration.BorderRadius = 32;
            this.pBlockList.ShadowDecoration.Depth = 15;
            this.pBlockList.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 0, 6, 6);
            this.pBlockList.Paint += new System.Windows.Forms.PaintEventHandler(this.pBlockList_Paint);
            // 
            // ibtnMemo
            // 
            resources.ApplyResources(this.ibtnMemo, "ibtnMemo");
            this.ibtnMemo.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnMemo.HoverState.Image = global::SAI.Properties.Resources.btn_memo;
            this.ibtnMemo.HoverState.ImageSize = new System.Drawing.Size(56, 56);
            this.ibtnMemo.Image = global::SAI.Properties.Resources.btn_memo;
            this.ibtnMemo.ImageOffset = new System.Drawing.Point(0, 0);
            this.ibtnMemo.ImageRotate = 0F;
            this.ibtnMemo.ImageSize = new System.Drawing.Size(56, 56);
            this.ibtnMemo.Name = "ibtnMemo";
            this.ibtnMemo.PressedState.ImageSize = new System.Drawing.Size(56, 56);
            this.ibtnMemo.Click += new System.EventHandler(this.ibtnMemo_Click);
            // 
            // ibtnDone
            // 
            this.ibtnDone.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnDone.HoverState.Image = global::SAI.Properties.Resources.btn_done_hover;
            this.ibtnDone.HoverState.ImageSize = new System.Drawing.Size(154, 39);
            this.ibtnDone.Image = global::SAI.Properties.Resources.btn_done;
            this.ibtnDone.ImageOffset = new System.Drawing.Point(0, 0);
            this.ibtnDone.ImageRotate = 0F;
            this.ibtnDone.ImageSize = new System.Drawing.Size(154, 39);
            resources.ApplyResources(this.ibtnDone, "ibtnDone");
            this.ibtnDone.Name = "ibtnDone";
            this.ibtnDone.PressedState.ImageSize = new System.Drawing.Size(154, 39);
            this.ibtnDone.Click += new System.EventHandler(this.ibtnDone_Click);
            // 
            // ibtnInfer
            // 
            resources.ApplyResources(this.ibtnInfer, "ibtnInfer");
            this.ibtnInfer.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnInfer.HoverState.Image = global::SAI.Properties.Resources.btn_infer_hover;
            this.ibtnInfer.HoverState.ImageSize = new System.Drawing.Size(38, 158);
            this.ibtnInfer.Image = global::SAI.Properties.Resources.btn_infer;
            this.ibtnInfer.ImageOffset = new System.Drawing.Point(0, 0);
            this.ibtnInfer.ImageRotate = 0F;
            this.ibtnInfer.ImageSize = new System.Drawing.Size(38, 158);
            this.ibtnInfer.Name = "ibtnInfer";
            this.ibtnInfer.PressedState.ImageSize = new System.Drawing.Size(38, 158);
            this.ibtnInfer.Click += new System.EventHandler(this.ibtnInfer_Click);
            // 
            // ibtnHome
            // 
            this.ibtnHome.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnHome.HoverState.Image = global::SAI.Properties.Resources.btn_home_hover;
            this.ibtnHome.HoverState.ImageSize = new System.Drawing.Size(39, 39);
            this.ibtnHome.Image = global::SAI.Properties.Resources.btn_home;
            this.ibtnHome.ImageOffset = new System.Drawing.Point(0, 0);
            this.ibtnHome.ImageRotate = 0F;
            this.ibtnHome.ImageSize = new System.Drawing.Size(39, 39);
            resources.ApplyResources(this.ibtnHome, "ibtnHome");
            this.ibtnHome.Name = "ibtnHome";
            this.ibtnHome.PressedState.ImageSize = new System.Drawing.Size(39, 39);
            this.ibtnHome.Click += new System.EventHandler(this.ibtnHome_Click);
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.Name = "lblTitle";
            // 
            // pSideInfer
            // 
            resources.ApplyResources(this.pSideInfer, "pSideInfer");
            this.pSideInfer.BackgroundImage = global::SAI.Properties.Resources.p_side_infer;
            this.pSideInfer.Controls.Add(this.pThreshold);
            this.pSideInfer.Controls.Add(this.tbarThreshold);
            this.pSideInfer.Controls.Add(this.ibtnTest);
            this.pSideInfer.Controls.Add(this.ibtnDownloadAIModel);
            this.pSideInfer.Controls.Add(this.ibtnGoNotion);
            this.pSideInfer.Controls.Add(this.ibtnAiFeedback);
            this.pSideInfer.Controls.Add(this.ibtnInfoGraph);
            this.pSideInfer.Controls.Add(this.ibtnInfoThreshold);
            this.pSideInfer.Controls.Add(this.pInferAccuracy);
            this.pSideInfer.Controls.Add(this.lblInferGraph);
            this.pSideInfer.Controls.Add(this.lblThreshold);
            this.pSideInfer.Controls.Add(this.lblInfer);
            this.pSideInfer.Name = "pSideInfer";
            this.pSideInfer.Paint += new System.Windows.Forms.PaintEventHandler(this.pInferContent_Paint);
            // 
            // pThreshold
            // 
            this.pThreshold.BackgroundImage = global::SAI.Properties.Resources.tbox_threshold;
            resources.ApplyResources(this.pThreshold, "pThreshold");
            this.pThreshold.Controls.Add(this.tboxThreshold);
            this.pThreshold.Name = "pThreshold";
            // 
            // tboxThreshold
            // 
            resources.ApplyResources(this.tboxThreshold, "tboxThreshold");
            this.tboxThreshold.BorderColor = System.Drawing.Color.Transparent;
            this.tboxThreshold.BorderThickness = 0;
            this.tboxThreshold.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tboxThreshold.DefaultText = "";
            this.tboxThreshold.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.tboxThreshold.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.tboxThreshold.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tboxThreshold.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tboxThreshold.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tboxThreshold.ForeColor = System.Drawing.Color.Black;
            this.tboxThreshold.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tboxThreshold.Name = "tboxThreshold";
            this.tboxThreshold.PlaceholderText = "";
            this.tboxThreshold.SelectedText = "";
            // 
            // tbarThreshold
            // 
            resources.ApplyResources(this.tbarThreshold, "tbarThreshold");
            this.tbarThreshold.Name = "tbarThreshold";
            this.tbarThreshold.ThumbColor = System.Drawing.Color.Gold;
            // 
            // ibtnTest
            // 
            this.ibtnTest.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnTest.HoverState.Image = global::SAI.Properties.Resources.btn_goNotion_hover;
            this.ibtnTest.HoverState.ImageSize = new System.Drawing.Size(160, 50);
            this.ibtnTest.Image = global::SAI.Properties.Resources.btn_goNotion;
            this.ibtnTest.ImageOffset = new System.Drawing.Point(0, 0);
            this.ibtnTest.ImageRotate = 0F;
            this.ibtnTest.ImageSize = new System.Drawing.Size(160, 50);
            resources.ApplyResources(this.ibtnTest, "ibtnTest");
            this.ibtnTest.Name = "ibtnTest";
            this.ibtnTest.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            // 
            // ibtnDownloadAIModel
            // 
            this.ibtnDownloadAIModel.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnDownloadAIModel.HoverState.Image = global::SAI.Properties.Resources.btn_download_aimodel_hover;
            this.ibtnDownloadAIModel.HoverState.ImageSize = new System.Drawing.Size(160, 50);
            this.ibtnDownloadAIModel.Image = global::SAI.Properties.Resources.btn_download_aimodel;
            this.ibtnDownloadAIModel.ImageOffset = new System.Drawing.Point(0, 0);
            this.ibtnDownloadAIModel.ImageRotate = 0F;
            this.ibtnDownloadAIModel.ImageSize = new System.Drawing.Size(160, 50);
            resources.ApplyResources(this.ibtnDownloadAIModel, "ibtnDownloadAIModel");
            this.ibtnDownloadAIModel.Name = "ibtnDownloadAIModel";
            this.ibtnDownloadAIModel.PressedState.ImageSize = new System.Drawing.Size(160, 50);
            // 
            // ibtnGoNotion
            // 
            this.ibtnGoNotion.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnGoNotion.HoverState.Image = global::SAI.Properties.Resources.btn_goNotion_hover;
            this.ibtnGoNotion.HoverState.ImageSize = new System.Drawing.Size(160, 50);
            this.ibtnGoNotion.Image = global::SAI.Properties.Resources.btn_goNotion;
            this.ibtnGoNotion.ImageOffset = new System.Drawing.Point(0, 0);
            this.ibtnGoNotion.ImageRotate = 0F;
            this.ibtnGoNotion.ImageSize = new System.Drawing.Size(160, 50);
            resources.ApplyResources(this.ibtnGoNotion, "ibtnGoNotion");
            this.ibtnGoNotion.Name = "ibtnGoNotion";
            this.ibtnGoNotion.PressedState.ImageSize = new System.Drawing.Size(160, 50);
            this.ibtnGoNotion.Click += new System.EventHandler(this.ibtnGoNotion_Click);
            // 
            // ibtnAiFeedback
            // 
            this.ibtnAiFeedback.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnAiFeedback.HoverState.Image = global::SAI.Properties.Resources.btn_aifeedback;
            this.ibtnAiFeedback.HoverState.ImageSize = new System.Drawing.Size(347, 90);
            this.ibtnAiFeedback.Image = global::SAI.Properties.Resources.btn_aifeedback;
            this.ibtnAiFeedback.ImageOffset = new System.Drawing.Point(0, 0);
            this.ibtnAiFeedback.ImageRotate = 0F;
            this.ibtnAiFeedback.ImageSize = new System.Drawing.Size(347, 90);
            resources.ApplyResources(this.ibtnAiFeedback, "ibtnAiFeedback");
            this.ibtnAiFeedback.Name = "ibtnAiFeedback";
            this.ibtnAiFeedback.PressedState.ImageSize = new System.Drawing.Size(347, 90);
            // 
            // ibtnInfoGraph
            // 
            this.ibtnInfoGraph.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnInfoGraph.HoverState.ImageSize = new System.Drawing.Size(17, 17);
            this.ibtnInfoGraph.Image = global::SAI.Properties.Resources.btn_info;
            this.ibtnInfoGraph.ImageOffset = new System.Drawing.Point(0, 0);
            this.ibtnInfoGraph.ImageRotate = 0F;
            this.ibtnInfoGraph.ImageSize = new System.Drawing.Size(17, 17);
            resources.ApplyResources(this.ibtnInfoGraph, "ibtnInfoGraph");
            this.ibtnInfoGraph.Name = "ibtnInfoGraph";
            this.ibtnInfoGraph.PressedState.ImageSize = new System.Drawing.Size(17, 17);
            // 
            // ibtnInfoThreshold
            // 
            this.ibtnInfoThreshold.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnInfoThreshold.HoverState.ImageSize = new System.Drawing.Size(12, 12);
            this.ibtnInfoThreshold.Image = global::SAI.Properties.Resources.btn_info;
            this.ibtnInfoThreshold.ImageOffset = new System.Drawing.Point(0, 0);
            this.ibtnInfoThreshold.ImageRotate = 0F;
            this.ibtnInfoThreshold.ImageSize = new System.Drawing.Size(12, 12);
            resources.ApplyResources(this.ibtnInfoThreshold, "ibtnInfoThreshold");
            this.ibtnInfoThreshold.Name = "ibtnInfoThreshold";
            this.ibtnInfoThreshold.PressedState.ImageSize = new System.Drawing.Size(12, 12);
            // 
            // pInferAccuracy
            // 
            this.pInferAccuracy.BackgroundImage = global::SAI.Properties.Resources.p_sideinfer_accuracy;
            resources.ApplyResources(this.pInferAccuracy, "pInferAccuracy");
            this.pInferAccuracy.Controls.Add(this.ibtnSelectInferImage);
            this.pInferAccuracy.Controls.Add(this.pboxInferAccuracy);
            this.pInferAccuracy.Name = "pInferAccuracy";
            // 
            // ibtnSelectInferImage
            // 
            this.ibtnSelectInferImage.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnSelectInferImage.HoverState.Image = global::SAI.Properties.Resources.btn_selectinferimage_hover;
            this.ibtnSelectInferImage.HoverState.ImageSize = new System.Drawing.Size(144, 36);
            this.ibtnSelectInferImage.Image = global::SAI.Properties.Resources.btn_selectinferimage;
            this.ibtnSelectInferImage.ImageOffset = new System.Drawing.Point(0, 0);
            this.ibtnSelectInferImage.ImageRotate = 0F;
            this.ibtnSelectInferImage.ImageSize = new System.Drawing.Size(144, 36);
            resources.ApplyResources(this.ibtnSelectInferImage, "ibtnSelectInferImage");
            this.ibtnSelectInferImage.Name = "ibtnSelectInferImage";
            this.ibtnSelectInferImage.PressedState.ImageSize = new System.Drawing.Size(144, 36);
            // 
            // pboxInferAccuracy
            // 
            this.pboxInferAccuracy.Image = global::SAI.Properties.Resources.img_bounding_example;
            this.pboxInferAccuracy.ImageRotate = 0F;
            resources.ApplyResources(this.pboxInferAccuracy, "pboxInferAccuracy");
            this.pboxInferAccuracy.Name = "pboxInferAccuracy";
            this.pboxInferAccuracy.TabStop = false;
            // 
            // lblInferGraph
            // 
            this.lblInferGraph.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lblInferGraph, "lblInferGraph");
            this.lblInferGraph.Name = "lblInferGraph";
            // 
            // lblThreshold
            // 
            this.lblThreshold.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lblThreshold, "lblThreshold");
            this.lblThreshold.Name = "lblThreshold";
            this.lblThreshold.Click += new System.EventHandler(this.lblThreshold_Click);
            // 
            // lblInfer
            // 
            this.lblInfer.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lblInfer, "lblInfer");
            this.lblInfer.Name = "lblInfer";
            // 
            // pMemo
            // 
            this.pMemo.BackgroundImage = global::SAI.Properties.Resources.p_memo;
            resources.ApplyResources(this.pMemo, "pMemo");
            this.pMemo.Controls.Add(this.tboxMemo);
            this.pMemo.Controls.Add(this.ibtnCloseMemo);
            this.pMemo.Controls.Add(this.ibtnQuestionMemo);
            this.pMemo.Name = "pMemo";
            // 
            // tboxMemo
            // 
            this.tboxMemo.BorderColor = System.Drawing.Color.Transparent;
            this.tboxMemo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tboxMemo.DefaultText = "";
            this.tboxMemo.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.tboxMemo.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.tboxMemo.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tboxMemo.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tboxMemo.FillColor = System.Drawing.Color.Transparent;
            this.tboxMemo.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            resources.ApplyResources(this.tboxMemo, "tboxMemo");
            this.tboxMemo.ForeColor = System.Drawing.Color.Black;
            this.tboxMemo.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tboxMemo.Name = "tboxMemo";
            this.tboxMemo.PlaceholderForeColor = System.Drawing.Color.Transparent;
            this.tboxMemo.PlaceholderText = "";
            this.tboxMemo.SelectedText = "";
            // 
            // ibtnCloseMemo
            // 
            this.ibtnCloseMemo.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnCloseMemo.HoverState.ImageSize = new System.Drawing.Size(31, 31);
            this.ibtnCloseMemo.Image = global::SAI.Properties.Resources.btn_close1;
            this.ibtnCloseMemo.ImageOffset = new System.Drawing.Point(0, 0);
            this.ibtnCloseMemo.ImageRotate = 0F;
            this.ibtnCloseMemo.ImageSize = new System.Drawing.Size(29, 29);
            resources.ApplyResources(this.ibtnCloseMemo, "ibtnCloseMemo");
            this.ibtnCloseMemo.Name = "ibtnCloseMemo";
            this.ibtnCloseMemo.PressedState.ImageSize = new System.Drawing.Size(31, 31);
            this.ibtnCloseMemo.Click += new System.EventHandler(this.ibtnCloseMemo_Click);
            // 
            // ibtnQuestionMemo
            // 
            this.ibtnQuestionMemo.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnQuestionMemo.HoverState.ImageSize = new System.Drawing.Size(31, 31);
            this.ibtnQuestionMemo.Image = global::SAI.Properties.Resources.btn_question_memo;
            this.ibtnQuestionMemo.ImageOffset = new System.Drawing.Point(0, 0);
            this.ibtnQuestionMemo.ImageRotate = 0F;
            this.ibtnQuestionMemo.ImageSize = new System.Drawing.Size(29, 29);
            resources.ApplyResources(this.ibtnQuestionMemo, "ibtnQuestionMemo");
            this.ibtnQuestionMemo.Name = "ibtnQuestionMemo";
            this.ibtnQuestionMemo.PressedState.ImageSize = new System.Drawing.Size(31, 31);
            // 
            // UcPracticeBlockCode
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImage = global::SAI.Properties.Resources.img_background;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.pMemo);
            this.Controls.Add(this.pSideInfer);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pMain);
            this.Controls.Add(this.ibtnMemo);
            this.Controls.Add(this.ibtnDone);
            this.Controls.Add(this.ibtnInfer);
            this.Controls.Add(this.ibtnHome);
            this.DoubleBuffered = true;
            this.Name = "UcPracticeBlockCode";
            this.Load += new System.EventHandler(this.UcPraticeBlockCode_Load);
            this.pMain.ResumeLayout(false);
            this.pCode.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.webViewCode)).EndInit();
            this.pZoomCode.ResumeLayout(false);
            this.pBlock.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.webViewBlock)).EndInit();
            this.pTopBlock.ResumeLayout(false);
            this.pZoomBlock.ResumeLayout(false);
            this.pSideInfer.ResumeLayout(false);
            this.pSideInfer.PerformLayout();
            this.pThreshold.ResumeLayout(false);
            this.pInferAccuracy.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pboxInferAccuracy)).EndInit();
            this.pMemo.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Guna.UI2.WinForms.Guna2ImageButton ibtnHome;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnInfer;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnDone;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnMemo;
        private Guna.UI2.WinForms.Guna2Panel pZoomBlock;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnPlusBlock;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnMinusBlock;
        private Guna.UI2.WinForms.Guna2TextBox tboxZoomBlock;
        private Guna.UI2.WinForms.Guna2Panel pMain;
        private Guna.UI2.WinForms.Guna2Panel pBlockList;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnCopy;
        private System.Windows.Forms.Panel pTopBlock;
        private Guna.UI2.WinForms.Guna2ImageButton btnRunModel;
        private Guna.UI2.WinForms.Guna2ImageButton btnPreBlock;
        private Guna.UI2.WinForms.Guna2ImageButton btnTrashBlock;
        private Guna.UI2.WinForms.Guna2ImageButton btnNextBlock;
        private Guna.UI2.WinForms.Guna2Panel pBlock;
        private Guna.UI2.WinForms.Guna2Panel pCode;
        private System.Windows.Forms.Panel pTopCode;
        private Guna.UI2.WinForms.Guna2Panel pZoomCode;
        private Guna.UI2.WinForms.Guna2TextBox tboxZoomCode;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnMinusCode;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnPlusCode;
        private System.Windows.Forms.Panel panel2;
        private Microsoft.Web.WebView2.WinForms.WebView2 webViewCode;
        private Microsoft.Web.WebView2.WinForms.WebView2 webViewBlock;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitle;
        private Guna.UI2.WinForms.Guna2Panel pSideInfer;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnCloseInfer;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblThreshold;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblInfer;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblInferGraph;
        private Guna.UI2.WinForms.Guna2Panel pInferAccuracy;
        private Guna.UI2.WinForms.Guna2PictureBox pboxInferAccuracy;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnInfoThreshold;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnInfoGraph;
        private Guna.UI2.WinForms.Guna2Panel pMemo;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnAiFeedback;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnGoNotion;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnDownloadAIModel;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnSelectInferImage;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnQuestionMemo;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnCloseMemo;
        private Guna.UI2.WinForms.Guna2TextBox tboxMemo;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnTest;
        private Guna.UI2.WinForms.Guna2Panel pThreshold;
        private Guna.UI2.WinForms.Guna2TextBox tboxThreshold;
        private Guna.UI2.WinForms.Guna2TrackBar tbarThreshold;
    }
}
