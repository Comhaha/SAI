using System;
using System.Windows.Forms;
using System.Drawing;

namespace SAI.SAI.App.Forms
{
    public partial class BaseForm : Form

    {
        private PictureBox img_background;
        public BaseForm()
        {
            this.AutoScaleMode = AutoScaleMode.None; // DPI 자동 조정 끄기
            this.ClientSize = new Size(1280, 720);

            this.StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(1280, 720);
			MaximizeBox = false;
            MaximumSize = new Size(1280, 720);
            MinimumSize = new Size(1280, 720);
			this.StartPosition = FormStartPosition.CenterScreen;

            // 폼 사이즈 고정 설정
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = true;  
            this.ControlBox = true;
            this.ShowIcon = true;    

            // 사이즈 고정
            this.MinimumSize = new Size(1280, 720);
            this.MaximumSize = new Size(1280, 720);
            
            InitializeBackgroundImage();
        }

        private void InitializeBackgroundImage()
        {
            img_background = new PictureBox
            {
                Dock = DockStyle.Fill,
                Image = Properties.Resources.img_background,
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            this.Controls.Add(img_background);
            img_background.SendToBack();
        }

        private void BaseForm_Load(object sender, EventArgs e)
        {

        }

    }

}
