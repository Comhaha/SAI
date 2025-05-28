using System;
using System.Drawing;
using System.Windows.Forms;
using SAI.SAI.App.Views.Common;
using static SAI.SAI.Application.Service.PythonService;

namespace SAI.SAI.App.Forms.Dialogs
{
    public partial class DialogSetting : Form
    {
        private enum GpuMode
        {
            Local,
            Server
        }

        // gpu 초기 상태 Local로 설정
        private GpuMode selectedGpuMode = GpuMode.Local;

        public DialogSetting()
        {
            InitializeComponent();

            DialogUtils.ApplyDefaultStyle(this, Color.Gray);

            ButtonUtils.SetupButton(btnSave, "btn_save_clicked", "btn_save");
            ButtonUtils.SetupButton(btnClose, "btn_close_setting_clicked", "btn_close_setting");
            ButtonUtils.SetTransparentStyle(btnLocal);
            ButtonUtils.SetTransparentStyle(btnServer);

            // gpu 초기 상태 Local로 설정
            SetRadioButtonState(selectedGpuMode);

            btnLocal.Click += BtnLocal_Click;
            btnServer.Click += BtnServer_Click;
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void BtnLocal_Click(object sender, EventArgs e)
        {
            selectedGpuMode = GpuMode.Local;
            SetRadioButtonState(selectedGpuMode);
        }

        private void BtnServer_Click(object sender, EventArgs e)
        {
            selectedGpuMode = GpuMode.Server;
            SetRadioButtonState(selectedGpuMode);
        }

        // RadioButton처럼 사용하기 위한 메서드
        private void SetRadioButtonState(GpuMode gpuMode)
        {
            btnLocal.BackgroundImage = (gpuMode == GpuMode.Local)
                ? Properties.Resources.btn_checked
            : Properties.Resources.btn_unchecked;

            btnServer.BackgroundImage = (gpuMode == GpuMode.Server)
                ? Properties.Resources.btn_checked
                : Properties.Resources.btn_unchecked;
        }
    }
}
