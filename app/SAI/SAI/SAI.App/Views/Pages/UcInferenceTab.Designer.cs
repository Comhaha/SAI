namespace SAI.SAI.App.Views.Pages
{
    partial class UcInferenceTab
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.ibtnDownloadAIModel = new Guna.UI2.WinForms.Guna2ImageButton();
            this.ibtnGoNotion = new Guna.UI2.WinForms.Guna2ImageButton();
            this.ibtnAiFeedback = new Guna.UI2.WinForms.Guna2ImageButton();
            this.fpParent = new System.Windows.Forms.FlowLayoutPanel();
            this.pPleaseControlThreshold = new Guna.UI2.WinForms.Guna2Panel();
            this.pleaseControlThreshold = new SAI.App.Views.Pages.AutoSizeLabel();
            this.btnSelectInferImage = new Guna.UI2.WinForms.Guna2Button();
            this.pboxInferAccuracy = new Guna.UI2.WinForms.Guna2PictureBox();
            this.lblModelInfer = new SAI.App.Views.Pages.AutoSizeLabel();
            this.fplblModelInfer = new System.Windows.Forms.FlowLayoutPanel();
            this.fplblThreshold = new System.Windows.Forms.FlowLayoutPanel();
            this.lblThreshold = new SAI.App.Views.Pages.AutoSizeLabel();
            this.btnInfoThreshold = new Guna.UI2.WinForms.Guna2Button();
            this.fpTrackBar = new System.Windows.Forms.FlowLayoutPanel();
            this.tbarThreshold = new Guna.UI2.WinForms.Guna2TrackBar();
            this.pThreshold = new Guna.UI2.WinForms.Guna2Panel();
            this.tboxThreshold = new Guna.UI2.WinForms.Guna2TextBox();
            this.fplblInferGraph = new System.Windows.Forms.FlowLayoutPanel();
            this.lblInferGraph = new SAI.App.Views.Pages.AutoSizeLabel();
            this.btnInfoGraph = new Guna.UI2.WinForms.Guna2Button();
            this.pInferImage = new Guna.UI2.WinForms.Guna2Panel();
            this.pCsvChart = new Guna.UI2.WinForms.Guna2Panel();
            this.ucCsvChart1 = new SAI.App.Views.Pages.UcCsvChart();
            this.pPleaseControlThreshold.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxInferAccuracy)).BeginInit();
            this.fplblModelInfer.SuspendLayout();
            this.fplblThreshold.SuspendLayout();
            this.fpTrackBar.SuspendLayout();
            this.pThreshold.SuspendLayout();
            this.fplblInferGraph.SuspendLayout();
            this.pInferImage.SuspendLayout();
            this.pCsvChart.SuspendLayout();
            this.SuspendLayout();
            // 
            // ibtnDownloadAIModel
            // 
            this.ibtnDownloadAIModel.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnDownloadAIModel.HoverState.Image = global::SAI.Properties.Resources.btn_download_aimodel_hover;
            this.ibtnDownloadAIModel.HoverState.ImageSize = new System.Drawing.Size(240, 75);
            this.ibtnDownloadAIModel.Image = global::SAI.Properties.Resources.btn_download_aimodel;
            this.ibtnDownloadAIModel.ImageOffset = new System.Drawing.Point(0, 0);
            this.ibtnDownloadAIModel.ImageRotate = 0F;
            this.ibtnDownloadAIModel.ImageSize = new System.Drawing.Size(240, 75);
            this.ibtnDownloadAIModel.Location = new System.Drawing.Point(324, 2269);
            this.ibtnDownloadAIModel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 100);
            this.ibtnDownloadAIModel.Name = "ibtnDownloadAIModel";
            this.ibtnDownloadAIModel.PressedState.ImageSize = new System.Drawing.Size(160, 50);
            this.ibtnDownloadAIModel.Size = new System.Drawing.Size(240, 75);
            this.ibtnDownloadAIModel.TabIndex = 9;
            this.ibtnDownloadAIModel.Click += new System.EventHandler(this.ibtnDownloadAIModel_Click);
            // 
            // ibtnGoNotion
            // 
            this.ibtnGoNotion.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnGoNotion.HoverState.Image = global::SAI.Properties.Resources.btn_goNotion_hover;
            this.ibtnGoNotion.HoverState.ImageSize = new System.Drawing.Size(240, 75);
            this.ibtnGoNotion.Image = global::SAI.Properties.Resources.btn_goNotion;
            this.ibtnGoNotion.ImageOffset = new System.Drawing.Point(0, 0);
            this.ibtnGoNotion.ImageRotate = 0F;
            this.ibtnGoNotion.ImageSize = new System.Drawing.Size(240, 75);
            this.ibtnGoNotion.Location = new System.Drawing.Point(66, 2269);
            this.ibtnGoNotion.Margin = new System.Windows.Forms.Padding(3, 3, 3, 100);
            this.ibtnGoNotion.Name = "ibtnGoNotion";
            this.ibtnGoNotion.PressedState.ImageSize = new System.Drawing.Size(240, 75);
            this.ibtnGoNotion.Size = new System.Drawing.Size(240, 75);
            this.ibtnGoNotion.TabIndex = 10;
            this.ibtnGoNotion.Click += new System.EventHandler(this.ibtnGoNotion_Click);
            // 
            // ibtnAiFeedback
            // 
            this.ibtnAiFeedback.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnAiFeedback.HoverState.Image = global::SAI.Properties.Resources.btn_aifeedback;
            this.ibtnAiFeedback.HoverState.ImageSize = new System.Drawing.Size(520, 135);
            this.ibtnAiFeedback.Image = global::SAI.Properties.Resources.btn_aifeedback;
            this.ibtnAiFeedback.ImageOffset = new System.Drawing.Point(0, 0);
            this.ibtnAiFeedback.ImageRotate = 0F;
            this.ibtnAiFeedback.ImageSize = new System.Drawing.Size(520, 135);
            this.ibtnAiFeedback.Location = new System.Drawing.Point(55, 2112);
            this.ibtnAiFeedback.Name = "ibtnAiFeedback";
            this.ibtnAiFeedback.PressedState.ImageSize = new System.Drawing.Size(520, 135);
            this.ibtnAiFeedback.Size = new System.Drawing.Size(520, 135);
            this.ibtnAiFeedback.TabIndex = 11;
            // 
            // fpParent
            // 
            this.fpParent.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fpParent.Location = new System.Drawing.Point(45, 0);
            this.fpParent.Name = "fpParent";
            this.fpParent.Size = new System.Drawing.Size(585, 2380);
            this.fpParent.TabIndex = 12;
            // 
            // pPleaseControlThreshold
            // 
            this.pPleaseControlThreshold.Controls.Add(this.pleaseControlThreshold);
            this.pPleaseControlThreshold.ForeColor = System.Drawing.Color.Tomato;
            this.pPleaseControlThreshold.Location = new System.Drawing.Point(313, 535);
            this.pPleaseControlThreshold.Name = "pPleaseControlThreshold";
            this.pPleaseControlThreshold.Size = new System.Drawing.Size(231, 30);
            this.pPleaseControlThreshold.TabIndex = 21;
            // 
            // pleaseControlThreshold
            // 
            this.pleaseControlThreshold.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pleaseControlThreshold.Font = new System.Drawing.Font("Noto Sans KR", 9F);
            this.pleaseControlThreshold.ForeColor = System.Drawing.Color.Tomato;
            this.pleaseControlThreshold.Location = new System.Drawing.Point(0, 0);
            this.pleaseControlThreshold.Name = "pleaseControlThreshold";
            this.pleaseControlThreshold.Size = new System.Drawing.Size(231, 30);
            this.pleaseControlThreshold.TabIndex = 0;
            this.pleaseControlThreshold.Text = "threshold를 설정해주세요";
            this.pleaseControlThreshold.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.pleaseControlThreshold.Visible = false;
            // 
            // btnSelectInferImage
            // 
            this.btnSelectInferImage.BackgroundImage = global::SAI.Properties.Resources.btn_selectinferimage;
            this.btnSelectInferImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSelectInferImage.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSelectInferImage.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSelectInferImage.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSelectInferImage.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSelectInferImage.FillColor = System.Drawing.Color.Transparent;
            this.btnSelectInferImage.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSelectInferImage.ForeColor = System.Drawing.Color.White;
            this.btnSelectInferImage.Location = new System.Drawing.Point(7, 3);
            this.btnSelectInferImage.Name = "btnSelectInferImage";
            this.btnSelectInferImage.Size = new System.Drawing.Size(494, 278);
            this.btnSelectInferImage.TabIndex = 24;
            this.btnSelectInferImage.Click += new System.EventHandler(this.btnSelectInferImage_Click);
            // 
            // pboxInferAccuracy
            // 
            this.pboxInferAccuracy.BackgroundImage = global::SAI.Properties.Resources.p_sideinfer_accuracy;
            this.pboxInferAccuracy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pboxInferAccuracy.ErrorImage = null;
            this.pboxInferAccuracy.FillColor = System.Drawing.Color.Transparent;
            this.pboxInferAccuracy.ImageRotate = 0F;
            this.pboxInferAccuracy.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pboxInferAccuracy.InitialImage = global::SAI.Properties.Resources.p_sideinfer_accuracy;
            this.pboxInferAccuracy.Location = new System.Drawing.Point(7, 3);
            this.pboxInferAccuracy.Name = "pboxInferAccuracy";
            this.pboxInferAccuracy.Size = new System.Drawing.Size(494, 278);
            this.pboxInferAccuracy.TabIndex = 26;
            this.pboxInferAccuracy.TabStop = false;
            // 
            // lblModelInfer
            // 
            this.lblModelInfer.Font = new System.Drawing.Font("Noto Sans KR", 9F, System.Drawing.FontStyle.Bold);
            this.lblModelInfer.Location = new System.Drawing.Point(3, 3);
            this.lblModelInfer.Name = "lblModelInfer";
            this.lblModelInfer.Size = new System.Drawing.Size(200, 50);
            this.lblModelInfer.TabIndex = 0;
            this.lblModelInfer.Text = "모델 추론";
            this.lblModelInfer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fplblModelInfer
            // 
            this.fplblModelInfer.Controls.Add(this.lblModelInfer);
            this.fplblModelInfer.Location = new System.Drawing.Point(45, 35);
            this.fplblModelInfer.Name = "fplblModelInfer";
            this.fplblModelInfer.Size = new System.Drawing.Size(261, 60);
            this.fplblModelInfer.TabIndex = 23;
            // 
            // fplblThreshold
            // 
            this.fplblThreshold.Controls.Add(this.lblThreshold);
            this.fplblThreshold.Controls.Add(this.btnInfoThreshold);
            this.fplblThreshold.Font = new System.Drawing.Font("Noto Sans KR Medium", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.fplblThreshold.Location = new System.Drawing.Point(45, 122);
            this.fplblThreshold.Name = "fplblThreshold";
            this.fplblThreshold.Size = new System.Drawing.Size(261, 54);
            this.fplblThreshold.TabIndex = 0;
            // 
            // lblThreshold
            // 
            this.lblThreshold.Font = new System.Drawing.Font("Noto Sans KR Medium", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThreshold.Location = new System.Drawing.Point(3, 3);
            this.lblThreshold.Name = "lblThreshold";
            this.lblThreshold.Size = new System.Drawing.Size(179, 50);
            this.lblThreshold.TabIndex = 1;
            this.lblThreshold.Text = "Threshold";
            this.lblThreshold.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnInfoThreshold
            // 
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
            this.btnInfoThreshold.Location = new System.Drawing.Point(188, 20);
            this.btnInfoThreshold.Margin = new System.Windows.Forms.Padding(3, 20, 3, 3);
            this.btnInfoThreshold.Name = "btnInfoThreshold";
            this.btnInfoThreshold.Size = new System.Drawing.Size(15, 15);
            this.btnInfoThreshold.TabIndex = 24;
            // 
            // fpTrackBar
            // 
            this.fpTrackBar.Controls.Add(this.tbarThreshold);
            this.fpTrackBar.Controls.Add(this.pThreshold);
            this.fpTrackBar.Location = new System.Drawing.Point(45, 190);
            this.fpTrackBar.Name = "fpTrackBar";
            this.fpTrackBar.Size = new System.Drawing.Size(505, 51);
            this.fpTrackBar.TabIndex = 0;
            // 
            // tbarThreshold
            // 
            this.tbarThreshold.Location = new System.Drawing.Point(3, 10);
            this.tbarThreshold.Margin = new System.Windows.Forms.Padding(3, 10, 30, 3);
            this.tbarThreshold.Name = "tbarThreshold";
            this.tbarThreshold.Size = new System.Drawing.Size(366, 25);
            this.tbarThreshold.TabIndex = 14;
            this.tbarThreshold.ThumbColor = System.Drawing.Color.Gold;
            // 
            // pThreshold
            // 
            this.pThreshold.BackgroundImage = global::SAI.Properties.Resources.tbox_threshold;
            this.pThreshold.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pThreshold.Controls.Add(this.tboxThreshold);
            this.pThreshold.Location = new System.Drawing.Point(402, 3);
            this.pThreshold.Name = "pThreshold";
            this.pThreshold.Size = new System.Drawing.Size(73, 37);
            this.pThreshold.TabIndex = 15;
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
            this.tboxThreshold.Location = new System.Drawing.Point(6, 8);
            this.tboxThreshold.Margin = new System.Windows.Forms.Padding(6);
            this.tboxThreshold.Name = "tboxThreshold";
            this.tboxThreshold.PlaceholderForeColor = System.Drawing.Color.Black;
            this.tboxThreshold.PlaceholderText = "";
            this.tboxThreshold.SelectedText = "";
            this.tboxThreshold.Size = new System.Drawing.Size(58, 19);
            this.tboxThreshold.TabIndex = 10;
            this.tboxThreshold.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // fplblInferGraph
            // 
            this.fplblInferGraph.Controls.Add(this.lblInferGraph);
            this.fplblInferGraph.Controls.Add(this.btnInfoGraph);
            this.fplblInferGraph.Location = new System.Drawing.Point(47, 592);
            this.fplblInferGraph.Name = "fplblInferGraph";
            this.fplblInferGraph.Size = new System.Drawing.Size(261, 60);
            this.fplblInferGraph.TabIndex = 24;
            // 
            // lblInferGraph
            // 
            this.lblInferGraph.Font = new System.Drawing.Font("Noto Sans KR", 9F, System.Drawing.FontStyle.Bold);
            this.lblInferGraph.Location = new System.Drawing.Point(3, 3);
            this.lblInferGraph.Name = "lblInferGraph";
            this.lblInferGraph.Size = new System.Drawing.Size(200, 50);
            this.lblInferGraph.TabIndex = 0;
            this.lblInferGraph.Text = "모델 성능 그래프";
            this.lblInferGraph.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnInfoGraph
            // 
            this.btnInfoGraph.BackgroundImage = global::SAI.Properties.Resources.btn_info_17;
            this.btnInfoGraph.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnInfoGraph.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnInfoGraph.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnInfoGraph.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnInfoGraph.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnInfoGraph.FillColor = System.Drawing.Color.Transparent;
            this.btnInfoGraph.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnInfoGraph.ForeColor = System.Drawing.Color.White;
            this.btnInfoGraph.Location = new System.Drawing.Point(209, 16);
            this.btnInfoGraph.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.btnInfoGraph.Name = "btnInfoGraph";
            this.btnInfoGraph.Size = new System.Drawing.Size(25, 25);
            this.btnInfoGraph.TabIndex = 27;
            // 
            // pInferImage
            // 
            this.pInferImage.Controls.Add(this.btnSelectInferImage);
            this.pInferImage.Controls.Add(this.pboxInferAccuracy);
            this.pInferImage.Location = new System.Drawing.Point(45, 244);
            this.pInferImage.Name = "pInferImage";
            this.pInferImage.Size = new System.Drawing.Size(505, 284);
            this.pInferImage.TabIndex = 27;
            // 
            // pCsvChart
            // 
            this.pCsvChart.Controls.Add(this.ucCsvChart1);
            this.pCsvChart.Location = new System.Drawing.Point(45, 667);
            this.pCsvChart.Name = "pCsvChart";
            this.pCsvChart.Size = new System.Drawing.Size(525, 1428);
            this.pCsvChart.TabIndex = 0;
            // 
            // ucCsvChart1
            // 
            this.ucCsvChart1.Location = new System.Drawing.Point(2, 2);
            this.ucCsvChart1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ucCsvChart1.Name = "ucCsvChart1";
            this.ucCsvChart1.Size = new System.Drawing.Size(520, 1423);
            this.ucCsvChart1.TabIndex = 0;
            // 
            // UcInferenceTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pCsvChart);
            this.Controls.Add(this.pPleaseControlThreshold);
            this.Controls.Add(this.pInferImage);
            this.Controls.Add(this.fpTrackBar);
            this.Controls.Add(this.fplblInferGraph);
            this.Controls.Add(this.fplblThreshold);
            this.Controls.Add(this.ibtnAiFeedback);
            this.Controls.Add(this.fplblModelInfer);
            this.Controls.Add(this.ibtnGoNotion);
            this.Controls.Add(this.ibtnDownloadAIModel);
            this.Controls.Add(this.fpParent);
            this.Name = "UcInferenceTab";
            this.Size = new System.Drawing.Size(630, 2380);
            this.pPleaseControlThreshold.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pboxInferAccuracy)).EndInit();
            this.fplblModelInfer.ResumeLayout(false);
            this.fplblThreshold.ResumeLayout(false);
            this.fpTrackBar.ResumeLayout(false);
            this.pThreshold.ResumeLayout(false);
            this.fplblInferGraph.ResumeLayout(false);
            this.pInferImage.ResumeLayout(false);
            this.pCsvChart.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2ImageButton ibtnDownloadAIModel;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnGoNotion;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnAiFeedback;
        private System.Windows.Forms.FlowLayoutPanel fpParent;
        private SAI.App.Views.Pages.AutoSizeLabel lblModelInfer;
        private System.Windows.Forms.FlowLayoutPanel fplblModelInfer;
        private System.Windows.Forms.FlowLayoutPanel fplblThreshold;
        private SAI.App.Views.Pages.AutoSizeLabel lblThreshold;
        private Guna.UI2.WinForms.Guna2Button btnInfoThreshold;
        private System.Windows.Forms.FlowLayoutPanel fpTrackBar;
        private Guna.UI2.WinForms.Guna2TrackBar tbarThreshold;
        private Guna.UI2.WinForms.Guna2Panel pThreshold;
        private Guna.UI2.WinForms.Guna2TextBox tboxThreshold;
        private System.Windows.Forms.FlowLayoutPanel fplblInferGraph;
        private SAI.App.Views.Pages.AutoSizeLabel lblInferGraph;
        private Guna.UI2.WinForms.Guna2Button btnInfoGraph;
        private Guna.UI2.WinForms.Guna2Button btnSelectInferImage;
        private Guna.UI2.WinForms.Guna2PictureBox pboxInferAccuracy;
        private Guna.UI2.WinForms.Guna2Panel pInferImage;
        private Guna.UI2.WinForms.Guna2Panel pPleaseControlThreshold;
        private SAI.App.Views.Pages.AutoSizeLabel pleaseControlThreshold;
        private Guna.UI2.WinForms.Guna2Panel pCsvChart;
        private SAI.App.Views.Pages.UcCsvChart ucCsvChart1;
    }
}
