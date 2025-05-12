namespace SAI.SAI.App.Forms.Dialogs
{
	partial class DialogCompleteTutorial
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
			this.labelTitle = new System.Windows.Forms.Label();
			this.btnClose = new Guna.UI2.WinForms.Guna2Button();
			this.panelIcon = new Guna.UI2.WinForms.Guna2Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.labelContent = new System.Windows.Forms.Label();
			this.btnLearnModel = new Guna.UI2.WinForms.Guna2Button();
			this.panelTitleBar.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelTitleBar
			// 
			this.panelTitleBar.BackgroundImage = global::SAI.Properties.Resources.bg_blue_titlebar_629;
			this.panelTitleBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.panelTitleBar.Controls.Add(this.labelTitle);
			this.panelTitleBar.Controls.Add(this.btnClose);
			this.panelTitleBar.Location = new System.Drawing.Point(0, 0);
			this.panelTitleBar.Name = "panelTitleBar";
			this.panelTitleBar.Size = new System.Drawing.Size(629, 40);
			this.panelTitleBar.TabIndex = 0;
			// 
			// labelTitle
			// 
			this.labelTitle.AutoSize = true;
			this.labelTitle.BackColor = System.Drawing.Color.Transparent;
			this.labelTitle.Font = new System.Drawing.Font("Noto Sans KR", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
			this.labelTitle.Location = new System.Drawing.Point(25, 8);
			this.labelTitle.Name = "labelTitle";
			this.labelTitle.Size = new System.Drawing.Size(113, 25);
			this.labelTitle.TabIndex = 1;
			this.labelTitle.Text = "튜토리얼 완료";
			// 
			// btnClose
			// 
			this.btnClose.BackColor = System.Drawing.Color.Transparent;
			this.btnClose.BackgroundImage = global::SAI.Properties.Resources.bg_blue_btn_close;
			this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnClose.BorderColor = System.Drawing.Color.Transparent;
			this.btnClose.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnClose.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnClose.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnClose.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnClose.FillColor = System.Drawing.Color.Transparent;
			this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.btnClose.ForeColor = System.Drawing.Color.White;
			this.btnClose.Image = global::SAI.Properties.Resources.btn_close;
			this.btnClose.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.btnClose.ImageOffset = new System.Drawing.Point(8, 0);
			this.btnClose.ImageSize = new System.Drawing.Size(15, 16);
			this.btnClose.Location = new System.Drawing.Point(569, 0);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(60, 40);
			this.btnClose.TabIndex = 1;
			// 
			// panelIcon
			// 
			this.panelIcon.BackColor = System.Drawing.Color.Transparent;
			this.panelIcon.BackgroundImage = global::SAI.Properties.Resources.icon_check_good;
			this.panelIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.panelIcon.Location = new System.Drawing.Point(35, 74);
			this.panelIcon.Name = "panelIcon";
			this.panelIcon.Size = new System.Drawing.Size(25, 30);
			this.panelIcon.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Font = new System.Drawing.Font("Noto Sans KR Medium", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
			this.label1.Location = new System.Drawing.Point(66, 70);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(360, 34);
			this.label1.TabIndex = 2;
			this.label1.Text = "ToDoList를 모두 완수하였습니다.";
			// 
			// labelContent
			// 
			this.labelContent.AutoSize = true;
			this.labelContent.BackColor = System.Drawing.Color.Transparent;
			this.labelContent.Font = new System.Drawing.Font("Noto Sans KR", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.labelContent.Location = new System.Drawing.Point(69, 114);
			this.labelContent.Name = "labelContent";
			this.labelContent.Size = new System.Drawing.Size(467, 58);
			this.labelContent.TabIndex = 3;
			this.labelContent.Text = "모델 학습 단계의 TO DO LIST를 모두 완수하였습니다.\r\n[실습하러 가기] 버튼을 누르면 다음 단계로 이동합니다. ";
			// 
			// btnLearnModel
			// 
			this.btnLearnModel.BackColor = System.Drawing.Color.Transparent;
			this.btnLearnModel.BackgroundImage = global::SAI.Properties.Resources.btn_gotrain;
			this.btnLearnModel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnLearnModel.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnLearnModel.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnLearnModel.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnLearnModel.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnLearnModel.FillColor = System.Drawing.Color.Transparent;
			this.btnLearnModel.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.btnLearnModel.ForeColor = System.Drawing.Color.White;
			this.btnLearnModel.Location = new System.Drawing.Point(460, 202);
			this.btnLearnModel.Margin = new System.Windows.Forms.Padding(4);
			this.btnLearnModel.Name = "btnLearnModel";
			this.btnLearnModel.Size = new System.Drawing.Size(129, 40);
			this.btnLearnModel.TabIndex = 4;
			// 
			// DialogCompleteTutorial
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.Gray;
			this.BackgroundImage = global::SAI.Properties.Resources.bg_dialog_white_629_282;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.ClientSize = new System.Drawing.Size(629, 282);
			this.Controls.Add(this.btnLearnModel);
			this.Controls.Add(this.labelContent);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.panelIcon);
			this.Controls.Add(this.panelTitleBar);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.Name = "DialogCompleteTutorial";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.TopMost = true;
			this.panelTitleBar.ResumeLayout(false);
			this.panelTitleBar.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Guna.UI2.WinForms.Guna2Panel panelTitleBar;
		private Guna.UI2.WinForms.Guna2Button btnClose;
		private System.Windows.Forms.Label labelTitle;
		private Guna.UI2.WinForms.Guna2Panel panelIcon;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label labelContent;
		private Guna.UI2.WinForms.Guna2Button btnLearnModel;
	}
}