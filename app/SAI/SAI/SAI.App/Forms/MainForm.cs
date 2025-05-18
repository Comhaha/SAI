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

namespace SAI
{
	public partial class MainForm : Form, IMainView
	{
		private MainPresenter presenter;

		public MainForm()
		{
			InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;

            presenter = new MainPresenter(this);

			// 사이즈 고정
			MinimumSize = new Size(1280, 720);
			AutoScaleMode = AutoScaleMode.Dpi;
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

		private int pageWidth = 1280;
		private int pageHeight = 720;
		private void MainForm_Resize(object sender, EventArgs e)
		{
			int maxWidth = 1280;
			int maxHeight = 690;

			// 현재 폼의 너비와 높이
			int formWidth = this.ClientSize.Width;
			int formHeight = this.ClientSize.Height;

			// 비율 계산 (비율 유지한 채 확대)
			float scaleX = (float)formWidth / maxWidth;
			float scaleY = (float)formHeight / maxHeight;
			float scale = Math.Min(scaleX, scaleY); // 둘 중 작은 값으로 비율 유지

			// 확대된 크기 계산 (최대 1280 x 720)
			int newWidth = (int)(maxWidth * scale);
			int newHeight = (int)(maxHeight * scale);

			pageHeight = newHeight;
			pageWidth = newWidth;

			// 중앙 정렬 위치 계산
			int x = (formWidth - newWidth) / 2;
			int y = (formHeight - newHeight) / 2;

			var barHeight = tpTitlebar.Size.Height;

			// 위치와 크기 조정
			mainPanel.Size = new Size(newWidth, newHeight - barHeight);

			// panel사이즈가 몇인지 확인
			//MessageBox.Show(mainPanel.Size.ToString());
		}


		private void MainForm_Load(object sender, EventArgs e)
		{
			// 초기 페이지인 UcSelectType을 불러온다.
			presenter.Initialize();
        }

		// 이건 Presenter가 호출할 메서드(UI에 있는 패널에 있던 페이지를 지우고, 크기를 채우고, 페이지를 넣는다.)
		public void LoadPage(UserControl page)
		{
			page.Dock = DockStyle.Fill;
			mainPanel.Controls.Clear();
			mainPanel.BackColor = this.BackColor;
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
				this.Size = new Size(1280, 720);
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