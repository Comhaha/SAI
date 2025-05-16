using System;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace SAI.SAI.App.Forms.Dialogs
{
    public partial class ProgressDialog : Form
    {
        private Guna2ProgressBar progressBar;
        private Label lblStatus;

        public ProgressDialog()
        {
            InitializeComponent();
            InitializeCustomUI();
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void InitializeCustomUI()
        {
            this.progressBar = new Guna2ProgressBar();
            this.lblStatus = new Label();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 40);
            this.progressBar.Name = "progressBar";
            this.progressBar.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.progressBar.ProgressColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.progressBar.Size = new System.Drawing.Size(376, 20);
            this.progressBar.TabIndex = 0;
            this.progressBar.Text = "guna2ProgressBar1";
            this.progressBar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(12, 15);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(53, 12);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "준비 중...";
            // 
            // ProgressDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 80);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.progressBar);
            this.Name = "ProgressDialog";
            this.Text = "진행 상황";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        public void UpdateProgress(double progress, string message)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateProgress(progress, message)));
                return;
            }

            progressBar.Value = (int)progress;
            lblStatus.Text = message;
        }

        public void Reset()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(Reset));
                return;
            }

            progressBar.Value = 0;
            lblStatus.Text = "준비 중...";
        }
    }
}