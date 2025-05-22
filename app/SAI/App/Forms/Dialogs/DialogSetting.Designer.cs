namespace SAI.App.Forms.Dialogs
{
	partial class DialogSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogSetting));
            this.panelTitleBar = new Guna.UI2.WinForms.Guna2Panel();
            this.btnClose = new Guna.UI2.WinForms.Guna2Button();
            this.pCodeSetting = new Guna.UI2.WinForms.Guna2Panel();
            this.btnDark = new Guna.UI2.WinForms.Guna2Button();
            this.btnLight = new Guna.UI2.WinForms.Guna2Button();
            this.tboxPath = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnSave = new Guna.UI2.WinForms.Guna2Button();
            this.panelTitleBar.SuspendLayout();
            this.pCodeSetting.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.Transparent;
            this.panelTitleBar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelTitleBar.BackgroundImage")));
            this.panelTitleBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelTitleBar.Controls.Add(this.btnClose);
            this.panelTitleBar.Location = new System.Drawing.Point(-3, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(600, 50);
            this.panelTitleBar.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClose.BackgroundImage")));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.BorderColor = System.Drawing.Color.Transparent;
            this.btnClose.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnClose.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnClose.FillColor = System.Drawing.Color.Transparent;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClose.ForeColor = System.Drawing.Color.Transparent;
            this.btnClose.Location = new System.Drawing.Point(537, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(60, 40);
            this.btnClose.TabIndex = 11;
            // 
            // pCodeSetting
            // 
            this.pCodeSetting.Controls.Add(this.btnDark);
            this.pCodeSetting.Controls.Add(this.btnLight);
            this.pCodeSetting.Location = new System.Drawing.Point(95, 109);
            this.pCodeSetting.Name = "pCodeSetting";
            this.pCodeSetting.Size = new System.Drawing.Size(124, 41);
            this.pCodeSetting.TabIndex = 1;
            // 
            // btnDark
            // 
            this.btnDark.BackColor = System.Drawing.Color.Transparent;
            this.btnDark.BackgroundImage = global::SAI.Properties.Resources.btn_dark;
            this.btnDark.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDark.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDark.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDark.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDark.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDark.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDark.FillColor = System.Drawing.Color.Transparent;
            this.btnDark.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnDark.ForeColor = System.Drawing.Color.White;
            this.btnDark.Location = new System.Drawing.Point(62, 0);
            this.btnDark.Name = "btnDark";
            this.btnDark.Size = new System.Drawing.Size(62, 41);
            this.btnDark.TabIndex = 1;
            // 
            // btnLight
            // 
            this.btnLight.BackgroundImage = global::SAI.Properties.Resources.btn_light;
            this.btnLight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLight.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLight.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLight.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLight.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLight.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnLight.FillColor = System.Drawing.Color.Transparent;
            this.btnLight.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnLight.ForeColor = System.Drawing.Color.White;
            this.btnLight.Location = new System.Drawing.Point(0, 0);
            this.btnLight.Name = "btnLight";
            this.btnLight.Size = new System.Drawing.Size(62, 41);
            this.btnLight.TabIndex = 0;
            // 
            // tboxPath
            // 
            this.tboxPath.BackgroundImage = global::SAI.Properties.Resources.pbox_setting1;
            this.tboxPath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tboxPath.BorderColor = System.Drawing.Color.Transparent;
            this.tboxPath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tboxPath.DefaultText = "";
            this.tboxPath.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.tboxPath.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.tboxPath.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tboxPath.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tboxPath.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tboxPath.Font = new System.Drawing.Font("Noto Sans KR Medium", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tboxPath.ForeColor = System.Drawing.Color.Black;
            this.tboxPath.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tboxPath.Location = new System.Drawing.Point(95, 197);
            this.tboxPath.Margin = new System.Windows.Forms.Padding(6);
            this.tboxPath.Name = "tboxPath";
            this.tboxPath.PlaceholderForeColor = System.Drawing.Color.Black;
            this.tboxPath.PlaceholderText = "클릭하여 경로 선택";
            this.tboxPath.ReadOnly = true;
            this.tboxPath.SelectedText = "";
            this.tboxPath.Size = new System.Drawing.Size(406, 41);
            this.tboxPath.TabIndex = 2;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.BackgroundImage = global::SAI.Properties.Resources.btn_save;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.BorderColor = System.Drawing.Color.Transparent;
            this.btnSave.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSave.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSave.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSave.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSave.FillColor = System.Drawing.Color.Transparent;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSave.ForeColor = System.Drawing.Color.Transparent;
            this.btnSave.Location = new System.Drawing.Point(224, 264);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(153, 41);
            this.btnSave.TabIndex = 3;
            // 
            // DialogSetting
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Gray;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(600, 355);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tboxPath);
            this.Controls.Add(this.pCodeSetting);
            this.Controls.Add(this.panelTitleBar);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Transparent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "DialogSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.TopMost = true;
            this.panelTitleBar.ResumeLayout(false);
            this.pCodeSetting.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelTitleBar;
        private Guna.UI2.WinForms.Guna2Button btnClose;
        private Guna.UI2.WinForms.Guna2Panel pCodeSetting;
        private Guna.UI2.WinForms.Guna2TextBox tboxPath;
        private Guna.UI2.WinForms.Guna2Button btnSave;
        private Guna.UI2.WinForms.Guna2Button btnDark;
        private Guna.UI2.WinForms.Guna2Button btnLight;
    }
}