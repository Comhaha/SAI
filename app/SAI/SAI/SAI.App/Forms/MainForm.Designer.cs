namespace SAI
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			this.mainPanel = new Guna.UI2.WinForms.Guna2Panel();
			this.guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
			this.titlebar = new Guna.UI2.WinForms.Guna2Panel();
			this.pTitlebarRight = new Guna.UI2.WinForms.Guna2Panel();
			this.btnSetting = new Guna.UI2.WinForms.Guna2Button();
			this.btnMinScreen = new Guna.UI2.WinForms.Guna2Button();
			this.btnFullScreen = new Guna.UI2.WinForms.Guna2Button();
			this.btnClose = new Guna.UI2.WinForms.Guna2Button();
			this.pTitlebarTitle = new Guna.UI2.WinForms.Guna2Panel();
			this.titlebar.SuspendLayout();
			this.pTitlebarRight.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainPanel
			// 
			this.mainPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.mainPanel.BackColor = System.Drawing.Color.Transparent;
			this.mainPanel.Location = new System.Drawing.Point(0, 30);
			this.mainPanel.Margin = new System.Windows.Forms.Padding(0);
			this.mainPanel.MinimumSize = new System.Drawing.Size(1280, 720);
			this.mainPanel.Name = "mainPanel";
			this.mainPanel.Size = new System.Drawing.Size(1280, 720);
			this.mainPanel.TabIndex = 5;
			// 
			// guna2DragControl1
			// 
			this.guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
			this.guna2DragControl1.UseTransparentDrag = true;
			// 
			// titlebar
			// 
			this.titlebar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.titlebar.BackColor = System.Drawing.Color.Transparent;
			this.titlebar.BackgroundImage = global::SAI.Properties.Resources.titlebar1;
			this.titlebar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.titlebar.Controls.Add(this.pTitlebarRight);
			this.titlebar.Controls.Add(this.pTitlebarTitle);
			this.titlebar.Location = new System.Drawing.Point(0, 0);
			this.titlebar.Name = "titlebar";
			this.titlebar.Size = new System.Drawing.Size(1280, 30);
			this.titlebar.TabIndex = 6;
			// 
			// pTitlebarRight
			// 
			this.pTitlebarRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pTitlebarRight.BackgroundImage = global::SAI.Properties.Resources.bg_titlebar_right;
			this.pTitlebarRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.pTitlebarRight.Controls.Add(this.btnSetting);
			this.pTitlebarRight.Controls.Add(this.btnMinScreen);
			this.pTitlebarRight.Controls.Add(this.btnFullScreen);
			this.pTitlebarRight.Controls.Add(this.btnClose);
			this.pTitlebarRight.Location = new System.Drawing.Point(1150, 0);
			this.pTitlebarRight.Name = "pTitlebarRight";
			this.pTitlebarRight.Size = new System.Drawing.Size(130, 30);
			this.pTitlebarRight.TabIndex = 2;
			// 
			// btnSetting
			// 
			this.btnSetting.BackgroundImage = global::SAI.Properties.Resources.btn_titlebar_setting;
			this.btnSetting.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.btnSetting.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnSetting.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnSetting.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnSetting.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnSetting.FillColor = System.Drawing.Color.Transparent;
			this.btnSetting.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.btnSetting.ForeColor = System.Drawing.Color.White;
			this.btnSetting.Location = new System.Drawing.Point(0, 0);
			this.btnSetting.Margin = new System.Windows.Forms.Padding(0);
			this.btnSetting.Name = "btnSetting";
			this.btnSetting.Size = new System.Drawing.Size(30, 30);
			this.btnSetting.TabIndex = 0;
			this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
			// 
			// btnMinScreen
			// 
			this.btnMinScreen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnMinScreen.BackgroundImage = global::SAI.Properties.Resources.btn_titlebar_minscreen;
			this.btnMinScreen.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnMinScreen.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnMinScreen.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnMinScreen.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnMinScreen.FillColor = System.Drawing.Color.Transparent;
			this.btnMinScreen.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.btnMinScreen.ForeColor = System.Drawing.Color.White;
			this.btnMinScreen.Location = new System.Drawing.Point(40, 0);
			this.btnMinScreen.Margin = new System.Windows.Forms.Padding(0);
			this.btnMinScreen.Name = "btnMinScreen";
			this.btnMinScreen.Size = new System.Drawing.Size(30, 30);
			this.btnMinScreen.TabIndex = 1;
			this.btnMinScreen.Click += new System.EventHandler(this.btnMinScreen_Click);
			// 
			// btnFullScreen
			// 
			this.btnFullScreen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFullScreen.BackgroundImage = global::SAI.Properties.Resources.btn_titlebar_fullscreen;
			this.btnFullScreen.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnFullScreen.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnFullScreen.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnFullScreen.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnFullScreen.FillColor = System.Drawing.Color.Transparent;
			this.btnFullScreen.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.btnFullScreen.ForeColor = System.Drawing.Color.White;
			this.btnFullScreen.Location = new System.Drawing.Point(70, 0);
			this.btnFullScreen.Margin = new System.Windows.Forms.Padding(0);
			this.btnFullScreen.Name = "btnFullScreen";
			this.btnFullScreen.Size = new System.Drawing.Size(30, 30);
			this.btnFullScreen.TabIndex = 0;
			this.btnFullScreen.Click += new System.EventHandler(this.btnFullScreen_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.BackgroundImage = global::SAI.Properties.Resources.btn_titlebar_close;
			this.btnClose.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnClose.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnClose.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnClose.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnClose.FillColor = System.Drawing.Color.Transparent;
			this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.btnClose.ForeColor = System.Drawing.Color.White;
			this.btnClose.Location = new System.Drawing.Point(100, 0);
			this.btnClose.Margin = new System.Windows.Forms.Padding(0);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(30, 30);
			this.btnClose.TabIndex = 0;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// pTitlebarTitle
			// 
			this.pTitlebarTitle.BackgroundImage = global::SAI.Properties.Resources.bg_titlebar_title;
			this.pTitlebarTitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.pTitlebarTitle.Location = new System.Drawing.Point(0, 0);
			this.pTitlebarTitle.Name = "pTitlebarTitle";
			this.pTitlebarTitle.Size = new System.Drawing.Size(50, 30);
			this.pTitlebarTitle.TabIndex = 0;
			// 
			// MainForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackgroundImage = global::SAI.Properties.Resources.img_background;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.ClientSize = new System.Drawing.Size(1280, 750);
			this.Controls.Add(this.titlebar);
			this.Controls.Add(this.mainPanel);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Margin = new System.Windows.Forms.Padding(2);
			this.MinimumSize = new System.Drawing.Size(1280, 750);
			this.Name = "MainForm";
			this.Text = "메인페이지";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.titlebar.ResumeLayout(false);
			this.pTitlebarRight.ResumeLayout(false);
			this.ResumeLayout(false);

        }

		#endregion
		private Guna.UI2.WinForms.Guna2Panel mainPanel;
		private Guna.UI2.WinForms.Guna2Panel titlebar;
		private Guna.UI2.WinForms.Guna2Button btnFullScreen;
		private Guna.UI2.WinForms.Guna2Button btnClose;
		private Guna.UI2.WinForms.Guna2Button btnMinScreen;
		private Guna.UI2.WinForms.Guna2Button btnSetting;
		private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
		private Guna.UI2.WinForms.Guna2Panel pTitlebarTitle;
		private Guna.UI2.WinForms.Guna2Panel pTitlebarRight;
	}
}
