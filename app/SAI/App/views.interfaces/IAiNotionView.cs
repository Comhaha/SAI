using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI.App.Views.Interfaces
{
    public interface IAiNotionView : IAiBuzyAwareView
    {
        void ShowMarkdown(string html);         // 피드백 결과를 마크다운 HTML로 보여줄 때
        void ShowAuthButton(bool on);           // 인증 버튼 가시성
        void ShowError(string msg);             // 실패 알림
        void WebLoadUrl(string url);            // WebView2 에 URL 로드

        event EventHandler AuthStartRequested;  // “인증” 버튼 클릭
        void RegisterWebNavigation(Action<string> onNavigate); // WebView2 URI 콜백

        void RegisterWebResponse(Func<string, Task> onJsonReceived);
    }
}
