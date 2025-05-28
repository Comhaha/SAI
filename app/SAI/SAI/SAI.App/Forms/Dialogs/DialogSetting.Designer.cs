namespace SAI.SAI.App.Forms.Dialogs
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
            this.panelTitleBar = new Guna.UI2.WinForms.Guna2Panel();
            this.btnClose = new Guna.UI2.WinForms.Guna2Button();
            this.btnSave = new Guna.UI2.WinForms.Guna2Button();
            this.lblTitle = new SAI.App.Views.Pages.AutoSizeLabel();
            this.btnLocal = new Guna.UI2.WinForms.Guna2Button();
            this.btnServer = new Guna.UI2.WinForms.Guna2Button();
            this.lblLocal = new SAI.App.Views.Pages.AutoSizeLabel();
            this.lblServer = new SAI.App.Views.Pages.AutoSizeLabel();
            this.panelTitleBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackgroundImage = global::SAI.Properties.Resources.bg_titlebar_setting;
            this.panelTitleBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelTitleBar.BorderColor = System.Drawing.Color.Transparent;
            this.panelTitleBar.Controls.Add(this.btnClose);
            this.panelTitleBar.CustomBorderColor = System.Drawing.Color.Transparent;
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(0, 0);
            this.panelTitleBar.Margin = new System.Windows.Forms.Padding(0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(796, 60);
            this.panelTitleBar.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.BackgroundImage = global::SAI.Properties.Resources.btn_close_setting;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnClose.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnClose.FillColor = System.Drawing.Color.Transparent;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(716, 0);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(80, 60);
            this.btnClose.TabIndex = 7;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackgroundImage = global::SAI.Properties.Resources.btn_save;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSave.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSave.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSave.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSave.FillColor = System.Drawing.Color.Transparent;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(295, 290);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(207, 47);
            this.btnSave.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Noto Sans KR Medium", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTitle.Location = new System.Drawing.Point(295, 135);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(207, 27);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "모델 학습 GPU 설정";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnLocal
            // 
            this.btnLocal.BackgroundImage = global::SAI.Properties.Resources.btn_checked;
            this.btnLocal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLocal.BorderColor = System.Drawing.Color.Transparent;
            this.btnLocal.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLocal.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLocal.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLocal.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLocal.FillColor = System.Drawing.Color.Transparent;
            this.btnLocal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnLocal.ForeColor = System.Drawing.Color.Transparent;
            this.btnLocal.Location = new System.Drawing.Point(322, 180);
            this.btnLocal.Name = "btnLocal";
            this.btnLocal.Size = new System.Drawing.Size(24, 24);
            this.btnLocal.TabIndex = 3;
            // 
            // btnServer
            // 
            this.btnServer.BackgroundImage = global::SAI.Properties.Resources.btn_unchecked;
            this.btnServer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnServer.BorderColor = System.Drawing.Color.Transparent;
            this.btnServer.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnServer.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnServer.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnServer.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnServer.FillColor = System.Drawing.Color.Transparent;
            this.btnServer.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnServer.ForeColor = System.Drawing.Color.Transparent;
            this.btnServer.Location = new System.Drawing.Point(322, 222);
            this.btnServer.Name = "btnServer";
            this.btnServer.Size = new System.Drawing.Size(24, 24);
            this.btnServer.TabIndex = 4;
            // 
            // lblLocal
            // 
            this.lblLocal.BackColor = System.Drawing.Color.Transparent;
            this.lblLocal.Font = new System.Drawing.Font("Noto Sans KR Medium", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblLocal.Location = new System.Drawing.Point(352, 177);
            this.lblLocal.Name = "lblLocal";
            this.lblLocal.Size = new System.Drawing.Size(150, 27);
            this.lblLocal.TabIndex = 5;
            this.lblLocal.Text = "Local";
            this.lblLocal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblServer
            // 
            this.lblServer.BackColor = System.Drawing.Color.Transparent;
            this.lblServer.Font = new System.Drawing.Font("Noto Sans KR Medium", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblServer.Location = new System.Drawing.Point(352, 219);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(90, 27);
            this.lblServer.TabIndex = 6;
            this.lblServer.Text = "Server";
            this.lblServer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DialogSetting
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::SAI.Properties.Resources.bg_dialog_setting;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(796, 402);
            this.Controls.Add(this.lblServer);
            this.Controls.Add(this.lblLocal);
            this.Controls.Add(this.btnServer);
            this.Controls.Add(this.btnLocal);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.panelTitleBar);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Transparent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "DialogSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DialogSetting";
            this.TopMost = true;
            this.panelTitleBar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelTitleBar;
        private Guna.UI2.WinForms.Guna2Button btnSave;
        private Guna.UI2.WinForms.Guna2Button btnLocal;
        private Guna.UI2.WinForms.Guna2Button btnServer;
        private SAI.App.Views.Pages.AutoSizeLabel lblTitle;
        private SAI.App.Views.Pages.AutoSizeLabel lblLocal;
        private SAI.App.Views.Pages.AutoSizeLabel lblServer;
        private Guna.UI2.WinForms.Guna2Button btnClose;
    }
}