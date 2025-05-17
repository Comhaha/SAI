using System.Windows.Forms;

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
			this.tpContentParent = new System.Windows.Forms.TableLayoutPanel();
			this.tpParent = new System.Windows.Forms.TableLayoutPanel();
			this.autoSizeTitle = new SAI.App.Views.Pages.AutoSizeLabel();
			this.tpBtnParent.SuspendLayout();
			this.tpContentParent.SuspendLayout();
			this.tpParent.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnImage
			// 
			this.btnImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnImage.BackColor = System.Drawing.Color.Transparent;
			this.btnImage.BackgroundImage = global::SAI.Properties.Resources.btn_image;
			this.btnImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.btnImage.DefaultAutoSize = true;
			this.btnImage.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnImage.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnImage.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnImage.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnImage.FillColor = System.Drawing.Color.Transparent;
			this.btnImage.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.btnImage.ForeColor = System.Drawing.Color.White;
			this.btnImage.Location = new System.Drawing.Point(51, 31);
			this.btnImage.Margin = new System.Windows.Forms.Padding(0);
			this.btnImage.Name = "btnImage";
			this.btnImage.Size = new System.Drawing.Size(304, 571);
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
			this.btnAudio.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.btnAudio.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnAudio.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnAudio.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnAudio.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnAudio.FillColor = System.Drawing.Color.Transparent;
			this.btnAudio.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.btnAudio.ForeColor = System.Drawing.Color.White;
			this.btnAudio.Location = new System.Drawing.Point(761, 31);
			this.btnAudio.Margin = new System.Windows.Forms.Padding(0);
			this.btnAudio.Name = "btnAudio";
			this.btnAudio.Size = new System.Drawing.Size(304, 571);
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
			this.btnPose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.btnPose.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnPose.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnPose.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnPose.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnPose.FillColor = System.Drawing.Color.Transparent;
			this.btnPose.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.btnPose.ForeColor = System.Drawing.Color.White;
			this.btnPose.Location = new System.Drawing.Point(406, 31);
			this.btnPose.Margin = new System.Windows.Forms.Padding(0);
			this.btnPose.Name = "btnPose";
			this.btnPose.Size = new System.Drawing.Size(304, 571);
			this.btnPose.TabIndex = 11;
			this.btnPose.Click += new System.EventHandler(this.btnPose_Click);
			// 
			// tpBtnParent
			// 
			this.tpBtnParent.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tpBtnParent.BackColor = System.Drawing.Color.LightSalmon;
			this.tpBtnParent.ColumnCount = 7;
			this.tpContentParent.SetColumnSpan(this.tpBtnParent, 3);
			this.tpBtnParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.621072F));
			this.tpBtnParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.1719F));
			this.tpBtnParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.621072F));
			this.tpBtnParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.1719F));
			this.tpBtnParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.621072F));
			this.tpBtnParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.1719F));
			this.tpBtnParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.621072F));
			this.tpBtnParent.Controls.Add(this.btnImage, 1, 1);
			this.tpBtnParent.Controls.Add(this.btnPose, 3, 1);
			this.tpBtnParent.Controls.Add(this.btnAudio, 5, 1);
			this.tpBtnParent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tpBtnParent.Location = new System.Drawing.Point(0, 158);
			this.tpBtnParent.Margin = new System.Windows.Forms.Padding(0);
			this.tpBtnParent.Name = "tpBtnParent";
			this.tpBtnParent.RowCount = 3;
			this.tpBtnParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
			this.tpBtnParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
			this.tpBtnParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
			this.tpBtnParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tpBtnParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tpBtnParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tpBtnParent.Size = new System.Drawing.Size(1122, 635);
			this.tpBtnParent.TabIndex = 12;
			// 
			// tpContentParent
			// 
			this.tpContentParent.BackColor = System.Drawing.Color.IndianRed;
			this.tpContentParent.ColumnCount = 1;
			this.tpContentParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tpContentParent.Controls.Add(this.autoSizeTitle, 0, 0);
			this.tpContentParent.Controls.Add(this.tpBtnParent, 0, 1);
			this.tpContentParent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tpContentParent.Location = new System.Drawing.Point(379, 269);
			this.tpContentParent.Name = "tpContentParent";
			this.tpContentParent.RowCount = 2;
			this.tpContentParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tpContentParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
			this.tpContentParent.Size = new System.Drawing.Size(1122, 793);
			this.tpContentParent.TabIndex = 13;
			// 
			// tpParent
			// 
			this.tpParent.BackColor = System.Drawing.Color.LightGray;
			this.tpParent.ColumnCount = 3;
			this.tpParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tpParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
			this.tpParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tpParent.Controls.Add(this.tpContentParent, 1, 1);
			this.tpParent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tpParent.Location = new System.Drawing.Point(0, 0);
			this.tpParent.Name = "tpParent";
			this.tpParent.RowCount = 3;
			this.tpParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tpParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
			this.tpParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tpParent.Size = new System.Drawing.Size(1880, 1332);
			this.tpParent.TabIndex = 14;
			this.tpParent.Paint += new System.Windows.Forms.PaintEventHandler(this.tpParent_Paint);
			// 
			// autoSizeTitle
			// 
			this.autoSizeTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.autoSizeTitle.Font = new System.Drawing.Font("Noto Sans KR SemiBold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.autoSizeTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
			this.autoSizeTitle.Location = new System.Drawing.Point(40, 0);
			this.autoSizeTitle.Margin = new System.Windows.Forms.Padding(40, 0, 40, 40);
			this.autoSizeTitle.Name = "autoSizeTitle";
			this.autoSizeTitle.Size = new System.Drawing.Size(1042, 118);
			this.autoSizeTitle.TabIndex = 14;
			this.autoSizeTitle.Text = "학습 데이터 종류를 선택하세요.";
			this.autoSizeTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// UcSelectType
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoSize = true;
			this.Controls.Add(this.tpParent);
			this.Name = "UcSelectType";
			this.Size = new System.Drawing.Size(1880, 1332);
			this.tpBtnParent.ResumeLayout(false);
			this.tpBtnParent.PerformLayout();
			this.tpContentParent.ResumeLayout(false);
			this.tpParent.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion
		private Guna.UI2.WinForms.Guna2Button btnImage;
		private Guna.UI2.WinForms.Guna2Button btnAudio;
		private Guna.UI2.WinForms.Guna2Button btnPose;
		private System.Windows.Forms.TableLayoutPanel tpBtnParent;
		private System.Windows.Forms.TableLayoutPanel tpContentParent;
		private SAI.App.Views.Pages.AutoSizeLabel autoSizeTitle;
		private System.Windows.Forms.TableLayoutPanel tpParent;
	}
}
