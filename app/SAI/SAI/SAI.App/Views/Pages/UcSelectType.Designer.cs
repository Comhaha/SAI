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
			this.pTItle = new System.Windows.Forms.Panel();
			this.tp = new System.Windows.Forms.TableLayoutPanel();
			this.tpTitle = new System.Windows.Forms.TableLayoutPanel();
			this.tpBtn = new System.Windows.Forms.TableLayoutPanel();
			this.tp.SuspendLayout();
			this.tpTitle.SuspendLayout();
			this.tpBtn.SuspendLayout();
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
			this.btnImage.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnImage.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnImage.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnImage.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnImage.FillColor = System.Drawing.Color.Transparent;
			this.btnImage.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.btnImage.ForeColor = System.Drawing.Color.White;
			this.btnImage.Location = new System.Drawing.Point(3, 3);
			this.btnImage.Name = "btnImage";
			this.btnImage.Size = new System.Drawing.Size(337, 455);
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
			this.btnAudio.Location = new System.Drawing.Point(403, 3);
			this.btnAudio.Name = "btnAudio";
			this.btnAudio.Size = new System.Drawing.Size(337, 455);
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
			this.btnPose.Location = new System.Drawing.Point(803, 3);
			this.btnPose.Name = "btnPose";
			this.btnPose.Size = new System.Drawing.Size(340, 455);
			this.btnPose.TabIndex = 11;
			this.btnPose.Click += new System.EventHandler(this.btnPose_Click);
			// 
			// pTItle
			// 
			this.pTItle.BackColor = System.Drawing.Color.Transparent;
			this.pTItle.BackgroundImage = global::SAI.Properties.Resources.pSelectTypeTitle1;
			this.pTItle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.pTItle.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pTItle.Location = new System.Drawing.Point(232, 3);
			this.pTItle.Name = "pTItle";
			this.pTItle.Size = new System.Drawing.Size(681, 83);
			this.pTItle.TabIndex = 12;
			// 
			// tp
			// 
			this.tp.BackColor = System.Drawing.Color.White;
			this.tp.BackgroundImage = global::SAI.Properties.Resources.img_background1;
			this.tp.ColumnCount = 3;
			this.tp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
			this.tp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tp.Controls.Add(this.tpTitle, 1, 1);
			this.tp.Controls.Add(this.tpBtn, 1, 2);
			this.tp.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tp.ForeColor = System.Drawing.Color.White;
			this.tp.Location = new System.Drawing.Point(0, 0);
			this.tp.Name = "tp";
			this.tp.RowCount = 4;
			this.tp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.83333F));
			this.tp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.40741F));
			this.tp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 43.24074F));
			this.tp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.33333F));
			this.tp.Size = new System.Drawing.Size(1920, 1080);
			this.tp.TabIndex = 13;
			// 
			// tpTitle
			// 
			this.tpTitle.BackColor = System.Drawing.Color.Transparent;
			this.tpTitle.ColumnCount = 3;
			this.tpTitle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tpTitle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
			this.tpTitle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tpTitle.Controls.Add(this.pTItle, 1, 0);
			this.tpTitle.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tpTitle.Location = new System.Drawing.Point(387, 174);
			this.tpTitle.Name = "tpTitle";
			this.tpTitle.RowCount = 2;
			this.tpTitle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 69.85294F));
			this.tpTitle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.14706F));
			this.tpTitle.Size = new System.Drawing.Size(1146, 128);
			this.tpTitle.TabIndex = 0;
			// 
			// tpBtn
			// 
			this.tpBtn.BackColor = System.Drawing.Color.Transparent;
			this.tpBtn.ColumnCount = 5;
			this.tpBtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
			this.tpBtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
			this.tpBtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
			this.tpBtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
			this.tpBtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
			this.tpBtn.Controls.Add(this.btnImage, 0, 0);
			this.tpBtn.Controls.Add(this.btnPose, 4, 0);
			this.tpBtn.Controls.Add(this.btnAudio, 2, 0);
			this.tpBtn.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tpBtn.Location = new System.Drawing.Point(387, 308);
			this.tpBtn.Name = "tpBtn";
			this.tpBtn.RowCount = 1;
			this.tpBtn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tpBtn.Size = new System.Drawing.Size(1146, 461);
			this.tpBtn.TabIndex = 1;
			// 
			// UcSelectType
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.BackgroundImage = global::SAI.Properties.Resources.img_background1;
			this.Controls.Add(this.tp);
			this.DoubleBuffered = true;
			this.Name = "UcSelectType";
			this.Size = new System.Drawing.Size(1920, 1080);
			this.tp.ResumeLayout(false);
			this.tpTitle.ResumeLayout(false);
			this.tpBtn.ResumeLayout(false);
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
		private TableLayoutPanel tpContentTitle;
		private Panel pTItle;
		private TableLayoutPanel tp;
		private TableLayoutPanel tpTitle;
		private TableLayoutPanel tpBtn;
	}
}
