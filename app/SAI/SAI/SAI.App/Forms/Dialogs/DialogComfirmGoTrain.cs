using SAI.SAI.App.Models;
using SAI.SAI.App.Presenters;
using SAI.SAI.App.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SAI.SAI.App.Forms.Dialogs
{
	public partial class DialogComfirmGoTrain : Form
	{
		private DialogLoadPagePresenter presenter;
		public DialogComfirmGoTrain()
		{
			InitializeComponent();


			// 부모 기준 중앙
			this.StartPosition = FormStartPosition.CenterParent;
			// 기존 타이틀바 삭제
			this.FormBorderStyle = FormBorderStyle.None;
			// 떴을 때 이 다이얼로그가 가장 위에 있고 다이얼로그를 끄기 전에는 다른 건 못 누르게!
			this.TopMost = true;

			// 배경을 투명하게 하기 위해서
			this.BackColor = Color.Gray;           // 투명 처리할 색
			this.TransparencyKey = Color.Gray;

			// btnClose
			btnClose.BackColor = Color.Transparent;
			btnClose.PressedColor = Color.Transparent;
			btnClose.CheckedState.FillColor = Color.Transparent;
			btnClose.DisabledState.FillColor = Color.Transparent;
			btnClose.HoverState.FillColor = Color.Transparent;
			btnClose.Click += (s, e) => { this.Close(); };
			// btnClose 마우스 입력 될 때
			btnClose.MouseEnter += (s, e) =>
			{
				btnClose.BackColor = Color.Transparent;
				btnClose.BackgroundImage = Properties.Resources.bg_blue_btn_close_clicked;
			};
			// btnClose 마우스 떠날때
			btnClose.MouseLeave += (s, e) =>
			{
				btnClose.BackgroundImage = Properties.Resources.bg_blue_btn_close;
			};

			// btnOk
			btnOk.HoverState.FillColor = Color.Transparent;
			btnOk.PressedColor = Color.Transparent;
			btnOk.CheckedState.FillColor = Color.Transparent;
			btnOk.HoverState.FillColor = Color.Transparent;
			btnOk.BackColor = Color.Transparent;
			btnOk.Click += (s, e) => {
				LogCsvModel.instance.clear();

				var blocklyModel = BlocklyModel.Instance;
				blocklyModel.blockAllCode = "";
				blocklyModel.blockCode = "";
				blocklyModel.imgPath = "";
				presenter.clickGoPracticeGuide();


				// tutorial에서 생성한 모델 삭제
				string baseDir = AppDomain.CurrentDomain.BaseDirectory;
				string modelPath = Path.GetFullPath(Path.Combine(baseDir, "SAI.Application", "Python", "runs", "detect", "train", "weights", "best.pt"));

				if (File.Exists(modelPath))
				{
					File.Delete(modelPath);
				}

				this.Close();
				this.Dispose();
			};
			// btnOk 마우스 입력 될 때
			btnOk.MouseEnter += (s, e) =>
			{
				btnOk.BackColor = Color.Transparent;
				btnOk.BackgroundImage = Properties.Resources.btn_blue_ok_clicked;
			};
			// btnOk 마우스 떠날때
			btnOk.MouseLeave += (s, e) =>
			{
				btnOk.BackColor = Color.Transparent;
				btnOk.BackgroundImage = Properties.Resources.btn_blue_ok;
			};

			// btnCancel
			btnCancel.BackColor = Color.Transparent;
			btnCancel.PressedColor = Color.Transparent;
			btnCancel.CheckedState.FillColor = Color.Transparent;
			btnCancel.DisabledState.FillColor = Color.Transparent;
			btnCancel.HoverState.FillColor = Color.Transparent;
			btnCancel.Click += (s, e) => { this.Close(); this.Dispose(); };
			// btnCancel 마우스 입력 될 때
			btnCancel.MouseEnter += (s, e) =>
			{
				btnCancel.BackColor = Color.Transparent;
				btnCancel.BackgroundImage = Properties.Resources.btn_white_cancel_clicked;
			};
			// btnCancel 마우스 떠날때
			btnCancel.MouseLeave += (s, e) =>
			{
				btnCancel.BackgroundImage = Properties.Resources.btn_white_cancel;
			};
		}
		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);

			var view = this.Owner as IMainView;
			presenter = new DialogLoadPagePresenter(view);
		}
	}
}
