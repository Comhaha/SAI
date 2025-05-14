using System.Drawing;
using System.Windows.Forms;
using SAI.SAI.App.Presenters;
using SAI.SAI.App.Views.Interfaces;
using SAI.SAI.App.Views.Common;


namespace SAI.SAI.App.Forms.Dialogs
{
	public partial class DialogSetting : Form, ISettingView
	{

        private readonly SettingPresenter presenter;
        private bool isLightTheme;
        private bool originalTheme;
        private string originalPath;

        public DialogSetting()
        {
            InitializeComponent();
            presenter = new SettingPresenter(this);

            DialogUtils.ApplyDefaultStyle(this, Color.Gray);

            // ToolTip 설정 (제대로 안됨)
            ToolTipUtils.CustomToolTip(tboxPath, "모델 저장 경로를 변경하려면 클릭하세요.");

            // 버튼 설정(버튼 이름, 호버 이미지, default 이미지)
            ButtonUtils.SetupButton(btnClose, "btn_close_select_model_clicked", "btn_close_select_model");
            ButtonUtils.SetupButton(btnSave, "btn_save_clicked", "btn_save");
            ButtonUtils.SetTransparentStyle(btnLight);
            ButtonUtils.SetTransparentStyle(btnDark);

            presenter.OnViewLoaded();

            // 백업값 저장
            originalTheme = isLightTheme;
            originalPath = tboxPath.Text;

            btnClose.Click += (s, e) =>
            {
                // 원래 상태 복원
                presenter.OnThemeChanged(originalTheme);
                presenter.OnPathChanged(originalPath);
                SetTheme(originalTheme);
                SetPath(originalPath);  

                presenter.OnCancel();
            };

            btnSave.Click += (s, e) => presenter.OnSave();

            btnLight.Click += (s, e) =>
            {
                isLightTheme = true;
                UpdateThemeButtonUI();
                presenter.OnThemeChanged(true);
            };

            btnDark.Click += (s, e) =>
            {
                isLightTheme = false;
                UpdateThemeButtonUI();
                presenter.OnThemeChanged(false);
            };

            tboxPath.Click += (s, e) =>
            {
                using (FolderBrowserDialog dlg = new FolderBrowserDialog())
                {
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        tboxPath.Text = dlg.SelectedPath;
                        presenter.OnPathChanged(dlg.SelectedPath);
                    }
                }
            };
        }

        private void UpdateThemeButtonUI()
        {
            btnLight.BackgroundImage = isLightTheme ? Properties.Resources.btn_light_clicked : Properties.Resources.btn_light;
            btnDark.BackgroundImage = isLightTheme ? Properties.Resources.btn_dark : Properties.Resources.btn_dark_clicked;
        }

        // ISettingView 구현
        public void SetTheme(bool isLight)
        {
            isLightTheme = isLight;
            UpdateThemeButtonUI();
        }

        public void SetPath(string path)
        {
            tboxPath.Text = path;
        }

        public bool IsLightThemeSelected() => isLightTheme;

        public string GetSelectedPath() => tboxPath.Text;

        public void CloseDialog() => this.Close();
    }
}
