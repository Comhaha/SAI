using System;
using System.Windows.Forms;

namespace SAI.SAI.App.Forms
{
    public partial class SplashForm : Form
    {
        private readonly Timer timer;
        public SplashForm()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Size = new System.Drawing.Size(1920, 1080);

            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        
        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            this.Close();
        }
    }
}
