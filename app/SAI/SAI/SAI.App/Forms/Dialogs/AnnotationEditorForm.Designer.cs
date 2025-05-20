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
            this.saveBtn = new Guna.UI2.WinForms.Guna2Button();
            this.xBtn = new Guna.UI2.WinForms.Guna2Button();
            this.cancelBtn = new Guna.UI2.WinForms.Guna2Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.annotationText1 = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.pleaseNamePanel = new Guna.UI2.WinForms.Guna2Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
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
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.annotationText1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(40, 83);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(421, 42);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // annotationText1
            // 
            this.annotationText1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.annotationText1.DefaultText = "";
            this.annotationText1.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.annotationText1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.annotationText1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.annotationText1.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.annotationText1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.annotationText1.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.annotationText1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.annotationText1.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.annotationText1.Location = new System.Drawing.Point(5, 6);
            this.annotationText1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.annotationText1.MaximumSize = new System.Drawing.Size(415, 36);
            this.annotationText1.MinimumSize = new System.Drawing.Size(415, 36);
            this.annotationText1.Name = "annotationText1";
            this.annotationText1.PlaceholderText = "";
            this.annotationText1.SelectedText = "";
            this.annotationText1.Size = new System.Drawing.Size(415, 36);
            this.annotationText1.TabIndex = 8;
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel1.BorderColor = System.Drawing.Color.Transparent;
            this.guna2Panel1.BorderThickness = 1;
            this.guna2Panel1.Location = new System.Drawing.Point(79, 6);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(421, 42);
            this.guna2Panel1.TabIndex = 6;
            // 
            // pleaseNamePanel
            // 
            this.pleaseNamePanel.BackColor = System.Drawing.Color.Transparent;
            this.pleaseNamePanel.BackgroundImage = global::SAI.Properties.Resources.bg_warning_empty;
            this.pleaseNamePanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pleaseNamePanel.FillColor = System.Drawing.Color.Transparent;
            this.pleaseNamePanel.Location = new System.Drawing.Point(43, 130);
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
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.pleaseNamePanel);
            this.Controls.Add(this.xBtn);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.saveBtn);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AnnotationEditorForm";
            this.TransparencyKey = System.Drawing.Color.DimGray;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Button saveBtn;
        private Guna.UI2.WinForms.Guna2Button xBtn;
        private Guna.UI2.WinForms.Guna2Button cancelBtn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2Panel pleaseNamePanel;
        private Guna.UI2.WinForms.Guna2TextBox annotationText1;
    }
}