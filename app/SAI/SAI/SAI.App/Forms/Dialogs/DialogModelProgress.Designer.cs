namespace SAI.SAI.App.Forms.Dialogs
{
    partial class DialogModelProgress
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
            this.guna2ProgressBar1 = new Guna.UI2.WinForms.Guna2ProgressBar();
            this.lblEpoch = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblRemaining = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblStatus = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.panelTitleBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackgroundImage = global::SAI.Properties.Resources.bg_titlebar_train_progress;
            this.panelTitleBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelTitleBar.Controls.Add(this.btnClose);
            this.panelTitleBar.Location = new System.Drawing.Point(0, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(580, 40);
            this.panelTitleBar.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.BackgroundImage = global::SAI.Properties.Resources.btn_yellow_close_train;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnClose.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnClose.FillColor = System.Drawing.Color.Transparent;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(520, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(60, 40);
            this.btnClose.TabIndex = 1;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // guna2ProgressBar1
            // 
            this.guna2ProgressBar1.BackColor = System.Drawing.Color.Transparent;
            this.guna2ProgressBar1.BackgroundImage = global::SAI.Properties.Resources.bg_train_progressbar;
            this.guna2ProgressBar1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.guna2ProgressBar1.BorderColor = System.Drawing.Color.Transparent;
            this.guna2ProgressBar1.FillColor = System.Drawing.Color.Transparent;
            this.guna2ProgressBar1.ForeColor = System.Drawing.Color.Transparent;
            this.guna2ProgressBar1.Location = new System.Drawing.Point(52, 145);
            this.guna2ProgressBar1.Name = "guna2ProgressBar1";
            this.guna2ProgressBar1.Size = new System.Drawing.Size(476, 26);
            this.guna2ProgressBar1.TabIndex = 1;
            this.guna2ProgressBar1.Text = "progressBar";
            this.guna2ProgressBar1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // lblEpoch
            // 
            this.lblEpoch.BackColor = System.Drawing.Color.Transparent;
            this.lblEpoch.Font = new System.Drawing.Font("Noto Sans KR Medium", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblEpoch.Location = new System.Drawing.Point(300, 83);
            this.lblEpoch.Name = "lblEpoch";
            this.lblEpoch.Size = new System.Drawing.Size(111, 41);
            this.lblEpoch.TabIndex = 2;
            this.lblEpoch.Text = "001/100";
            // 
            // lblRemaining
            // 
            this.lblRemaining.BackColor = System.Drawing.Color.Transparent;
            this.lblRemaining.Font = new System.Drawing.Font("Noto Sans KR Medium", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblRemaining.Location = new System.Drawing.Point(300, 109);
            this.lblRemaining.Name = "lblRemaining";
            this.lblRemaining.Size = new System.Drawing.Size(117, 41);
            this.lblRemaining.TabIndex = 3;
            this.lblRemaining.Text = "00:29:59";
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.Font = new System.Drawing.Font("Noto Sans KR Medium", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblStatus.Location = new System.Drawing.Point(255, 177);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(115, 41);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "준비 중...";
            // 
            // DialogModelProgress
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::SAI.Properties.Resources.bg_dialog_train_progress;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(580, 256);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblRemaining);
            this.Controls.Add(this.lblEpoch);
            this.Controls.Add(this.guna2ProgressBar1);
            this.Controls.Add(this.panelTitleBar);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "DialogModelProgress";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DialogModelProgress";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Transparent;
            this.panelTitleBar.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelTitleBar;
        private Guna.UI2.WinForms.Guna2Button btnClose;
        private Guna.UI2.WinForms.Guna2ProgressBar guna2ProgressBar1;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblEpoch;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblRemaining;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblStatus;
    }
}