
using System.Diagnostics.Tracing;

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
            this.pMain = new Guna.UI2.WinForms.Guna2Panel();
            this.cAlertPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.pToDoList = new Guna.UI2.WinForms.Guna2Panel();
            this.pBlockList = new Guna.UI2.WinForms.Guna2Panel();
            this.pTxtDescription = new Guna.UI2.WinForms.Guna2Panel();
            this.btnVisualizeResult = new Guna.UI2.WinForms.Guna2Button();
            this.btnModelInference = new Guna.UI2.WinForms.Guna2Button();
            this.btnImgPath = new Guna.UI2.WinForms.Guna2Button();
            this.btnResultGraph = new Guna.UI2.WinForms.Guna2Button();
            this.btnMachineLearning = new Guna.UI2.WinForms.Guna2Button();
            this.btnLoadDataset = new Guna.UI2.WinForms.Guna2Button();
            this.btnLoadModel = new Guna.UI2.WinForms.Guna2Button();
            this.btnPip = new Guna.UI2.WinForms.Guna2Button();
            this.btnBlockStart = new Guna.UI2.WinForms.Guna2Button();
            this.pCode = new Guna.UI2.WinForms.Guna2Panel();
            this.btnCopy = new Guna.UI2.WinForms.Guna2Button();
            this.pZoomCode = new Guna.UI2.WinForms.Guna2Panel();
            this.tboxZoomCode = new Guna.UI2.WinForms.Guna2TextBox();
            this.ibtnMinusCode = new Guna.UI2.WinForms.Guna2ImageButton();
            this.ibtnPlusCode = new Guna.UI2.WinForms.Guna2ImageButton();
            this.ibtnCloseInfer = new Guna.UI2.WinForms.Guna2ImageButton();
            this.pTopCode = new System.Windows.Forms.Panel();
            this.ucCode1 = new SAI.App.Views.Pages.UcCode();
            this.pBlock = new Guna.UI2.WinForms.Guna2Panel();
            this.webViewblock = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.pTopBlock = new System.Windows.Forms.Panel();
            this.btnRunModel = new Guna.UI2.WinForms.Guna2Button();
            this.pErrorImg = new Guna.UI2.WinForms.Guna2Panel();
            this.ibtnMemo = new Guna.UI2.WinForms.Guna2ImageButton();
            this.ibtnDone = new Guna.UI2.WinForms.Guna2ImageButton();
            this.ibtnInfer = new Guna.UI2.WinForms.Guna2ImageButton();
            this.ibtnHome = new Guna.UI2.WinForms.Guna2ImageButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pSideInfer = new Guna.UI2.WinForms.Guna2Panel();
            this.tpModelGraph = new System.Windows.Forms.TableLayoutPanel();
            this.lblModelGraph = new SAI.App.Views.Pages.AutoSizeLabel();
            this.btnSelectInferImage = new Guna.UI2.WinForms.Guna2Button();
            this.pboxInferAccuracy = new Guna.UI2.WinForms.Guna2PictureBox();
            this.ptxtThreshold = new Guna.UI2.WinForms.Guna2Panel();
            this.lblThreshold = new SAI.App.Views.Pages.AutoSizeLabel();
            this.btnInfoGraph = new Guna.UI2.WinForms.Guna2Button();
            this.ptxtInfer = new Guna.UI2.WinForms.Guna2Panel();
            this.lblinfer = new SAI.App.Views.Pages.AutoSizeLabel();
            this.btnInfoThreshold = new Guna.UI2.WinForms.Guna2Button();
            this.pFake = new Guna.UI2.WinForms.Guna2Panel();
            this.ucCsvChart1 = new SAI.App.Views.Pages.UcCsvChart();
            this.pThreshold = new Guna.UI2.WinForms.Guna2Panel();
            this.tboxThreshold = new Guna.UI2.WinForms.Guna2TextBox();
            this.tbarThreshold = new Guna.UI2.WinForms.Guna2TrackBar();
            this.ibtnDownloadAIModel = new Guna.UI2.WinForms.Guna2ImageButton();
            this.ibtnGoNotion = new Guna.UI2.WinForms.Guna2ImageButton();
            this.ibtnAiFeedback = new Guna.UI2.WinForms.Guna2ImageButton();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.pleaseControlThreshold = new SAI.App.Views.Pages.AutoSizeLabel();
            this.pMemo = new Guna.UI2.WinForms.Guna2Panel();
            this.mAlertPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.btnCloseMemo = new Guna.UI2.WinForms.Guna2Button();
            this.btnQuestionMemo = new Guna.UI2.WinForms.Guna2Button();
            this.tboxMemo = new Guna.UI2.WinForms.Guna2TextBox();
            this.pErrorToast = new Guna.UI2.WinForms.Guna2Panel();
            this.tpParentError = new System.Windows.Forms.TableLayoutPanel();
            this.pErrorToastCloseBtn = new Guna.UI2.WinForms.Guna2Button();
            this.tpContentError = new System.Windows.Forms.TableLayoutPanel();
            this.tpErrorHeader = new System.Windows.Forms.TableLayoutPanel();
            this.pError = new System.Windows.Forms.Panel();
            this.lbErrorType = new SAI.App.Views.Pages.AutoSizeLabel();
            this.lbMissingType = new SAI.App.Views.Pages.AutoSizeLabel();
            this.lbErrorMessage = new SAI.App.Views.Pages.AutoSizeLabel();
            this.lblTitle = new SAI.App.Views.Pages.AutoSizeLabel();
            this.pMain.SuspendLayout();
            this.pBlockList.SuspendLayout();
            this.pCode.SuspendLayout();
            this.pZoomCode.SuspendLayout();
            this.pBlock.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webViewblock)).BeginInit();
            this.pTopBlock.SuspendLayout();
            this.pSideInfer.SuspendLayout();
            this.tpModelGraph.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxInferAccuracy)).BeginInit();
            this.ptxtThreshold.SuspendLayout();
            this.ptxtInfer.SuspendLayout();
            this.pThreshold.SuspendLayout();
            this.guna2Panel1.SuspendLayout();
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
            this.pMain.Controls.Add(this.cAlertPanel);
            this.pMain.Controls.Add(this.pToDoList);
            this.pMain.Controls.Add(this.pBlockList);
            this.pMain.Controls.Add(this.pCode);
            this.pMain.Controls.Add(this.pBlock);
            this.pMain.FillColor = System.Drawing.Color.Transparent;
            this.pMain.ForeColor = System.Drawing.Color.Transparent;
            this.pMain.Name = "pMain";
            // 
            // cAlertPanel
            // 
            this.cAlertPanel.BackgroundImage = global::SAI.Properties.Resources.p_copy_alarm;
            resources.ApplyResources(this.cAlertPanel, "cAlertPanel");
            this.cAlertPanel.BorderColor = System.Drawing.Color.Transparent;
            this.cAlertPanel.CustomBorderColor = System.Drawing.Color.Transparent;
            this.cAlertPanel.Name = "cAlertPanel";
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
            this.pBlockList.Controls.Add(this.pTxtDescription);
            this.pBlockList.Controls.Add(this.btnVisualizeResult);
            this.pBlockList.Controls.Add(this.btnModelInference);
            this.pBlockList.Controls.Add(this.btnImgPath);
            this.pBlockList.Controls.Add(this.btnResultGraph);
            this.pBlockList.Controls.Add(this.btnMachineLearning);
            this.pBlockList.Controls.Add(this.btnLoadDataset);
            this.pBlockList.Controls.Add(this.btnLoadModel);
            this.pBlockList.Controls.Add(this.btnPip);
            this.pBlockList.Controls.Add(this.btnBlockStart);
            this.pBlockList.Name = "pBlockList";
            // 
            // pTxtDescription
            // 
            this.pTxtDescription.BackgroundImage = global::SAI.Properties.Resources.lbl_img_path;
            resources.ApplyResources(this.pTxtDescription, "pTxtDescription");
            this.pTxtDescription.Name = "pTxtDescription";
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
            this.pCode.Controls.Add(this.btnCopy);
            this.pCode.Controls.Add(this.pZoomCode);
            this.pCode.Controls.Add(this.ibtnCloseInfer);
            this.pCode.Controls.Add(this.pTopCode);
            this.pCode.Controls.Add(this.ucCode1);
            this.pCode.Name = "pCode";
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
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
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
            this.tboxZoomCode.ForeColor = System.Drawing.Color.Black;
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
            // ucCode1
            // 
            resources.ApplyResources(this.ucCode1, "ucCode1");
            this.ucCode1.Name = "ucCode1";
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
            this.pTopBlock.Controls.Add(this.btnRunModel);
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
            this.btnRunModel.Name = "btnRunModel";
            this.btnRunModel.Click += new System.EventHandler(this.btnRunModel_Click);
            // 
            // pErrorImg
            // 
            this.pErrorImg.BackgroundImage = global::SAI.Properties.Resources.icon_error;
            resources.ApplyResources(this.pErrorImg, "pErrorImg");
            this.pErrorImg.Name = "pErrorImg";
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
            // ibtnDone
            // 
            this.ibtnDone.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnDone.HoverState.Image = global::SAI.Properties.Resources.btn_go_pratice_hover;
            this.ibtnDone.HoverState.ImageSize = new System.Drawing.Size(231, 59);
            this.ibtnDone.Image = global::SAI.Properties.Resources.btn_go_pratice;
            this.ibtnDone.ImageOffset = new System.Drawing.Point(0, 0);
            this.ibtnDone.ImageRotate = 0F;
            this.ibtnDone.ImageSize = new System.Drawing.Size(231, 59);
            resources.ApplyResources(this.ibtnDone, "ibtnDone");
            this.ibtnDone.Name = "ibtnDone";
            this.ibtnDone.PressedState.ImageSize = new System.Drawing.Size(231, 59);
            this.ibtnDone.Click += new System.EventHandler(this.ibtnDone_Click);
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
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // pSideInfer
            // 
            resources.ApplyResources(this.pSideInfer, "pSideInfer");
            this.pSideInfer.BackgroundImage = global::SAI.Properties.Resources.p_side_infer;
            this.pSideInfer.Controls.Add(this.tpModelGraph);
            this.pSideInfer.Controls.Add(this.btnSelectInferImage);
            this.pSideInfer.Controls.Add(this.pboxInferAccuracy);
            this.pSideInfer.Controls.Add(this.ptxtThreshold);
            this.pSideInfer.Controls.Add(this.btnInfoGraph);
            this.pSideInfer.Controls.Add(this.ptxtInfer);
            this.pSideInfer.Controls.Add(this.btnInfoThreshold);
            this.pSideInfer.Controls.Add(this.pFake);
            this.pSideInfer.Controls.Add(this.ucCsvChart1);
            this.pSideInfer.Controls.Add(this.pThreshold);
            this.pSideInfer.Controls.Add(this.tbarThreshold);
            this.pSideInfer.Controls.Add(this.ibtnDownloadAIModel);
            this.pSideInfer.Controls.Add(this.ibtnGoNotion);
            this.pSideInfer.Controls.Add(this.ibtnAiFeedback);
            this.pSideInfer.Controls.Add(this.guna2Panel1);
            this.pSideInfer.Name = "pSideInfer";
            // 
            // tpModelGraph
            // 
            resources.ApplyResources(this.tpModelGraph, "tpModelGraph");
            this.tpModelGraph.Controls.Add(this.lblModelGraph, 0, 0);
            this.tpModelGraph.Name = "tpModelGraph";
            // 
            // lblModelGraph
            // 
            resources.ApplyResources(this.lblModelGraph, "lblModelGraph");
            this.lblModelGraph.Name = "lblModelGraph";
            this.lblModelGraph.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSelectInferImage
            // 
            this.btnSelectInferImage.BackgroundImage = global::SAI.Properties.Resources.btn_selectinferimage;
            resources.ApplyResources(this.btnSelectInferImage, "btnSelectInferImage");
            this.btnSelectInferImage.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSelectInferImage.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSelectInferImage.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSelectInferImage.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSelectInferImage.FillColor = System.Drawing.Color.Transparent;
            this.btnSelectInferImage.ForeColor = System.Drawing.Color.White;
            this.btnSelectInferImage.Name = "btnSelectInferImage";
            // 
            // pboxInferAccuracy
            // 
            this.pboxInferAccuracy.BackgroundImage = global::SAI.Properties.Resources.p_sideinfer_accuracy;
            resources.ApplyResources(this.pboxInferAccuracy, "pboxInferAccuracy");
            this.pboxInferAccuracy.FillColor = System.Drawing.Color.Transparent;
            this.pboxInferAccuracy.ImageRotate = 0F;
            this.pboxInferAccuracy.InitialImage = global::SAI.Properties.Resources.p_sideinfer_accuracy;
            this.pboxInferAccuracy.Name = "pboxInferAccuracy";
            this.pboxInferAccuracy.TabStop = false;
            this.pboxInferAccuracy.Click += new System.EventHandler(this.pboxInferAccuracy_Click);
            // 
            // ptxtThreshold
            // 
            this.ptxtThreshold.Controls.Add(this.lblThreshold);
            resources.ApplyResources(this.ptxtThreshold, "ptxtThreshold");
            this.ptxtThreshold.Name = "ptxtThreshold";
            // 
            // lblThreshold
            // 
            resources.ApplyResources(this.lblThreshold, "lblThreshold");
            this.lblThreshold.Name = "lblThreshold";
            this.lblThreshold.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblThreshold.Load += new System.EventHandler(this.lblThreshold_Load);
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
            // ptxtInfer
            // 
            this.ptxtInfer.Controls.Add(this.lblinfer);
            resources.ApplyResources(this.ptxtInfer, "ptxtInfer");
            this.ptxtInfer.Name = "ptxtInfer";
            // 
            // lblinfer
            // 
            resources.ApplyResources(this.lblinfer, "lblinfer");
            this.lblinfer.Name = "lblinfer";
            this.lblinfer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            // pFake
            // 
            this.pFake.BorderColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.pFake, "pFake");
            this.pFake.Name = "pFake";
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
            this.tboxThreshold.PlaceholderText = "";
            this.tboxThreshold.SelectedText = "";
            this.tboxThreshold.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            this.ibtnDownloadAIModel.PressedState.ImageSize = new System.Drawing.Size(240, 75);
            this.ibtnDownloadAIModel.Click += new System.EventHandler(this.ibtnDownloadAIModel_Click);
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
            // guna2Panel1
            // 
            this.guna2Panel1.Controls.Add(this.pleaseControlThreshold);
            this.guna2Panel1.ForeColor = System.Drawing.Color.Tomato;
            resources.ApplyResources(this.guna2Panel1, "guna2Panel1");
            this.guna2Panel1.Name = "guna2Panel1";
            // 
            // pleaseControlThreshold
            // 
            resources.ApplyResources(this.pleaseControlThreshold, "pleaseControlThreshold");
            this.pleaseControlThreshold.ForeColor = System.Drawing.Color.Tomato;
            this.pleaseControlThreshold.Name = "pleaseControlThreshold";
            this.pleaseControlThreshold.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pMemo
            // 
            this.pMemo.BackgroundImage = global::SAI.Properties.Resources.p_memo;
            resources.ApplyResources(this.pMemo, "pMemo");
            this.pMemo.Controls.Add(this.mAlertPanel);
            this.pMemo.Controls.Add(this.btnCloseMemo);
            this.pMemo.Controls.Add(this.btnQuestionMemo);
            this.pMemo.Controls.Add(this.tboxMemo);
            this.pMemo.Name = "pMemo";
            // 
            // mAlertPanel
            // 
            this.mAlertPanel.BackgroundImage = global::SAI.Properties.Resources.memo_alert;
            resources.ApplyResources(this.mAlertPanel, "mAlertPanel");
            this.mAlertPanel.Name = "mAlertPanel";
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
            this.tpParentError.Controls.Add(this.pErrorToastCloseBtn, 2, 1);
            this.tpParentError.Controls.Add(this.tpContentError, 1, 1);
            this.tpParentError.Name = "tpParentError";
            // 
            // pErrorToastCloseBtn
            // 
            resources.ApplyResources(this.pErrorToastCloseBtn, "pErrorToastCloseBtn");
            this.pErrorToastCloseBtn.BorderColor = System.Drawing.Color.Transparent;
            this.pErrorToastCloseBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.pErrorToastCloseBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.pErrorToastCloseBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.pErrorToastCloseBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.pErrorToastCloseBtn.FillColor = System.Drawing.Color.Transparent;
            this.pErrorToastCloseBtn.ForeColor = System.Drawing.Color.White;
            this.pErrorToastCloseBtn.HoverState.FillColor = System.Drawing.Color.Transparent;
            this.pErrorToastCloseBtn.HoverState.Image = global::SAI.Properties.Resources.btn_close_guide_clicked;
            this.pErrorToastCloseBtn.Image = global::SAI.Properties.Resources.btn_close_guide;
            this.pErrorToastCloseBtn.ImageSize = new System.Drawing.Size(25, 25);
            this.pErrorToastCloseBtn.Name = "pErrorToastCloseBtn";
            this.pErrorToastCloseBtn.PressedColor = System.Drawing.Color.Transparent;
            this.pErrorToastCloseBtn.Click += new System.EventHandler(this.pErrorToastCloseBtn_Click);
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
            // lblTitle
            // 
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UcTutorialBlockCode
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImage = global::SAI.Properties.Resources.img_background1;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.pErrorToast);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pMemo);
            this.Controls.Add(this.pSideInfer);
            this.Controls.Add(this.pMain);
            this.Controls.Add(this.ibtnMemo);
            this.Controls.Add(this.ibtnDone);
            this.Controls.Add(this.ibtnInfer);
            this.Controls.Add(this.ibtnHome);
            this.DoubleBuffered = true;
            this.Name = "UcTutorialBlockCode";
            this.Load += new System.EventHandler(this.UcTutorialBlockCode_Load);
            this.pMain.ResumeLayout(false);
            this.pBlockList.ResumeLayout(false);
            this.pCode.ResumeLayout(false);
            this.pZoomCode.ResumeLayout(false);
            this.pBlock.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.webViewblock)).EndInit();
            this.pTopBlock.ResumeLayout(false);
            this.pSideInfer.ResumeLayout(false);
            this.tpModelGraph.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pboxInferAccuracy)).EndInit();
            this.ptxtThreshold.ResumeLayout(false);
            this.ptxtInfer.ResumeLayout(false);
            this.pThreshold.ResumeLayout(false);
            this.guna2Panel1.ResumeLayout(false);
            this.pMemo.ResumeLayout(false);
            this.pErrorToast.ResumeLayout(false);
            this.tpParentError.ResumeLayout(false);
            this.tpContentError.ResumeLayout(false);
            this.tpContentError.PerformLayout();
            this.tpErrorHeader.ResumeLayout(false);
            this.tpErrorHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2ImageButton ibtnHome;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnInfer;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnDone;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnMemo;
        private Guna.UI2.WinForms.Guna2Panel pMain;
        private System.Windows.Forms.Panel pTopBlock;
        private Guna.UI2.WinForms.Guna2Panel pBlock;
        private Guna.UI2.WinForms.Guna2Panel pCode;
        private System.Windows.Forms.Panel pTopCode;
        private Guna.UI2.WinForms.Guna2Panel pZoomCode;
        private Guna.UI2.WinForms.Guna2TextBox tboxZoomCode;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnMinusCode;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnPlusCode;
        private System.Windows.Forms.Panel panel2;
        private Guna.UI2.WinForms.Guna2Panel pBlockList;
        private Guna.UI2.WinForms.Guna2Panel pToDoList;
        private Microsoft.Web.WebView2.WinForms.WebView2 webViewblock;
        private Guna.UI2.WinForms.Guna2Button btnBlockStart;
        private Guna.UI2.WinForms.Guna2Button btnPip;
        private Guna.UI2.WinForms.Guna2Button btnLoadModel;
        private Guna.UI2.WinForms.Guna2Button btnLoadDataset;
        private Guna.UI2.WinForms.Guna2Button btnMachineLearning;
        private Guna.UI2.WinForms.Guna2Button btnResultGraph;
        private Guna.UI2.WinForms.Guna2Button btnImgPath;
        private Guna.UI2.WinForms.Guna2Button btnModelInference;
        private Guna.UI2.WinForms.Guna2Button btnVisualizeResult;
        private Guna.UI2.WinForms.Guna2Panel pSideInfer;
        private Guna.UI2.WinForms.Guna2TextBox tboxThreshold;
        private Guna.UI2.WinForms.Guna2TrackBar tbarThreshold;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnDownloadAIModel;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnGoNotion;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnAiFeedback;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnCloseInfer;
        private Guna.UI2.WinForms.Guna2Panel pMemo;
        private Guna.UI2.WinForms.Guna2TextBox tboxMemo;
        private Guna.UI2.WinForms.Guna2Panel pThreshold;
        private Guna.UI2.WinForms.Guna2Button btnRunModel;
        private UcCsvChart ucCsvChart1;
        private SAI.App.Views.Pages.UcCode ucCode1;
        private Guna.UI2.WinForms.Guna2Button btnQuestionMemo;
        private Guna.UI2.WinForms.Guna2Button btnCloseMemo;
        private Guna.UI2.WinForms.Guna2Panel pFake;
        private Guna.UI2.WinForms.Guna2Button btnCopy;
        private Guna.UI2.WinForms.Guna2Button btnInfoThreshold;
        private Guna.UI2.WinForms.Guna2Button btnInfoGraph;
        private Guna.UI2.WinForms.Guna2Panel cAlertPanel;
        private Guna.UI2.WinForms.Guna2Panel mAlertPanel;
        private Guna.UI2.WinForms.Guna2Panel pTxtDescription;
		private System.Windows.Forms.TableLayoutPanel tpParentError;
		private System.Windows.Forms.TableLayoutPanel tpContentError;
		private System.Windows.Forms.TableLayoutPanel tpErrorHeader;
		private SAI.App.Views.Pages.AutoSizeLabel lbErrorType;
		private System.Windows.Forms.Panel pError;
		private SAI.App.Views.Pages.AutoSizeLabel lbMissingType;
		private SAI.App.Views.Pages.AutoSizeLabel lbErrorMessage;
        private Guna.UI2.WinForms.Guna2Panel pErrorImg;
		private Guna.UI2.WinForms.Guna2Panel pErrorToast;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private SAI.App.Views.Pages.AutoSizeLabel pleaseControlThreshold;
        private Guna.UI2.WinForms.Guna2Panel ptxtInfer;
        private SAI.App.Views.Pages.AutoSizeLabel lblinfer;
        private Guna.UI2.WinForms.Guna2Panel ptxtThreshold;
        private SAI.App.Views.Pages.AutoSizeLabel lblThreshold;
        private Guna.UI2.WinForms.Guna2Button btnSelectInferImage;
        private Guna.UI2.WinForms.Guna2PictureBox pboxInferAccuracy;
        private SAI.App.Views.Pages.AutoSizeLabel lblTitle;
        private Guna.UI2.WinForms.Guna2Button pErrorToastCloseBtn;
		private System.Windows.Forms.TableLayoutPanel tpModelGraph;
		private SAI.App.Views.Pages.AutoSizeLabel lblModelGraph;
	}
}
