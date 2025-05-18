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
			this.btnClose = new Guna.UI2.WinForms.Guna2Button();
			this.btnLearnModel = new Guna.UI2.WinForms.Guna2Button();
			this.panelTitleBar.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelTitleBar
			// 
			this.panelTitleBar.BackgroundImage = global::SAI.Properties.Resources.bg_titlebar_complete_tutorial;
			this.panelTitleBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.panelTitleBar.Controls.Add(this.btnClose);
			this.panelTitleBar.Location = new System.Drawing.Point(0, 0);
			this.panelTitleBar.Name = "panelTitleBar";
			this.panelTitleBar.Size = new System.Drawing.Size(729, 50);
			this.panelTitleBar.TabIndex = 0;
			// 
			// btnClose
			// 
			this.btnClose.BackColor = System.Drawing.Color.Transparent;
			this.btnClose.BackgroundImage = global::SAI.Properties.Resources.bg_blue_btn_close;
			this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.btnClose.BorderColor = System.Drawing.Color.Transparent;
			this.btnClose.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnClose.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnClose.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnClose.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnClose.FillColor = System.Drawing.Color.Transparent;
			this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.btnClose.ForeColor = System.Drawing.Color.White;
			this.btnClose.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.btnClose.ImageOffset = new System.Drawing.Point(8, 0);
			this.btnClose.ImageSize = new System.Drawing.Size(15, 16);
			this.btnClose.Location = new System.Drawing.Point(658, 0);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(71, 49);
			this.btnClose.TabIndex = 1;
			// 
			// btnLearnModel
			// 
			this.btnLearnModel.BackColor = System.Drawing.Color.Transparent;
			this.btnLearnModel.BackgroundImage = global::SAI.Properties.Resources.btn_gotrain;
			this.btnLearnModel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.btnLearnModel.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnLearnModel.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnLearnModel.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnLearnModel.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnLearnModel.FillColor = System.Drawing.Color.Transparent;
			this.btnLearnModel.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.btnLearnModel.ForeColor = System.Drawing.Color.White;
			this.btnLearnModel.Location = new System.Drawing.Point(466, 251);
			this.btnLearnModel.Margin = new System.Windows.Forms.Padding(0);
			this.btnLearnModel.Name = "btnLearnModel";
			this.btnLearnModel.Size = new System.Drawing.Size(213, 50);
			this.btnLearnModel.TabIndex = 4;
			// 
			// DialogCompleteTutorial
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.Gray;
			this.BackgroundImage = global::SAI.Properties.Resources.bg_dialog_complete_tutorial;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.ClientSize = new System.Drawing.Size(729, 341);
			this.Controls.Add(this.btnLearnModel);
			this.Controls.Add(this.panelTitleBar);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.Name = "DialogCompleteTutorial";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.TopMost = true;
			this.panelTitleBar.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Guna.UI2.WinForms.Guna2Panel panelTitleBar;
		private Guna.UI2.WinForms.Guna2Button btnClose;
		private Guna.UI2.WinForms.Guna2Button btnLearnModel;
	}
}