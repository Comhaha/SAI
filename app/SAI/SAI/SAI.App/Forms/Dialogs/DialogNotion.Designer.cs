namespace SAI.SAI.App.Forms.Dialogs
{
    partial class DialogNotion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogNotion));
            this.pTitleBar = new Guna.UI2.WinForms.Guna2Panel();
            this.btnClose = new Guna.UI2.WinForms.Guna2Button();
            this.pInfo = new Guna.UI2.WinForms.Guna2Panel();
            this.lblInfo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.webView2 = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.lblCode = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.tboxSecretKey = new Guna.UI2.WinForms.Guna2TextBox();
            this.ibtnEnter = new Guna.UI2.WinForms.Guna2ImageButton();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.authButton = new Guna.UI2.WinForms.Guna2Button();
            this.pTitleBar.SuspendLayout();
            this.pInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webView2)).BeginInit();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pTitleBar
            // 
            this.pTitleBar.BackgroundImage = global::SAI.Properties.Resources.bg_white_titlebar_notion1;
            this.pTitleBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pTitleBar.Controls.Add(this.btnClose);
            this.pTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTitleBar.Location = new System.Drawing.Point(0, 0);
            this.pTitleBar.Name = "pTitleBar";
            this.pTitleBar.Size = new System.Drawing.Size(1060, 40);
            this.pTitleBar.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BackgroundImage = global::SAI.Properties.Resources.btn_close_white;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.BorderColor = System.Drawing.Color.Transparent;
            this.btnClose.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnClose.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnClose.FillColor = System.Drawing.Color.Transparent;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnClose.ImageOffset = new System.Drawing.Point(8, 0);
            this.btnClose.ImageSize = new System.Drawing.Size(15, 16);
            this.btnClose.Location = new System.Drawing.Point(1000, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(60, 40);
            this.btnClose.TabIndex = 3;
            // 
            // pInfo
            // 
            this.pInfo.Controls.Add(this.lblInfo);
            this.pInfo.Controls.Add(this.webView2);
            this.pInfo.Location = new System.Drawing.Point(45, 115);
            this.pInfo.Name = "pInfo";
            this.pInfo.Size = new System.Drawing.Size(980, 433);
            this.pInfo.TabIndex = 1;
            // 
            // lblInfo
            // 
            this.lblInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblInfo.Font = new System.Drawing.Font("Noto Sans KR Medium", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblInfo.ForeColor = System.Drawing.Color.White;
            this.lblInfo.Location = new System.Drawing.Point(204, 196);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(368, 27);
            this.lblInfo.TabIndex = 3;
            this.lblInfo.Text = "코드를 입력해야 AI 보고서를 확인하실 수 있습니다.";
            // 
            // webView2
            // 
            this.webView2.AllowExternalDrop = true;
            this.webView2.CreationProperties = null;
            this.webView2.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView2.Location = new System.Drawing.Point(0, -3);
            this.webView2.Name = "webView2";
            this.webView2.Size = new System.Drawing.Size(980, 433);
            this.webView2.TabIndex = 2;
            this.webView2.Visible = false;
            this.webView2.ZoomFactor = 1D;
            // 
            // lblCode
            // 
            this.lblCode.BackColor = System.Drawing.Color.Transparent;
            this.lblCode.Font = new System.Drawing.Font("Noto Sans KR Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblCode.Location = new System.Drawing.Point(66, 61);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(39, 31);
            this.lblCode.TabIndex = 2;
            this.lblCode.Text = "코드";
            // 
            // tboxSecretKey
            // 
            this.tboxSecretKey.BackColor = System.Drawing.Color.Transparent;
            this.tboxSecretKey.BorderColor = System.Drawing.Color.Transparent;
            this.tboxSecretKey.BorderThickness = 0;
            this.tboxSecretKey.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tboxSecretKey.DefaultText = "";
            this.tboxSecretKey.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.tboxSecretKey.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.tboxSecretKey.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tboxSecretKey.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tboxSecretKey.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tboxSecretKey.Font = new System.Drawing.Font("Noto Sans KR Medium", 4.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tboxSecretKey.ForeColor = System.Drawing.Color.Black;
            this.tboxSecretKey.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tboxSecretKey.Location = new System.Drawing.Point(4, 4);
            this.tboxSecretKey.Margin = new System.Windows.Forms.Padding(4);
            this.tboxSecretKey.Name = "tboxSecretKey";
            this.tboxSecretKey.PlaceholderForeColor = System.Drawing.Color.White;
            this.tboxSecretKey.PlaceholderText = "";
            this.tboxSecretKey.SelectedText = "";
            this.tboxSecretKey.Size = new System.Drawing.Size(144, 17);
            this.tboxSecretKey.TabIndex = 3;
            // 
            // ibtnEnter
            // 
            this.ibtnEnter.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnEnter.HoverState.Image = global::SAI.Properties.Resources.btn_enter_hover;
            this.ibtnEnter.HoverState.ImageSize = new System.Drawing.Size(53, 25);
            this.ibtnEnter.Image = global::SAI.Properties.Resources.btn_enter;
            this.ibtnEnter.ImageOffset = new System.Drawing.Point(0, 0);
            this.ibtnEnter.ImageRotate = 0F;
            this.ibtnEnter.ImageSize = new System.Drawing.Size(53, 25);
            this.ibtnEnter.Location = new System.Drawing.Point(288, 65);
            this.ibtnEnter.Name = "ibtnEnter";
            this.ibtnEnter.PressedState.ImageSize = new System.Drawing.Size(53, 25);
            this.ibtnEnter.Size = new System.Drawing.Size(53, 25);
            this.ibtnEnter.TabIndex = 4;
            this.ibtnEnter.Click += new System.EventHandler(this.ibtnEnter_Click);
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackgroundImage = global::SAI.Properties.Resources.tbox_secretkey;
            this.guna2Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.guna2Panel1.Controls.Add(this.tboxSecretKey);
            this.guna2Panel1.Location = new System.Drawing.Point(141, 65);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(152, 25);
            this.guna2Panel1.TabIndex = 5;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(376, 65);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(167, 21);
            this.progressBar.TabIndex = 6;
            // 
            // authButton
            // 
            this.authButton.BackColor = System.Drawing.Color.Transparent;
            this.authButton.BackgroundImage = global::SAI.Properties.Resources.btn_auth;
            this.authButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.authButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.authButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.authButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.authButton.FillColor = System.Drawing.Color.Transparent;
            this.authButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.authButton.ForeColor = System.Drawing.Color.White;
            this.authButton.Location = new System.Drawing.Point(857, 57);
            this.authButton.Name = "authButton";
            this.authButton.Size = new System.Drawing.Size(168, 34);
            this.authButton.TabIndex = 8;
            this.authButton.Visible = false;
            this.authButton.Click += new System.EventHandler(this.authButton_Click);
            // 
            // DialogNotion
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1060, 583);
            this.Controls.Add(this.authButton);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.ibtnEnter);
            this.Controls.Add(this.lblCode);
            this.Controls.Add(this.pInfo);
            this.Controls.Add(this.pTitleBar);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "DialogNotion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.DialogNotion_Load);
            this.pTitleBar.ResumeLayout(false);
            this.pInfo.ResumeLayout(false);
            this.pInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webView2)).EndInit();
            this.guna2Panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pTitleBar;
        private Guna.UI2.WinForms.Guna2Panel pInfo;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView2;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblInfo;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblCode;
        private Guna.UI2.WinForms.Guna2TextBox tboxSecretKey;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnEnter;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.ProgressBar progressBar;
        private Guna.UI2.WinForms.Guna2Button btnClose;
        private Guna.UI2.WinForms.Guna2Button authButton;
    }
}