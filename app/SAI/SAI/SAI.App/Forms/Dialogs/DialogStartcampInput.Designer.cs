namespace SAI.SAI.App.Forms.Dialogs
{
    partial class DialogStartcampInput
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
            this.btnClose2 = new Guna.UI2.WinForms.Guna2Button();
            this.btnStartcampInput = new Guna.UI2.WinForms.Guna2Button();
            this.tbarThreshold = new Guna.UI2.WinForms.Guna2TrackBar();
            this.lblThreshold = new SAI.App.Views.Pages.AutoSizeLabel();
            this.pThreshold = new Guna.UI2.WinForms.Guna2Panel();
            this.tboxThreshold = new Guna.UI2.WinForms.Guna2TextBox();
            this.ibtnSizeup = new Guna.UI2.WinForms.Guna2ImageButton();
            this.pboxStartcampInput = new Guna.UI2.WinForms.Guna2PictureBox();
            this.pleaseControlThreshold = new SAI.App.Views.Pages.AutoSizeLabel();
            this.btnStartcampInfer = new Guna.UI2.WinForms.Guna2ImageButton();
            this.btnInfoThreshold = new Guna.UI2.WinForms.Guna2Button();
            this.panelTitleBar.SuspendLayout();
            this.pThreshold.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxStartcampInput)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.Transparent;
            this.panelTitleBar.BackgroundImage = global::SAI.Properties.Resources.bg_titlebar_input;
            this.panelTitleBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelTitleBar.Controls.Add(this.btnClose2);
            this.panelTitleBar.ForeColor = System.Drawing.Color.Transparent;
            this.panelTitleBar.Location = new System.Drawing.Point(0, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(870, 62);
            this.panelTitleBar.TabIndex = 0;
            this.panelTitleBar.Paint += new System.Windows.Forms.PaintEventHandler(this.panelTitleBar_Paint);
            // 
            // btnClose2
            // 
            this.btnClose2.BackgroundImage = global::SAI.Properties.Resources.btn_close_prepare;
            this.btnClose2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose2.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnClose2.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnClose2.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnClose2.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnClose2.FillColor = System.Drawing.Color.Transparent;
            this.btnClose2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClose2.ForeColor = System.Drawing.Color.White;
            this.btnClose2.Location = new System.Drawing.Point(790, 0);
            this.btnClose2.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose2.Name = "btnClose2";
            this.btnClose2.Size = new System.Drawing.Size(80, 60);
            this.btnClose2.TabIndex = 8;
            // 
            // btnStartcampInput
            // 
            this.btnStartcampInput.BackColor = System.Drawing.Color.Transparent;
            this.btnStartcampInput.BackgroundImage = global::SAI.Properties.Resources.btn_startcamp_input;
            this.btnStartcampInput.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnStartcampInput.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnStartcampInput.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnStartcampInput.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnStartcampInput.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnStartcampInput.FillColor = System.Drawing.Color.Transparent;
            this.btnStartcampInput.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnStartcampInput.ForeColor = System.Drawing.Color.White;
            this.btnStartcampInput.ImageSize = new System.Drawing.Size(720, 687);
            this.btnStartcampInput.Location = new System.Drawing.Point(79, 211);
            this.btnStartcampInput.Name = "btnStartcampInput";
            this.btnStartcampInput.Size = new System.Drawing.Size(720, 687);
            this.btnStartcampInput.TabIndex = 4;
            this.btnStartcampInput.Click += new System.EventHandler(this.btnStartcampInput_Click);
            // 
            // tbarThreshold
            // 
            this.tbarThreshold.BackColor = System.Drawing.Color.Transparent;
            this.tbarThreshold.Location = new System.Drawing.Point(84, 168);
            this.tbarThreshold.Name = "tbarThreshold";
            this.tbarThreshold.Size = new System.Drawing.Size(265, 21);
            this.tbarThreshold.TabIndex = 10;
            this.tbarThreshold.ThumbColor = System.Drawing.Color.Gold;
            this.tbarThreshold.Scroll += new System.Windows.Forms.ScrollEventHandler(this.tbarThreshold_Scroll);
            // 
            // lblThreshold
            // 
            this.lblThreshold.BackColor = System.Drawing.Color.Transparent;
            this.lblThreshold.Font = new System.Drawing.Font("Noto Sans KR Medium", 13.8F, System.Drawing.FontStyle.Bold);
            this.lblThreshold.Location = new System.Drawing.Point(79, 82);
            this.lblThreshold.Name = "lblThreshold";
            this.lblThreshold.Size = new System.Drawing.Size(170, 46);
            this.lblThreshold.TabIndex = 11;
            this.lblThreshold.Text = "Threshold";
            this.lblThreshold.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblThreshold.Load += new System.EventHandler(this.lblThreshold_Load);
            // 
            // pThreshold
            // 
            this.pThreshold.BackgroundImage = global::SAI.Properties.Resources.tbox_threshold;
            this.pThreshold.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pThreshold.Controls.Add(this.tboxThreshold);
            this.pThreshold.Location = new System.Drawing.Point(369, 158);
            this.pThreshold.Name = "pThreshold";
            this.pThreshold.Size = new System.Drawing.Size(73, 37);
            this.pThreshold.TabIndex = 13;
            // 
            // tboxThreshold
            // 
            this.tboxThreshold.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tboxThreshold.BorderColor = System.Drawing.Color.Transparent;
            this.tboxThreshold.BorderThickness = 0;
            this.tboxThreshold.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tboxThreshold.DefaultText = "";
            this.tboxThreshold.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.tboxThreshold.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.tboxThreshold.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tboxThreshold.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tboxThreshold.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tboxThreshold.Font = new System.Drawing.Font("Noto Sans KR Medium", 10.2F, System.Drawing.FontStyle.Bold);
            this.tboxThreshold.ForeColor = System.Drawing.Color.Black;
            this.tboxThreshold.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tboxThreshold.Location = new System.Drawing.Point(7, 8);
            this.tboxThreshold.Margin = new System.Windows.Forms.Padding(6);
            this.tboxThreshold.Name = "tboxThreshold";
            this.tboxThreshold.PlaceholderText = "";
            this.tboxThreshold.SelectedText = "";
            this.tboxThreshold.Size = new System.Drawing.Size(58, 19);
            this.tboxThreshold.TabIndex = 10;
            this.tboxThreshold.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ibtnSizeup
            // 
            this.ibtnSizeup.BackColor = System.Drawing.Color.Transparent;
            this.ibtnSizeup.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnSizeup.HoverState.Image = global::SAI.Properties.Resources.btn_startcamp_sizeup_hover;
            this.ibtnSizeup.HoverState.ImageSize = new System.Drawing.Size(130, 50);
            this.ibtnSizeup.Image = global::SAI.Properties.Resources.btn_startcamp_sizeup;
            this.ibtnSizeup.ImageOffset = new System.Drawing.Point(0, 0);
            this.ibtnSizeup.ImageRotate = 0F;
            this.ibtnSizeup.ImageSize = new System.Drawing.Size(130, 50);
            this.ibtnSizeup.Location = new System.Drawing.Point(660, 141);
            this.ibtnSizeup.Name = "ibtnSizeup";
            this.ibtnSizeup.PressedState.ImageSize = new System.Drawing.Size(130, 50);
            this.ibtnSizeup.Size = new System.Drawing.Size(139, 61);
            this.ibtnSizeup.TabIndex = 14;
            this.ibtnSizeup.Click += new System.EventHandler(this.ibtnSizeup_Click);
            // 
            // pboxStartcampInput
            // 
            this.pboxStartcampInput.BackColor = System.Drawing.Color.Transparent;
            this.pboxStartcampInput.BackgroundImage = global::SAI.Properties.Resources.p_Startcamp_input;
            this.pboxStartcampInput.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pboxStartcampInput.ErrorImage = null;
            this.pboxStartcampInput.FillColor = System.Drawing.Color.Transparent;
            this.pboxStartcampInput.ImageRotate = 0F;
            this.pboxStartcampInput.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pboxStartcampInput.InitialImage = global::SAI.Properties.Resources.p_sideinfer_accuracy;
            this.pboxStartcampInput.Location = new System.Drawing.Point(79, 211);
            this.pboxStartcampInput.Name = "pboxStartcampInput";
            this.pboxStartcampInput.Size = new System.Drawing.Size(720, 687);
            this.pboxStartcampInput.TabIndex = 24;
            this.pboxStartcampInput.TabStop = false;
            this.pboxStartcampInput.Click += new System.EventHandler(this.pboxStartcampInput_Click);
            // 
            // pleaseControlThreshold
            // 
            this.pleaseControlThreshold.BackColor = System.Drawing.Color.Transparent;
            this.pleaseControlThreshold.Font = new System.Drawing.Font("Noto Sans KR", 9F);
            this.pleaseControlThreshold.ForeColor = System.Drawing.Color.Tomato;
            this.pleaseControlThreshold.Location = new System.Drawing.Point(80, 125);
            this.pleaseControlThreshold.Name = "pleaseControlThreshold";
            this.pleaseControlThreshold.Size = new System.Drawing.Size(241, 32);
            this.pleaseControlThreshold.TabIndex = 25;
            this.pleaseControlThreshold.Text = "threshold를 설정해주세요";
            this.pleaseControlThreshold.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnStartcampInfer
            // 
            this.btnStartcampInfer.BackColor = System.Drawing.Color.Transparent;
            this.btnStartcampInfer.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnStartcampInfer.HoverState.Image = global::SAI.Properties.Resources.btn_dialog_infer_click;
            this.btnStartcampInfer.HoverState.ImageSize = new System.Drawing.Size(130, 50);
            this.btnStartcampInfer.Image = global::SAI.Properties.Resources.btn_dialog_infer1;
            this.btnStartcampInfer.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnStartcampInfer.ImageRotate = 0F;
            this.btnStartcampInfer.ImageSize = new System.Drawing.Size(130, 50);
            this.btnStartcampInfer.Location = new System.Drawing.Point(517, 141);
            this.btnStartcampInfer.Name = "btnStartcampInfer";
            this.btnStartcampInfer.PressedState.ImageSize = new System.Drawing.Size(130, 50);
            this.btnStartcampInfer.Size = new System.Drawing.Size(139, 61);
            this.btnStartcampInfer.TabIndex = 14;
            this.btnStartcampInfer.Click += new System.EventHandler(this.btnStartcampInfer_Click);
            // 
            // btnInfoThreshold
            // 
            this.btnInfoThreshold.BackColor = System.Drawing.Color.Transparent;
            this.btnInfoThreshold.BackgroundImage = global::SAI.Properties.Resources.btn_info_12;
            this.btnInfoThreshold.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnInfoThreshold.BorderColor = System.Drawing.Color.Transparent;
            this.btnInfoThreshold.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnInfoThreshold.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnInfoThreshold.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnInfoThreshold.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnInfoThreshold.FillColor = System.Drawing.Color.Transparent;
            this.btnInfoThreshold.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnInfoThreshold.ForeColor = System.Drawing.Color.White;
            this.btnInfoThreshold.Location = new System.Drawing.Point(255, 104);
            this.btnInfoThreshold.Name = "btnInfoThreshold";
            this.btnInfoThreshold.Size = new System.Drawing.Size(15, 15);
            this.btnInfoThreshold.TabIndex = 26;
            // 
            // DialogStartcampInput
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::SAI.Properties.Resources.bg_dialog_modelInference;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(870, 941);
            this.Controls.Add(this.btnInfoThreshold);
            this.Controls.Add(this.btnStartcampInfer);
            this.Controls.Add(this.pleaseControlThreshold);
            this.Controls.Add(this.pboxStartcampInput);
            this.Controls.Add(this.ibtnSizeup);
            this.Controls.Add(this.pThreshold);
            this.Controls.Add(this.lblThreshold);
            this.Controls.Add(this.tbarThreshold);
            this.Controls.Add(this.btnStartcampInput);
            this.Controls.Add(this.panelTitleBar);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "DialogStartcampInput";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DialogInput";
            this.Load += new System.EventHandler(this.DialogStartcampInput_Load_1);
            this.panelTitleBar.ResumeLayout(false);
            this.pThreshold.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pboxStartcampInput)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelTitleBar;
        private Guna.UI2.WinForms.Guna2Button btnClose2;
        private Guna.UI2.WinForms.Guna2Button btnStartcampInput;
        private Guna.UI2.WinForms.Guna2TrackBar tbarThreshold;
        private SAI.App.Views.Pages.AutoSizeLabel lblThreshold;
        private Guna.UI2.WinForms.Guna2Panel pThreshold;
        private Guna.UI2.WinForms.Guna2TextBox tboxThreshold;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnSizeup;
        private Guna.UI2.WinForms.Guna2PictureBox pboxStartcampInput;
        private SAI.App.Views.Pages.AutoSizeLabel pleaseControlThreshold;
        private Guna.UI2.WinForms.Guna2ImageButton btnStartcampInfer;
        private Guna.UI2.WinForms.Guna2Button btnInfoThreshold;
    }
}