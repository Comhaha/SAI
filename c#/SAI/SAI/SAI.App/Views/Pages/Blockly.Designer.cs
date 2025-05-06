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
			this.components = new System.ComponentModel.Container();
			this.chromiumWebBrowser1 = new CefSharp.WinForms.ChromiumWebBrowser();
			this.btnPip = new Guna.UI2.WinForms.Guna2Button();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.SuspendLayout();
			// 
			// chromiumWebBrowser1
			// 
			this.chromiumWebBrowser1.ActivateBrowserOnCreation = false;
			this.chromiumWebBrowser1.Location = new System.Drawing.Point(399, 23);
			this.chromiumWebBrowser1.Name = "chromiumWebBrowser1";
			this.chromiumWebBrowser1.Size = new System.Drawing.Size(700, 800);
			this.chromiumWebBrowser1.TabIndex = 0;
			// 
			// btnPip
			// 
			this.btnPip.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnPip.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnPip.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnPip.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnPip.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.btnPip.ForeColor = System.Drawing.Color.White;
			this.btnPip.Location = new System.Drawing.Point(80, 106);
			this.btnPip.Name = "btnPip";
			this.btnPip.Size = new System.Drawing.Size(265, 45);
			this.btnPip.TabIndex = 1;
			this.btnPip.Text = "패키지 설치";
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
			// 
			// Blockly
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btnPip);
			this.Controls.Add(this.chromiumWebBrowser1);
			this.Name = "Blockly";
			this.Size = new System.Drawing.Size(1920, 1080);
			this.ResumeLayout(false);

		}

		#endregion

		private CefSharp.WinForms.ChromiumWebBrowser chromiumWebBrowser1;
		private Guna.UI2.WinForms.Guna2Button btnPip;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
	}
}
