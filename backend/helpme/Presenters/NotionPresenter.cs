using System;
using System.Threading.Tasks;
using helpme.Models;
using helpme.Services;
using helpme.Views;

namespace helpme.Presenters
{
    public class NotionPresenter
    {
        private readonly INotionView _view;
        private readonly NotionApiService _apiService;
        private NotionExportResponse _currentExportResponse;
        private AuthCheckResponse _authCheckResponse;

        public NotionPresenter(INotionView view, NotionApiService apiService)
        {
            _view = view;
            _apiService = apiService;

            // View 이벤트 구독
            _view.ExportButtonClicked += async (sender, e) => await HandleExport();
            _view.AuthButtonClicked += HandleAuth;
            
            // 인증 확인 버튼 이벤트도 구독 (필요한 경우)
            // _view.CheckAuthButtonClicked += async (sender, e) => await HandleAuthComplete();
        }

        private async Task HandleExport()
        {
            try
            {
                if (string.IsNullOrEmpty(_view.UUID))
                {
                    _view.ShowErrorMessage("UUID를 입력해주세요.");
                    return;
                }

                _view.ExportButtonEnabled = false;
                
                // 1단계: 인증 상태 확인
                _view.StatusMessage = "인증 상태 확인 중...";
                _authCheckResponse = await _apiService.CheckAuthStatus(_view.UUID);

                if (_authCheckResponse == null)
                {
                    _view.ShowErrorMessage("인증 상태 확인 실패. 서버 연결을 확인해주세요.");
                    return;
                }

                if (_authCheckResponse.Authenticated)
                {
                    // 인증이 이미 완료된 경우, 바로 내보내기 진행
                    await PerformExport();
                }
                else
                {
                    // 인증이 필요한 경우
                    _view.StatusMessage = "Notion 인증이 필요합니다.";
                    if (!string.IsNullOrEmpty(_authCheckResponse.AuthUrl))
                    {
                        _view.ShowAuthButton(_authCheckResponse.AuthUrl);
                    }
                    else
                    {
                        _view.ShowErrorMessage("인증 URL이 제공되지 않았습니다.");
                    }
                }
            }
            catch (Exception ex)
            {
                _view.ShowErrorMessage($"오류가 발생했습니다: {ex.Message}\n\n디버그 정보:\n{ex.StackTrace}");
            }
            finally
            {
                _view.ExportButtonEnabled = true;
            }
        }

        private async Task PerformExport()
        {
            try
            {
                _view.StatusMessage = "Notion으로 내보내는 중...";
                
                _currentExportResponse = await _apiService.ExportReport(_view.UUID);

                if (_currentExportResponse != null && _currentExportResponse.Success)
                {
                    _view.ShowSuccessMessage("Notion으로 보고서가 성공적으로 내보내졌습니다.");
                    _view.StatusMessage = "내보내기 완료";
                }
                else
                {
                    string message = _currentExportResponse?.Message ?? "알 수 없는 오류가 발생했습니다.";
                    
                    if (message.Contains("인증"))
                    {
                        // 인증 오류인 경우 다시 인증 체크
                        await HandleExport();
                    }
                    else
                    {
                        _view.ShowErrorMessage($"내보내기 실패: {message}");
                    }
                }
            }
            catch (Exception ex)
            {
                _view.ShowErrorMessage($"내보내기 중 오류가 발생했습니다: {ex.Message}");
            }
        }

        private void HandleAuth(object sender, EventArgs e)
        {
            try
            {
                if (_authCheckResponse != null && !string.IsNullOrEmpty(_authCheckResponse.AuthUrl))
                {
                    _view.StatusMessage = "브라우저에서 인증을 진행하세요...";
                    _view.OpenAuthUrl(_authCheckResponse.AuthUrl);
                    
                    // 폴링을 시작하지 않고, 사용자가 수동으로 다시 시도하도록 안내
                    _view.StatusMessage = "인증 완료 후 내보내기를 다시 시도해주세요.";
                }
                else
                {
                    _view.ShowErrorMessage("인증 URL이 없습니다. 먼저 내보내기를 시도해주세요.");
                }
            }
            catch (Exception ex)
            {
                _view.ShowErrorMessage($"인증 처리 중 오류가 발생했습니다: {ex.Message}\n\n디버그 정보:\n{ex.StackTrace}");
            }
        }

        public async Task HandleAuthComplete()
        {
            try
            {
                _view.StatusMessage = "인증 완료 확인 중...";
                
                // 인증 완료 후 다시 상태 확인
                var response = await _apiService.CheckAuthStatus(_view.UUID);
                
                if (response != null && response.Authenticated)
                {
                    _view.HideAuthButton();
                    _view.ShowSuccessMessage("Notion 인증이 완료되었습니다.");
                    _view.StatusMessage = "인증 완료";
                    
                    // 인증 완료 후 자동으로 내보내기 실행
                    await PerformExport();
                }
                else
                {
                    _view.ShowErrorMessage("인증이 아직 완료되지 않았습니다. 잠시 후 다시 시도해주세요.");
                }
            }
            catch (Exception ex)
            {
                _view.ShowErrorMessage($"인증 처리 중 오류가 발생했습니다: {ex.Message}\n\n디버그 정보:\n{ex.StackTrace}");
            }
        }
    }
}
