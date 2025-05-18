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
			this.SuspendLayout();
			// 
			// btnImage
			// 
			this.btnImage.BackColor = System.Drawing.Color.Transparent;
			this.btnImage.BackgroundImage = global::SAI.Properties.Resources.btn_image;
			this.btnImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnImage.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnImage.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnImage.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnImage.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnImage.FillColor = System.Drawing.Color.Transparent;
			this.btnImage.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.btnImage.ForeColor = System.Drawing.Color.White;
			this.btnImage.Location = new System.Drawing.Point(325, 280);
			this.btnImage.Name = "btnImage";
			this.btnImage.Size = new System.Drawing.Size(313, 481);
			this.btnImage.TabIndex = 0;
			this.btnImage.Click += new System.EventHandler(this.btnImage_Click);
			// 
			// btnAudio
			// 
			this.btnAudio.BackColor = System.Drawing.Color.Transparent;
			this.btnAudio.BackgroundImage = global::SAI.Properties.Resources.btn_audio;
			this.btnAudio.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnAudio.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnAudio.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnAudio.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnAudio.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnAudio.FillColor = System.Drawing.Color.Transparent;
			this.btnAudio.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.btnAudio.ForeColor = System.Drawing.Color.White;
			this.btnAudio.Location = new System.Drawing.Point(801, 280);
			this.btnAudio.Name = "btnAudio";
			this.btnAudio.Size = new System.Drawing.Size(313, 481);
			this.btnAudio.TabIndex = 10;
			this.btnAudio.Click += new System.EventHandler(this.btnAudio_Click);
			// 
			// btnPose
			// 
			this.btnPose.BackColor = System.Drawing.Color.Transparent;
			this.btnPose.BackgroundImage = global::SAI.Properties.Resources.btn_pose;
			this.btnPose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnPose.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnPose.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnPose.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnPose.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnPose.FillColor = System.Drawing.Color.Transparent;
			this.btnPose.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.btnPose.ForeColor = System.Drawing.Color.White;
			this.btnPose.Location = new System.Drawing.Point(1278, 280);
			this.btnPose.Name = "btnPose";
			this.btnPose.Size = new System.Drawing.Size(313, 481);
			this.btnPose.TabIndex = 11;
			this.btnPose.Click += new System.EventHandler(this.btnPose_Click);
			// 
			// pTItle
			// 
			this.pTItle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pTItle.BackColor = System.Drawing.Color.Transparent;
			this.pTItle.BackgroundImage = global::SAI.Properties.Resources.pSelectTypeTitle1;
			this.pTItle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.pTItle.Location = new System.Drawing.Point(479, 116);
			this.pTItle.Name = "pTItle";
			this.pTItle.Size = new System.Drawing.Size(956, 72);
			this.pTItle.TabIndex = 12;
			// 
			// UcSelectType
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackgroundImage = global::SAI.Properties.Resources.img_background1;
			this.Controls.Add(this.pTItle);
			this.Controls.Add(this.btnPose);
			this.Controls.Add(this.btnAudio);
			this.Controls.Add(this.btnImage);
			this.Name = "UcSelectType";
			this.Size = new System.Drawing.Size(1920, 1080);
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
	}
}
