using Guna.UI2.WinForms.Enums;

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
            this.pTopCode = new System.Windows.Forms.Panel();
            this.cAlertPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.btnCopy = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2TextBox1 = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnMinus = new Guna.UI2.WinForms.Guna2ImageButton();
            this.guna2ImageButton2 = new Guna.UI2.WinForms.Guna2ImageButton();
            this.ucCode２ = new SAI.App.Views.Pages.UcCode();
            this.webViewCode = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.pZoomCode = new Guna.UI2.WinForms.Guna2Panel();
            this.tboxZoomCode = new Guna.UI2.WinForms.Guna2TextBox();
            this.ibtnMinusCode = new Guna.UI2.WinForms.Guna2ImageButton();
            this.ibtnPlusCode = new Guna.UI2.WinForms.Guna2ImageButton();
            this.pBlock = new Guna.UI2.WinForms.Guna2Panel();
            this.webViewblock = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.pTopBlock = new System.Windows.Forms.Panel();
            this.btnRunModel = new Guna.UI2.WinForms.Guna2Button();
            this.btnPreBlock = new Guna.UI2.WinForms.Guna2Button();
            this.btnNextBlock = new Guna.UI2.WinForms.Guna2Button();
            this.btnTrash = new Guna.UI2.WinForms.Guna2Button();
            this.pBlockList = new Guna.UI2.WinForms.Guna2Panel();
            this.pSelectBlock = new Guna.UI2.WinForms.Guna2Panel();
            this.pSelectBlockvScrollBar = new Guna.UI2.WinForms.Guna2VScrollBar();
            this.ibtnMemo = new Guna.UI2.WinForms.Guna2ImageButton();
            this.ibtnInfer = new Guna.UI2.WinForms.Guna2ImageButton();
            this.ibtnHome = new Guna.UI2.WinForms.Guna2ImageButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.pSideInfer = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.pleaseControlThreshold = new SAI.App.Views.Pages.AutoSizeLabel();
            this.ucCsvChart1 = new SAI.App.Views.Pages.UcCsvChart();
            this.pThreshold = new Guna.UI2.WinForms.Guna2Panel();
            this.tboxThreshold = new Guna.UI2.WinForms.Guna2TextBox();
            this.tbarThreshold = new Guna.UI2.WinForms.Guna2TrackBar();
            this.ibtnDownloadAIModel = new Guna.UI2.WinForms.Guna2ImageButton();
            this.ibtnGoNotion = new Guna.UI2.WinForms.Guna2ImageButton();
            this.ibtnAiFeedback = new Guna.UI2.WinForms.Guna2ImageButton();
            this.btnInfoGraph = new Guna.UI2.WinForms.Guna2Button();
            this.btnInfoThreshold = new Guna.UI2.WinForms.Guna2Button();
            this.pInferAccuracy = new Guna.UI2.WinForms.Guna2Panel();
            this.btnSelectInferImage = new Guna.UI2.WinForms.Guna2Button();
            this.pboxInferAccuracy = new Guna.UI2.WinForms.Guna2PictureBox();
            this.lblInferGraph = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblThreshold = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblInfer = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.pFake = new Guna.UI2.WinForms.Guna2Panel();
            this.pMemo = new Guna.UI2.WinForms.Guna2Panel();
            this.mAlertPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.tboxMemo = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnQuestionMemo = new Guna.UI2.WinForms.Guna2Button();
            this.btnCloseMemo = new Guna.UI2.WinForms.Guna2Button();
            this.pErrorToast = new Guna.UI2.WinForms.Guna2Panel();
            this.tpParentError = new System.Windows.Forms.TableLayoutPanel();
            this.tpContentError = new System.Windows.Forms.TableLayoutPanel();
            this.tpErrorHeader = new System.Windows.Forms.TableLayoutPanel();
            this.pError = new System.Windows.Forms.Panel();
            this.lbErrorType = new SAI.App.Views.Pages.AutoSizeLabel();
            this.lbMissingType = new SAI.App.Views.Pages.AutoSizeLabel();
            this.lbErrorMessage = new SAI.App.Views.Pages.AutoSizeLabel();
            this.pMain.SuspendLayout();
            this.pCode.SuspendLayout();
            this.guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webViewCode)).BeginInit();
            this.webViewCode.SuspendLayout();
            this.pZoomCode.SuspendLayout();
            this.pBlock.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webViewblock)).BeginInit();
            this.pTopBlock.SuspendLayout();
            this.pBlockList.SuspendLayout();
            this.pSelectBlock.SuspendLayout();
            this.pSideInfer.SuspendLayout();
            this.guna2Panel2.SuspendLayout();
            this.pThreshold.SuspendLayout();
            this.pInferAccuracy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxInferAccuracy)).BeginInit();
            this.pMemo.SuspendLayout();
            this.pErrorToast.SuspendLayout();
            this.tpParentError.SuspendLayout();
            this.tpContentError.SuspendLayout();
            this.tpErrorHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pMain
            // 
            this.pMain.BackColor = System.Drawing.Color.Transparent;
            this.pMain.BackgroundImage = global::SAI.Properties.Resources.img_frame_shadow;
            resources.ApplyResources(this.pMain, "pMain");
            this.pMain.Controls.Add(this.pCode);
            this.pMain.Controls.Add(this.pBlock);
            this.pMain.Controls.Add(this.pBlockList);
            this.pMain.FillColor = System.Drawing.Color.Transparent;
            this.pMain.ForeColor = System.Drawing.Color.Transparent;
            this.pMain.Name = "pMain";
            // 
            // pCode
            // 
            this.pCode.BackgroundImage = global::SAI.Properties.Resources.p_blockCode;
            resources.ApplyResources(this.pCode, "pCode");
            this.pCode.Controls.Add(this.ibtnCloseInfer);
            this.pCode.Controls.Add(this.pTopCode);
            this.pCode.Controls.Add(this.cAlertPanel);
            this.pCode.Controls.Add(this.btnCopy);
            this.pCode.Controls.Add(this.guna2Panel1);
            this.pCode.Controls.Add(this.ucCode２);
            this.pCode.Controls.Add(this.webViewCode);
            this.pCode.Name = "pCode";
            // 
            // ibtnCloseInfer
            // 
            this.ibtnCloseInfer.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnCloseInfer.HoverState.Image = global::SAI.Properties.Resources.btn_close_infer_hover;
            this.ibtnCloseInfer.HoverState.ImageSize = new System.Drawing.Size(58, 230);
            this.ibtnCloseInfer.Image = global::SAI.Properties.Resources.btn_close_infer;
            this.ibtnCloseInfer.ImageOffset = new System.Drawing.Point(0, 0);
            this.ibtnCloseInfer.ImageRotate = 0F;
            this.ibtnCloseInfer.ImageSize = new System.Drawing.Size(58, 230);
            resources.ApplyResources(this.ibtnCloseInfer, "ibtnCloseInfer");
            this.ibtnCloseInfer.Name = "ibtnCloseInfer";
            this.ibtnCloseInfer.PressedState.ImageSize = new System.Drawing.Size(58, 230);
            this.ibtnCloseInfer.Click += new System.EventHandler(this.ibtnCloseInfer_Click);
            // 
            // pTopCode
            // 
            this.pTopCode.BackColor = System.Drawing.Color.Transparent;
            this.pTopCode.BackgroundImage = global::SAI.Properties.Resources.p_topCode;
            resources.ApplyResources(this.pTopCode, "pTopCode");
            this.pTopCode.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pTopCode.Name = "pTopCode";
            // 
            // cAlertPanel
            // 
            this.cAlertPanel.BackgroundImage = global::SAI.Properties.Resources.copy_alert;
            resources.ApplyResources(this.cAlertPanel, "cAlertPanel");
            this.cAlertPanel.Name = "cAlertPanel";
            // 
            // btnCopy
            // 
            this.btnCopy.BackgroundImage = global::SAI.Properties.Resources.btn_copy;
            resources.ApplyResources(this.btnCopy, "btnCopy");
            this.btnCopy.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCopy.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCopy.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCopy.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCopy.FillColor = System.Drawing.Color.Transparent;
            this.btnCopy.ForeColor = System.Drawing.Color.White;
            this.btnCopy.Name = "btnCopy";
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackgroundImage = global::SAI.Properties.Resources.btn_zoom;
            resources.ApplyResources(this.guna2Panel1, "guna2Panel1");
            this.guna2Panel1.Controls.Add(this.guna2TextBox1);
            this.guna2Panel1.Controls.Add(this.btnMinus);
            this.guna2Panel1.Controls.Add(this.guna2ImageButton2);
            this.guna2Panel1.Name = "guna2Panel1";
            // 
            // guna2TextBox1
            // 
            this.guna2TextBox1.BorderColor = System.Drawing.Color.Black;
            this.guna2TextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.guna2TextBox1.DefaultText = "";
            this.guna2TextBox1.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.guna2TextBox1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.guna2TextBox1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox1.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox1.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            resources.ApplyResources(this.guna2TextBox1, "guna2TextBox1");
            this.guna2TextBox1.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox1.Name = "guna2TextBox1";
            this.guna2TextBox1.PlaceholderText = "";
            this.guna2TextBox1.SelectedText = "";
            // 
            // btnMinus
            // 
            resources.ApplyResources(this.btnMinus, "btnMinus");
            this.btnMinus.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnMinus.HoverState.Image = global::SAI.Properties.Resources.btn_minus;
            this.btnMinus.HoverState.ImageSize = new System.Drawing.Size(9, 9);
            this.btnMinus.Image = global::SAI.Properties.Resources.btn_minus;
            this.btnMinus.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnMinus.ImageRotate = 0F;
            this.btnMinus.ImageSize = new System.Drawing.Size(14, 14);
            this.btnMinus.Name = "btnMinus";
            this.btnMinus.PressedState.ImageSize = new System.Drawing.Size(9, 9);
            this.btnMinus.UseTransparentBackground = true;
            // 
            // guna2ImageButton2
            // 
            this.guna2ImageButton2.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.guna2ImageButton2.HoverState.ImageSize = new System.Drawing.Size(9, 9);
            this.guna2ImageButton2.Image = global::SAI.Properties.Resources.btn_plus;
            this.guna2ImageButton2.ImageOffset = new System.Drawing.Point(0, 0);
            this.guna2ImageButton2.ImageRotate = 0F;
            this.guna2ImageButton2.ImageSize = new System.Drawing.Size(14, 14);
            resources.ApplyResources(this.guna2ImageButton2, "guna2ImageButton2");
            this.guna2ImageButton2.Name = "guna2ImageButton2";
            this.guna2ImageButton2.PressedState.ImageSize = new System.Drawing.Size(9, 9);
            // 
            // ucCode２
            // 
            this.ucCode２.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.ucCode２, "ucCode２");
            this.ucCode２.Name = "ucCode２";
            this.ucCode２.Load += new System.EventHandler(this.ucCode1_Load);
            // 
            // webViewCode
            // 
            this.webViewCode.AllowExternalDrop = true;
            this.webViewCode.Controls.Add(this.pZoomCode);
            this.webViewCode.CreationProperties = null;
            this.webViewCode.DefaultBackgroundColor = System.Drawing.Color.White;
            resources.ApplyResources(this.webViewCode, "webViewCode");
            this.webViewCode.Name = "webViewCode";
            this.webViewCode.ZoomFactor = 1D;
            // 
            // pZoomCode
            // 
            this.pZoomCode.BackgroundImage = global::SAI.Properties.Resources.btn_zoom;
            resources.ApplyResources(this.pZoomCode, "pZoomCode");
            this.pZoomCode.Controls.Add(this.tboxZoomCode);
            this.pZoomCode.Controls.Add(this.ibtnMinusCode);
            this.pZoomCode.Controls.Add(this.ibtnPlusCode);
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
            this.ibtnMinusCode.ImageSize = new System.Drawing.Size(14, 14);
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
            this.ibtnPlusCode.ImageSize = new System.Drawing.Size(14, 14);
            resources.ApplyResources(this.ibtnPlusCode, "ibtnPlusCode");
            this.ibtnPlusCode.Name = "ibtnPlusCode";
            this.ibtnPlusCode.PressedState.ImageSize = new System.Drawing.Size(9, 9);
            // 
            // pBlock
            // 
            this.pBlock.BackgroundImage = global::SAI.Properties.Resources.p_block;
            resources.ApplyResources(this.pBlock, "pBlock");
            this.pBlock.Controls.Add(this.webViewblock);
            this.pBlock.Controls.Add(this.pTopBlock);
            this.pBlock.Name = "pBlock";
            // 
            // webViewblock
            // 
            this.webViewblock.AllowExternalDrop = true;
            this.webViewblock.BackColor = System.Drawing.Color.White;
            this.webViewblock.CreationProperties = null;
            this.webViewblock.DefaultBackgroundColor = System.Drawing.Color.White;
            resources.ApplyResources(this.webViewblock, "webViewblock");
            this.webViewblock.Name = "webViewblock";
            this.webViewblock.ZoomFactor = 1D;
            // 
            // pTopBlock
            // 
            this.pTopBlock.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.pTopBlock, "pTopBlock");
            this.pTopBlock.Controls.Add(this.btnRunModel);
            this.pTopBlock.Controls.Add(this.btnPreBlock);
            this.pTopBlock.Controls.Add(this.btnNextBlock);
            this.pTopBlock.Controls.Add(this.btnTrash);
            this.pTopBlock.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pTopBlock.Name = "pTopBlock";
            // 
            // btnRunModel
            // 
            this.btnRunModel.BackgroundImage = global::SAI.Properties.Resources.btn_run_model;
            resources.ApplyResources(this.btnRunModel, "btnRunModel");
            this.btnRunModel.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnRunModel.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnRunModel.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnRunModel.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnRunModel.FillColor = System.Drawing.Color.Transparent;
            this.btnRunModel.ForeColor = System.Drawing.Color.White;
            this.btnRunModel.ImageSize = new System.Drawing.Size(0, 0);
            this.btnRunModel.Name = "btnRunModel";
            this.btnRunModel.Click += new System.EventHandler(this.btnRunModel_Click);
            // 
            // btnPreBlock
            // 
            this.btnPreBlock.BackgroundImage = global::SAI.Properties.Resources.btn_pre_block1;
            resources.ApplyResources(this.btnPreBlock, "btnPreBlock");
            this.btnPreBlock.BorderColor = System.Drawing.Color.Transparent;
            this.btnPreBlock.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnPreBlock.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnPreBlock.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnPreBlock.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnPreBlock.FillColor = System.Drawing.Color.Transparent;
            this.btnPreBlock.ForeColor = System.Drawing.Color.White;
            this.btnPreBlock.Name = "btnPreBlock";
            this.btnPreBlock.Click += new System.EventHandler(this.btnPreBlock_Click);
            // 
            // btnNextBlock
            // 
            this.btnNextBlock.BackgroundImage = global::SAI.Properties.Resources.btn_next_block1;
            resources.ApplyResources(this.btnNextBlock, "btnNextBlock");
            this.btnNextBlock.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnNextBlock.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnNextBlock.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnNextBlock.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnNextBlock.FillColor = System.Drawing.Color.Transparent;
            this.btnNextBlock.ForeColor = System.Drawing.Color.White;
            this.btnNextBlock.Name = "btnNextBlock";
            this.btnNextBlock.Click += new System.EventHandler(this.btnNextBlock_Click);
            // 
            // btnTrash
            // 
            this.btnTrash.BackgroundImage = global::SAI.Properties.Resources.btn_trash_block;
            resources.ApplyResources(this.btnTrash, "btnTrash");
            this.btnTrash.BorderColor = System.Drawing.Color.Transparent;
            this.btnTrash.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnTrash.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnTrash.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnTrash.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnTrash.FillColor = System.Drawing.Color.Transparent;
            this.btnTrash.ForeColor = System.Drawing.Color.White;
            this.btnTrash.Name = "btnTrash";
            this.btnTrash.Click += new System.EventHandler(this.btnTrash_Click);
            // 
            // pBlockList
            // 
            this.pBlockList.BackColor = System.Drawing.Color.Transparent;
            this.pBlockList.BackgroundImage = global::SAI.Properties.Resources.p_block;
            resources.ApplyResources(this.pBlockList, "pBlockList");
            this.pBlockList.BorderColor = System.Drawing.Color.Transparent;
            this.pBlockList.Controls.Add(this.pSelectBlock);
            this.pBlockList.FillColor = System.Drawing.Color.Transparent;
            this.pBlockList.Name = "pBlockList";
            this.pBlockList.ShadowDecoration.BorderRadius = 32;
            this.pBlockList.ShadowDecoration.Depth = 15;
            this.pBlockList.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 0, 6, 6);
            // 
            // pSelectBlock
            // 
            resources.ApplyResources(this.pSelectBlock, "pSelectBlock");
            this.pSelectBlock.BackColor = System.Drawing.Color.White;
            this.pSelectBlock.Controls.Add(this.pSelectBlockvScrollBar);
            this.pSelectBlock.Name = "pSelectBlock";
            // 
            // pSelectBlockvScrollBar
            // 
            this.pSelectBlockvScrollBar.FillColor = System.Drawing.Color.WhiteSmoke;
            this.pSelectBlockvScrollBar.InUpdate = false;
            this.pSelectBlockvScrollBar.LargeChange = 70;
            resources.ApplyResources(this.pSelectBlockvScrollBar, "pSelectBlockvScrollBar");
            this.pSelectBlockvScrollBar.Name = "pSelectBlockvScrollBar";
            this.pSelectBlockvScrollBar.ScrollbarSize = 10;
            this.pSelectBlockvScrollBar.ThumbColor = System.Drawing.Color.DarkGray;
            this.pSelectBlockvScrollBar.ThumbStyle = Guna.UI2.WinForms.Enums.ThumbStyle.Inset;
            // 
            // ibtnMemo
            // 
            resources.ApplyResources(this.ibtnMemo, "ibtnMemo");
            this.ibtnMemo.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnMemo.HoverState.Image = global::SAI.Properties.Resources.btn_memo;
            this.ibtnMemo.HoverState.ImageSize = new System.Drawing.Size(87, 87);
            this.ibtnMemo.Image = global::SAI.Properties.Resources.btn_memo;
            this.ibtnMemo.ImageOffset = new System.Drawing.Point(0, 0);
            this.ibtnMemo.ImageRotate = 0F;
            this.ibtnMemo.ImageSize = new System.Drawing.Size(87, 87);
            this.ibtnMemo.Name = "ibtnMemo";
            this.ibtnMemo.PressedState.ImageSize = new System.Drawing.Size(87, 87);
            this.ibtnMemo.Click += new System.EventHandler(this.ibtnMemo_Click);
            // 
            // ibtnInfer
            // 
            resources.ApplyResources(this.ibtnInfer, "ibtnInfer");
            this.ibtnInfer.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnInfer.HoverState.Image = global::SAI.Properties.Resources.btn_infer_hover;
            this.ibtnInfer.HoverState.ImageSize = new System.Drawing.Size(58, 230);
            this.ibtnInfer.Image = global::SAI.Properties.Resources.btn_infer;
            this.ibtnInfer.ImageOffset = new System.Drawing.Point(0, 0);
            this.ibtnInfer.ImageRotate = 0F;
            this.ibtnInfer.ImageSize = new System.Drawing.Size(58, 230);
            this.ibtnInfer.Name = "ibtnInfer";
            this.ibtnInfer.PressedState.ImageSize = new System.Drawing.Size(58, 230);
            this.ibtnInfer.Click += new System.EventHandler(this.ibtnInfer_Click);
            // 
            // ibtnHome
            // 
            this.ibtnHome.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnHome.HoverState.Image = global::SAI.Properties.Resources.btn_home_hover;
            this.ibtnHome.HoverState.ImageSize = new System.Drawing.Size(59, 59);
            this.ibtnHome.Image = global::SAI.Properties.Resources.btn_home;
            this.ibtnHome.ImageOffset = new System.Drawing.Point(0, 0);
            this.ibtnHome.ImageRotate = 0F;
            this.ibtnHome.ImageSize = new System.Drawing.Size(59, 59);
            resources.ApplyResources(this.ibtnHome, "ibtnHome");
            this.ibtnHome.Name = "ibtnHome";
            this.ibtnHome.PressedState.ImageSize = new System.Drawing.Size(59, 59);
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
            this.pSideInfer.Controls.Add(this.guna2Panel2);
            this.pSideInfer.Controls.Add(this.ucCsvChart1);
            this.pSideInfer.Controls.Add(this.pThreshold);
            this.pSideInfer.Controls.Add(this.tbarThreshold);
            this.pSideInfer.Controls.Add(this.ibtnDownloadAIModel);
            this.pSideInfer.Controls.Add(this.ibtnGoNotion);
            this.pSideInfer.Controls.Add(this.ibtnAiFeedback);
            this.pSideInfer.Controls.Add(this.btnInfoGraph);
            this.pSideInfer.Controls.Add(this.btnInfoThreshold);
            this.pSideInfer.Controls.Add(this.pInferAccuracy);
            this.pSideInfer.Controls.Add(this.lblInferGraph);
            this.pSideInfer.Controls.Add(this.lblThreshold);
            this.pSideInfer.Controls.Add(this.lblInfer);
            this.pSideInfer.Controls.Add(this.pFake);
            this.pSideInfer.Name = "pSideInfer";
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.Controls.Add(this.pleaseControlThreshold);
            resources.ApplyResources(this.guna2Panel2, "guna2Panel2");
            this.guna2Panel2.Name = "guna2Panel2";
            // 
            // pleaseControlThreshold
            // 
            resources.ApplyResources(this.pleaseControlThreshold, "pleaseControlThreshold");
            this.pleaseControlThreshold.ForeColor = System.Drawing.Color.Tomato;
            this.pleaseControlThreshold.Name = "pleaseControlThreshold";
            this.pleaseControlThreshold.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ucCsvChart1
            // 
            resources.ApplyResources(this.ucCsvChart1, "ucCsvChart1");
            this.ucCsvChart1.Name = "ucCsvChart1";
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
            this.tboxThreshold.PlaceholderForeColor = System.Drawing.Color.Black;
            this.tboxThreshold.PlaceholderText = "";
            this.tboxThreshold.SelectedText = "";
            // 
            // tbarThreshold
            // 
            resources.ApplyResources(this.tbarThreshold, "tbarThreshold");
            this.tbarThreshold.Name = "tbarThreshold";
            this.tbarThreshold.ThumbColor = System.Drawing.Color.Gold;
            // 
            // ibtnDownloadAIModel
            // 
            this.ibtnDownloadAIModel.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnDownloadAIModel.HoverState.Image = global::SAI.Properties.Resources.btn_download_aimodel_hover;
            this.ibtnDownloadAIModel.HoverState.ImageSize = new System.Drawing.Size(240, 75);
            this.ibtnDownloadAIModel.Image = global::SAI.Properties.Resources.btn_download_aimodel;
            this.ibtnDownloadAIModel.ImageOffset = new System.Drawing.Point(0, 0);
            this.ibtnDownloadAIModel.ImageRotate = 0F;
            this.ibtnDownloadAIModel.ImageSize = new System.Drawing.Size(240, 75);
            resources.ApplyResources(this.ibtnDownloadAIModel, "ibtnDownloadAIModel");
            this.ibtnDownloadAIModel.Name = "ibtnDownloadAIModel";
            this.ibtnDownloadAIModel.PressedState.ImageSize = new System.Drawing.Size(160, 50);
            this.ibtnDownloadAIModel.Click += new System.EventHandler(this.btnSaveModel_Click);
            // 
            // ibtnGoNotion
            // 
            this.ibtnGoNotion.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnGoNotion.HoverState.Image = global::SAI.Properties.Resources.btn_goNotion_hover;
            this.ibtnGoNotion.HoverState.ImageSize = new System.Drawing.Size(240, 75);
            this.ibtnGoNotion.Image = global::SAI.Properties.Resources.btn_goNotion;
            this.ibtnGoNotion.ImageOffset = new System.Drawing.Point(0, 0);
            this.ibtnGoNotion.ImageRotate = 0F;
            this.ibtnGoNotion.ImageSize = new System.Drawing.Size(240, 75);
            resources.ApplyResources(this.ibtnGoNotion, "ibtnGoNotion");
            this.ibtnGoNotion.Name = "ibtnGoNotion";
            this.ibtnGoNotion.PressedState.ImageSize = new System.Drawing.Size(240, 75);
            this.ibtnGoNotion.Click += new System.EventHandler(this.ibtnGoNotion_Click);
            // 
            // ibtnAiFeedback
            // 
            this.ibtnAiFeedback.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnAiFeedback.HoverState.Image = global::SAI.Properties.Resources.btn_aifeedback;
            this.ibtnAiFeedback.HoverState.ImageSize = new System.Drawing.Size(520, 135);
            this.ibtnAiFeedback.Image = global::SAI.Properties.Resources.btn_aifeedback;
            this.ibtnAiFeedback.ImageOffset = new System.Drawing.Point(0, 0);
            this.ibtnAiFeedback.ImageRotate = 0F;
            this.ibtnAiFeedback.ImageSize = new System.Drawing.Size(520, 135);
            resources.ApplyResources(this.ibtnAiFeedback, "ibtnAiFeedback");
            this.ibtnAiFeedback.Name = "ibtnAiFeedback";
            this.ibtnAiFeedback.PressedState.ImageSize = new System.Drawing.Size(520, 135);
            this.ibtnAiFeedback.Click += new System.EventHandler(this.ibtnAiFeedback_Click);
            // 
            // btnInfoGraph
            // 
            this.btnInfoGraph.BackgroundImage = global::SAI.Properties.Resources.btn_info_17;
            resources.ApplyResources(this.btnInfoGraph, "btnInfoGraph");
            this.btnInfoGraph.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnInfoGraph.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnInfoGraph.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnInfoGraph.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnInfoGraph.FillColor = System.Drawing.Color.Transparent;
            this.btnInfoGraph.ForeColor = System.Drawing.Color.White;
            this.btnInfoGraph.Name = "btnInfoGraph";
            // 
            // btnInfoThreshold
            // 
            this.btnInfoThreshold.BackgroundImage = global::SAI.Properties.Resources.btn_info_12;
            resources.ApplyResources(this.btnInfoThreshold, "btnInfoThreshold");
            this.btnInfoThreshold.BorderColor = System.Drawing.Color.Transparent;
            this.btnInfoThreshold.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnInfoThreshold.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnInfoThreshold.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnInfoThreshold.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnInfoThreshold.FillColor = System.Drawing.Color.Transparent;
            this.btnInfoThreshold.ForeColor = System.Drawing.Color.White;
            this.btnInfoThreshold.Name = "btnInfoThreshold";
            // 
            // pInferAccuracy
            // 
            this.pInferAccuracy.BackgroundImage = global::SAI.Properties.Resources.p_sideinfer_accuracy;
            resources.ApplyResources(this.pInferAccuracy, "pInferAccuracy");
            this.pInferAccuracy.Controls.Add(this.btnSelectInferImage);
            this.pInferAccuracy.Controls.Add(this.pboxInferAccuracy);
            this.pInferAccuracy.Name = "pInferAccuracy";
            // 
            // btnSelectInferImage
            // 
            this.btnSelectInferImage.BackgroundImage = global::SAI.Properties.Resources.btn_selectinferimage;
            resources.ApplyResources(this.btnSelectInferImage, "btnSelectInferImage");
            this.btnSelectInferImage.BorderColor = System.Drawing.Color.Transparent;
            this.btnSelectInferImage.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSelectInferImage.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSelectInferImage.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSelectInferImage.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSelectInferImage.FillColor = System.Drawing.Color.Transparent;
            this.btnSelectInferImage.ForeColor = System.Drawing.Color.White;
            this.btnSelectInferImage.Name = "btnSelectInferImage";
            this.btnSelectInferImage.Click += new System.EventHandler(this.btnSelectInferImage_Click);
            // 
            // pboxInferAccuracy
            // 
            resources.ApplyResources(this.pboxInferAccuracy, "pboxInferAccuracy");
            this.pboxInferAccuracy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pboxInferAccuracy.ImageRotate = 0F;
            this.pboxInferAccuracy.Name = "pboxInferAccuracy";
            this.pboxInferAccuracy.TabStop = false;
            this.pboxInferAccuracy.Click += new System.EventHandler(this.pboxInferAccuracy_Click);
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
            // 
            // lblInfer
            // 
            this.lblInfer.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lblInfer, "lblInfer");
            this.lblInfer.Name = "lblInfer";
            // 
            // pFake
            // 
            this.pFake.BorderColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.pFake, "pFake");
            this.pFake.Name = "pFake";
            // 
            // pMemo
            // 
            this.pMemo.BackgroundImage = global::SAI.Properties.Resources.p_memo;
            resources.ApplyResources(this.pMemo, "pMemo");
            this.pMemo.Controls.Add(this.mAlertPanel);
            this.pMemo.Controls.Add(this.tboxMemo);
            this.pMemo.Controls.Add(this.btnQuestionMemo);
            this.pMemo.Controls.Add(this.btnCloseMemo);
            this.pMemo.Name = "pMemo";
            // 
            // mAlertPanel
            // 
            this.mAlertPanel.BackgroundImage = global::SAI.Properties.Resources.memo_alert;
            resources.ApplyResources(this.mAlertPanel, "mAlertPanel");
            this.mAlertPanel.Name = "mAlertPanel";
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
            // btnQuestionMemo
            // 
            this.btnQuestionMemo.BackgroundImage = global::SAI.Properties.Resources.btn_question_memo;
            resources.ApplyResources(this.btnQuestionMemo, "btnQuestionMemo");
            this.btnQuestionMemo.BorderColor = System.Drawing.Color.Transparent;
            this.btnQuestionMemo.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnQuestionMemo.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnQuestionMemo.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnQuestionMemo.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnQuestionMemo.FillColor = System.Drawing.Color.Transparent;
            this.btnQuestionMemo.ForeColor = System.Drawing.Color.White;
            this.btnQuestionMemo.Name = "btnQuestionMemo";
            // 
            // btnCloseMemo
            // 
            this.btnCloseMemo.BackgroundImage = global::SAI.Properties.Resources.btn_close_25;
            resources.ApplyResources(this.btnCloseMemo, "btnCloseMemo");
            this.btnCloseMemo.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCloseMemo.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCloseMemo.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCloseMemo.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCloseMemo.FillColor = System.Drawing.Color.Transparent;
            this.btnCloseMemo.ForeColor = System.Drawing.Color.Transparent;
            this.btnCloseMemo.Name = "btnCloseMemo";
            this.btnCloseMemo.PressedColor = System.Drawing.Color.Transparent;
            this.btnCloseMemo.PressedDepth = 0;
            this.btnCloseMemo.Click += new System.EventHandler(this.btnCloseMemo_Click);
            // 
            // pErrorToast
            // 
            this.pErrorToast.BackgroundImage = global::SAI.Properties.Resources.bg_toast_error;
            resources.ApplyResources(this.pErrorToast, "pErrorToast");
            this.pErrorToast.Controls.Add(this.tpParentError);
            this.pErrorToast.Name = "pErrorToast";
            // 
            // tpParentError
            // 
            resources.ApplyResources(this.tpParentError, "tpParentError");
            this.tpParentError.Controls.Add(this.tpContentError, 1, 1);
            this.tpParentError.Name = "tpParentError";
            // 
            // tpContentError
            // 
            resources.ApplyResources(this.tpContentError, "tpContentError");
            this.tpContentError.Controls.Add(this.tpErrorHeader, 0, 0);
            this.tpContentError.Controls.Add(this.lbMissingType, 0, 1);
            this.tpContentError.Controls.Add(this.lbErrorMessage, 0, 2);
            this.tpContentError.Name = "tpContentError";
            // 
            // tpErrorHeader
            // 
            resources.ApplyResources(this.tpErrorHeader, "tpErrorHeader");
            this.tpErrorHeader.Controls.Add(this.pError, 0, 0);
            this.tpErrorHeader.Controls.Add(this.lbErrorType, 1, 0);
            this.tpErrorHeader.Name = "tpErrorHeader";
            // 
            // pError
            // 
            this.pError.BackgroundImage = global::SAI.Properties.Resources.icon_error;
            resources.ApplyResources(this.pError, "pError");
            this.pError.Name = "pError";
            // 
            // lbErrorType
            // 
            resources.ApplyResources(this.lbErrorType, "lbErrorType");
            this.lbErrorType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.lbErrorType.Name = "lbErrorType";
            this.lbErrorType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbMissingType
            // 
            resources.ApplyResources(this.lbMissingType, "lbMissingType");
            this.lbMissingType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.lbMissingType.Name = "lbMissingType";
            this.lbMissingType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbErrorMessage
            // 
            resources.ApplyResources(this.lbErrorMessage, "lbErrorMessage");
            this.lbErrorMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.lbErrorMessage.Name = "lbErrorMessage";
            this.lbErrorMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UcPracticeBlockCode
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImage = global::SAI.Properties.Resources.img_background1;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.pErrorToast);
            this.Controls.Add(this.pMemo);
            this.Controls.Add(this.pSideInfer);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pMain);
            this.Controls.Add(this.ibtnMemo);
            this.Controls.Add(this.ibtnInfer);
            this.Controls.Add(this.ibtnHome);
            this.DoubleBuffered = true;
            this.Name = "UcPracticeBlockCode";
            this.Load += new System.EventHandler(this.UcPracticeBlockCode_Load);
            this.pMain.ResumeLayout(false);
            this.pCode.ResumeLayout(false);
            this.guna2Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.webViewCode)).EndInit();
            this.webViewCode.ResumeLayout(false);
            this.pZoomCode.ResumeLayout(false);
            this.pBlock.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.webViewblock)).EndInit();
            this.pTopBlock.ResumeLayout(false);
            this.pBlockList.ResumeLayout(false);
            this.pSelectBlock.ResumeLayout(false);
            this.pSideInfer.ResumeLayout(false);
            this.pSideInfer.PerformLayout();
            this.guna2Panel2.ResumeLayout(false);
            this.pThreshold.ResumeLayout(false);
            this.pInferAccuracy.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pboxInferAccuracy)).EndInit();
            this.pMemo.ResumeLayout(false);
            this.pErrorToast.ResumeLayout(false);
            this.tpParentError.ResumeLayout(false);
            this.tpContentError.ResumeLayout(false);
            this.tpContentError.PerformLayout();
            this.tpErrorHeader.ResumeLayout(false);
            this.tpErrorHeader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Guna.UI2.WinForms.Guna2ImageButton ibtnHome;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnInfer;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnMemo;
        private Guna.UI2.WinForms.Guna2Panel pMain;
        private Guna.UI2.WinForms.Guna2Panel pBlockList;
        private System.Windows.Forms.Panel pTopBlock;
        private Guna.UI2.WinForms.Guna2Panel pBlock;
        private Guna.UI2.WinForms.Guna2Panel pCode;
        private System.Windows.Forms.Panel pTopCode;
        private Guna.UI2.WinForms.Guna2Panel pZoomCode;
        private Guna.UI2.WinForms.Guna2TextBox tboxZoomCode;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnMinusCode;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnPlusCode;
        private System.Windows.Forms.Panel panel2;
        private Microsoft.Web.WebView2.WinForms.WebView2 webViewCode;
        private Microsoft.Web.WebView2.WinForms.WebView2 webViewblock;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitle;
        private Guna.UI2.WinForms.Guna2Panel pSideInfer;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnCloseInfer;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblThreshold;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblInfer;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblInferGraph;
        private Guna.UI2.WinForms.Guna2Panel pInferAccuracy;
        private Guna.UI2.WinForms.Guna2PictureBox pboxInferAccuracy;
        private Guna.UI2.WinForms.Guna2Panel pMemo;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnAiFeedback;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnGoNotion;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnDownloadAIModel;
        private Guna.UI2.WinForms.Guna2TextBox tboxMemo;
        private Guna.UI2.WinForms.Guna2Panel pThreshold;
        private Guna.UI2.WinForms.Guna2TextBox tboxThreshold;
        private Guna.UI2.WinForms.Guna2TrackBar tbarThreshold;
        private Guna.UI2.WinForms.Guna2Button btnTrash;
        private Guna.UI2.WinForms.Guna2Button btnNextBlock;
        private Guna.UI2.WinForms.Guna2Button btnPreBlock;
        private Guna.UI2.WinForms.Guna2Button btnCloseMemo;
        private Guna.UI2.WinForms.Guna2Button btnQuestionMemo;
        private Guna.UI2.WinForms.Guna2Panel pFake;
        private Guna.UI2.WinForms.Guna2Button btnSelectInferImage;
        private Guna.UI2.WinForms.Guna2Button btnCopy;
        private Guna.UI2.WinForms.Guna2Button btnInfoThreshold;
        private Guna.UI2.WinForms.Guna2Button btnInfoGraph;
		private Guna.UI2.WinForms.Guna2Button btnRunModel;
		private Guna.UI2.WinForms.Guna2Panel pSelectBlock;
		private Guna.UI2.WinForms.Guna2VScrollBar pSelectBlockvScrollBar;
        private SAI.App.Views.Pages.UcCode ucCode２;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2TextBox guna2TextBox1;
        private Guna.UI2.WinForms.Guna2ImageButton btnMinus;
        private Guna.UI2.WinForms.Guna2ImageButton guna2ImageButton2;
        private Guna.UI2.WinForms.Guna2ImageButton btnTrashBlock;
        private UcImageChart ucChart1;
        private Guna.UI2.WinForms.Guna2Panel mAlertPanel;
        private Guna.UI2.WinForms.Guna2Panel cAlertPanel;
        private SAI.App.Views.Pages.UcCsvChart ucCsvChart1;
		private Guna.UI2.WinForms.Guna2Panel pErrorToast;
		private System.Windows.Forms.TableLayoutPanel tpParentError;
		private System.Windows.Forms.TableLayoutPanel tpContentError;
		private System.Windows.Forms.TableLayoutPanel tpErrorHeader;
		private System.Windows.Forms.Panel pError;
		private SAI.App.Views.Pages.AutoSizeLabel lbErrorType;
		private SAI.App.Views.Pages.AutoSizeLabel lbMissingType;
		private SAI.App.Views.Pages.AutoSizeLabel lbErrorMessage;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private SAI.App.Views.Pages.AutoSizeLabel pleaseControlThreshold;
    }
}
