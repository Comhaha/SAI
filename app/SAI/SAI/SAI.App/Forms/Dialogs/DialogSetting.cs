using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using SAI.SAI.App.Presenters;
using SAI.SAI.App.Views.Interfaces;
using System.Web.UI.WebControls;

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

            // 부모 기준 중앙
            this.StartPosition = FormStartPosition.CenterParent;
            // 기존 타이틀바 삭제
            this.FormBorderStyle = FormBorderStyle.None;
            // 떴을 때 이 다이얼로그가 가장 위에 있고 다이얼로그를 끄기 전에는 다른 건 못 누르게!
            this.TopMost = true;

            // 배경을 투명하게 하기 위해서
            this.BackColor = Color.Gray;  
            this.TransparencyKey = Color.Gray;

            // ToolTip 설정 (제대로 안됨)
            ToolTip toolTip = new ToolTip();
            toolTip.AutoPopDelay = 3000;
            toolTip.InitialDelay = 300;
            toolTip.ReshowDelay = 300;
            toolTip.ShowAlways = true;
            toolTip.OwnerDraw = true;
            toolTip.Draw += (s, e) =>
            {
                Font notoSans = new Font("Noto Sans KR", 9);
                Color bgColor = ColorTranslator.FromHtml("#EAEAEA");
                using (SolidBrush bgBrush = new SolidBrush(bgColor))
                {
                    e.Graphics.FillRectangle(bgBrush, e.Bounds);
                }
                e.DrawBackground();
                e.DrawBorder();
                e.Graphics.DrawString(e.ToolTipText, notoSans, Brushes.Black, new PointF(2, 2));
            };
            toolTip.Popup += (s, e) =>
            {
                Font notoSans = new Font("Noto Sans KR", 9);
                string text = "모델 저장 경로를 변경하려면 클릭하세요.";
                Size size = TextRenderer.MeasureText(text, notoSans);
                e.ToolTipSize = new Size(size.Width + 8, size.Height + 4);
            };
            toolTip.SetToolTip(tboxPath, "모델 저장 경로를 변경하려면 클릭하세요.");

            SetupButton(btnClose);
            SetupButton(btnSave);
            SetupButton(btnLight);
            SetupButton(btnDark);

            btnClose.MouseEnter += (s, e) =>
            {
                btnClose.BackColor = Color.Transparent;
                btnClose.BackgroundImage = Properties.Resources.btn_close_select_model_clicked;
            };

            btnClose.MouseLeave += (s, e) =>
            {
                btnClose.BackgroundImage = Properties.Resources.btn_close_select_model;
            };

            btnSave.MouseEnter += (s, e) =>
            {
                btnSave.BackColor = Color.Transparent;
                btnSave.BackgroundImage = Properties.Resources.btn_save_clicked;
            };

            btnSave.MouseLeave += (s, e) =>
            {
                btnSave.BackgroundImage = Properties.Resources.btn_save;
            };

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

        void SetupButton(Guna.UI2.WinForms.Guna2Button btn)
            {
                btn.BackColor = Color.Transparent;
                btn.PressedColor = Color.Transparent;
                btn.CheckedState.FillColor = Color.Transparent;
                btn.DisabledState.FillColor = Color.Transparent;
                btn.HoverState.FillColor = Color.Transparent;
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
