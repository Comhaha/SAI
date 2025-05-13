namespace SAI.SAI.App.Views.Pages
{
    partial class YoloTutorial
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
            this.YoloTutorialRun = new Guna.UI2.WinForms.Guna2Button();
            this.logOutput = new Guna.UI2.WinForms.Guna2TextBox();
            this.SuspendLayout();
            // 
            // YoloTutorialRun
            // 
            this.YoloTutorialRun.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.YoloTutorialRun.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.YoloTutorialRun.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.YoloTutorialRun.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.YoloTutorialRun.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.YoloTutorialRun.ForeColor = System.Drawing.Color.White;
            this.YoloTutorialRun.Location = new System.Drawing.Point(282, 232);
            this.YoloTutorialRun.Name = "YoloTutorialRun";
            this.YoloTutorialRun.Size = new System.Drawing.Size(180, 45);
            this.YoloTutorialRun.TabIndex = 0;
            this.YoloTutorialRun.Text = "YoloTutorialRun";
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
            this.logOutput.Size = new System.Drawing.Size(775, 521);
            this.logOutput.TabIndex = 1;
            this.logOutput.Visible = false;
            // 
            // YoloTutorial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.logOutput);
            this.Controls.Add(this.YoloTutorialRun);
            this.Name = "YoloTutorial";
            this.Size = new System.Drawing.Size(775, 521);
            this.Load += new System.EventHandler(this.YoloTutorial_Load);
            this.ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button YoloTutorialRun;
        private Guna.UI2.WinForms.Guna2TextBox logOutput;
    }
}
