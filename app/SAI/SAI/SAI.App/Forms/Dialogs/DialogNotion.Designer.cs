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
            this.tboxSecretKey = new Guna.UI2.WinForms.Guna2TextBox();
            this.ibtnEnter = new Guna.UI2.WinForms.Guna2ImageButton();
            this.pSecretKey = new Guna.UI2.WinForms.Guna2Panel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.authButton = new Guna.UI2.WinForms.Guna2Button();
            this.pTitleBar.SuspendLayout();
            this.pInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webView2)).BeginInit();
            this.pSecretKey.SuspendLayout();
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
            this.pTitleBar.Size = new System.Drawing.Size(1591, 45);
            this.pTitleBar.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BackgroundImage = global::SAI.Properties.Resources.btn_close_zoomChart;
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
            this.btnClose.Location = new System.Drawing.Point(1511, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(80, 45);
            this.btnClose.TabIndex = 3;
            // 
            // pInfo
            // 
            this.pInfo.Controls.Add(this.lblInfo);
            this.pInfo.Controls.Add(this.webView2);
            this.pInfo.Location = new System.Drawing.Point(60, 165);
            this.pInfo.Name = "pInfo";
            this.pInfo.Size = new System.Drawing.Size(1471, 650);
            this.pInfo.TabIndex = 1;
            // 
            // lblInfo
            // 
            this.lblInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblInfo.Font = new System.Drawing.Font("Noto Sans KR Medium", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblInfo.ForeColor = System.Drawing.Color.White;
            this.lblInfo.Location = new System.Drawing.Point(449, 305);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(572, 41);
            this.lblInfo.TabIndex = 3;
            this.lblInfo.Text = "코드를 입력해야 AI 보고서를 확인하실 수 있습니다.";
            // 
            // webView2
            // 
            this.webView2.AllowExternalDrop = true;
            this.webView2.CreationProperties = null;
            this.webView2.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView2.Location = new System.Drawing.Point(0, 0);
            this.webView2.Name = "webView2";
            this.webView2.Size = new System.Drawing.Size(1471, 650);
            this.webView2.TabIndex = 2;
            this.webView2.Visible = false;
            this.webView2.ZoomFactor = 1D;
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
            this.tboxSecretKey.Font = new System.Drawing.Font("Noto Sans KR Medium", 7.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tboxSecretKey.ForeColor = System.Drawing.Color.Black;
            this.tboxSecretKey.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tboxSecretKey.Location = new System.Drawing.Point(3, 2);
            this.tboxSecretKey.Margin = new System.Windows.Forms.Padding(0);
            this.tboxSecretKey.Name = "tboxSecretKey";
            this.tboxSecretKey.PlaceholderForeColor = System.Drawing.Color.White;
            this.tboxSecretKey.PlaceholderText = "";
            this.tboxSecretKey.SelectedText = "";
            this.tboxSecretKey.Size = new System.Drawing.Size(222, 35);
            this.tboxSecretKey.TabIndex = 3;
            // 
            // ibtnEnter
            // 
            this.ibtnEnter.BackgroundImage = global::SAI.Properties.Resources.btn_enter;
            this.ibtnEnter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ibtnEnter.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnEnter.HoverState.Image = global::SAI.Properties.Resources.btn_enter_hover;
            this.ibtnEnter.HoverState.ImageSize = new System.Drawing.Size(80, 38);
            this.ibtnEnter.Image = ((System.Drawing.Image)(resources.GetObject("ibtnEnter.Image")));
            this.ibtnEnter.ImageOffset = new System.Drawing.Point(0, 0);
            this.ibtnEnter.ImageRotate = 0F;
            this.ibtnEnter.ImageSize = new System.Drawing.Size(0, 0);
            this.ibtnEnter.Location = new System.Drawing.Point(379, 98);
            this.ibtnEnter.Name = "ibtnEnter";
            this.ibtnEnter.PressedState.ImageSize = new System.Drawing.Size(80, 38);
            this.ibtnEnter.Size = new System.Drawing.Size(80, 38);
            this.ibtnEnter.TabIndex = 4;
            this.ibtnEnter.Click += new System.EventHandler(this.ibtnEnter_Click);
            // 
            // pSecretKey
            // 
            this.pSecretKey.BackgroundImage = global::SAI.Properties.Resources.tbox_secretkey;
            this.pSecretKey.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pSecretKey.Controls.Add(this.tboxSecretKey);
            this.pSecretKey.Location = new System.Drawing.Point(151, 98);
            this.pSecretKey.Name = "pSecretKey";
            this.pSecretKey.Size = new System.Drawing.Size(228, 38);
            this.pSecretKey.TabIndex = 5;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(491, 98);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(200, 38);
            this.progressBar.TabIndex = 6;
            // 
            // authButton
            // 
            this.authButton.BackColor = System.Drawing.Color.Transparent;
            this.authButton.BackgroundImage = global::SAI.Properties.Resources.btn_auth;
            this.authButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.authButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.authButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.authButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.authButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.authButton.FillColor = System.Drawing.Color.Transparent;
            this.authButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.authButton.ForeColor = System.Drawing.Color.White;
            this.authButton.Location = new System.Drawing.Point(1279, 89);
            this.authButton.Name = "authButton";
            this.authButton.Size = new System.Drawing.Size(252, 51);
            this.authButton.TabIndex = 8;
            this.authButton.Visible = false;
            this.authButton.Click += new System.EventHandler(this.authButton_Click);
            // 
            // DialogNotion
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::SAI.Properties.Resources.bg_dialog_white_notion;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1591, 875);
            this.Controls.Add(this.authButton);
            this.Controls.Add(this.ibtnEnter);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.pSecretKey);
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
            this.pSecretKey.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pTitleBar;
        private Guna.UI2.WinForms.Guna2Panel pInfo;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView2;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblInfo;
        private Guna.UI2.WinForms.Guna2TextBox tboxSecretKey;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnEnter;
        private Guna.UI2.WinForms.Guna2Panel pSecretKey;
        private System.Windows.Forms.ProgressBar progressBar;
        private Guna.UI2.WinForms.Guna2Button btnClose;
        private Guna.UI2.WinForms.Guna2Button authButton;
    }
}