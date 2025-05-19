namespace SAI.SAI.App.Forms.Dialogs
{
	partial class DialogHomeFromTrain
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
			this.btnOk = new Guna.UI2.WinForms.Guna2Button();
			this.btnCancel = new Guna.UI2.WinForms.Guna2Button();
			this.panelTitleBar.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelTitleBar
			// 
			this.panelTitleBar.BackgroundImage = global::SAI.Properties.Resources.bg_titlebar_home_from_train;
			this.panelTitleBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.panelTitleBar.Controls.Add(this.btnClose);
			this.panelTitleBar.Location = new System.Drawing.Point(0, 0);
			this.panelTitleBar.Name = "panelTitleBar";
			this.panelTitleBar.Size = new System.Drawing.Size(684, 50);
			this.panelTitleBar.TabIndex = 0;
			// 
			// btnClose
			// 
			this.btnClose.BackColor = System.Drawing.Color.Transparent;
			this.btnClose.BackgroundImage = global::SAI.Properties.Resources.bg_btn_close;
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
			this.btnClose.Location = new System.Drawing.Point(613, 0);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(71, 49);
			this.btnClose.TabIndex = 1;
			// 
			// btnOk
			// 
			this.btnOk.BackColor = System.Drawing.Color.Transparent;
			this.btnOk.BackgroundImage = global::SAI.Properties.Resources.btn_red_ok;
			this.btnOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.btnOk.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnOk.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnOk.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnOk.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnOk.FillColor = System.Drawing.Color.Transparent;
			this.btnOk.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.btnOk.ForeColor = System.Drawing.Color.White;
			this.btnOk.Location = new System.Drawing.Point(509, 291);
			this.btnOk.Margin = new System.Windows.Forms.Padding(0);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(125, 50);
			this.btnOk.TabIndex = 4;
			// 
			// btnCancel
			// 
			this.btnCancel.BackColor = System.Drawing.Color.Transparent;
			this.btnCancel.BackgroundImage = global::SAI.Properties.Resources.btn_white_cancel;
			this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.btnCancel.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnCancel.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnCancel.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnCancel.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnCancel.FillColor = System.Drawing.Color.Transparent;
			this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.btnCancel.ForeColor = System.Drawing.Color.White;
			this.btnCancel.ImageSize = new System.Drawing.Size(110, 42);
			this.btnCancel.Location = new System.Drawing.Point(364, 291);
			this.btnCancel.Margin = new System.Windows.Forms.Padding(0);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(125, 50);
			this.btnCancel.TabIndex = 5;
			// 
			// DialogHomeFromTrain
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.Gray;
			this.BackgroundImage = global::SAI.Properties.Resources.bg_dialog_home_from_train;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.ClientSize = new System.Drawing.Size(684, 381);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.panelTitleBar);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.Name = "DialogHomeFromTrain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "X";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.DialogHomeFromTrain_Load);
			this.panelTitleBar.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Guna.UI2.WinForms.Guna2Panel panelTitleBar;
		private Guna.UI2.WinForms.Guna2Button btnClose;
		private Guna.UI2.WinForms.Guna2Button btnOk;
		private Guna.UI2.WinForms.Guna2Button btnCancel;
	}
}