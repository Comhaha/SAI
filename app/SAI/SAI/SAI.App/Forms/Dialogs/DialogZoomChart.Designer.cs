namespace SAI.SAI.App.Forms.Dialogs
{
    partial class DialogZoomChart
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
            this.btnCloseCustom = new Guna.UI2.WinForms.Guna2Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelChartArea = new Guna.UI2.WinForms.Guna2Panel();
            this.panelTitleBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackgroundImage = global::SAI.Properties.Resources.bg_titlebar_zoomChart;
            this.panelTitleBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelTitleBar.Controls.Add(this.btnCloseCustom);
            this.panelTitleBar.Controls.Add(this.lblTitle);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(0, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(900, 60);
            this.panelTitleBar.TabIndex = 0;
            // 
            // btnCloseCustom
            // 
            this.btnCloseCustom.BackgroundImage = global::SAI.Properties.Resources.btn_close_zoomChart;
            this.btnCloseCustom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCloseCustom.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCloseCustom.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCloseCustom.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCloseCustom.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCloseCustom.FillColor = System.Drawing.Color.Transparent;
            this.btnCloseCustom.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCloseCustom.ForeColor = System.Drawing.Color.White;
            this.btnCloseCustom.Location = new System.Drawing.Point(828, 0);
            this.btnCloseCustom.Name = "btnCloseCustom";
            this.btnCloseCustom.Size = new System.Drawing.Size(72, 60);
            this.btnCloseCustom.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.White;
            this.lblTitle.Font = new System.Drawing.Font("Noto Sans KR", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTitle.Location = new System.Drawing.Point(29, 14);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(84, 35);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "label2";
            // 
            // panelChartArea
            // 
            this.panelChartArea.BackColor = System.Drawing.Color.Transparent;
            this.panelChartArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChartArea.Location = new System.Drawing.Point(0, 60);
            this.panelChartArea.Name = "panelChartArea";
            this.panelChartArea.Size = new System.Drawing.Size(900, 540);
            this.panelChartArea.TabIndex = 1;
            // 
            // DialogZoomChart
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::SAI.Properties.Resources.bg_dialog_zoomChart;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.panelChartArea);
            this.Controls.Add(this.panelTitleBar);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "DialogZoomChart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.TopMost = true;
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelTitleBar;
        private Guna.UI2.WinForms.Guna2Button btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelContent;
        private Guna.UI2.WinForms.Guna2Button btnOk;
        private Guna.UI2.WinForms.Guna2Button btnCancel;
        private Guna.UI2.WinForms.Guna2Panel panelChartArea;
        private System.Windows.Forms.Label lblTitle;
        private Guna.UI2.WinForms.Guna2Button btnCloseCustom;
    }
}