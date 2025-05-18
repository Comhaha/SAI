using System;
using System.Windows.Forms;
using SAI.SAI.App.Forms;
using SAI.SAI.App.Views.Interfaces;
using SAI.SAI.App.Presenters;
using System.Drawing;
using SAI.SAI.App.Views.Pages;
using System.Linq;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Text.RegularExpressions;
using SAI.SAI.App.Models;
using SAI.SAI.App.Forms.Dialogs;
using Microsoft.Win32;

namespace SAI
{
	public partial class MainForm : Form, IMainView
	{
		private MainPresenter presenter;

		public MainForm()
		{
			InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
			this.mainPanel.MinimumSize = new Size(1920, 1080);
			this.mainPanel.Size = new Size(1920, 1080);
			this.AutoScaleMode = AutoScaleMode.None;

			presenter = new MainPresenter(this);

			// 사이즈 고정
			MinimumSize = new Size(1920, 1080);
			guna2DragControl1.TargetControl = tpTitlebar;
			guna2DragControl1.TransparentWhileDrag = false;
			guna2DragControl1.UseTransparentDrag = false;
			this.DoubleBuffered = true;
			this.Resize += MainForm_Resize;

			// btnClose
			btnClose.BackColor = Color.Transparent;
			btnClose.PressedColor = Color.Transparent;
			btnClose.CheckedState.FillColor = Color.Transparent;
			btnClose.DisabledState.FillColor = Color.Transparent;
			btnClose.HoverState.FillColor = Color.Transparent;
			// btnClose 마우스 입력 될 때
			btnClose.MouseEnter += (s, e) =>
			{
				btnClose.BackColor = Color.Transparent;
				btnClose.BackgroundImage = Properties.Resources.btn_titlebar_close_clicked;
			};
			// btnClose 마우스 떠날때
			btnClose.MouseLeave += (s, e) =>
			{
				btnClose.BackgroundImage = Properties.Resources.btn_titlebar_close;
			};


			// btnSetting
			btnSetting.BackColor = Color.Transparent;
			btnSetting.PressedColor = Color.Transparent;
			btnSetting.CheckedState.FillColor = Color.Transparent;
			btnSetting.DisabledState.FillColor = Color.Transparent;
			btnSetting.HoverState.FillColor = Color.Transparent;
			// btnSetting 마우스 입력 될 때
			btnSetting.MouseEnter += (s, e) =>
			{
				btnSetting.BackColor = Color.Transparent;
				btnSetting.BackgroundImage = Properties.Resources.btn_titlebar_setting_clicked;
			};
			// btnSetting 마우스 떠날때
			btnSetting.MouseLeave += (s, e) =>
			{
				btnSetting.BackgroundImage = Properties.Resources.btn_titlebar_setting;
			};


			// btnMinScreen
			btnMinScreen.BackColor = Color.Transparent;
			btnMinScreen.PressedColor = Color.Transparent;
			btnMinScreen.CheckedState.FillColor = Color.Transparent;
			btnMinScreen.DisabledState.FillColor = Color.Transparent;
			btnMinScreen.HoverState.FillColor = Color.Transparent;
			// btnMinScreen 마우스 입력 될 때
			btnMinScreen.MouseEnter += (s, e) =>
			{
				btnMinScreen.BackColor = Color.Transparent;
				btnMinScreen.BackgroundImage = Properties.Resources.btn_titlebar_minscreen_clicked;
			};
			// btnMinScreen 마우스 떠날때
			btnMinScreen.MouseLeave += (s, e) =>
			{
				btnMinScreen.BackgroundImage = Properties.Resources.btn_titlebar_minscreen;
			};

			// btnFullScreen
			btnFullScreen.BackColor = Color.Transparent;
			btnFullScreen.PressedColor = Color.Transparent;
			btnFullScreen.CheckedState.FillColor = Color.Transparent;
			btnFullScreen.DisabledState.FillColor = Color.Transparent;
			btnFullScreen.HoverState.FillColor = Color.Transparent;
			// btnFullScreen 마우스 입력 될 때
			btnFullScreen.MouseEnter += (s, e) =>
			{
				btnFullScreen.BackColor = Color.Transparent;
				btnFullScreen.BackgroundImage = Properties.Resources.btn_titlebar_fullscreen_clicked;
			};
			// btnFullScreen 마우스 떠날때
			btnFullScreen.MouseLeave += (s, e) =>
			{
				btnFullScreen.BackgroundImage = Properties.Resources.btn_titlebar_fullscreen;
			};
		}

		private int pageWidth = 1920;
		private int pageHeight = 1080;
		private void MainForm_Resize(object sender, EventArgs e)
		{
			int formWidth = this.ClientSize.Width;
			int formHeight = this.ClientSize.Height;

			int x = (formWidth - pageWidth) / 2;
			int y = (formHeight - pageHeight) / 2;

			mainPanel.Location = new Point(x, y);  // ✨ 중앙 정렬 위치 설정

			// 확인용
			MessageBox.Show($"Panel 위치: {mainPanel.Location}, 크기: {mainPanel.Size}");
		}


		private void MainForm_Load(object sender, EventArgs e)
		{
			// 초기 페이지인 UcSelectType을 불러온다.
			presenter.Initialize();
        }

		// 이건 Presenter가 호출할 메서드(UI에 있는 패널에 있던 페이지를 지우고, 크기를 채우고, 페이지를 넣는다.)
		public void LoadPage(UserControl page)
		{
			page.Size = new Size(pageWidth, pageHeight);
			mainPanel.Controls.Clear();
			mainPanel.Controls.Add(page);
			mainPanel.BringToFront();
		}

		public void CheckedDialogDeleteModel(bool check)
		{
			var model = MainModel.Instance;
			model.DontShowDeleteModelDialog = check;
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			using (var dialog = new DialogConfirmExit())
			{
				dialog.ShowDialog();
			}
		}

		private void btnFullScreen_Click(object sender, EventArgs e)
		{
			if(this.WindowState == FormWindowState.Maximized)
			{
				// 원래 사이즈로
				this.WindowState = FormWindowState.Normal;
				this.Size = new Size(1920, 1080);
			}
			else
			{
				// 최대화
				this.WindowState = FormWindowState.Maximized;
			}
		}

		private void btnMinScreen_Click(object sender, EventArgs e)
		{
			// 최소화
			this.WindowState = FormWindowState.Minimized;
		}
	}
}