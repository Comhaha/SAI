namespace SAI.SAI.App.Views.Pages
{
    partial class UcTutorialGuide
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
            this.nextBtn = new Guna.UI2.WinForms.Guna2CircleButton();
            this.preBtn = new Guna.UI2.WinForms.Guna2CircleButton();
            this.goLabelingBtn = new Guna.UI2.WinForms.Guna2CircleButton();
            this.exit = new Guna.UI2.WinForms.Guna2Button();
            this.goToLabeling = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // nextBtn
            // 
            this.nextBtn.BackColor = System.Drawing.Color.Transparent;
            this.nextBtn.BackgroundImage = global::SAI.Properties.Resources.ic_next;
            this.nextBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.nextBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.nextBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.nextBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.nextBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.nextBtn.FillColor = System.Drawing.Color.Transparent;
            this.nextBtn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.nextBtn.ForeColor = System.Drawing.Color.White;
            this.nextBtn.Location = new System.Drawing.Point(1166, 424);
            this.nextBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nextBtn.Name = "nextBtn";
            this.nextBtn.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.nextBtn.Size = new System.Drawing.Size(47, 51);
            this.nextBtn.TabIndex = 0;
            // 
            // preBtn
            // 
            this.preBtn.BackColor = System.Drawing.Color.Transparent;
            this.preBtn.BackgroundImage = global::SAI.Properties.Resources.ic_pre;
            this.preBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.preBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.preBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.preBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.preBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.preBtn.FillColor = System.Drawing.Color.Transparent;
            this.preBtn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.preBtn.ForeColor = System.Drawing.Color.White;
            this.preBtn.Location = new System.Drawing.Point(253, 424);
            this.preBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.preBtn.Name = "preBtn";
            this.preBtn.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.preBtn.Size = new System.Drawing.Size(47, 51);
            this.preBtn.TabIndex = 1;
            // 
            // goLabelingBtn
            // 
            this.goLabelingBtn.BackColor = System.Drawing.Color.Transparent;
            this.goLabelingBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.goLabelingBtn.BorderColor = System.Drawing.Color.Transparent;
            this.goLabelingBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.goLabelingBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.goLabelingBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.goLabelingBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.goLabelingBtn.FillColor = System.Drawing.Color.Transparent;
            this.goLabelingBtn.FocusedColor = System.Drawing.Color.Transparent;
            this.goLabelingBtn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.goLabelingBtn.ForeColor = System.Drawing.Color.White;
            this.goLabelingBtn.HoverState.Image = global::SAI.Properties.Resources.btn_goLabelingHover;
            this.goLabelingBtn.Image = global::SAI.Properties.Resources.btn_goLabeling;
            this.goLabelingBtn.ImageSize = new System.Drawing.Size(212, 46);
            this.goLabelingBtn.Location = new System.Drawing.Point(968, 662);
            this.goLabelingBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.goLabelingBtn.Name = "goLabelingBtn";
            this.goLabelingBtn.PressedColor = System.Drawing.Color.Transparent;
            this.goLabelingBtn.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.goLabelingBtn.Size = new System.Drawing.Size(245, 58);
            this.goLabelingBtn.TabIndex = 2;
            this.goLabelingBtn.Visible = false;
            // 
            // exit
            // 
            this.exit.BackColor = System.Drawing.Color.Transparent;
            this.exit.BackgroundImage = global::SAI.Properties.Resources.btn_close;
            this.exit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.exit.BorderColor = System.Drawing.Color.Transparent;
            this.exit.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.exit.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.exit.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.exit.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.exit.FillColor = System.Drawing.Color.Transparent;
            this.exit.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.exit.ForeColor = System.Drawing.Color.Transparent;
            this.exit.Location = new System.Drawing.Point(989, 35);
            this.exit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(46, 50);
            this.exit.TabIndex = 4;
            // 
            // goToLabeling
            // 
            this.goToLabeling.BackColor = System.Drawing.Color.Transparent;
            this.goToLabeling.BackgroundImage = global::SAI.Properties.Resources.btn_close;
            this.goToLabeling.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.goToLabeling.BorderColor = System.Drawing.Color.Transparent;
            this.goToLabeling.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.goToLabeling.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.goToLabeling.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.goToLabeling.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.goToLabeling.FillColor = System.Drawing.Color.Transparent;
            this.goToLabeling.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.goToLabeling.ForeColor = System.Drawing.Color.Transparent;
            this.goToLabeling.Location = new System.Drawing.Point(1183, 166);
            this.goToLabeling.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.goToLabeling.Name = "goToLabeling";
            this.goToLabeling.Size = new System.Drawing.Size(34, 38);
            this.goToLabeling.TabIndex = 5;
            // 
            // UcTutorialGuide
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::SAI.Properties.Resources.전체튜토리얼가이드1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.goToLabeling);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.goLabelingBtn);
            this.Controls.Add(this.preBtn);
            this.Controls.Add(this.nextBtn);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UcTutorialGuide";
            this.Size = new System.Drawing.Size(1463, 900);
            this.Load += new System.EventHandler(this.UcTutorialGuide_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2CircleButton nextBtn;
        private Guna.UI2.WinForms.Guna2CircleButton preBtn;
        private Guna.UI2.WinForms.Guna2CircleButton goLabelingBtn;
        private Guna.UI2.WinForms.Guna2Button exit;
        private Guna.UI2.WinForms.Guna2Button goToLabeling;
    }
}
