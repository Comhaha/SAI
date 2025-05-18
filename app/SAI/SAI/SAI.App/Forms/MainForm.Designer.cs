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
            this.btnSetting = new Guna.UI2.WinForms.Guna2Button();
            this.btnMinScreen = new Guna.UI2.WinForms.Guna2Button();
            this.btnClose = new Guna.UI2.WinForms.Guna2Button();
            this.mainPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.tpParent = new System.Windows.Forms.TableLayoutPanel();
            this.tpTitlebar = new System.Windows.Forms.TableLayoutPanel();
            this.tpTitlebarBtn = new System.Windows.Forms.TableLayoutPanel();
            this.btnFullScreen = new Guna.UI2.WinForms.Guna2Button();
            this.tpParent.SuspendLayout();
            this.tpTitlebar.SuspendLayout();
            this.tpTitlebarBtn.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSetting
            // 
            this.btnSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetting.BackgroundImage = global::SAI.Properties.Resources.btn_titlebar_setting;
            this.btnSetting.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSetting.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSetting.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSetting.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSetting.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSetting.FillColor = System.Drawing.Color.Transparent;
            this.btnSetting.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSetting.ForeColor = System.Drawing.Color.Black;
            this.btnSetting.Location = new System.Drawing.Point(0, 0);
            this.btnSetting.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(60, 64);
            this.btnSetting.TabIndex = 0;
            // 
            // btnMinScreen
            // 
            this.btnMinScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinScreen.BackgroundImage = global::SAI.Properties.Resources.btn_titlebar_minscreen;
            this.btnMinScreen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnMinScreen.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnMinScreen.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnMinScreen.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnMinScreen.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnMinScreen.FillColor = System.Drawing.Color.Transparent;
            this.btnMinScreen.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnMinScreen.ForeColor = System.Drawing.Color.White;
            this.btnMinScreen.Location = new System.Drawing.Point(60, 0);
            this.btnMinScreen.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.btnMinScreen.Name = "btnMinScreen";
            this.btnMinScreen.Size = new System.Drawing.Size(60, 64);
            this.btnMinScreen.TabIndex = 1;
            this.btnMinScreen.Click += new System.EventHandler(this.btnMinScreen_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BackgroundImage = global::SAI.Properties.Resources.btn_titlebar_close;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnClose.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnClose.FillColor = System.Drawing.Color.Transparent;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(180, 0);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(63, 64);
            this.btnClose.TabIndex = 0;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // mainPanel
            // 
            this.mainPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.mainPanel.AutoScroll = true;
            this.mainPanel.BackColor = System.Drawing.Color.White;
            this.mainPanel.Location = new System.Drawing.Point(335, 309);
            this.mainPanel.Margin = new System.Windows.Forms.Padding(0);
            this.mainPanel.MinimumSize = new System.Drawing.Size(1920, 1080);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1920, 1080);
            this.mainPanel.TabIndex = 5;
            // 
            // guna2DragControl1
            // 
            this.guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2DragControl1.UseTransparentDrag = true;
            // 
            // tpParent
            // 
            this.tpParent.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tpParent.ColumnCount = 1;
            this.tpParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tpParent.Controls.Add(this.tpTitlebar, 0, 0);
            this.tpParent.Controls.Add(this.mainPanel, 0, 1);
            this.tpParent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tpParent.Location = new System.Drawing.Point(0, 0);
            this.tpParent.Margin = new System.Windows.Forms.Padding(0);
            this.tpParent.Name = "tpParent";
            this.tpParent.RowCount = 2;
            this.tpParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.2F));
            this.tpParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95.8F));
            this.tpParent.Size = new System.Drawing.Size(2590, 1630);
            this.tpParent.TabIndex = 0;
            // 
            // tpTitlebar
            // 
            this.tpTitlebar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tpTitlebar.BackgroundImage = global::SAI.Properties.Resources.titlebar1;
            this.tpTitlebar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tpTitlebar.ColumnCount = 2;
            this.tpTitlebar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90.625F));
            this.tpTitlebar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.375F));
            this.tpTitlebar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tpTitlebar.Controls.Add(this.tpTitlebarBtn, 1, 0);
            this.tpTitlebar.Location = new System.Drawing.Point(0, 0);
            this.tpTitlebar.Margin = new System.Windows.Forms.Padding(0);
            this.tpTitlebar.Name = "tpTitlebar";
            this.tpTitlebar.RowCount = 1;
            this.tpTitlebar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tpTitlebar.Size = new System.Drawing.Size(2590, 68);
            this.tpTitlebar.TabIndex = 3;
            // 
            // tpTitlebarBtn
            // 
            this.tpTitlebarBtn.BackColor = System.Drawing.Color.Transparent;
            this.tpTitlebarBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.tpTitlebarBtn.ColumnCount = 4;
            this.tpTitlebarBtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tpTitlebarBtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tpTitlebarBtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tpTitlebarBtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tpTitlebarBtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tpTitlebarBtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tpTitlebarBtn.Controls.Add(this.btnFullScreen, 2, 0);
            this.tpTitlebarBtn.Controls.Add(this.btnClose, 3, 0);
            this.tpTitlebarBtn.Controls.Add(this.btnMinScreen, 1, 0);
            this.tpTitlebarBtn.Controls.Add(this.btnSetting, 0, 0);
            this.tpTitlebarBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tpTitlebarBtn.Location = new System.Drawing.Point(2347, 0);
            this.tpTitlebarBtn.Margin = new System.Windows.Forms.Padding(0);
            this.tpTitlebarBtn.Name = "tpTitlebarBtn";
            this.tpTitlebarBtn.RowCount = 1;
            this.tpTitlebarBtn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tpTitlebarBtn.Size = new System.Drawing.Size(243, 68);
            this.tpTitlebarBtn.TabIndex = 1;
            // 
            // btnFullScreen
            // 
            this.btnFullScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFullScreen.BackgroundImage = global::SAI.Properties.Resources.btn_titlebar_fullscreen;
            this.btnFullScreen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnFullScreen.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnFullScreen.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnFullScreen.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnFullScreen.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnFullScreen.FillColor = System.Drawing.Color.Transparent;
            this.btnFullScreen.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnFullScreen.ForeColor = System.Drawing.Color.White;
            this.btnFullScreen.Location = new System.Drawing.Point(120, 0);
            this.btnFullScreen.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.btnFullScreen.Name = "btnFullScreen";
            this.btnFullScreen.Size = new System.Drawing.Size(60, 64);
            this.btnFullScreen.TabIndex = 2;
            this.btnFullScreen.Click += new System.EventHandler(this.btnFullScreen_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(2590, 1630);
            this.Controls.Add(this.tpParent);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(2558, 1502);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "메인페이지";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tpParent.ResumeLayout(false);
            this.tpTitlebar.ResumeLayout(false);
            this.tpTitlebarBtn.ResumeLayout(false);
            this.ResumeLayout(false);

        }

		#endregion
		private Guna.UI2.WinForms.Guna2Panel mainPanel;
		private Guna.UI2.WinForms.Guna2Button btnClose;
		private Guna.UI2.WinForms.Guna2Button btnMinScreen;
		private Guna.UI2.WinForms.Guna2Button btnSetting;
		private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
		private System.Windows.Forms.TableLayoutPanel tpParent;
		private System.Windows.Forms.TableLayoutPanel tpTitlebar;
		private System.Windows.Forms.TableLayoutPanel tpTitlebarBtn;
		private Guna.UI2.WinForms.Guna2Button btnFullScreen;
	}
}
