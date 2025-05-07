using System;
using System.Windows.Forms;
using SAI.SAI.App.Forms;
using CefSharp;
using CefSharp.WinForms;
using System.IO;
using SAI.SAI.App.Views.Interfaces;
using SAI.SAI.App.Presenters;
using SAI.SAI.App.Models;
using SAI.SAI.App.Views.Pages;
using System.Linq;
using System.Web.UI.WebControls;
using System.Drawing;


namespace SAI
{
    public partial class MainForm : Form, IMainView
    {
		private MainPresenter presenter;

		public MainForm()
        {
            InitializeComponent();
			presenter = new MainPresenter(this);

			Size = new Size(1280, 720);
			MaximizeBox = false;
			MaximumSize = new Size(1280, 720);
			MinimumSize = new Size(1280, 720);
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			// 초기 페이지인 Blockly를 불러온다.
			presenter.Initialize();
		}

		private void guna2HtmlLabel1_Click(object sender, EventArgs e)
		{

		}

		// 이건 Presenter가 호출할 메서드(UI에 있는 패널에 있던 페이지를 지우고, 크기를 채우고, 페이지를 넣는다.)
		public void LoadPage(UserControl page)
		{
			guna2Panel1.Controls.Clear();
			page.Dock = DockStyle.Fill;
			guna2Panel1.BackColor = Color.Transparent;
			guna2Panel1.Controls.Add(page);
			guna2Panel1.BringToFront();
		}
	}
}
