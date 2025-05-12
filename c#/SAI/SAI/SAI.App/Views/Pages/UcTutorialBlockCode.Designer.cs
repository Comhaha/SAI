
namespace SAI.SAI.App.Views.Pages
{
    partial class UcTutorialBlockCode
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcTutorialBlockCode));
			this.lblTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
			this.pMain = new Guna.UI2.WinForms.Guna2Panel();
			this.pToDoList = new Guna.UI2.WinForms.Guna2Panel();
			this.pBlockList = new Guna.UI2.WinForms.Guna2Panel();
			this.btnVisualizeResult = new Guna.UI2.WinForms.Guna2Button();
			this.btnModelInference = new Guna.UI2.WinForms.Guna2Button();
			this.btnImgPath = new Guna.UI2.WinForms.Guna2Button();
			this.btnResultGraph = new Guna.UI2.WinForms.Guna2Button();
			this.btnMachineLearning = new Guna.UI2.WinForms.Guna2Button();
			this.btnLoadDataset = new Guna.UI2.WinForms.Guna2Button();
			this.btnLoadModel = new Guna.UI2.WinForms.Guna2Button();
			this.btnPip = new Guna.UI2.WinForms.Guna2Button();
			this.labelBlockContent = new System.Windows.Forms.Label();
			this.labelBlockTitle = new System.Windows.Forms.Label();
			this.btnBlockStart = new Guna.UI2.WinForms.Guna2Button();
			this.pCode = new Guna.UI2.WinForms.Guna2Panel();
			this.webViewCode = new Microsoft.Web.WebView2.WinForms.WebView2();
			this.ibtnCopy = new Guna.UI2.WinForms.Guna2ImageButton();
			this.pTopCode = new System.Windows.Forms.Panel();
			this.pZoomCode = new Guna.UI2.WinForms.Guna2Panel();
			this.tboxZoomCode = new Guna.UI2.WinForms.Guna2TextBox();
			this.ibtnMinusCode = new Guna.UI2.WinForms.Guna2ImageButton();
			this.ibtnPlusCode = new Guna.UI2.WinForms.Guna2ImageButton();
			this.pBlock = new Guna.UI2.WinForms.Guna2Panel();
			this.webViewblock = new Microsoft.Web.WebView2.WinForms.WebView2();
			this.pTopBlock = new System.Windows.Forms.Panel();
			this.ibtnRunModel = new Guna.UI2.WinForms.Guna2ImageButton();
			this.ibtnPreBlock = new Guna.UI2.WinForms.Guna2ImageButton();
			this.ibtnTrashBlock = new Guna.UI2.WinForms.Guna2ImageButton();
			this.ibtnNextBlock = new Guna.UI2.WinForms.Guna2ImageButton();
			this.pZoomBlock = new Guna.UI2.WinForms.Guna2Panel();
			this.tboxZoomBlock = new Guna.UI2.WinForms.Guna2TextBox();
			this.ibtnMinusBlock = new Guna.UI2.WinForms.Guna2ImageButton();
			this.ibtnPlusBlock = new Guna.UI2.WinForms.Guna2ImageButton();
			this.ibtnMemo = new Guna.UI2.WinForms.Guna2ImageButton();
			this.ibtnDone = new Guna.UI2.WinForms.Guna2ImageButton();
			this.ibtnInfer = new Guna.UI2.WinForms.Guna2ImageButton();
			this.ibtnHome = new Guna.UI2.WinForms.Guna2ImageButton();
			this.panel2 = new System.Windows.Forms.Panel();
			this.pMain.SuspendLayout();
			this.pBlockList.SuspendLayout();
			this.pCode.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.webViewCode)).BeginInit();
			this.pZoomCode.SuspendLayout();
			this.pBlock.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.webViewblock)).BeginInit();
			this.pTopBlock.SuspendLayout();
			this.pZoomBlock.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblTitle
			// 
			this.lblTitle.BackColor = System.Drawing.Color.Transparent;
			resources.ApplyResources(this.lblTitle, "lblTitle");
			this.lblTitle.Name = "lblTitle";
			// 
			// pMain
			// 
			this.pMain.BackColor = System.Drawing.Color.Transparent;
			this.pMain.BackgroundImage = global::SAI.Properties.Resources.img_frame_shadow;
			resources.ApplyResources(this.pMain, "pMain");
			this.pMain.BorderRadius = 32;
			this.pMain.BorderThickness = 1;
			this.pMain.Controls.Add(this.pToDoList);
			this.pMain.Controls.Add(this.pBlockList);
			this.pMain.Controls.Add(this.pCode);
			this.pMain.Controls.Add(this.pBlock);
			this.pMain.FillColor = System.Drawing.Color.Transparent;
			this.pMain.ForeColor = System.Drawing.Color.Transparent;
			this.pMain.Name = "pMain";
			this.pMain.ShadowDecoration.BorderRadius = 32;
			this.pMain.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 0, 6, 6);
			this.pMain.Paint += new System.Windows.Forms.PaintEventHandler(this.pMain_Paint);
			// 
			// pToDoList
			// 
			this.pToDoList.BackgroundImage = global::SAI.Properties.Resources.p_todolist_step1;
			resources.ApplyResources(this.pToDoList, "pToDoList");
			this.pToDoList.Name = "pToDoList";
			// 
			// pBlockList
			// 
			this.pBlockList.BackgroundImage = global::SAI.Properties.Resources.p_blocklist_tutorial;
			resources.ApplyResources(this.pBlockList, "pBlockList");
			this.pBlockList.Controls.Add(this.btnVisualizeResult);
			this.pBlockList.Controls.Add(this.btnModelInference);
			this.pBlockList.Controls.Add(this.btnImgPath);
			this.pBlockList.Controls.Add(this.btnResultGraph);
			this.pBlockList.Controls.Add(this.btnMachineLearning);
			this.pBlockList.Controls.Add(this.btnLoadDataset);
			this.pBlockList.Controls.Add(this.btnLoadModel);
			this.pBlockList.Controls.Add(this.btnPip);
			this.pBlockList.Controls.Add(this.labelBlockContent);
			this.pBlockList.Controls.Add(this.labelBlockTitle);
			this.pBlockList.Controls.Add(this.btnBlockStart);
			this.pBlockList.Name = "pBlockList";
			// 
			// btnVisualizeResult
			// 
			this.btnVisualizeResult.BackgroundImage = global::SAI.Properties.Resources.btnVisualizeResult;
			resources.ApplyResources(this.btnVisualizeResult, "btnVisualizeResult");
			this.btnVisualizeResult.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnVisualizeResult.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnVisualizeResult.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnVisualizeResult.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnVisualizeResult.FillColor = System.Drawing.Color.Transparent;
			this.btnVisualizeResult.ForeColor = System.Drawing.Color.White;
			this.btnVisualizeResult.Name = "btnVisualizeResult";
			// 
			// btnModelInference
			// 
			this.btnModelInference.BackgroundImage = global::SAI.Properties.Resources.btnModelInference;
			resources.ApplyResources(this.btnModelInference, "btnModelInference");
			this.btnModelInference.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnModelInference.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnModelInference.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnModelInference.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnModelInference.FillColor = System.Drawing.Color.Transparent;
			this.btnModelInference.ForeColor = System.Drawing.Color.White;
			this.btnModelInference.Name = "btnModelInference";
			// 
			// btnImgPath
			// 
			this.btnImgPath.BackgroundImage = global::SAI.Properties.Resources.btnImgPath;
			resources.ApplyResources(this.btnImgPath, "btnImgPath");
			this.btnImgPath.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnImgPath.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnImgPath.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnImgPath.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnImgPath.FillColor = System.Drawing.Color.Transparent;
			this.btnImgPath.ForeColor = System.Drawing.Color.White;
			this.btnImgPath.Name = "btnImgPath";
			// 
			// btnResultGraph
			// 
			this.btnResultGraph.BackgroundImage = global::SAI.Properties.Resources.btnResultGraph;
			resources.ApplyResources(this.btnResultGraph, "btnResultGraph");
			this.btnResultGraph.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnResultGraph.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnResultGraph.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnResultGraph.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnResultGraph.FillColor = System.Drawing.Color.Transparent;
			this.btnResultGraph.ForeColor = System.Drawing.Color.White;
			this.btnResultGraph.Name = "btnResultGraph";
			// 
			// btnMachineLearning
			// 
			this.btnMachineLearning.BackgroundImage = global::SAI.Properties.Resources.btnMachineLearning;
			resources.ApplyResources(this.btnMachineLearning, "btnMachineLearning");
			this.btnMachineLearning.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnMachineLearning.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnMachineLearning.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnMachineLearning.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnMachineLearning.FillColor = System.Drawing.Color.Transparent;
			this.btnMachineLearning.ForeColor = System.Drawing.Color.White;
			this.btnMachineLearning.Name = "btnMachineLearning";
			// 
			// btnLoadDataset
			// 
			this.btnLoadDataset.BackgroundImage = global::SAI.Properties.Resources.btnLoadDataset;
			resources.ApplyResources(this.btnLoadDataset, "btnLoadDataset");
			this.btnLoadDataset.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnLoadDataset.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnLoadDataset.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnLoadDataset.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnLoadDataset.FillColor = System.Drawing.Color.Transparent;
			this.btnLoadDataset.ForeColor = System.Drawing.Color.White;
			this.btnLoadDataset.Name = "btnLoadDataset";
			// 
			// btnLoadModel
			// 
			this.btnLoadModel.BackgroundImage = global::SAI.Properties.Resources.btnLoadModel;
			resources.ApplyResources(this.btnLoadModel, "btnLoadModel");
			this.btnLoadModel.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnLoadModel.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnLoadModel.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnLoadModel.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnLoadModel.FillColor = System.Drawing.Color.Transparent;
			this.btnLoadModel.ForeColor = System.Drawing.Color.White;
			this.btnLoadModel.Name = "btnLoadModel";
			// 
			// btnPip
			// 
			this.btnPip.BackgroundImage = global::SAI.Properties.Resources.btnPipInstall;
			resources.ApplyResources(this.btnPip, "btnPip");
			this.btnPip.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnPip.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnPip.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnPip.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnPip.FillColor = System.Drawing.Color.Transparent;
			this.btnPip.ForeColor = System.Drawing.Color.White;
			this.btnPip.Name = "btnPip";
			// 
			// labelBlockContent
			// 
			resources.ApplyResources(this.labelBlockContent, "labelBlockContent");
			this.labelBlockContent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
			this.labelBlockContent.Name = "labelBlockContent";
			// 
			// labelBlockTitle
			// 
			resources.ApplyResources(this.labelBlockTitle, "labelBlockTitle");
			this.labelBlockTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
			this.labelBlockTitle.Name = "labelBlockTitle";
			// 
			// btnBlockStart
			// 
			this.btnBlockStart.BackgroundImage = global::SAI.Properties.Resources.btnBlockStart;
			resources.ApplyResources(this.btnBlockStart, "btnBlockStart");
			this.btnBlockStart.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnBlockStart.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnBlockStart.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnBlockStart.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnBlockStart.FillColor = System.Drawing.Color.Transparent;
			this.btnBlockStart.ForeColor = System.Drawing.Color.White;
			this.btnBlockStart.Name = "btnBlockStart";
			// 
			// pCode
			// 
			this.pCode.BackgroundImage = global::SAI.Properties.Resources.p_block;
			resources.ApplyResources(this.pCode, "pCode");
			this.pCode.Controls.Add(this.webViewCode);
			this.pCode.Controls.Add(this.ibtnCopy);
			this.pCode.Controls.Add(this.pTopCode);
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
			this.pBlock.Controls.Add(this.webViewblock);
			this.pBlock.Controls.Add(this.pTopBlock);
			this.pBlock.Controls.Add(this.pZoomBlock);
			this.pBlock.Name = "pBlock";
			this.pBlock.Paint += new System.Windows.Forms.PaintEventHandler(this.guna2Panel1_Paint);
			// 
			// webViewblock
			// 
			this.webViewblock.AllowExternalDrop = true;
			this.webViewblock.CreationProperties = null;
			this.webViewblock.DefaultBackgroundColor = System.Drawing.Color.White;
			resources.ApplyResources(this.webViewblock, "webViewblock");
			this.webViewblock.Name = "webViewblock";
			this.webViewblock.TabStop = false;
			this.webViewblock.ZoomFactor = 0.7D;
			this.webViewblock.ZoomFactorChanged += new System.EventHandler<System.EventArgs>(this.webViewblock_ZoomFactorChanged);
			// 
			// pTopBlock
			// 
			this.pTopBlock.BackColor = System.Drawing.Color.Transparent;
			resources.ApplyResources(this.pTopBlock, "pTopBlock");
			this.pTopBlock.Controls.Add(this.ibtnRunModel);
			this.pTopBlock.Controls.Add(this.ibtnPreBlock);
			this.pTopBlock.Controls.Add(this.ibtnTrashBlock);
			this.pTopBlock.Controls.Add(this.ibtnNextBlock);
			this.pTopBlock.ForeColor = System.Drawing.SystemColors.ControlText;
			this.pTopBlock.Name = "pTopBlock";
			this.pTopBlock.Paint += new System.Windows.Forms.PaintEventHandler(this.pTopCenter_Paint);
			// 
			// ibtnRunModel
			// 
			this.ibtnRunModel.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
			this.ibtnRunModel.HoverState.ImageSize = new System.Drawing.Size(10, 12);
			this.ibtnRunModel.Image = global::SAI.Properties.Resources.btn_run_model;
			this.ibtnRunModel.ImageOffset = new System.Drawing.Point(0, 0);
			this.ibtnRunModel.ImageRotate = 0F;
			this.ibtnRunModel.ImageSize = new System.Drawing.Size(10, 12);
			resources.ApplyResources(this.ibtnRunModel, "ibtnRunModel");
			this.ibtnRunModel.Name = "ibtnRunModel";
			this.ibtnRunModel.PressedState.ImageSize = new System.Drawing.Size(10, 12);
			this.ibtnRunModel.Click += new System.EventHandler(this.ibtnRunModel_Click);
			// 
			// ibtnPreBlock
			// 
			this.ibtnPreBlock.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
			this.ibtnPreBlock.HoverState.ImageSize = new System.Drawing.Size(15, 10);
			this.ibtnPreBlock.Image = global::SAI.Properties.Resources.btn_pre_block;
			this.ibtnPreBlock.ImageOffset = new System.Drawing.Point(0, 0);
			this.ibtnPreBlock.ImageRotate = 0F;
			this.ibtnPreBlock.ImageSize = new System.Drawing.Size(15, 10);
			resources.ApplyResources(this.ibtnPreBlock, "ibtnPreBlock");
			this.ibtnPreBlock.Name = "ibtnPreBlock";
			this.ibtnPreBlock.PressedState.ImageSize = new System.Drawing.Size(15, 10);
			// 
			// ibtnTrashBlock
			// 
			this.ibtnTrashBlock.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
			this.ibtnTrashBlock.HoverState.ImageSize = new System.Drawing.Size(13, 14);
			this.ibtnTrashBlock.Image = global::SAI.Properties.Resources.btn_trash_block;
			this.ibtnTrashBlock.ImageOffset = new System.Drawing.Point(0, 0);
			this.ibtnTrashBlock.ImageRotate = 0F;
			this.ibtnTrashBlock.ImageSize = new System.Drawing.Size(13, 14);
			resources.ApplyResources(this.ibtnTrashBlock, "ibtnTrashBlock");
			this.ibtnTrashBlock.Name = "ibtnTrashBlock";
			this.ibtnTrashBlock.PressedState.ImageSize = new System.Drawing.Size(13, 14);
			this.ibtnTrashBlock.Click += new System.EventHandler(this.btnTrashBlock_Click);
			// 
			// ibtnNextBlock
			// 
			this.ibtnNextBlock.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
			this.ibtnNextBlock.HoverState.ImageSize = new System.Drawing.Size(15, 10);
			this.ibtnNextBlock.Image = global::SAI.Properties.Resources.btn_next_block;
			this.ibtnNextBlock.ImageOffset = new System.Drawing.Point(0, 0);
			this.ibtnNextBlock.ImageRotate = 0F;
			this.ibtnNextBlock.ImageSize = new System.Drawing.Size(15, 10);
			resources.ApplyResources(this.ibtnNextBlock, "ibtnNextBlock");
			this.ibtnNextBlock.Name = "ibtnNextBlock";
			this.ibtnNextBlock.PressedState.ImageSize = new System.Drawing.Size(15, 10);
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
			this.ibtnDone.HoverState.Image = global::SAI.Properties.Resources.btn_go_pratice_hover;
			this.ibtnDone.HoverState.ImageSize = new System.Drawing.Size(154, 39);
			this.ibtnDone.Image = global::SAI.Properties.Resources.btn_go_pratice;
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
			this.ibtnHome.Click += new System.EventHandler(this.guna2ImageButton1_Click_1);
			// 
			// panel2
			// 
			resources.ApplyResources(this.panel2, "panel2");
			this.panel2.Name = "panel2";
			// 
			// UcTutorialBlockCode
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
			this.Name = "UcTutorialBlockCode";
			this.Load += new System.EventHandler(this.UcTutorialBlockCode_Load);
			this.pMain.ResumeLayout(false);
			this.pBlockList.ResumeLayout(false);
			this.pCode.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.webViewCode)).EndInit();
			this.pZoomCode.ResumeLayout(false);
			this.pBlock.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.webViewblock)).EndInit();
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
        private Guna.UI2.WinForms.Guna2ImageButton ibtnCopy;
        private System.Windows.Forms.Panel pTopBlock;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnRunModel;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnPreBlock;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnTrashBlock;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnNextBlock;
        private Guna.UI2.WinForms.Guna2Panel pBlock;
        private Guna.UI2.WinForms.Guna2Panel pCode;
        private System.Windows.Forms.Panel pTopCode;
        private Guna.UI2.WinForms.Guna2Panel pZoomCode;
        private Guna.UI2.WinForms.Guna2TextBox tboxZoomCode;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnMinusCode;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnPlusCode;
        private System.Windows.Forms.Panel panel2;
        private Microsoft.Web.WebView2.WinForms.WebView2 webViewCode;
        private Guna.UI2.WinForms.Guna2Panel pBlockList;
        private Guna.UI2.WinForms.Guna2Panel pToDoList;
        private Microsoft.Web.WebView2.WinForms.WebView2 webViewblock;
		private Guna.UI2.WinForms.Guna2Button btnBlockStart;
		private System.Windows.Forms.Label labelBlockTitle;
		private System.Windows.Forms.Label labelBlockContent;
		private Guna.UI2.WinForms.Guna2Button btnPip;
		private Guna.UI2.WinForms.Guna2Button btnLoadModel;
		private Guna.UI2.WinForms.Guna2Button btnLoadDataset;
		private Guna.UI2.WinForms.Guna2Button btnMachineLearning;
		private Guna.UI2.WinForms.Guna2Button btnResultGraph;
		private Guna.UI2.WinForms.Guna2Button btnImgPath;
		private Guna.UI2.WinForms.Guna2Button btnModelInference;
		private Guna.UI2.WinForms.Guna2Button btnVisualizeResult;
	}
}
