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
			this.btnPip = new Guna.UI2.WinForms.Guna2Button();
			this.btnLoadModel = new Guna.UI2.WinForms.Guna2Button();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.btnDialog = new Guna.UI2.WinForms.Guna2CircleButton();
			this.btnLoadDataset = new Guna.UI2.WinForms.Guna2Button();
			this.btnMachineLearning = new Guna.UI2.WinForms.Guna2Button();
			this.btnStart = new Guna.UI2.WinForms.Guna2Button();
			this.btnResultGraph = new Guna.UI2.WinForms.Guna2Button();
			this.webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
			((System.ComponentModel.ISupportInitialize)(this.webView21)).BeginInit();
			this.SuspendLayout();
			// 
			// btnPip
			// 
			this.btnPip.BackColor = System.Drawing.Color.Transparent;
			this.btnPip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnPip.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnPip.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnPip.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnPip.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnPip.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.btnPip.ForeColor = System.Drawing.Color.White;
			this.btnPip.ImageSize = new System.Drawing.Size(59, 50);
			this.btnPip.Location = new System.Drawing.Point(77, 63);
			this.btnPip.Margin = new System.Windows.Forms.Padding(2);
			this.btnPip.Name = "btnPip";
			this.btnPip.Size = new System.Drawing.Size(212, 38);
			this.btnPip.TabIndex = 1;
			this.btnPip.Text = "패키지 설치";
			// 
			// btnLoadModel
			// 
			this.btnLoadModel.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnLoadModel.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnLoadModel.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnLoadModel.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnLoadModel.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.btnLoadModel.ForeColor = System.Drawing.Color.White;
			this.btnLoadModel.Location = new System.Drawing.Point(77, 106);
			this.btnLoadModel.Name = "btnLoadModel";
			this.btnLoadModel.Size = new System.Drawing.Size(212, 38);
			this.btnLoadModel.TabIndex = 2;
			this.btnLoadModel.Text = "모델 불러오기";
			// 
			// richTextBox1
			// 
			this.richTextBox1.Location = new System.Drawing.Point(863, 33);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(325, 626);
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
			// btnLoadDataset
			// 
			this.btnLoadDataset.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnLoadDataset.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnLoadDataset.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnLoadDataset.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnLoadDataset.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.btnLoadDataset.ForeColor = System.Drawing.Color.White;
			this.btnLoadDataset.Location = new System.Drawing.Point(77, 150);
			this.btnLoadDataset.Name = "btnLoadDataset";
			this.btnLoadDataset.Size = new System.Drawing.Size(212, 38);
			this.btnLoadDataset.TabIndex = 5;
			this.btnLoadDataset.Text = "데이터 불러오기";
			// 
			// btnMachineLearning
			// 
			this.btnMachineLearning.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnMachineLearning.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnMachineLearning.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnMachineLearning.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnMachineLearning.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.btnMachineLearning.ForeColor = System.Drawing.Color.White;
			this.btnMachineLearning.Location = new System.Drawing.Point(77, 194);
			this.btnMachineLearning.Name = "btnMachineLearning";
			this.btnMachineLearning.Size = new System.Drawing.Size(212, 38);
			this.btnMachineLearning.TabIndex = 6;
			this.btnMachineLearning.Text = "모델 학습하기";
			// 
			// btnStart
			// 
			this.btnStart.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnStart.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnStart.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnStart.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnStart.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.btnStart.ForeColor = System.Drawing.Color.White;
			this.btnStart.Location = new System.Drawing.Point(77, 20);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(212, 38);
			this.btnStart.TabIndex = 7;
			this.btnStart.Text = "start";
			// 
			// btnResultGraph
			// 
			this.btnResultGraph.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnResultGraph.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnResultGraph.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnResultGraph.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnResultGraph.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.btnResultGraph.ForeColor = System.Drawing.Color.White;
			this.btnResultGraph.Location = new System.Drawing.Point(77, 238);
			this.btnResultGraph.Name = "btnResultGraph";
			this.btnResultGraph.Size = new System.Drawing.Size(212, 38);
			this.btnResultGraph.TabIndex = 8;
			this.btnResultGraph.Text = "학습 결과 그래프 출력하기";
			// 
			// webView21
			// 
			this.webView21.AllowExternalDrop = true;
			this.webView21.CreationProperties = null;
			this.webView21.Cursor = System.Windows.Forms.Cursors.Hand;
			this.webView21.DefaultBackgroundColor = System.Drawing.Color.White;
			this.webView21.Location = new System.Drawing.Point(362, 33);
			this.webView21.Margin = new System.Windows.Forms.Padding(0);
			this.webView21.Name = "webView21";
			this.webView21.Size = new System.Drawing.Size(447, 626);
			this.webView21.TabIndex = 9;
			this.webView21.TabStop = false;
			this.webView21.ZoomFactor = 1D;
			// 
			// Blockly
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackgroundImage = global::SAI.Properties.Resources.img_background;
			this.Controls.Add(this.webView21);
			this.Controls.Add(this.btnResultGraph);
			this.Controls.Add(this.btnStart);
			this.Controls.Add(this.btnMachineLearning);
			this.Controls.Add(this.btnLoadDataset);
			this.Controls.Add(this.btnDialog);
			this.Controls.Add(this.richTextBox1);
			this.Controls.Add(this.btnLoadModel);
			this.Controls.Add(this.btnPip);
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "Blockly";
			this.Size = new System.Drawing.Size(1280, 720);
			((System.ComponentModel.ISupportInitialize)(this.webView21)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private Guna.UI2.WinForms.Guna2Button btnPip;
		private Guna.UI2.WinForms.Guna2Button btnLoadModel;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private Guna.UI2.WinForms.Guna2CircleButton btnDialog;
		private Guna.UI2.WinForms.Guna2Button btnLoadDataset;
		private Guna.UI2.WinForms.Guna2Button btnMachineLearning;
		private Guna.UI2.WinForms.Guna2Button btnStart;
		private Guna.UI2.WinForms.Guna2Button btnResultGraph;
		private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
	}
}
