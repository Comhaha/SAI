namespace SAI.SAI.App.Views.Pages
{
    partial class UcLabelGuide
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
            this.guna2CircleButton1 = new Guna.UI2.WinForms.Guna2CircleButton();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.showLevel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.imageContainer = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.accuracyLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.toolZoom = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.guna2HtmlLabel4 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.toolBox = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.toolRedo = new Guna.UI2.WinForms.Guna2ImageButton();
            this.toolUndo = new Guna.UI2.WinForms.Guna2ImageButton();
            this.toolVisible = new Guna.UI2.WinForms.Guna2ImageButton();
            this.toolLabelingSquare = new Guna.UI2.WinForms.Guna2ImageButton();
            this.toolDelete = new Guna.UI2.WinForms.Guna2ImageButton();
            this.toolLabelingPolygon = new Guna.UI2.WinForms.Guna2ImageButton();
            this.toolHand = new Guna.UI2.WinForms.Guna2ImageButton();
            this.guna2HtmlLabel3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.currentLevel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.pictureBoxImage = new Guna.UI2.WinForms.Guna2PictureBox();
            this.nextBtn = new Guna.UI2.WinForms.Guna2CircleButton();
            this.preBtn = new Guna.UI2.WinForms.Guna2CircleButton();
            this.leftPanel = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.class1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.imageContainer.SuspendLayout();
            this.toolZoom.SuspendLayout();
            this.toolBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).BeginInit();
            this.leftPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2CircleButton1
            // 
            this.guna2CircleButton1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2CircleButton1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2CircleButton1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2CircleButton1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2CircleButton1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2CircleButton1.ForeColor = System.Drawing.Color.White;
            this.guna2CircleButton1.Location = new System.Drawing.Point(154, 29);
            this.guna2CircleButton1.Name = "guna2CircleButton1";
            this.guna2CircleButton1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.guna2CircleButton1.Size = new System.Drawing.Size(39, 39);
            this.guna2CircleButton1.TabIndex = 0;
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Noto Sans KR Medium", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(212, 29);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(132, 37);
            this.guna2HtmlLabel1.TabIndex = 1;
            this.guna2HtmlLabel1.Text = "0단계 라벨링";
            this.guna2HtmlLabel1.Click += new System.EventHandler(this.guna2HtmlLabel1_Click_4);
            // 
            // showLevel
            // 
            this.showLevel.BackColor = System.Drawing.Color.Transparent;
            this.showLevel.Font = new System.Drawing.Font("Noto Sans KR Medium", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.showLevel.Location = new System.Drawing.Point(620, 29);
            this.showLevel.Name = "showLevel";
            this.showLevel.Size = new System.Drawing.Size(40, 37);
            this.showLevel.TabIndex = 2;
            this.showLevel.Text = "1/9";
            this.showLevel.Click += new System.EventHandler(this.guna2HtmlLabel2_Click);
            // 
            // imageContainer
            // 
            this.imageContainer.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.imageContainer.BorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.imageContainer.BorderRadius = 30;
            this.imageContainer.BorderThickness = 3;
            this.imageContainer.Controls.Add(this.accuracyLabel);
            this.imageContainer.Controls.Add(this.toolZoom);
            this.imageContainer.Controls.Add(this.toolBox);
            this.imageContainer.Controls.Add(this.currentLevel);
            this.imageContainer.Controls.Add(this.pictureBoxImage);
            this.imageContainer.CustomizableEdges.BottomLeft = false;
            this.imageContainer.CustomizableEdges.TopLeft = false;
            this.imageContainer.Location = new System.Drawing.Point(317, 103);
            this.imageContainer.Name = "imageContainer";
            this.imageContainer.Size = new System.Drawing.Size(868, 532);
            this.imageContainer.TabIndex = 3;
            this.imageContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.mainPanel_Paint);
            // 
            // accuracyLabel
            // 
            this.accuracyLabel.BackColor = System.Drawing.Color.Transparent;
            this.accuracyLabel.Font = new System.Drawing.Font("Noto Sans KR Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.accuracyLabel.Location = new System.Drawing.Point(23, 74);
            this.accuracyLabel.Name = "accuracyLabel";
            this.accuracyLabel.Size = new System.Drawing.Size(82, 26);
            this.accuracyLabel.TabIndex = 12;
            this.accuracyLabel.Text = "정확도 예시";
            // 
            // toolZoom
            // 
            this.toolZoom.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.toolZoom.BorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.toolZoom.BorderRadius = 10;
            this.toolZoom.BorderThickness = 3;
            this.toolZoom.Controls.Add(this.guna2HtmlLabel4);
            this.toolZoom.Location = new System.Drawing.Point(788, 413);
            this.toolZoom.Name = "toolZoom";
            this.toolZoom.Size = new System.Drawing.Size(49, 88);
            this.toolZoom.TabIndex = 11;
            // 
            // guna2HtmlLabel4
            // 
            this.guna2HtmlLabel4.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel4.Font = new System.Drawing.Font("Noto Sans KR Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.guna2HtmlLabel4.Location = new System.Drawing.Point(23, 26);
            this.guna2HtmlLabel4.Name = "guna2HtmlLabel4";
            this.guna2HtmlLabel4.Size = new System.Drawing.Size(3, 2);
            this.guna2HtmlLabel4.TabIndex = 9;
            this.guna2HtmlLabel4.Text = null;
            // 
            // toolBox
            // 
            this.toolBox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.toolBox.BorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.toolBox.BorderRadius = 10;
            this.toolBox.BorderThickness = 3;
            this.toolBox.Controls.Add(this.toolRedo);
            this.toolBox.Controls.Add(this.toolUndo);
            this.toolBox.Controls.Add(this.toolVisible);
            this.toolBox.Controls.Add(this.toolLabelingSquare);
            this.toolBox.Controls.Add(this.toolDelete);
            this.toolBox.Controls.Add(this.toolLabelingPolygon);
            this.toolBox.Controls.Add(this.toolHand);
            this.toolBox.Controls.Add(this.guna2HtmlLabel3);
            this.toolBox.Location = new System.Drawing.Point(788, 26);
            this.toolBox.Name = "toolBox";
            this.toolBox.Size = new System.Drawing.Size(49, 355);
            this.toolBox.TabIndex = 10;
            this.toolBox.Paint += new System.Windows.Forms.PaintEventHandler(this.guna2GradientPanel1_Paint_1);
            // 
            // toolRedo
            // 
            this.toolRedo.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.toolRedo.HoverState.ImageSize = new System.Drawing.Size(64, 64);
            this.toolRedo.Image = global::SAI.Properties.Resources.replay;
            this.toolRedo.ImageOffset = new System.Drawing.Point(0, 0);
            this.toolRedo.ImageRotate = 0F;
            this.toolRedo.ImageSize = new System.Drawing.Size(30, 30);
            this.toolRedo.Location = new System.Drawing.Point(10, 208);
            this.toolRedo.Name = "toolRedo";
            this.toolRedo.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            this.toolRedo.Size = new System.Drawing.Size(30, 30);
            this.toolRedo.TabIndex = 15;
            // 
            // toolUndo
            // 
            this.toolUndo.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.toolUndo.HoverState.ImageSize = new System.Drawing.Size(64, 64);
            this.toolUndo.Image = global::SAI.Properties.Resources.replay1;
            this.toolUndo.ImageOffset = new System.Drawing.Point(0, 0);
            this.toolUndo.ImageRotate = 0F;
            this.toolUndo.ImageSize = new System.Drawing.Size(30, 30);
            this.toolUndo.Location = new System.Drawing.Point(10, 162);
            this.toolUndo.Name = "toolUndo";
            this.toolUndo.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            this.toolUndo.Size = new System.Drawing.Size(30, 30);
            this.toolUndo.TabIndex = 14;
            // 
            // toolVisible
            // 
            this.toolVisible.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.toolVisible.HoverState.ImageSize = new System.Drawing.Size(64, 64);
            this.toolVisible.Image = global::SAI.Properties.Resources.visibility_off;
            this.toolVisible.ImageOffset = new System.Drawing.Point(0, 0);
            this.toolVisible.ImageRotate = 0F;
            this.toolVisible.ImageSize = new System.Drawing.Size(30, 30);
            this.toolVisible.Location = new System.Drawing.Point(10, 304);
            this.toolVisible.Name = "toolVisible";
            this.toolVisible.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            this.toolVisible.Size = new System.Drawing.Size(30, 30);
            this.toolVisible.TabIndex = 13;
            // 
            // toolLabelingSquare
            // 
            this.toolLabelingSquare.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.toolLabelingSquare.HoverState.ImageSize = new System.Drawing.Size(64, 64);
            this.toolLabelingSquare.Image = global::SAI.Properties.Resources.check_box_outline_blank;
            this.toolLabelingSquare.ImageOffset = new System.Drawing.Point(0, 0);
            this.toolLabelingSquare.ImageRotate = 0F;
            this.toolLabelingSquare.ImageSize = new System.Drawing.Size(30, 30);
            this.toolLabelingSquare.Location = new System.Drawing.Point(9, 63);
            this.toolLabelingSquare.Name = "toolLabelingSquare";
            this.toolLabelingSquare.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            this.toolLabelingSquare.Size = new System.Drawing.Size(30, 30);
            this.toolLabelingSquare.TabIndex = 12;
            // 
            // toolDelete
            // 
            this.toolDelete.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.toolDelete.HoverState.ImageSize = new System.Drawing.Size(64, 64);
            this.toolDelete.Image = global::SAI.Properties.Resources.delete;
            this.toolDelete.ImageOffset = new System.Drawing.Point(0, 0);
            this.toolDelete.ImageRotate = 0F;
            this.toolDelete.ImageSize = new System.Drawing.Size(23, 23);
            this.toolDelete.Location = new System.Drawing.Point(14, 259);
            this.toolDelete.Name = "toolDelete";
            this.toolDelete.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            this.toolDelete.Size = new System.Drawing.Size(23, 23);
            this.toolDelete.TabIndex = 11;
            // 
            // toolLabelingPolygon
            // 
            this.toolLabelingPolygon.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.toolLabelingPolygon.HoverState.ImageSize = new System.Drawing.Size(64, 64);
            this.toolLabelingPolygon.Image = global::SAI.Properties.Resources.ph_polygon_thin;
            this.toolLabelingPolygon.ImageOffset = new System.Drawing.Point(0, 0);
            this.toolLabelingPolygon.ImageRotate = 0F;
            this.toolLabelingPolygon.ImageSize = new System.Drawing.Size(30, 30);
            this.toolLabelingPolygon.Location = new System.Drawing.Point(10, 112);
            this.toolLabelingPolygon.Name = "toolLabelingPolygon";
            this.toolLabelingPolygon.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            this.toolLabelingPolygon.Size = new System.Drawing.Size(30, 30);
            this.toolLabelingPolygon.TabIndex = 10;
            // 
            // toolHand
            // 
            this.toolHand.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.toolHand.HoverState.ImageSize = new System.Drawing.Size(30, 30);
            this.toolHand.Image = global::SAI.Properties.Resources.back_hand;
            this.toolHand.ImageOffset = new System.Drawing.Point(0, 0);
            this.toolHand.ImageRotate = 0F;
            this.toolHand.ImageSize = new System.Drawing.Size(30, 30);
            this.toolHand.Location = new System.Drawing.Point(10, 17);
            this.toolHand.Name = "toolHand";
            this.toolHand.PressedState.ImageSize = new System.Drawing.Size(30, 30);
            this.toolHand.Size = new System.Drawing.Size(30, 30);
            this.toolHand.TabIndex = 0;
            // 
            // guna2HtmlLabel3
            // 
            this.guna2HtmlLabel3.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel3.Font = new System.Drawing.Font("Noto Sans KR Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.guna2HtmlLabel3.Location = new System.Drawing.Point(23, 26);
            this.guna2HtmlLabel3.Name = "guna2HtmlLabel3";
            this.guna2HtmlLabel3.Size = new System.Drawing.Size(3, 2);
            this.guna2HtmlLabel3.TabIndex = 9;
            this.guna2HtmlLabel3.Text = null;
            // 
            // currentLevel
            // 
            this.currentLevel.BackColor = System.Drawing.Color.Transparent;
            this.currentLevel.Font = new System.Drawing.Font("Noto Sans KR Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.currentLevel.Location = new System.Drawing.Point(23, 26);
            this.currentLevel.Name = "currentLevel";
            this.currentLevel.Size = new System.Drawing.Size(67, 26);
            this.currentLevel.TabIndex = 9;
            this.currentLevel.Text = "단계 예시";
            // 
            // pictureBoxImage
            // 
            this.pictureBoxImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBoxImage.FillColor = System.Drawing.Color.Silver;
            this.pictureBoxImage.Image = global::SAI.Properties.Resources._1;
            this.pictureBoxImage.ImageRotate = 0F;
            this.pictureBoxImage.Location = new System.Drawing.Point(98, 52);
            this.pictureBoxImage.Name = "pictureBoxImage";
            this.pictureBoxImage.Size = new System.Drawing.Size(640, 424);
            this.pictureBoxImage.TabIndex = 0;
            this.pictureBoxImage.TabStop = false;
            this.pictureBoxImage.WaitOnLoad = true;
            this.pictureBoxImage.Click += new System.EventHandler(this.pictureBoxImage_Click_1);
            // 
            // nextBtn
            // 
            this.nextBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.nextBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.nextBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.nextBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.nextBtn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.nextBtn.ForeColor = System.Drawing.Color.White;
            this.nextBtn.Location = new System.Drawing.Point(1213, 341);
            this.nextBtn.Name = "nextBtn";
            this.nextBtn.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.nextBtn.Size = new System.Drawing.Size(39, 39);
            this.nextBtn.TabIndex = 5;
            // 
            // preBtn
            // 
            this.preBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.preBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.preBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.preBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.preBtn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.preBtn.ForeColor = System.Drawing.Color.White;
            this.preBtn.Location = new System.Drawing.Point(28, 341);
            this.preBtn.Name = "preBtn";
            this.preBtn.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.preBtn.Size = new System.Drawing.Size(39, 39);
            this.preBtn.TabIndex = 6;
            // 
            // leftPanel
            // 
            this.leftPanel.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.leftPanel.BorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.leftPanel.BorderRadius = 30;
            this.leftPanel.BorderThickness = 3;
            this.leftPanel.Controls.Add(this.class1);
            this.leftPanel.Controls.Add(this.guna2HtmlLabel2);
            this.leftPanel.CustomizableEdges.BottomRight = false;
            this.leftPanel.CustomizableEdges.TopRight = false;
            this.leftPanel.Location = new System.Drawing.Point(98, 103);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Size = new System.Drawing.Size(223, 532);
            this.leftPanel.TabIndex = 4;
            this.leftPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.guna2GradientPanel1_Paint);
            // 
            // class1
            // 
            this.class1.BackColor = System.Drawing.Color.Transparent;
            this.class1.Font = new System.Drawing.Font("Noto Sans KR Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.class1.Location = new System.Drawing.Point(56, 87);
            this.class1.Name = "class1";
            this.class1.Size = new System.Drawing.Size(82, 26);
            this.class1.TabIndex = 8;
            this.class1.Text = "클래스 예시";
            // 
            // guna2HtmlLabel2
            // 
            this.guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel2.Font = new System.Drawing.Font("Noto Sans KR Medium", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.guna2HtmlLabel2.Location = new System.Drawing.Point(41, 26);
            this.guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            this.guna2HtmlLabel2.Size = new System.Drawing.Size(132, 37);
            this.guna2HtmlLabel2.TabIndex = 7;
            this.guna2HtmlLabel2.Text = "0단계 라벨링";
            // 
            // UcLabelGuide
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SAI.Properties.Resources.img_background;
            this.Controls.Add(this.leftPanel);
            this.Controls.Add(this.preBtn);
            this.Controls.Add(this.nextBtn);
            this.Controls.Add(this.imageContainer);
            this.Controls.Add(this.showLevel);
            this.Controls.Add(this.guna2HtmlLabel1);
            this.Controls.Add(this.guna2CircleButton1);
            this.Name = "UcLabelGuide";
            this.Size = new System.Drawing.Size(1280, 720);
            this.imageContainer.ResumeLayout(false);
            this.imageContainer.PerformLayout();
            this.toolZoom.ResumeLayout(false);
            this.toolZoom.PerformLayout();
            this.toolBox.ResumeLayout(false);
            this.toolBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).EndInit();
            this.leftPanel.ResumeLayout(false);
            this.leftPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2CircleButton guna2CircleButton1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel showLevel;
        private Guna.UI2.WinForms.Guna2GradientPanel imageContainer;
        private Guna.UI2.WinForms.Guna2CircleButton nextBtn;
        private Guna.UI2.WinForms.Guna2CircleButton preBtn;
        private Guna.UI2.WinForms.Guna2GradientPanel leftPanel;
        private Guna.UI2.WinForms.Guna2PictureBox pictureBoxImage;
        private Guna.UI2.WinForms.Guna2HtmlLabel class1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2HtmlLabel currentLevel;
        private Guna.UI2.WinForms.Guna2GradientPanel toolZoom;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel4;
        private Guna.UI2.WinForms.Guna2GradientPanel toolBox;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel3;
        private Guna.UI2.WinForms.Guna2HtmlLabel accuracyLabel;
        private Guna.UI2.WinForms.Guna2ImageButton toolRedo;
        private Guna.UI2.WinForms.Guna2ImageButton toolUndo;
        private Guna.UI2.WinForms.Guna2ImageButton toolVisible;
        private Guna.UI2.WinForms.Guna2ImageButton toolLabelingSquare;
        private Guna.UI2.WinForms.Guna2ImageButton toolDelete;
        private Guna.UI2.WinForms.Guna2ImageButton toolLabelingPolygon;
        private Guna.UI2.WinForms.Guna2ImageButton toolHand;
    }
}
