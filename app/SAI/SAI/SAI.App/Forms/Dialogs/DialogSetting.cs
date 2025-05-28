using System;
using System.Drawing;
using System.Windows.Forms;
using SAI.SAI.App.Views.Common;
using SAI.SAI.App.Models;

namespace SAI.SAI.App.Forms.Dialogs
{
    public partial class DialogSetting : Form
    {

        private GpuType selectedGpuType;

        public DialogSetting()
        {
            InitializeComponent();

            DialogUtils.ApplyDefaultStyle(this, Color.Gray);

            ButtonUtils.SetupButton(btnSave, "btn_save_clicked", "btn_save");
            ButtonUtils.SetupButton(btnClose, "btn_close_setting_clicked", "btn_close_setting");
            ButtonUtils.SetTransparentStyle(btnLocal);
            ButtonUtils.SetTransparentStyle(btnServer);

            selectedGpuType = BlocklyModel.Instance.gpuType;
            SetRadioButtonState(selectedGpuType);

            btnLocal.Click += BtnLocal_Click;
            btnServer.Click += BtnServer_Click;
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
            //Console.WriteLine($"[DEBUG] 저장된 GPU 타입: {BlocklyModel.Instance.gpuType}");
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            BlocklyModel.Instance.gpuType = selectedGpuType;
            this.Close();
            //Console.WriteLine($"[DEBUG] 저장된 GPU 타입: {BlocklyModel.Instance.gpuType}");
        }

        private void BtnLocal_Click(object sender, EventArgs e)
        {
            selectedGpuType = GpuType.Local;
            SetRadioButtonState(selectedGpuType);
        }

        private void BtnServer_Click(object sender, EventArgs e)
        {
            selectedGpuType = GpuType.Server;
            SetRadioButtonState(selectedGpuType);
        }

        // RadioButton처럼 사용하기 위한 메서드
        private void SetRadioButtonState(GpuType gpuType)
        {
            btnLocal.BackgroundImage = (gpuType == GpuType.Local)
                ? Properties.Resources.btn_checked
            : Properties.Resources.btn_unchecked;

            btnServer.BackgroundImage = (gpuType == GpuType.Server)
                ? Properties.Resources.btn_checked
                : Properties.Resources.btn_unchecked;
        }
    }
}
