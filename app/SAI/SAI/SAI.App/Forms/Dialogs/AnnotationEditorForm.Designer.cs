namespace SAI.SAI.App.Forms.Dialogs
{
    partial class AnnotationEditorForm
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
            this.annotationText = new Guna.UI2.WinForms.Guna2TextBox();
            this.saveBtn = new Guna.UI2.WinForms.Guna2Button();
            this.xBtn = new Guna.UI2.WinForms.Guna2Button();
            this.cancelBtn = new Guna.UI2.WinForms.Guna2Button();
            this.pleaseNamePanel = new Guna.UI2.WinForms.Guna2Panel();
            this.SuspendLayout();
            // 
            // annotationText
            // 
            this.annotationText.BorderColor = System.Drawing.Color.Transparent;
            this.annotationText.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.annotationText.DefaultText = "";
            this.annotationText.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.annotationText.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.annotationText.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.annotationText.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.annotationText.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.annotationText.Font = new System.Drawing.Font("Noto Sans KR", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.annotationText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.annotationText.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.annotationText.Location = new System.Drawing.Point(39, 82);
            this.annotationText.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.annotationText.Name = "annotationText";
            this.annotationText.PlaceholderForeColor = System.Drawing.Color.DarkGray;
            this.annotationText.PlaceholderText = "클래스 입력";
            this.annotationText.SelectedText = "";
            this.annotationText.Size = new System.Drawing.Size(420, 45);
            this.annotationText.TabIndex = 0;
            this.annotationText.TextChanged += new System.EventHandler(this.annotationText_TextChanged);
            // 
            // saveBtn
            // 
            this.saveBtn.BackColor = System.Drawing.Color.Transparent;
            this.saveBtn.BackgroundImage = global::SAI.Properties.Resources.Frame_10789;
            this.saveBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.saveBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.saveBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.saveBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.saveBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.saveBtn.FillColor = System.Drawing.Color.Transparent;
            this.saveBtn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.saveBtn.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.saveBtn.Location = new System.Drawing.Point(354, 155);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.PressedColor = System.Drawing.Color.Transparent;
            this.saveBtn.Size = new System.Drawing.Size(107, 38);
            this.saveBtn.TabIndex = 1;
            // 
            // xBtn
            // 
            this.xBtn.BackColor = System.Drawing.Color.Transparent;
            this.xBtn.BackgroundImage = global::SAI.Properties.Resources.btn_close_pink;
            this.xBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.xBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.xBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.xBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.xBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.xBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.xBtn.FillColor = System.Drawing.Color.Transparent;
            this.xBtn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.xBtn.ForeColor = System.Drawing.Color.White;
            this.xBtn.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.xBtn.ImageSize = new System.Drawing.Size(13, 13);
            this.xBtn.Location = new System.Drawing.Point(430, 0);
            this.xBtn.Name = "xBtn";
            this.xBtn.Size = new System.Drawing.Size(70, 48);
            this.xBtn.TabIndex = 3;
            // 
            // cancelBtn
            // 
            this.cancelBtn.BackgroundImage = global::SAI.Properties.Resources.Frame_10788;
            this.cancelBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cancelBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.cancelBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.cancelBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.cancelBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.cancelBtn.FillColor = System.Drawing.Color.Transparent;
            this.cancelBtn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cancelBtn.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.cancelBtn.Location = new System.Drawing.Point(231, 155);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.PressedColor = System.Drawing.Color.Transparent;
            this.cancelBtn.Size = new System.Drawing.Size(107, 38);
            this.cancelBtn.TabIndex = 2;
            // 
            // pleaseNamePanel
            // 
            this.pleaseNamePanel.BackColor = System.Drawing.Color.Transparent;
            this.pleaseNamePanel.BackgroundImage = global::SAI.Properties.Resources.bg_warning_empty;
            this.pleaseNamePanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pleaseNamePanel.FillColor = System.Drawing.Color.Transparent;
            this.pleaseNamePanel.Location = new System.Drawing.Point(39, 130);
            this.pleaseNamePanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pleaseNamePanel.Name = "pleaseNamePanel";
            this.pleaseNamePanel.Size = new System.Drawing.Size(157, 14);
            this.pleaseNamePanel.TabIndex = 4;
            // 
            // AnnotationEditorForm
            // 
            this.AcceptButton = this.saveBtn;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.BackgroundImage = global::SAI.Properties.Resources.annotation_모달;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CancelButton = this.xBtn;
            this.ClientSize = new System.Drawing.Size(499, 228);
            this.Controls.Add(this.pleaseNamePanel);
            this.Controls.Add(this.xBtn);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.annotationText);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AnnotationEditorForm";
            this.TransparencyKey = System.Drawing.Color.DimGray;
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2TextBox annotationText;
        private Guna.UI2.WinForms.Guna2Button saveBtn;
        private Guna.UI2.WinForms.Guna2Button xBtn;
        private Guna.UI2.WinForms.Guna2Button cancelBtn;
        private Guna.UI2.WinForms.Guna2Panel pleaseNamePanel;
    }
}