using SAI.SAI.App.Presenters;
using SAI.SAI.App.Views.Interfaces;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SAI.SAI.App.Views.Pages
{
    public partial class UcSelectType : UserControl, IUcShowDialogView
    {
        private UcShowDialogPresenter presenter;
		private readonly IMainView mainView;
		public UcSelectType(IMainView view)
        {
            InitializeComponent();
			this.mainView = view;
			presenter = new UcShowDialogPresenter(this);

			// btnImage
			btnImage.BackColor = Color.Transparent;
			btnImage.PressedColor = Color.Transparent;
			btnImage.CheckedState.FillColor = Color.Transparent;
			btnImage.DisabledState.FillColor = Color.Transparent;
			btnImage.HoverState.FillColor = Color.Transparent;
			// btnImage 마우스 입력 될 때
			btnImage.MouseEnter += (s, e) =>
			{
				btnImage.BackColor = Color.Transparent;
				btnImage.BackgroundImage = Properties.Resources.btn_image_hover;
			};
			// btnImage 마우스 떠날때
			btnImage.MouseLeave += (s, e) =>
			{
				btnImage.BackgroundImage = Properties.Resources.btn_image;
			};

			// btnAudio
			btnAudio.BackColor = Color.Transparent;
			btnAudio.PressedColor = Color.Transparent;
			btnAudio.CheckedState.FillColor = Color.Transparent;
			btnAudio.DisabledState.FillColor = Color.Transparent;
			btnAudio.HoverState.FillColor = Color.Transparent;
			// btnAudio 마우스 입력 될 때
			btnAudio.MouseEnter += (s, e) =>
			{
				btnAudio.BackColor = Color.Transparent;
				btnAudio.BackgroundImage = Properties.Resources.btn_audio_hover;
			};
			// btnAudio 마우스 떠날때
			btnAudio.MouseLeave += (s, e) =>
			{
				btnAudio.BackgroundImage = Properties.Resources.btn_audio;
			};

			// btnPose
			btnPose.BackColor = Color.Transparent;
			btnPose.PressedColor = Color.Transparent;
			btnPose.CheckedState.FillColor = Color.Transparent;
			btnPose.DisabledState.FillColor = Color.Transparent;
			btnPose.HoverState.FillColor = Color.Transparent;
			// btnPose 마우스 입력 될 때
			btnPose.MouseEnter += (s, e) =>
			{
				btnPose.BackColor = Color.Transparent;
				btnPose.BackgroundImage = Properties.Resources.btn_pose_hover;
			};
			// btnPose 마우스 떠날때
			btnPose.MouseLeave += (s, e) =>
			{
				btnPose.BackgroundImage = Properties.Resources.btn_pose;
			};
		}

		private void btnImage_Click(object sender, EventArgs e)
		{
			presenter.clickType("image");
		}

		private void btnAudio_Click(object sender, EventArgs e)
		{
			presenter.clickType("audio");
		}

		private void btnPose_Click(object sender, EventArgs e)
		{
			presenter.clickType("pose");
		}

		void IUcShowDialogView.showDialog(Form dialog)
		{
			dialog.Owner = mainView as Form;
			dialog.ShowDialog();
		}
	}
}
