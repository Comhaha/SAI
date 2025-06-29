using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.UI.HtmlControls;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using SAI.SAI.App.Models;
using SAI.SAI.App.Presenters;
using SAI.SAI.App.Views.Common;
using SAI.SAI.App.Views.Interfaces;
using SAI.SAI.Application.Service;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using static ScintillaNET.Style;

namespace SAI.SAI.App.Forms.Dialogs
{
    public partial class DialogNotion : Form, IAiFeedbackView, IAiNotionView
    {
        //webView2.Visible = false 되어 있어요. 로직 작성하실 때
        // secretkey 입력하고 맞으면 pInfo.Visible = false; webView2.Visible = true; 하고

        private readonly string _initialMemo;
        private readonly double _thresholdValue;
        private readonly string _resultImagePath;
        private AiFeedbackPresenter _feedPresenter;
        private AiNotionPresenter _notionPresenter;
        private bool _webInit;
        private bool isTutorial;

        private const string redirectBase = "https://k12d201.p.ssafy.io/api/notion/callback";

        public DialogNotion(string memo, double thresholdValue, string resultImagePath, bool isTutorial)
        {
            InitializeComponent();

            this.isTutorial = isTutorial;

            pInfo.BackColor = SystemColors.Control;

            DialogUtils.ApplyDefaultStyle(this, Color.Gray);

            ButtonUtils.SetupButton(btnClose, "btn_close_zoomChart_clicked", "btn_close_zoomChart");
            btnClose.Click += (s, e) => { this.Close(); };
            ButtonUtils.SetupButton(authButton, "btn_auth_clicked", "btn_auth");

            _initialMemo = memo;
            _thresholdValue = thresholdValue;
            _resultImagePath = resultImagePath;

            InitWebOnce();

            this.TopMost = false;
			guna2DragControl1.TargetControl = pTitleBar;
			guna2DragControl1.TransparentWhileDrag = false;
			guna2DragControl1.UseTransparentDrag = false;

			lbError.Visible = false;

            pbGif.Visible = false;
            pbGif.Image = Properties.Resources.notionLoading;
            pbGif.SizeMode = PictureBoxSizeMode.Zoom;
            pbGif.Padding = new Padding(0, 0, 0, 10);
		}

		/* ---------------- IAiFeedbackView ---------------- */
		//Code 경로
		public string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        public string CodeText => this.isTutorial ? 
                                Path.GetFullPath(Path.Combine(baseDir, "SAI.Application", "Python", "scripts", "tutorial_script.py")) :
                                Path.GetFullPath(Path.Combine(baseDir, "SAI.Application", "Python", "scripts", "train_script.py"));
        //log 사진 경로
        public string LogImagePath => Path.GetFullPath(Path.Combine(baseDir, "SAI.Application", "Python", "runs", "detect", "train", "results.png"));
        //무슨 사진이 결과 사진인지?
        public string ResultImagePath => Path.GetFullPath(_resultImagePath);
        public string memo => _initialMemo;

        public double thresholdValue => _thresholdValue;


        public event EventHandler SendRequested;
        private void ibtnEnter_Click(object sender, EventArgs e)
        {
            ibtnEnter.BackgroundImage = Properties.Resources.btn_enter_hover;
			string token = tboxSecretKey.Text;

            // 로띠 뜨기
            pbGif.Visible = true;


			if (string.IsNullOrWhiteSpace(token))
            {
				lbError.Visible = true;
				lbError.Text = "토큰을 입력해주세요.";
				ibtnEnter.BackgroundImage = Properties.Resources.btn_enter;
				return;
            }

            var service = new AiFeedbackService("https://k12d201.p.ssafy.io", token);
            _feedPresenter = new AiFeedbackPresenter(this, service);

            SendRequested?.Invoke(this, new EventArgs());
        }

        public void ShowSendResult(bool ok, string id, string md)
        {
            if (!ok) { ShowError(md); return; }



            string html = Markdig.Markdown.ToHtml(md);
            ShowMarkdown(html);
            ShowAuthButton(true);
        }
        /* -------------------------------------------------- */

        /* ---------------- IBusyAwareView  ---------------- */
        public void SetBusy(BusyContext ctx, bool on)
        {
            switch (ctx)
            {
                case BusyContext.Feedback:
                    ibtnEnter.Enabled = !on;
                    break;
                case BusyContext.OAuth:
                    authButton.Enabled = !on;
                    break;
            }
        }
        /* -------------------------------------------------- */

        /* ---------------- IAiNotionView ------------------- */
        public void ShowMarkdown(string html)
        {
            InitWebOnce();

			// 로띠 끄기
			pbGif.Visible = false;

			webView2.NavigateToString(html);
			webView2.Visible = true;
            lblInfo.Visible = false;

			ibtnEnter.BackgroundImage = Properties.Resources.btn_enter;
        }

        public void ShowAuthButton(bool on) => authButton.Visible = on;

        public void ShowError(string msg)
        {
            webView2.Visible = false;
            lbError.Text = msg;
            tpError.Visible = true;
			lblInfo.Text = msg;
            lblInfo.Visible = true;
        }

