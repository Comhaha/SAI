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

			presenter = new MainPresenter(this);

			// 사이즈 고정
			Size = new Size(1280, 750);
			MinimumSize = new Size(1280, 750);
			AutoScaleMode = AutoScaleMode.None;
			guna2DragControl1.TargetControl = titlebar;
			guna2DragControl1.TransparentWhileDrag = false;
			guna2DragControl1.UseTransparentDrag = false;

			this.Resize += MainForm_Resize;
		}

		private void MainForm_Resize(object sender, EventArgs e)
		{
			int maxWidth = 1280;
			int maxHeight = 750;

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

			newWidth = Math.Min(newWidth, maxWidth);
			newHeight = Math.Min(newHeight, maxHeight);

			// 중앙 정렬 위치 계산
			int x = (formWidth - newWidth) / 2;
			int y = (formHeight - newHeight) / 2;

			// 위치와 크기 조정
			mainPanel.Location = new Point(x, y + 30); // titlebar 때문에 y는 약간 내림
			mainPanel.Size = new Size(newWidth, newHeight - 30);

			titlebar.Location = new Point(0, 0);
			titlebar.Size = new Size(formWidth, 30); // 타이틀바는 항상 높이 30
		}


		private void MainForm_Load(object sender, EventArgs e)
		{
			// 초기 페이지인 Blockly를 불러온다.
			presenter.Initialize();
		}

		// 이건 Presenter가 호출할 메서드(UI에 있는 패널에 있던 페이지를 지우고, 크기를 채우고, 페이지를 넣는다.)
		public void LoadPage(UserControl page)
		{
			page.Size = new Size(1280, 720);
			mainPanel.Controls.Clear();
			mainPanel.BackColor = Color.Transparent;
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
				this.Size = new Size(1280, 750);
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

		private void btnSetting_Click(object sender, EventArgs e)
		{
			using(var dialog = new DialogSetting())
			{
				dialog.ShowDialog();
			}
		}
	}
}
