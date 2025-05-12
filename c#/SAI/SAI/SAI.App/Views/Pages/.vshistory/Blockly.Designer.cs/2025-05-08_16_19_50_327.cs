namespace SAI.SAI.App.Views.Pages
{
	partial class Blockly
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
			this.chromiumWebBrowser1 = new CefSharp.WinForms.ChromiumWebBrowser();
			this.btnPip = new Guna.UI2.WinForms.Guna2Button();
			this.btnHello = new Guna.UI2.WinForms.Guna2Button();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.btnDialog = new Guna.UI2.WinForms.Guna2CircleButton();
			this.SuspendLayout();
			// 
			// chromiumWebBrowser1
			// 
			this.chromiumWebBrowser1.ActivateBrowserOnCreation = false;
			this.chromiumWebBrowser1.Location = new System.Drawing.Point(319, 19);
			this.chromiumWebBrowser1.Margin = new System.Windows.Forms.Padding(2);
			this.chromiumWebBrowser1.Name = "chromiumWebBrowser1";
			this.chromiumWebBrowser1.Size = new System.Drawing.Size(500, 662);
			this.chromiumWebBrowser1.TabIndex = 0;
			// 
			// btnPip
			// 
			this.btnPip.BackColor = System.Drawing.Color.Transparent;
			this.btnPip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnPip.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnPip.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnPip.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnPip.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnPip.FillColor = System.Drawing.Color.Red;
			this.btnPip.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.btnPip.ForeColor = System.Drawing.Color.White;
			this.btnPip.ImageSize = new System.Drawing.Size(59, 50);
			this.btnPip.Location = new System.Drawing.Point(64, 88);
			this.btnPip.Margin = new System.Windows.Forms.Padding(2);
			this.btnPip.Name = "btnPip";
			this.btnPip.Size = new System.Drawing.Size(212, 38);
			this.btnPip.TabIndex = 1;
			this.btnPip.Text = "패키지 설치";
			// 
			// btnHello
			// 
			this.btnHello.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnHello.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnHello.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnHello.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnHello.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.btnHello.ForeColor = System.Drawing.Color.White;
			this.btnHello.Location = new System.Drawing.Point(64, 142);
			this.btnHello.Name = "btnHello";
			this.btnHello.Size = new System.Drawing.Size(212, 45);
			this.btnHello.TabIndex = 2;
			this.btnHello.Text = "안녕!";
			// 
			// richTextBox1
			// 
			this.richTextBox1.Location = new System.Drawing.Point(863, 20);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(325, 661);
			this.richTextBox1.TabIndex = 3;
			this.richTextBox1.Text = "";
			// 
			// btnDialog
			// 
			this.btnDialog.BackColor = System.Drawing.Color.Transparent;
			this.btnDialog.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnDialog.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnDialog.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnDialog.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnDialog.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.btnDialog.ForeColor = System.Drawing.Color.White;
			this.btnDialog.Location = new System.Drawing.Point(77, 488);
			this.btnDialog.Name = "btnDialog";
			this.btnDialog.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
			this.btnDialog.Size = new System.Drawing.Size(148, 148);
			this.btnDialog.TabIndex = 4;
			this.btnDialog.Text = "showdialog";
			this.btnDialog.Click += new System.EventHandler(this.btnDialog_Click);
			// 
			// Blockly
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackgroundImage = global::SAI.Properties.Resources.img_background;
			this.Controls.Add(this.btnDialog);
			this.Controls.Add(this.richTextBox1);
			this.Controls.Add(this.btnHello);
			this.Controls.Add(this.btnPip);
			this.Controls.Add(this.chromiumWebBrowser1);
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "Blockly";
			this.Size = new System.Drawing.Size(1280, 720);
			this.ResumeLayout(false);

		}

		#endregion

		private CefSharp.WinForms.ChromiumWebBrowser chromiumWebBrowser1;
		private Guna.UI2.WinForms.Guna2Button btnPip;
		private Guna.UI2.WinForms.Guna2Button btnHello;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private Guna.UI2.WinForms.Guna2CircleButton btnDialog;
	}
}