        public void WebLoadUrl(string url)
        {
            Console.WriteLine("Notion Oauth url : " + url );
            InitWebOnce();
            webView2.Source = new Uri(url);
            webView2.Visible = true;
        }

        public event EventHandler AuthStartRequested;

        private void authButton_Click(object s, EventArgs e)
        {
            _notionPresenter = new AiNotionPresenter(this, NotionModel.Instance.RedirectUrl, redirectBase);


            AuthStartRequested?.Invoke(this, EventArgs.Empty);
        }

        public void RegisterWebNavigation(Action<string> handler)
        {
            InitWebOnce();
            tpError.Visible = false;
			webView2.CoreWebView2.NavigationStarting += (s, e) => handler(e.Uri);
        }

        public void RegisterWebResponse(Func<string, Task> onJsonReceived)
        {
            InitWebOnce();
            webView2.CoreWebView2.WebResourceResponseReceived += async (s, e) =>
            {
                try
                {
                    Console.WriteLine($"[WebView2] 응답 수신됨 - 요청 URL: {e.Request.Uri}");
                    if (!e.Request.Uri.StartsWith(redirectBase, StringComparison.OrdinalIgnoreCase))
                        return;

                    string contentType = null;
                    foreach (var h in e.Response.Headers)
                    {
                        Console.WriteLine($"[Header] {h.Key}: {h.Value}");

                        if (h.Key.Equals("Content-Type", StringComparison.OrdinalIgnoreCase))
                        {
                            contentType = h.Value;
                            break;
                        }
                    }

                    if (contentType?.StartsWith("application/json") != true)
                    {
                        Console.WriteLine("[WebView2] JSON 응답이 아님. 처리 생략.");
                        return;
                    }

                    Console.WriteLine("[WebView2] JSON 응답 감지됨. 스트림 읽기 시작...");

                    var stream = await e.Response.GetContentAsync();
                    if (stream == null)
                    {
                        Console.WriteLine("[WebView2] 응답 스트림 없음.");
                        return;
                    }

                    string json;
                    using (var sr = new StreamReader(stream, Encoding.UTF8, true))
                        json = await sr.ReadToEndAsync();

                    Console.WriteLine("[WebView2] JSON 응답 내용:\n" + json);

                    await onJsonReceived(json);
                }
                catch (Exception ex)
                {
                    ShowError("JSON 응답 처리 중 오류: " + ex.Message);
                    Console.WriteLine("[WebView2] JSON 처리 중 오류: " + ex.Message);
                }
            };
        }

        /* -------------------------------------------------- */

        /* ---------------- WebView2 Init ------------------- */
        private async void InitWebOnce()
        {
            if (_webInit) return;
            _webInit = true;

            await webView2.EnsureCoreWebView2Async();

            webView2.CoreWebView2.AddWebResourceRequestedFilter(
        "${redirectBase}*",
        CoreWebView2WebResourceContext.XmlHttpRequest);

        }
        /* -------------------------------------------------- */

        //        private void ShowWebView2MarkDown(string feedback)
        //        {
        //            string html = Markdig.Markdown.ToHtml(feedback);

        //            string htmlPage = $@"
        //<!doctype html>
        //<html>
        //<head>
        //    <meta charset='utf-8'>
        //    <style>
        //        body {{
        //            font-family: 'Segoe UI', sans-serif;
        //            padding: 20px;
        //            background-color: #1D1D1D;
        //            color: white;
        //        }}
        //        h1, h2, h3 {{ color: #ffcc00; }}
        //        pre, code {{ background: #2d2d2d; padding: 5px; border-radius: 4px; }}
        //    </style>
        //</head>
        //<body>
        //    {html}
        //</body>
        //</html>";
        //            InitWebView();
        //            webView2.NavigateToString(htmlPage);

        //            lblInfo.Visible = false;
        //            webView2.Visible = true;
        //            authButton.Visible = true;
        //        }

        //public void SetBusy(bool b)
        //{
        //    ibtnEnter.Enabled = !b;
        //    progressBar.Visible = b;
        //    progressBar.Style = b ? ProgressBarStyle.Marquee
        //                            : ProgressBarStyle.Continuous;
        //}

        //private async void InitWebView()
        //{
        //    await webView2.EnsureCoreWebView2Async();

        //    webView2.CoreWebView2.NavigationStarting += (s, e) =>
        //    {
        //        if (e.Uri.StartsWith(redirectBase, StringComparison.OrdinalIgnoreCase))
        //        {
        //            var nvc = System.Web.HttpUtility.ParseQueryString(new Uri(e.Uri).Query);
        //            bool ok = nvc["success"] == "1";
        //            string url = nvc["url"];          // 성공 시 내보낸 페이지 URL
        //            string msg = nvc["message"];      // 실패 시 메시지

        //            _presenter.OnWebCallback(ok, ok ? url : msg);
        //            e.Cancel = true;                  // 리디렉트 페이지 표시 안 함
        //        }
        //    };
        //}

        private void ibtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DialogNotion_Load(object sender, EventArgs e)
        {
            Console.WriteLine(CodeText);
        }
    }
}
