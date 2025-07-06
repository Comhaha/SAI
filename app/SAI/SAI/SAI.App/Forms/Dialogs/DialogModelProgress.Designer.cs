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
            this.components = new System.ComponentModel.Container();
            this.panelTitleBar = new Guna.UI2.WinForms.Guna2Panel();
            this.btnMinimize = new Guna.UI2.WinForms.Guna2Button();
            this.btnClose = new Guna.UI2.WinForms.Guna2Button();
            this.progressBarModelLearning = new Guna.UI2.WinForms.Guna2ProgressBar();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblStatus = new SAI.App.Views.Pages.AutoSizeLabel();
            this.guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.progressGuideLabel = new SAI.App.Views.Pages.AutoSizeLabel();
            this.panelTitleBar.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackgroundImage = global::SAI.Properties.Resources.bg_titlebar_modelProgress;
            this.panelTitleBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelTitleBar.Controls.Add(this.btnMinimize);
            this.panelTitleBar.Controls.Add(this.btnClose);
            this.panelTitleBar.Location = new System.Drawing.Point(0, 0);
            this.panelTitleBar.Margin = new System.Windows.Forms.Padding(0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(865, 50);
            this.panelTitleBar.TabIndex = 0;
            // 
            // btnMinimize
            // 
            this.btnMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimize.BackColor = System.Drawing.Color.Transparent;
            this.btnMinimize.BackgroundImage = global::SAI.Properties.Resources.btn_yellow_minimize;
            this.btnMinimize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnMinimize.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnMinimize.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnMinimize.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnMinimize.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnMinimize.FillColor = System.Drawing.Color.Transparent;
            this.btnMinimize.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnMinimize.ForeColor = System.Drawing.Color.Transparent;
            this.btnMinimize.Location = new System.Drawing.Point(745, 0);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(50, 49);
            this.btnMinimize.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BackgroundImage = global::SAI.Properties.Resources.btn_close_modelProgress;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnClose.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnClose.FillColor = System.Drawing.Color.Transparent;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(795, 0);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(70, 50);
            this.btnClose.TabIndex = 1;
            // 
            // progressBarModelLearning
            // 
            this.progressBarModelLearning.BackColor = System.Drawing.Color.Transparent;
            this.progressBarModelLearning.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.progressBarModelLearning.BorderRadius = 17;
            this.progressBarModelLearning.BorderThickness = 1;
            this.progressBarModelLearning.FillColor = System.Drawing.Color.Transparent;
            this.progressBarModelLearning.ForeColor = System.Drawing.Color.Transparent;
            this.progressBarModelLearning.Location = new System.Drawing.Point(124, 213);
            this.progressBarModelLearning.Name = "progressBarModelLearning";
            this.progressBarModelLearning.Size = new System.Drawing.Size(625, 40);
            this.progressBarModelLearning.TabIndex = 1;
            this.progressBarModelLearning.Text = "progressBar";
            this.progressBarModelLearning.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.Controls.Add(this.lblStatus);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(127, 150);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(600, 39);
            this.flowLayoutPanel1.TabIndex = 5;
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Noto Sans KR Medium", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblStatus.Location = new System.Drawing.Point(3, 3);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(597, 36);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "준비 중...";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // guna2DragControl1
            // 
            this.guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2DragControl1.UseTransparentDrag = true;
            // 
            // progressGuideLabel
            // 
            this.progressGuideLabel.BackColor = System.Drawing.Color.Transparent;
            this.progressGuideLabel.Font = new System.Drawing.Font("Noto Sans KR", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.progressGuideLabel.ForeColor = System.Drawing.Color.OrangeRed;
            this.progressGuideLabel.Location = new System.Drawing.Point(124, 262);
            this.progressGuideLabel.Name = "progressGuideLabel";
            this.progressGuideLabel.Size = new System.Drawing.Size(417, 32);
            this.progressGuideLabel.TabIndex = 6;
            this.progressGuideLabel.Text = "원활한 체험을 위해 사전학습 모델을 사용중입니다.";
            this.progressGuideLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.progressGuideLabel.Load += new System.EventHandler(this.progressGuideLabel_Load);
            // 
            // DialogModelProgress
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::SAI.Properties.Resources.bg_dialog_modelProgress;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(865, 415);
            this.Controls.Add(this.progressGuideLabel);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.progressBarModelLearning);
            this.Controls.Add(this.panelTitleBar);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "DialogModelProgress";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "vc";
            this.TransparencyKey = System.Drawing.Color.Transparent;
            this.panelTitleBar.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelTitleBar;
        private Guna.UI2.WinForms.Guna2Button btnClose;
        private Guna.UI2.WinForms.Guna2ProgressBar progressBarModelLearning;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private SAI.App.Views.Pages.AutoSizeLabel lblStatus;
		private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
		private Guna.UI2.WinForms.Guna2Button btnMinimize;
        private SAI.App.Views.Pages.AutoSizeLabel progressGuideLabel;
    }
}