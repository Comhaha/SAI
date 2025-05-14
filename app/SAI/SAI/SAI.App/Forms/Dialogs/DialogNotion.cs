using System;
using System.Drawing;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.Windows.Forms;
using SAI.SAI.App.Presenters;
using SAI.SAI.App.Views.Interfaces;
using SAI.SAI.Application.Service;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using static ScintillaNET.Style;

namespace SAI.SAI.App.Forms.Dialogs
{
    public partial class DialogNotion : Form, IAiFeedbackView
    {
        //webView2.Visible = false 되어 있어요. 로직 작성하실 때
        // secretkey 입력하고 맞으면 pInfo.Visible = false; webView2.Visible = true; 하고
        private AiFeedbackPresenter _presenter;

        public DialogNotion()
        {
            InitializeComponent();
            pInfo.BackColor = ColorTranslator.FromHtml("#1D1D1D");
        }

        //IAiFeedbackView
        //경로 넣기
        public string CodeText => "print(\"hello world\")";
        public string LogText => "log";
        public string ImagePath => "C:\\Users\\SSAFY\\Downloads\\logo.jpg";

        public event EventHandler SendRequested;
        private void ibtnEnter_Click(object sender, EventArgs e)
        {
            

            string token = tboxSecretKey.Text;

            if (string.IsNullOrWhiteSpace(token))
            {
                MessageBox.Show("토큰을 입력해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var service = new AiFeedbackService("https://k12d201.p.ssafy.io", token);
            _presenter = new AiFeedbackPresenter(this, service);

            SendRequested?.Invoke(this, new EventArgs());
            
        }

        public void ShowSendResult(bool isSuccess, string feedbackId, string feedback)
        {
            if (isSuccess)
            {
                //피드백 결과 출력
                ShowWebView2MarkDown(feedback);
            }
            else
            {
                MessageBox.Show(feedback);
            }

            

        }

        private void ShowWebView2MarkDown(string feedback)
        {
            string html = Markdig.Markdown.ToHtml(feedback);

            string htmlPage = $@"
<!doctype html>
<html>
<head>
    <meta charset='utf-8'>
    <style>
        body {{
            font-family: 'Segoe UI', sans-serif;
            padding: 20px;
            background-color: #1D1D1D;
            color: white;
        }}
        h1, h2, h3 {{ color: #ffcc00; }}
        pre, code {{ background: #2d2d2d; padding: 5px; border-radius: 4px; }}
    </style>
</head>
<body>
    {html}
</body>
</html>";
            InitWebView();
            webView2.NavigateToString(htmlPage);

            lblInfo.Visible = false;
            webView2.Visible = true;
            //authButton.Visible = true;
        }

        public void SetBusy(bool b)
        {
            ibtnEnter.Enabled = !b;
            progressBar.Visible = b;
            progressBar.Style = b ? ProgressBarStyle.Marquee
                                    : ProgressBarStyle.Continuous;
        }

        private async void InitWebView()
        {
            await webView2.EnsureCoreWebView2Async();
        }


        private void DialogNotion_Load(object sender, EventArgs e)
        {
            InitWebView();
        }

        private void ibtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void exportNotionButton_Click(object sender, EventArgs e)
        {

            exportNotionButton.Visible = false;
        }

        private void authButton_Click(object sender, EventArgs e)
        {
            //Notion으로 인증 처리

            //WebView2에 바로 redirectUrl 연결
            InitWebView();
            

            exportNotionButton.Visible = true;
            authButton.Visible = false;
        }
    }
}
