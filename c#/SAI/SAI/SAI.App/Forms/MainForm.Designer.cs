namespace SAI
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        private void InitializeComponent()
        {
			this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
			this.SuspendLayout();
			// 
			// guna2Panel1
			// 
			this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
			this.guna2Panel1.Margin = new System.Windows.Forms.Padding(2);
			this.guna2Panel1.Name = "guna2Panel1";
			this.guna2Panel1.Size = new System.Drawing.Size(1184, 659);
			this.guna2Panel1.TabIndex = 5;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1184, 659);
			this.Controls.Add(this.guna2Panel1);
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "MainForm";
			this.Text = "메인페이지";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);

        }

		#endregion
		private Guna.UI2.WinForms.Guna2Panel guna2Panel1;   
    }
}
