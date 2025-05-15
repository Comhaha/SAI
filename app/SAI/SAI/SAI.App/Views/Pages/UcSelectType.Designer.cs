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
			this.btnImage = new Guna.UI2.WinForms.Guna2Button();
			this.btnAudio = new Guna.UI2.WinForms.Guna2Button();
			this.btnPose = new Guna.UI2.WinForms.Guna2Button();
			this.tpBtnParent = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.lbTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
			this.tpBtnParent.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnImage
			// 
			this.btnImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnImage.BackColor = System.Drawing.Color.Transparent;
			this.btnImage.BackgroundImage = global::SAI.Properties.Resources.btn_image;
			this.btnImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.btnImage.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnImage.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnImage.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnImage.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnImage.FillColor = System.Drawing.Color.Transparent;
			this.btnImage.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.btnImage.ForeColor = System.Drawing.Color.White;
			this.btnImage.Location = new System.Drawing.Point(20, 20);
			this.btnImage.Margin = new System.Windows.Forms.Padding(20);
			this.btnImage.Name = "btnImage";
			this.btnImage.Size = new System.Drawing.Size(251, 315);
			this.btnImage.TabIndex = 0;
			this.btnImage.Click += new System.EventHandler(this.btnImage_Click);
			// 
			// btnAudio
			// 
			this.btnAudio.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAudio.BackColor = System.Drawing.Color.Transparent;
			this.btnAudio.BackgroundImage = global::SAI.Properties.Resources.btn_audio;
			this.btnAudio.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.btnAudio.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnAudio.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnAudio.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnAudio.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnAudio.FillColor = System.Drawing.Color.Transparent;
			this.btnAudio.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.btnAudio.ForeColor = System.Drawing.Color.White;
			this.btnAudio.Location = new System.Drawing.Point(311, 20);
			this.btnAudio.Margin = new System.Windows.Forms.Padding(20);
			this.btnAudio.Name = "btnAudio";
			this.btnAudio.Size = new System.Drawing.Size(251, 315);
			this.btnAudio.TabIndex = 10;
			this.btnAudio.Click += new System.EventHandler(this.btnAudio_Click);
			// 
			// btnPose
			// 
			this.btnPose.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPose.BackColor = System.Drawing.Color.Transparent;
			this.btnPose.BackgroundImage = global::SAI.Properties.Resources.btn_pose;
			this.btnPose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.btnPose.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnPose.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnPose.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnPose.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnPose.FillColor = System.Drawing.Color.Transparent;
			this.btnPose.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.btnPose.ForeColor = System.Drawing.Color.White;
			this.btnPose.Location = new System.Drawing.Point(602, 20);
			this.btnPose.Margin = new System.Windows.Forms.Padding(20);
			this.btnPose.Name = "btnPose";
			this.btnPose.Size = new System.Drawing.Size(253, 315);
			this.btnPose.TabIndex = 11;
			this.btnPose.Click += new System.EventHandler(this.btnPose_Click);
			// 
			// tpBtnParent
			// 
			this.tpBtnParent.BackColor = System.Drawing.Color.Transparent;
			this.tpBtnParent.ColumnCount = 3;
			this.tpBtnParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tpBtnParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tpBtnParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tpBtnParent.Controls.Add(this.btnImage, 0, 0);
			this.tpBtnParent.Controls.Add(this.btnPose, 2, 0);
			this.tpBtnParent.Controls.Add(this.btnAudio, 1, 0);
			this.tpBtnParent.Location = new System.Drawing.Point(3, 121);
			this.tpBtnParent.Name = "tpBtnParent";
			this.tpBtnParent.RowCount = 1;
			this.tpBtnParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tpBtnParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tpBtnParent.Size = new System.Drawing.Size(875, 355);
			this.tpBtnParent.TabIndex = 12;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.tpBtnParent, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.lbTitle, 0, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(212, 78);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.63465F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75.36534F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(881, 479);
			this.tableLayoutPanel1.TabIndex = 13;
			// 
			// lbTitle
			// 
			this.lbTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lbTitle.AutoSize = false;
			this.lbTitle.BackColor = System.Drawing.Color.Transparent;
			this.lbTitle.Font = new System.Drawing.Font("Noto Sans KR SemiBold", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbTitle.Location = new System.Drawing.Point(3, 3);
			this.lbTitle.Name = "lbTitle";
			this.lbTitle.Size = new System.Drawing.Size(875, 112);
			this.lbTitle.TabIndex = 14;
			this.lbTitle.Text = "학습 데이터 종류를 선택하세요";
			this.lbTitle.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// UcSelectType
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.AutoSize = true;
			this.BackgroundImage = global::SAI.Properties.Resources.img_background1;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "UcSelectType";
			this.Size = new System.Drawing.Size(1280, 720);
			this.tpBtnParent.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion
		private Guna.UI2.WinForms.Guna2Button btnImage;
		private Guna.UI2.WinForms.Guna2Button btnAudio;
		private Guna.UI2.WinForms.Guna2Button btnPose;
		private System.Windows.Forms.TableLayoutPanel tpBtnParent;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private Guna.UI2.WinForms.Guna2HtmlLabel lbTitle;
	}
}
