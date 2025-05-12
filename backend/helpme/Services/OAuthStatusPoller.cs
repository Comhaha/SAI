using System;
using System.Threading.Tasks;
using helpme.Presenters;
using helpme.Services;

namespace helpme.Services
{
    public class OAuthStatusChecker
    {
        private readonly NotionApiService _apiService;
        private readonly NotionPresenter _presenter;
        private readonly string _uuid;
        private bool _isChecking;

        public OAuthStatusChecker(NotionApiService apiService, NotionPresenter presenter, string uuid)
        {
            _apiService = apiService;
            _presenter = presenter;
            _uuid = uuid;
        }

        public async Task CheckAuthStatusOnce()
        {
            if (_isChecking) return;

            _isChecking = true;

            try
            {
                // 인증 완료 여부를 한 번만 확인
                var response = await _apiService.CheckAuthStatus(_uuid);
                if (response != null && response.Authenticated)
                {
                    // 인증이 완료된 경우
                    await _presenter.HandleAuthComplete();
                }
                // 인증이 필요한 경우는 별도 처리 불필요
            }
            catch (Exception ex)
            {
                // 오류 발생 시 로그 기록
                Console.WriteLine($"인증 상태 확인 중 오류 발생: {ex.Message}");
            }
            finally
            {
                _isChecking = false;
            }
        }
    }
}
