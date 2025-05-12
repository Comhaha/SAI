namespace SAI.SAI.App.Views.Pages
{
    partial class UcSelectType
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcSelectType));
            this.ibtnAudio = new Guna.UI2.WinForms.Guna2ImageButton();
            this.ibtnImage = new Guna.UI2.WinForms.Guna2ImageButton();
            this.ibtnPose = new Guna.UI2.WinForms.Guna2ImageButton();
            this.ibtnNext = new Guna.UI2.WinForms.Guna2ImageButton();
            this.lblTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblImage = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblAudio = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblPose = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.SuspendLayout();
            // 
            // ibtnAudio
            // 
            this.ibtnAudio.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnAudio.HoverState.Image = global::SAI.Properties.Resources.btn_audio_hover;
            this.ibtnAudio.HoverState.ImageSize = new System.Drawing.Size(217, 261);
            this.ibtnAudio.Image = global::SAI.Properties.Resources.btn_audio;
            this.ibtnAudio.ImageOffset = new System.Drawing.Point(0, 0);
            this.ibtnAudio.ImageRotate = 0F;
            this.ibtnAudio.ImageSize = new System.Drawing.Size(217, 261);
            this.ibtnAudio.IndicateFocus = true;
            this.ibtnAudio.Location = new System.Drawing.Point(515, 205);
            this.ibtnAudio.Name = "ibtnAudio";
            this.ibtnAudio.PressedState.ImageSize = new System.Drawing.Size(217, 261);
            this.ibtnAudio.Size = new System.Drawing.Size(217, 261);
            this.ibtnAudio.TabIndex = 0;
            // 
            // ibtnImage
            // 
            this.ibtnImage.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnImage.HoverState.Image = global::SAI.Properties.Resources.btn_image_hover;
            this.ibtnImage.HoverState.ImageSize = new System.Drawing.Size(217, 261);
            this.ibtnImage.Image = global::SAI.Properties.Resources.btn_image;
            this.ibtnImage.ImageOffset = new System.Drawing.Point(0, 0);
            this.ibtnImage.ImageRotate = 0F;
            this.ibtnImage.ImageSize = new System.Drawing.Size(217, 261);
            this.ibtnImage.IndicateFocus = true;
            this.ibtnImage.Location = new System.Drawing.Point(200, 205);
            this.ibtnImage.Name = "ibtnImage";
            this.ibtnImage.PressedState.ImageSize = new System.Drawing.Size(217, 261);
            this.ibtnImage.Size = new System.Drawing.Size(217, 261);
            this.ibtnImage.TabIndex = 1;
            this.ibtnImage.Click += new System.EventHandler(this.guna2ImageButton1_Click);
            // 
            // ibtnPose
            // 
            this.ibtnPose.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnPose.HoverState.Image = global::SAI.Properties.Resources.btn_pose_hover;
            this.ibtnPose.HoverState.ImageSize = new System.Drawing.Size(217, 261);
            this.ibtnPose.Image = global::SAI.Properties.Resources.btn_pose;
            this.ibtnPose.ImageOffset = new System.Drawing.Point(0, 0);
            this.ibtnPose.ImageRotate = 0F;
            this.ibtnPose.ImageSize = new System.Drawing.Size(217, 261);
            this.ibtnPose.IndicateFocus = true;
            this.ibtnPose.Location = new System.Drawing.Point(831, 205);
            this.ibtnPose.Name = "ibtnPose";
            this.ibtnPose.PressedState.ImageSize = new System.Drawing.Size(217, 261);
            this.ibtnPose.Size = new System.Drawing.Size(217, 261);
            this.ibtnPose.TabIndex = 2;
            // 
            // ibtnNext
            // 
            this.ibtnNext.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnNext.HoverState.Image = global::SAI.Properties.Resources.btn_next_select_hover;
            this.ibtnNext.HoverState.ImageSize = new System.Drawing.Size(213, 69);
            this.ibtnNext.Image = global::SAI.Properties.Resources.btn_next_select;
            this.ibtnNext.ImageOffset = new System.Drawing.Point(0, 0);
            this.ibtnNext.ImageRotate = 0F;
            this.ibtnNext.ImageSize = new System.Drawing.Size(213, 69);
            this.ibtnNext.Location = new System.Drawing.Point(515, 582);
            this.ibtnNext.Name = "ibtnNext";
            this.ibtnNext.PressedState.ImageSize = new System.Drawing.Size(213, 69);
            this.ibtnNext.Size = new System.Drawing.Size(213, 69);
            this.ibtnNext.TabIndex = 3;
            this.ibtnNext.Click += new System.EventHandler(this.ibtnNext_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Noto Sans KR", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTitle.Location = new System.Drawing.Point(131, 69);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1019, 118);
            this.lblTitle.TabIndex = 5;
            this.lblTitle.Text = "학습 데이터 종류를 선택하세요";
            this.lblTitle.Click += new System.EventHandler(this.guna2HtmlLabel2_Click);
            // 
            // lblImage
            // 
            this.lblImage.BackColor = System.Drawing.Color.Transparent;
            this.lblImage.Font = new System.Drawing.Font("Noto Sans KR", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblImage.Location = new System.Drawing.Point(241, 486);
            this.lblImage.Name = "lblImage";
            this.lblImage.Size = new System.Drawing.Size(135, 72);
            this.lblImage.TabIndex = 6;
            this.lblImage.Text = "이미지";
            // 
            // lblAudio
            // 
            this.lblAudio.BackColor = System.Drawing.Color.Transparent;
            this.lblAudio.Font = new System.Drawing.Font("Noto Sans KR", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblAudio.Location = new System.Drawing.Point(551, 488);
            this.lblAudio.Name = "lblAudio";
            this.lblAudio.Size = new System.Drawing.Size(135, 72);
            this.lblAudio.TabIndex = 7;
            this.lblAudio.Text = "오디오";
            this.lblAudio.Click += new System.EventHandler(this.guna2HtmlLabel1_Click);
            // 
            // lblPose
            // 
            this.lblPose.BackColor = System.Drawing.Color.Transparent;
            this.lblPose.Font = new System.Drawing.Font("Noto Sans KR", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblPose.Location = new System.Drawing.Point(893, 488);
            this.lblPose.Name = "lblPose";
            this.lblPose.Size = new System.Drawing.Size(91, 72);
            this.lblPose.TabIndex = 8;
            this.lblPose.Text = "포즈";
            // 
            // UcSelectType
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.Controls.Add(this.lblPose);
            this.Controls.Add(this.lblAudio);
            this.Controls.Add(this.lblImage);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.ibtnNext);
            this.Controls.Add(this.ibtnPose);
            this.Controls.Add(this.ibtnImage);
            this.Controls.Add(this.ibtnAudio);
            this.Name = "UcSelectType";
            this.Size = new System.Drawing.Size(1280, 720);
            this.Load += new System.EventHandler(this.UcSelectType_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2ImageButton ibtnAudio;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnImage;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnPose;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnNext;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblImage;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblAudio;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblPose;
    }
}
