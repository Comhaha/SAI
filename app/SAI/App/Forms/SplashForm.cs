using System;
using System.Windows.Forms;

namespace SAI.App.Forms
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

            timer = new Timer();
            timer.Interval = 1500;
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
