using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SAI.App.Views.Interfaces;

namespace SAI.App.Presenters
{
    public class AiNotionPresenter
    {
        private readonly IAiNotionView _view;
        private readonly string _oauthUrl;      // Notion Authorize URL
        private readonly string _redirectBase;  // Notion Authorize Callback URL

        public AiNotionPresenter(IAiNotionView view, string oauthUrl, string redirectBase)
        {
            _view = view;
            _oauthUrl = oauthUrl;
            _redirectBase = redirectBase;

            _view.AuthStartRequested += (_, __) => StartOAuth();
            _view.RegisterWebNavigation(OnNavigate);
            _view.RegisterWebResponse(OnJsonFromCallback);
        }

        /* OAuth 시작 */
        private void StartOAuth()
        {
            _view.ShowAuthButton(false);
            _view.SetBusy(BusyContext.OAuth, true);
            _view.WebLoadUrl(_oauthUrl);
        }

        /* WebView2 가 탐색할 때마다 호출됨 */
        public void OnNavigate(string url)
        {
            if (!url.StartsWith(_redirectBase, StringComparison.OrdinalIgnoreCase)) return;

            var qs = System.Web.HttpUtility.ParseQueryString(new Uri(url).Query);
            bool ok = qs["success"] == "1";
            string u = qs["url"];
            string m = qs["message"];

            _view.SetBusy(BusyContext.OAuth, false);

            if (ok) _view.WebLoadUrl(u);
            //else _view.ShowError(m ?? "export failed");

            _view.ShowAuthButton(true);
        }

        private async Task OnJsonFromCallback(string json)
        {
            Console.WriteLine("[Presenter] callback JSON:\n" + json);

            string notionUrl = null;
            try
            {
                var doc = JsonDocument.Parse(json);
                notionUrl = doc.RootElement
                               .GetProperty("result")
                               .GetProperty("url")
                               .GetString();
            }
            catch { /* 파싱 실패 무시 */ }

            if (!string.IsNullOrEmpty(notionUrl))
            {
                _view.WebLoadUrl(notionUrl);          // 성공 시 → Notion 페이지로 전환
            }
            else
            {
                _view.ShowError("Export 실패 혹은 URL 없음");
            }
        }
    }
}
