namespace SAI
{
    partial class MainForm
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

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
			this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
			this.chromiumWebBrowser1 = new CefSharp.WinForms.ChromiumWebBrowser();
			this.btnPip = new Guna.UI2.WinForms.Guna2Button();
			this.SuspendLayout();
			// 
			// guna2HtmlLabel1
			// 
			this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
			this.guna2HtmlLabel1.Font = new System.Drawing.Font("Pretendard", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.guna2HtmlLabel1.Location = new System.Drawing.Point(429, 403);
			this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
			this.guna2HtmlLabel1.Size = new System.Drawing.Size(989, 117);
			this.guna2HtmlLabel1.TabIndex = 1;
			this.guna2HtmlLabel1.Text = "임시　메인　페이지　입니다．";
			this.guna2HtmlLabel1.Click += new System.EventHandler(this.guna2HtmlLabel1_Click);
			// 
			// chromiumWebBrowser1
			// 
			this.chromiumWebBrowser1.ActivateBrowserOnCreation = false;
			this.chromiumWebBrowser1.Location = new System.Drawing.Point(574, 32);
			this.chromiumWebBrowser1.Name = "chromiumWebBrowser1";
			this.chromiumWebBrowser1.Size = new System.Drawing.Size(628, 721);
			this.chromiumWebBrowser1.TabIndex = 2;
			// 
			// btnPip
			// 
			this.btnPip.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnPip.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnPip.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnPip.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnPip.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.btnPip.ForeColor = System.Drawing.Color.White;
			this.btnPip.Location = new System.Drawing.Point(334, 121);
			this.btnPip.Name = "btnPip";
			this.btnPip.Size = new System.Drawing.Size(180, 45);
			this.btnPip.TabIndex = 3;
			this.btnPip.Text = "패키지 설치";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1920, 1080);
			this.Controls.Add(this.btnPip);
			this.Controls.Add(this.chromiumWebBrowser1);
			this.Controls.Add(this.guna2HtmlLabel1);
			this.Name = "MainForm";
			this.Text = "메인페이지";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
		private CefSharp.WinForms.ChromiumWebBrowser chromiumWebBrowser1;
		private Guna.UI2.WinForms.Guna2Button btnPip;
	}
}

