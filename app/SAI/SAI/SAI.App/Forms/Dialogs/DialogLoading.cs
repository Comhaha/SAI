using System;
using System.Windows.Forms;

namespace SAI.SAI.App.Forms.Dialogs
{
    public partial class DialogLoading : Form
    {
        public DialogLoading()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
            this.TopMost = true;
        }

        // 싱글톤 패턴으로 구현하여 중복 생성 방지
        private static DialogLoading instance;
        public static DialogLoading Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                {
                    instance = new DialogLoading();
                }
                return instance;
            }
        }

        // 로딩창 표시
        public static void ShowLoading()
        {
            if (Instance.InvokeRequired)
            {
                Instance.Invoke(new Action(ShowLoading));
                return;
            }
            Instance.Show();
            Instance.Refresh();
        }

        // 로딩창 닫기
        public static void HideLoading()
        {
            if (Instance.InvokeRequired)
            {
                Instance.Invoke(new Action(HideLoading));
                return;
            }
            Instance.Hide();
        }
    }
} 