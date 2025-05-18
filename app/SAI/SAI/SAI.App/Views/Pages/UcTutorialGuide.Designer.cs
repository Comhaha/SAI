using System.Windows.Forms;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcTutorialGuide));
            this.nextBtn = new Guna.UI2.WinForms.Guna2CircleButton();
            this.preBtn = new Guna.UI2.WinForms.Guna2CircleButton();
            this.goLabelingBtn = new Guna.UI2.WinForms.Guna2CircleButton();
            this.exit = new Guna.UI2.WinForms.Guna2Button();
            this.goToLabeling = new Guna.UI2.WinForms.Guna2Button();
            this.tutorialDoneBtn = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // nextBtn
            // 
            this.nextBtn.BackColor = System.Drawing.Color.Transparent;
            this.nextBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.nextBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.nextBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.nextBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.nextBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.nextBtn.FillColor = System.Drawing.Color.Transparent;
            this.nextBtn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.nextBtn.ForeColor = System.Drawing.Color.White;
            this.nextBtn.HoverState.Image = global::SAI.Properties.Resources.NoCircleArrowHoverR;
            this.nextBtn.Image = global::SAI.Properties.Resources.NoCircleArrowR;
            this.nextBtn.ImageSize = new System.Drawing.Size(70, 70);
            this.nextBtn.Location = new System.Drawing.Point(1527, 509);
            this.nextBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nextBtn.Name = "nextBtn";
            this.nextBtn.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.nextBtn.Size = new System.Drawing.Size(70, 70);
            this.nextBtn.TabIndex = 0;
            this.nextBtn.Click += new System.EventHandler(this.nextBtn_Click_1);
            // 
            // preBtn
            // 
            this.preBtn.BackColor = System.Drawing.Color.Transparent;
            this.preBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.preBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.preBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.preBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.preBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.preBtn.FillColor = System.Drawing.Color.Transparent;
            this.preBtn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.preBtn.ForeColor = System.Drawing.Color.White;
            this.preBtn.HoverState.Image = global::SAI.Properties.Resources.NoCircleArrowHoverL;
            this.preBtn.Image = global::SAI.Properties.Resources.NoCircleArrowL;
            this.preBtn.ImageSize = new System.Drawing.Size(70, 70);
            this.preBtn.Location = new System.Drawing.Point(321, 509);
            this.preBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.preBtn.Name = "preBtn";
            this.preBtn.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.preBtn.Size = new System.Drawing.Size(70, 70);
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
            this.goLabelingBtn.Image = ((System.Drawing.Image)(resources.GetObject("goLabelingBtn.Image")));
            this.goLabelingBtn.ImageSize = new System.Drawing.Size(168, 38);
            this.goLabelingBtn.Location = new System.Drawing.Point(1423, 813);
            this.goLabelingBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.goLabelingBtn.Name = "goLabelingBtn";
            this.goLabelingBtn.PressedColor = System.Drawing.Color.Transparent;
            this.goLabelingBtn.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.goLabelingBtn.Size = new System.Drawing.Size(170, 38);
            this.goLabelingBtn.TabIndex = 2;
            this.goLabelingBtn.Visible = false;
            this.goLabelingBtn.Click += new System.EventHandler(this.goLabelingBtn_Click_1);
            // 
            // exit
            // 
            this.exit.BackColor = System.Drawing.Color.Transparent;
            this.exit.BorderColor = System.Drawing.Color.Transparent;
            this.exit.FillColor = System.Drawing.Color.Transparent;
            this.exit.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.exit.ForeColor = System.Drawing.Color.Transparent;
            this.exit.HoverState.FillColor = System.Drawing.Color.Transparent;
            this.exit.HoverState.Image = global::SAI.Properties.Resources.btn_exit_3030_hover;
            this.exit.Image = global::SAI.Properties.Resources.btn_exit_3030;
            this.exit.ImageSize = new System.Drawing.Size(37, 37);
            this.exit.Location = new System.Drawing.Point(1802, 206);
            this.exit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.exit.Name = "exit";
            this.exit.PressedColor = System.Drawing.Color.Transparent;
            this.exit.Size = new System.Drawing.Size(40, 40);
            this.exit.TabIndex = 4;
            this.exit.Click += new System.EventHandler(this.exit_Click_1);
            // 
            // goToLabeling
            // 
            this.goToLabeling.BackColor = System.Drawing.Color.Transparent;
            this.goToLabeling.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.goToLabeling.BorderColor = System.Drawing.Color.Transparent;
            this.goToLabeling.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.goToLabeling.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.goToLabeling.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.goToLabeling.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.goToLabeling.FillColor = System.Drawing.Color.Transparent;
            this.goToLabeling.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.goToLabeling.ForeColor = System.Drawing.Color.Transparent;
            this.goToLabeling.HoverState.BorderColor = System.Drawing.Color.Transparent;
            this.goToLabeling.HoverState.CustomBorderColor = System.Drawing.Color.Transparent;
            this.goToLabeling.HoverState.FillColor = System.Drawing.Color.Transparent;
            this.goToLabeling.HoverState.Image = global::SAI.Properties.Resources.btn_close_whole_click;
            this.goToLabeling.Image = global::SAI.Properties.Resources.btn_close_whole_5;
            this.goToLabeling.ImageSize = new System.Drawing.Size(75, 60);
            this.goToLabeling.Location = new System.Drawing.Point(1555, 191);
            this.goToLabeling.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.goToLabeling.Name = "goToLabeling";
            this.goToLabeling.PressedColor = System.Drawing.Color.Transparent;
            this.goToLabeling.Size = new System.Drawing.Size(75, 55);
            this.goToLabeling.TabIndex = 5;
            this.goToLabeling.Click += new System.EventHandler(this.goToLabeling_Click_2);
            // 
            // tutorialDoneBtn
            // 
            this.tutorialDoneBtn.BackColor = System.Drawing.Color.Transparent;
            this.tutorialDoneBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.tutorialDoneBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.tutorialDoneBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.tutorialDoneBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.tutorialDoneBtn.FillColor = System.Drawing.Color.Transparent;
            this.tutorialDoneBtn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tutorialDoneBtn.ForeColor = System.Drawing.Color.White;
            this.tutorialDoneBtn.HoverState.BorderColor = System.Drawing.Color.Transparent;
            this.tutorialDoneBtn.HoverState.CustomBorderColor = System.Drawing.Color.Transparent;
            this.tutorialDoneBtn.HoverState.FillColor = System.Drawing.Color.Transparent;
            this.tutorialDoneBtn.HoverState.Image = global::SAI.Properties.Resources.btn_done_click;
            this.tutorialDoneBtn.Image = global::SAI.Properties.Resources.btn_done1;
            this.tutorialDoneBtn.ImageSize = new System.Drawing.Size(77, 35);
            this.tutorialDoneBtn.Location = new System.Drawing.Point(1610, 288);
            this.tutorialDoneBtn.Name = "tutorialDoneBtn";
            this.tutorialDoneBtn.PressedColor = System.Drawing.Color.Transparent;
            this.tutorialDoneBtn.Size = new System.Drawing.Size(90, 45);
            this.tutorialDoneBtn.TabIndex = 6;
            this.tutorialDoneBtn.Visible = false;
            this.tutorialDoneBtn.Click += new System.EventHandler(this.guna2Button1_Click);
            // 
            // UcTutorialGuide
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::SAI.Properties.Resources.전체튜토리얼가이드1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.tutorialDoneBtn);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.goLabelingBtn);
            this.Controls.Add(this.preBtn);
            this.Controls.Add(this.nextBtn);
            this.Controls.Add(this.goToLabeling);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UcTutorialGuide";
            this.Size = new System.Drawing.Size(1920, 1080);
            this.Load += new System.EventHandler(this.UcTutorialGuide_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2CircleButton nextBtn;
        private Guna.UI2.WinForms.Guna2CircleButton preBtn;
        private Guna.UI2.WinForms.Guna2CircleButton goLabelingBtn;
        private Guna.UI2.WinForms.Guna2Button exit;
        private Guna.UI2.WinForms.Guna2Button goToLabeling;
        private Guna.UI2.WinForms.Guna2Button tutorialDoneBtn;
    }
}
