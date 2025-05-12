using SAI.SAI.App.Models;
using SAI.SAI.App.Presenters;
using SAI.SAI.App.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SAI.SAI.App.Forms.Dialogs;

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
		}

        private void UcSelectType_Load(object sender, EventArgs e)
        {
        }

        private void ibtnImage_Click(object sender, EventArgs e)
        {
            presenter.clickType("image");
			ibtnImage.Image = Properties.Resources.btn_image_hover;
			ibtnAudio.Image = Properties.Resources.btn_audio;
			ibtnPose.Image = Properties.Resources.btn_pose;
		}

		private void ibtnAudio_Click(object sender, EventArgs e)
		{
			presenter.clickType("audio");
			ibtnImage.Image = Properties.Resources.btn_image;
			ibtnAudio.Image = Properties.Resources.btn_audio_hover;
			ibtnPose.Image = Properties.Resources.btn_pose;
		}

		private void ibtnPose_Click(object sender, EventArgs e)
		{
			presenter.clickType("pose");
			ibtnImage.Image = Properties.Resources.btn_image;
			ibtnAudio.Image = Properties.Resources.btn_audio;
			ibtnPose.Image = Properties.Resources.btn_pose_hover;
		}

        private void ibtnNext_Click(object sender, EventArgs e)
        {
            presenter.clickNext();
        }

		void IUcShowDialogView.showDialog(Form dialog)
		{
			dialog.Owner = mainView as Form;
			dialog.ShowDialog();
		}
    }
}
