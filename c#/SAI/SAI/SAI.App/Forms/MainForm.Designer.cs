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
            this.logOutput = new Guna.UI2.WinForms.Guna2TextBox();
            this.YoloRun = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Controls.Add(this.logOutput);
            this.guna2Panel1.Controls.Add(this.YoloRun);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Margin = new System.Windows.Forms.Padding(4);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(2496, 1440);
            this.guna2Panel1.TabIndex = 4;
            // 
            // logOutput
            // 
            this.logOutput.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.logOutput.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.logOutput.DefaultText = "";
            this.logOutput.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.logOutput.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.logOutput.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.logOutput.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.logOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logOutput.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.logOutput.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.logOutput.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.logOutput.Location = new System.Drawing.Point(0, 0);
            this.logOutput.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.logOutput.Multiline = true;
            this.logOutput.Name = "logOutput";
            this.logOutput.PlaceholderText = "";
            this.logOutput.ReadOnly = true;
            this.logOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logOutput.SelectedText = "";
            this.logOutput.Size = new System.Drawing.Size(2496, 1440);
            this.logOutput.TabIndex = 3;
            this.logOutput.Visible = false;
            this.logOutput.TextChanged += new System.EventHandler(this.logOutput_TextChanged);
            // 
            // YoloRun
            // 
            this.YoloRun.DefaultAutoSize = true;
            this.YoloRun.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.YoloRun.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.YoloRun.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.YoloRun.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.YoloRun.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.YoloRun.ForeColor = System.Drawing.Color.White;
            this.YoloRun.Location = new System.Drawing.Point(940, 305);
            this.YoloRun.Name = "YoloRun";
            this.YoloRun.Size = new System.Drawing.Size(125, 44);
            this.YoloRun.TabIndex = 2;
            this.YoloRun.Text = "YoloRun";
            this.YoloRun.Click += new System.EventHandler(this.guna2Button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2496, 1440);
            this.Controls.Add(this.guna2Panel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "메인페이지";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2Button YoloRun;
        private Guna.UI2.WinForms.Guna2TextBox logOutput;
    }
}
