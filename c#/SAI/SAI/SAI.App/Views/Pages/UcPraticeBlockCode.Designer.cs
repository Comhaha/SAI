
namespace SAI.SAI.App.Views.Pages
{
    partial class UcPraticeBlockCode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcPraticeBlockCode));
            this.lblTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.pMain = new Guna.UI2.WinForms.Guna2Panel();
            this.pCode = new Guna.UI2.WinForms.Guna2Panel();
            this.webViewCode = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.ibtnCopy = new Guna.UI2.WinForms.Guna2ImageButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pZoomCode = new Guna.UI2.WinForms.Guna2Panel();
            this.tboxZoomCode = new Guna.UI2.WinForms.Guna2TextBox();
            this.ibtnMinusCode = new Guna.UI2.WinForms.Guna2ImageButton();
            this.ibtnPlusCode = new Guna.UI2.WinForms.Guna2ImageButton();
            this.pBlock = new Guna.UI2.WinForms.Guna2Panel();
            this.webBrowserBlock = new CefSharp.WinForms.ChromiumWebBrowser();
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
            this.pMain.SuspendLayout();
            this.pCode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webViewCode)).BeginInit();
            this.pZoomCode.SuspendLayout();
            this.pBlock.SuspendLayout();
            this.pTopBlock.SuspendLayout();
            this.pZoomBlock.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Click += new System.EventHandler(this.lblTitle_Click);
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
            this.pCode.Controls.Add(this.webViewCode);
            this.pCode.Controls.Add(this.ibtnCopy);
            this.pCode.Controls.Add(this.panel1);
            this.pCode.Controls.Add(this.pZoomCode);
            this.pCode.Name = "pCode";
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
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel1.Name = "panel1";
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
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
            this.pBlock.Controls.Add(this.webBrowserBlock);
            this.pBlock.Controls.Add(this.pTopBlock);
            this.pBlock.Controls.Add(this.pZoomBlock);
            this.pBlock.Name = "pBlock";
            this.pBlock.Paint += new System.Windows.Forms.PaintEventHandler(this.guna2Panel1_Paint);
            // 
            // webBrowserBlock
            // 
            this.webBrowserBlock.ActivateBrowserOnCreation = false;
            resources.ApplyResources(this.webBrowserBlock, "webBrowserBlock");
            this.webBrowserBlock.Name = "webBrowserBlock";
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
            this.btnRunModel.HoverState.ImageSize = new System.Drawing.Size(10, 12);
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
            this.btnPreBlock.HoverState.ImageSize = new System.Drawing.Size(15, 10);
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
            this.btnTrashBlock.HoverState.ImageSize = new System.Drawing.Size(13, 14);
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
            this.btnNextBlock.HoverState.ImageSize = new System.Drawing.Size(15, 10);
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
            this.ibtnMinusBlock.HoverState.ImageSize = new System.Drawing.Size(9, 9);
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
            this.ibtnPlusBlock.HoverState.ImageSize = new System.Drawing.Size(9, 9);
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
            this.ibtnHome.Click += new System.EventHandler(this.guna2ImageButton1_Click_1);
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // UcPraticeBlockCode
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImage = global::SAI.Properties.Resources.img_background;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.pMain);
            this.Controls.Add(this.ibtnMemo);
            this.Controls.Add(this.ibtnDone);
            this.Controls.Add(this.ibtnInfer);
            this.Controls.Add(this.ibtnHome);
            this.Controls.Add(this.lblTitle);
            this.DoubleBuffered = true;
            this.Name = "UcPraticeBlockCode";
            this.Load += new System.EventHandler(this.UcPraticeBlockCode_Load);
            this.pMain.ResumeLayout(false);
            this.pCode.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.webViewCode)).EndInit();
            this.pZoomCode.ResumeLayout(false);
            this.pBlock.ResumeLayout(false);
            this.pTopBlock.ResumeLayout(false);
            this.pZoomBlock.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitle;
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
        private CefSharp.WinForms.ChromiumWebBrowser webBrowserBlock;
        private System.Windows.Forms.Panel pTopBlock;
        private Guna.UI2.WinForms.Guna2ImageButton btnRunModel;
        private Guna.UI2.WinForms.Guna2ImageButton btnPreBlock;
        private Guna.UI2.WinForms.Guna2ImageButton btnTrashBlock;
        private Guna.UI2.WinForms.Guna2ImageButton btnNextBlock;
        private Guna.UI2.WinForms.Guna2Panel pBlock;
        private Guna.UI2.WinForms.Guna2Panel pCode;
        private System.Windows.Forms.Panel panel1;
        private Guna.UI2.WinForms.Guna2Panel pZoomCode;
        private Guna.UI2.WinForms.Guna2TextBox tboxZoomCode;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnMinusCode;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnPlusCode;
        private System.Windows.Forms.Panel panel2;
        private Microsoft.Web.WebView2.WinForms.WebView2 webViewCode;
    }
}
