namespace SAI.SAI.App.Forms.Dialogs
{
    partial class DialogErrorInference
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
            this.btnOk = new Guna.UI2.WinForms.Guna2Button();
            this.lblErrorMessage = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.error = new SAI.App.Views.Pages.AutoSizeLabel();
            this.lblErrorMessage.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.Transparent;
            this.btnOk.BackgroundImage = global::SAI.Properties.Resources.btn_red_ok_15258;
            this.btnOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOk.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnOk.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnOk.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnOk.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnOk.FillColor = System.Drawing.Color.Transparent;
            this.btnOk.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.Location = new System.Drawing.Point(587, 269);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(152, 58);
            this.btnOk.TabIndex = 4;
            // 
            // lblErrorMessage
            // 
            this.lblErrorMessage.Controls.Add(this.error);
            this.lblErrorMessage.Location = new System.Drawing.Point(201, 151);
            this.lblErrorMessage.Name = "lblErrorMessage";
            this.lblErrorMessage.Size = new System.Drawing.Size(439, 42);
            this.lblErrorMessage.TabIndex = 5;
            // 
            // error
            // 
            this.error.BackColor = System.Drawing.Color.Transparent;
            this.error.Dock = System.Windows.Forms.DockStyle.Fill;
            this.error.Font = new System.Drawing.Font("Noto Sans KR", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.error.Location = new System.Drawing.Point(0, 0);
            this.error.Name = "error";
            this.error.Size = new System.Drawing.Size(439, 42);
            this.error.TabIndex = 0;
            this.error.Text = "오류";
            this.error.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.error.Load += new System.EventHandler(this.error_Load);
            // 
            // DialogErrorInference
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Gray;
            this.BackgroundImage = global::SAI.Properties.Resources.bg_dialog_error_inference;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(752, 340);
            this.Controls.Add(this.lblErrorMessage);
            this.Controls.Add(this.btnOk);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "DialogErrorInference";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "r";
            this.TopMost = true;
            this.lblErrorMessage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Button btnOk;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel lblErrorMessage;
        private SAI.App.Views.Pages.AutoSizeLabel error;
    }
}