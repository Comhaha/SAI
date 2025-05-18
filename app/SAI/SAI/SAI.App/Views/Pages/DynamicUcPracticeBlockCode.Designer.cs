namespace SAI.SAI.App.Views.Pages
{
    partial class DynamicUcPracticeBlockCode
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
            this.tpParent = new System.Windows.Forms.TableLayoutPanel();
            this.pMain = new Guna.UI2.WinForms.Guna2Panel();
            this.tpBlockParent = new System.Windows.Forms.TableLayoutPanel();
            this.pBlockList = new Guna.UI2.WinForms.Guna2Panel();
            this.tpSelectBlockParent = new System.Windows.Forms.TableLayoutPanel();
            this.pSelectBlock = new Guna.UI2.WinForms.Guna2Panel();
            this.pSelectBlockvScrollBar = new Guna.UI2.WinForms.Guna2VScrollBar();
            this.pBlock = new Guna.UI2.WinForms.Guna2Panel();
            this.tpBlocklyParent = new System.Windows.Forms.TableLayoutPanel();
            this.pTopBlock = new Guna.UI2.WinForms.Guna2Panel();
            this.tpIconParent = new System.Windows.Forms.TableLayoutPanel();
            this.btnPreBlock = new Guna.UI2.WinForms.Guna2Button();
            this.btnTrash = new Guna.UI2.WinForms.Guna2Button();
            this.btnRunModel = new Guna.UI2.WinForms.Guna2Button();
            this.btnNextBlock = new Guna.UI2.WinForms.Guna2Button();
            this.webViewblock = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.pCode = new Guna.UI2.WinForms.Guna2Panel();
            this.tpCodeParent = new System.Windows.Forms.TableLayoutPanel();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.webView2Code = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.tpHomeParent = new System.Windows.Forms.TableLayoutPanel();
            this.btnHome = new Guna.UI2.WinForms.Guna2Button();
            this.autoSizeLabel1 = new SAI.App.Views.Pages.AutoSizeLabel();
            this.tpInferParent = new System.Windows.Forms.TableLayoutPanel();
            this.btnStartInfer = new Guna.UI2.WinForms.Guna2Button();
            this.tpMemoParent = new System.Windows.Forms.TableLayoutPanel();
            this.btnMemo = new Guna.UI2.WinForms.Guna2Button();
            this.tpParent.SuspendLayout();
            this.pMain.SuspendLayout();
            this.tpBlockParent.SuspendLayout();
            this.pBlockList.SuspendLayout();
            this.tpSelectBlockParent.SuspendLayout();
            this.pSelectBlock.SuspendLayout();
            this.pBlock.SuspendLayout();
            this.tpBlocklyParent.SuspendLayout();
            this.pTopBlock.SuspendLayout();
            this.tpIconParent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webViewblock)).BeginInit();
            this.pCode.SuspendLayout();
            this.tpCodeParent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webView2Code)).BeginInit();
            this.tpHomeParent.SuspendLayout();
            this.tpInferParent.SuspendLayout();
            this.tpMemoParent.SuspendLayout();
            this.SuspendLayout();
            // 
            // tpParent
            // 
            this.tpParent.BackColor = System.Drawing.Color.DarkGray;
            this.tpParent.ColumnCount = 3;
            this.tpParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.62F));
            this.tpParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.27F));
            this.tpParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.11F));
            this.tpParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tpParent.Controls.Add(this.pMain, 1, 1);
            this.tpParent.Controls.Add(this.tpHomeParent, 1, 0);
            this.tpParent.Controls.Add(this.tpInferParent, 2, 1);
            this.tpParent.Controls.Add(this.tpMemoParent, 0, 1);
            this.tpParent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tpParent.Location = new System.Drawing.Point(0, 0);
            this.tpParent.Name = "tpParent";
            this.tpParent.RowCount = 3;
            this.tpParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.9F));
            this.tpParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 81F));
            this.tpParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.1F));
            this.tpParent.Size = new System.Drawing.Size(1280, 720);
            this.tpParent.TabIndex = 0;
            // 
            // pMain
            // 
            this.pMain.BackgroundImage = global::SAI.Properties.Resources.img_frame_shadow;
            this.pMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pMain.Controls.Add(this.tpBlockParent);
            this.pMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pMain.Location = new System.Drawing.Point(151, 103);
            this.pMain.Name = "pMain";
            this.pMain.Size = new System.Drawing.Size(1059, 577);
            this.pMain.TabIndex = 0;
            // 
            // tpBlockParent
            // 
            this.tpBlockParent.BackColor = System.Drawing.Color.Transparent;
            this.tpBlockParent.ColumnCount = 7;
            this.tpBlockParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3.1F));
            this.tpBlockParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tpBlockParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 1.9F));
            this.tpBlockParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tpBlockParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 1.9F));
            this.tpBlockParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tpBlockParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3.1F));
            this.tpBlockParent.Controls.Add(this.pBlockList, 1, 1);
            this.tpBlockParent.Controls.Add(this.pBlock, 3, 1);
            this.tpBlockParent.Controls.Add(this.pCode, 5, 1);
            this.tpBlockParent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tpBlockParent.Location = new System.Drawing.Point(0, 0);
            this.tpBlockParent.Name = "tpBlockParent";
            this.tpBlockParent.RowCount = 3;
            this.tpBlockParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.7F));
            this.tpBlockParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.8F));
            this.tpBlockParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.5F));
            this.tpBlockParent.Size = new System.Drawing.Size(1059, 577);
            this.tpBlockParent.TabIndex = 0;
            // 
            // pBlockList
            // 
            this.pBlockList.BackgroundImage = global::SAI.Properties.Resources.p_block;
            this.pBlockList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pBlockList.Controls.Add(this.tpSelectBlockParent);
            this.pBlockList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pBlockList.FillColor = System.Drawing.Color.Transparent;
            this.pBlockList.Location = new System.Drawing.Point(35, 35);
            this.pBlockList.Name = "pBlockList";
            this.pBlockList.Size = new System.Drawing.Size(311, 506);
            this.pBlockList.TabIndex = 0;
            // 
            // tpSelectBlockParent
            // 
            this.tpSelectBlockParent.ColumnCount = 3;
            this.tpSelectBlockParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.144694F));
            this.tpSelectBlockParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 87.78135F));
            this.tpSelectBlockParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.395498F));
            this.tpSelectBlockParent.Controls.Add(this.pSelectBlock, 1, 1);
            this.tpSelectBlockParent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tpSelectBlockParent.Location = new System.Drawing.Point(0, 0);
            this.tpSelectBlockParent.Name = "tpSelectBlockParent";
            this.tpSelectBlockParent.RowCount = 3;
            this.tpSelectBlockParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.743083F));
            this.tpSelectBlockParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89.52569F));
            this.tpSelectBlockParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.60778F));
            this.tpSelectBlockParent.Size = new System.Drawing.Size(311, 506);
            this.tpSelectBlockParent.TabIndex = 0;
            // 
            // pSelectBlock
            // 
            this.pSelectBlock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pSelectBlock.Controls.Add(this.pSelectBlockvScrollBar);
            this.pSelectBlock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pSelectBlock.Location = new System.Drawing.Point(18, 27);
            this.pSelectBlock.Name = "pSelectBlock";
            this.pSelectBlock.Size = new System.Drawing.Size(266, 447);
            this.pSelectBlock.TabIndex = 0;
            // 
            // pSelectBlockvScrollBar
            // 
            this.pSelectBlockvScrollBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.pSelectBlockvScrollBar.InUpdate = false;
            this.pSelectBlockvScrollBar.LargeChange = 10;
            this.pSelectBlockvScrollBar.Location = new System.Drawing.Point(254, 0);
            this.pSelectBlockvScrollBar.Name = "pSelectBlockvScrollBar";
            this.pSelectBlockvScrollBar.ScrollbarSize = 12;
            this.pSelectBlockvScrollBar.Size = new System.Drawing.Size(12, 447);
            this.pSelectBlockvScrollBar.TabIndex = 0;
            // 
            // pBlock
            // 
            this.pBlock.BackgroundImage = global::SAI.Properties.Resources.p_block;
            this.pBlock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pBlock.Controls.Add(this.tpBlocklyParent);
            this.pBlock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pBlock.Location = new System.Drawing.Point(372, 35);
            this.pBlock.Name = "pBlock";
            this.pBlock.Size = new System.Drawing.Size(311, 506);
            this.pBlock.TabIndex = 1;
            // 
            // tpBlocklyParent
            // 
            this.tpBlocklyParent.ColumnCount = 1;
            this.tpBlocklyParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tpBlocklyParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tpBlocklyParent.Controls.Add(this.pTopBlock, 0, 0);
            this.tpBlocklyParent.Controls.Add(this.webViewblock, 0, 1);
            this.tpBlocklyParent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tpBlocklyParent.Location = new System.Drawing.Point(0, 0);
            this.tpBlocklyParent.Name = "tpBlocklyParent";
            this.tpBlocklyParent.RowCount = 2;
            this.tpBlocklyParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.300395F));
            this.tpBlocklyParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 91.69961F));
            this.tpBlocklyParent.Size = new System.Drawing.Size(311, 506);
            this.tpBlocklyParent.TabIndex = 0;
            // 
            // pTopBlock
            // 
            this.pTopBlock.BackgroundImage = global::SAI.Properties.Resources.p_topbar;
            this.pTopBlock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pTopBlock.Controls.Add(this.tpIconParent);
            this.pTopBlock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pTopBlock.Location = new System.Drawing.Point(3, 3);
            this.pTopBlock.Name = "pTopBlock";
            this.pTopBlock.Size = new System.Drawing.Size(305, 36);
            this.pTopBlock.TabIndex = 0;
            // 
            // tpIconParent
            // 
            this.tpIconParent.ColumnCount = 9;
            this.tpIconParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.34914F));
            this.tpIconParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.985058F));
            this.tpIconParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.852874F));
            this.tpIconParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.985058F));
            this.tpIconParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3.209483F));
            this.tpIconParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.69828F));
            this.tpIconParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.71217F));
            this.tpIconParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.856656F));
            this.tpIconParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.351277F));
            this.tpIconParent.Controls.Add(this.btnPreBlock, 1, 1);
            this.tpIconParent.Controls.Add(this.btnTrash, 5, 1);
            this.tpIconParent.Controls.Add(this.btnRunModel, 7, 1);
            this.tpIconParent.Controls.Add(this.btnNextBlock, 3, 1);
            this.tpIconParent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tpIconParent.Location = new System.Drawing.Point(0, 0);
            this.tpIconParent.Name = "tpIconParent";
            this.tpIconParent.RowCount = 3;
            this.tpIconParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tpIconParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tpIconParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tpIconParent.Size = new System.Drawing.Size(305, 36);
            this.tpIconParent.TabIndex = 0;
            // 
            // btnPreBlock
            // 
            this.btnPreBlock.BackgroundImage = global::SAI.Properties.Resources.btn_pre_block1;
            this.btnPreBlock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPreBlock.BorderColor = System.Drawing.Color.Transparent;
            this.btnPreBlock.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnPreBlock.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnPreBlock.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnPreBlock.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnPreBlock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPreBlock.FillColor = System.Drawing.Color.Transparent;
            this.btnPreBlock.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnPreBlock.ForeColor = System.Drawing.Color.White;
            this.btnPreBlock.Location = new System.Drawing.Point(19, 6);
            this.btnPreBlock.Name = "btnPreBlock";
            this.btnPreBlock.Size = new System.Drawing.Size(24, 22);
            this.btnPreBlock.TabIndex = 18;
            // 
            // btnTrash
            // 
            this.btnTrash.BackgroundImage = global::SAI.Properties.Resources.btn_trash_block;
            this.btnTrash.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnTrash.BorderColor = System.Drawing.Color.Transparent;
            this.btnTrash.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnTrash.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnTrash.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnTrash.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnTrash.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTrash.FillColor = System.Drawing.Color.Transparent;
            this.btnTrash.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnTrash.ForeColor = System.Drawing.Color.White;
            this.btnTrash.Location = new System.Drawing.Point(96, 6);
            this.btnTrash.Name = "btnTrash";
            this.btnTrash.Size = new System.Drawing.Size(26, 22);
            this.btnTrash.TabIndex = 16;
            // 
            // btnRunModel
            // 
            this.btnRunModel.BackgroundImage = global::SAI.Properties.Resources.btn_run_model;
            this.btnRunModel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRunModel.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnRunModel.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnRunModel.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnRunModel.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnRunModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRunModel.FillColor = System.Drawing.Color.Transparent;
            this.btnRunModel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnRunModel.ForeColor = System.Drawing.Color.White;
            this.btnRunModel.ImageSize = new System.Drawing.Size(0, 0);
            this.btnRunModel.Location = new System.Drawing.Point(258, 6);
            this.btnRunModel.Name = "btnRunModel";
            this.btnRunModel.Size = new System.Drawing.Size(24, 22);
            this.btnRunModel.TabIndex = 17;
            // 
            // btnNextBlock
            // 
            this.btnNextBlock.BackgroundImage = global::SAI.Properties.Resources.btn_next_block1;
            this.btnNextBlock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnNextBlock.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnNextBlock.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnNextBlock.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnNextBlock.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnNextBlock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNextBlock.FillColor = System.Drawing.Color.Transparent;
            this.btnNextBlock.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnNextBlock.ForeColor = System.Drawing.Color.White;
            this.btnNextBlock.Location = new System.Drawing.Point(57, 6);
            this.btnNextBlock.Name = "btnNextBlock";
            this.btnNextBlock.Size = new System.Drawing.Size(24, 22);
            this.btnNextBlock.TabIndex = 21;
            // 
            // webViewblock
            // 
            this.webViewblock.AllowExternalDrop = true;
            this.webViewblock.CreationProperties = null;
            this.webViewblock.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webViewblock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webViewblock.Location = new System.Drawing.Point(3, 45);
            this.webViewblock.Name = "webViewblock";
            this.webViewblock.Size = new System.Drawing.Size(305, 458);
            this.webViewblock.TabIndex = 1;
            this.webViewblock.ZoomFactor = 1D;
            // 
            // pCode
            // 
            this.pCode.BackgroundImage = global::SAI.Properties.Resources.p_block;
            this.pCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pCode.Controls.Add(this.tpCodeParent);
            this.pCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pCode.Location = new System.Drawing.Point(709, 35);
            this.pCode.Name = "pCode";
            this.pCode.Size = new System.Drawing.Size(311, 506);
            this.pCode.TabIndex = 2;
            // 
            // tpCodeParent
            // 
            this.tpCodeParent.ColumnCount = 1;
            this.tpCodeParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tpCodeParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tpCodeParent.Controls.Add(this.guna2Panel1, 0, 0);
            this.tpCodeParent.Controls.Add(this.webView2Code, 0, 1);
            this.tpCodeParent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tpCodeParent.Location = new System.Drawing.Point(0, 0);
            this.tpCodeParent.Name = "tpCodeParent";
            this.tpCodeParent.RowCount = 2;
            this.tpCodeParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.300395F));
            this.tpCodeParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 91.69961F));
            this.tpCodeParent.Size = new System.Drawing.Size(311, 506);
            this.tpCodeParent.TabIndex = 1;
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackgroundImage = global::SAI.Properties.Resources.p_topbar;
            this.guna2Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel1.Location = new System.Drawing.Point(3, 3);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(305, 36);
            this.guna2Panel1.TabIndex = 0;
            // 
            // webView2Code
            // 
            this.webView2Code.AllowExternalDrop = true;
            this.webView2Code.CreationProperties = null;
            this.webView2Code.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView2Code.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webView2Code.Location = new System.Drawing.Point(3, 45);
            this.webView2Code.Name = "webView2Code";
            this.webView2Code.Size = new System.Drawing.Size(305, 458);
            this.webView2Code.TabIndex = 1;
            this.webView2Code.ZoomFactor = 1D;
            // 
            // tpHomeParent
            // 
            this.tpHomeParent.ColumnCount = 4;
            this.tpHomeParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3.470716F));
            this.tpHomeParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.249292F));
            this.tpHomeParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.554296F));
            this.tpHomeParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 84.8914F));
            this.tpHomeParent.Controls.Add(this.btnHome, 1, 1);
            this.tpHomeParent.Controls.Add(this.autoSizeLabel1, 2, 1);
            this.tpHomeParent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tpHomeParent.Location = new System.Drawing.Point(151, 3);
            this.tpHomeParent.Name = "tpHomeParent";
            this.tpHomeParent.RowCount = 3;
            this.tpHomeParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.12195F));
            this.tpHomeParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 54.87805F));
            this.tpHomeParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 11F));
            this.tpHomeParent.Size = new System.Drawing.Size(1059, 94);
            this.tpHomeParent.TabIndex = 1;
            // 
            // btnHome
            // 
            this.btnHome.BackgroundImage = global::SAI.Properties.Resources.btn_home;
            this.btnHome.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnHome.BorderColor = System.Drawing.Color.Transparent;
            this.btnHome.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnHome.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnHome.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnHome.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnHome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnHome.FillColor = System.Drawing.Color.Transparent;
            this.btnHome.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnHome.ForeColor = System.Drawing.Color.White;
            this.btnHome.Location = new System.Drawing.Point(39, 40);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(38, 39);
            this.btnHome.TabIndex = 0;
            // 
            // autoSizeLabel1
            // 
            this.autoSizeLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.autoSizeLabel1.Font = new System.Drawing.Font("Noto Sans KR Medium", 13.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.autoSizeLabel1.Location = new System.Drawing.Point(83, 40);
            this.autoSizeLabel1.Name = "autoSizeLabel1";
            this.autoSizeLabel1.Size = new System.Drawing.Size(73, 39);
            this.autoSizeLabel1.TabIndex = 1;
            this.autoSizeLabel1.Text = "실습";
            this.autoSizeLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tpInferParent
            // 
            this.tpInferParent.ColumnCount = 2;
            this.tpInferParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.5082F));
            this.tpInferParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.49181F));
            this.tpInferParent.Controls.Add(this.btnStartInfer, 1, 1);
            this.tpInferParent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tpInferParent.Location = new System.Drawing.Point(1216, 103);
            this.tpInferParent.Name = "tpInferParent";
            this.tpInferParent.RowCount = 3;
            this.tpInferParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.372617F));
            this.tpInferParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27.72964F));
            this.tpInferParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 67.07106F));
            this.tpInferParent.Size = new System.Drawing.Size(61, 577);
            this.tpInferParent.TabIndex = 2;
            // 
            // btnStartInfer
            // 
            this.btnStartInfer.BackgroundImage = global::SAI.Properties.Resources.btn_infer;
            this.btnStartInfer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnStartInfer.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnStartInfer.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnStartInfer.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnStartInfer.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnStartInfer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStartInfer.FillColor = System.Drawing.Color.Transparent;
            this.btnStartInfer.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnStartInfer.ForeColor = System.Drawing.Color.White;
            this.btnStartInfer.Location = new System.Drawing.Point(20, 33);
            this.btnStartInfer.Name = "btnStartInfer";
            this.btnStartInfer.Size = new System.Drawing.Size(38, 153);
            this.btnStartInfer.TabIndex = 0;
            // 
            // tpMemoParent
            // 
            this.tpMemoParent.BackColor = System.Drawing.Color.Transparent;
            this.tpMemoParent.ColumnCount = 3;
            this.tpMemoParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.87324F));
            this.tpMemoParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.77465F));
            this.tpMemoParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.64789F));
            this.tpMemoParent.Controls.Add(this.btnMemo, 1, 1);
            this.tpMemoParent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tpMemoParent.Location = new System.Drawing.Point(3, 103);
            this.tpMemoParent.Name = "tpMemoParent";
            this.tpMemoParent.RowCount = 3;
            this.tpMemoParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 84.7487F));
            this.tpMemoParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.26516F));
            this.tpMemoParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.812825F));
            this.tpMemoParent.Size = new System.Drawing.Size(142, 577);
            this.tpMemoParent.TabIndex = 3;
            // 
            // btnMemo
            // 
            this.btnMemo.BackgroundImage = global::SAI.Properties.Resources.btn_memo;
            this.btnMemo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnMemo.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnMemo.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnMemo.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnMemo.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnMemo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMemo.FillColor = System.Drawing.Color.Transparent;
            this.btnMemo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnMemo.ForeColor = System.Drawing.Color.White;
            this.btnMemo.Location = new System.Drawing.Point(44, 492);
            this.btnMemo.Name = "btnMemo";
            this.btnMemo.Size = new System.Drawing.Size(59, 59);
            this.btnMemo.TabIndex = 0;
            // 
            // DynamicUcPracticeBlockCode
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.tpParent);
            this.Name = "DynamicUcPracticeBlockCode";
            this.Size = new System.Drawing.Size(1280, 720);
            this.tpParent.ResumeLayout(false);
            this.pMain.ResumeLayout(false);
            this.tpBlockParent.ResumeLayout(false);
            this.pBlockList.ResumeLayout(false);
            this.tpSelectBlockParent.ResumeLayout(false);
            this.pSelectBlock.ResumeLayout(false);
            this.pBlock.ResumeLayout(false);
            this.tpBlocklyParent.ResumeLayout(false);
            this.pTopBlock.ResumeLayout(false);
            this.tpIconParent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.webViewblock)).EndInit();
            this.pCode.ResumeLayout(false);
            this.tpCodeParent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.webView2Code)).EndInit();
            this.tpHomeParent.ResumeLayout(false);
            this.tpInferParent.ResumeLayout(false);
            this.tpMemoParent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tpParent;
        private Guna.UI2.WinForms.Guna2Panel pMain;
        private System.Windows.Forms.TableLayoutPanel tpBlockParent;
        private Guna.UI2.WinForms.Guna2Panel pBlockList;
        private Guna.UI2.WinForms.Guna2Panel pBlock;
        private Guna.UI2.WinForms.Guna2Panel pCode;
        private System.Windows.Forms.TableLayoutPanel tpHomeParent;
        private Guna.UI2.WinForms.Guna2Button btnHome;
        private SAI.App.Views.Pages.AutoSizeLabel autoSizeLabel1;
        private Guna.UI2.WinForms.Guna2Button btnStartInfer;
        private System.Windows.Forms.TableLayoutPanel tpInferParent;
        private System.Windows.Forms.TableLayoutPanel tpSelectBlockParent;
        private Guna.UI2.WinForms.Guna2Panel pSelectBlock;
        private System.Windows.Forms.TableLayoutPanel tpBlocklyParent;
        private Guna.UI2.WinForms.Guna2Panel pTopBlock;
        private System.Windows.Forms.TableLayoutPanel tpCodeParent;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Microsoft.Web.WebView2.WinForms.WebView2 webViewblock;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView2Code;
        private System.Windows.Forms.TableLayoutPanel tpMemoParent;
        private Guna.UI2.WinForms.Guna2Button btnMemo;
        private System.Windows.Forms.TableLayoutPanel tpIconParent;
        private Guna.UI2.WinForms.Guna2Button btnTrash;
        private Guna.UI2.WinForms.Guna2Button btnRunModel;
        private Guna.UI2.WinForms.Guna2Button btnPreBlock;
        private Guna.UI2.WinForms.Guna2Button btnNextBlock;
        private Guna.UI2.WinForms.Guna2VScrollBar pSelectBlockvScrollBar;
    }
}
