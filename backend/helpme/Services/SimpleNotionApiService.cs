using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using helpme.Models;
using System.Windows.Forms;

namespace helpme.Services
{
    public class SimpleNotionApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public SimpleNotionApiService(string baseUrl)
        {
            _baseUrl = baseUrl;
            _httpClient = new HttpClient();
        }

        /// <summary>
        /// 인증 상태 확인
        /// </summary>
        public async Task<AuthCheckResponse> CheckAuthStatus(string uuid)
        {
            try
            {
                var url = $"{_baseUrl}/api/notion/auth/check?uuid={Uri.EscapeDataString(uuid)}";
                
                var response = await _httpClient.GetAsync(url);
                var responseJson = await response.Content.ReadAsStringAsync();
                
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"서버 오류: {response.StatusCode} - {responseJson}");
                }

                // BaseResponse 형태로 파싱
                var baseResponse = JsonSerializer.Deserialize<BaseResponse<AuthCheckResponse>>(responseJson);
                
                return baseResponse?.Result ?? throw new Exception("AuthCheckResponse가 null입니다.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"인증 상태 확인 오류: {ex.Message}", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        /// <summary>
        /// Notion으로 내보내기
        /// </summary>
        public async Task<NotionExportResponse> ExportReport(string uuid)
        {
            try
            {
                // 요청 데이터 준비
                var request = new NotionExportRequest { Uuid = uuid };
                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                // POST 요청
                var url = $"{_baseUrl}/api/notion/export";
                var response = await _httpClient.PostAsync(url, content);
                var responseJson = await response.Content.ReadAsStringAsync();

                // 응답이 성공인지 확인
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"서버 오류: {response.StatusCode} - {responseJson}");
                }

                // BaseResponse 형태로 파싱
                var baseResponse = JsonSerializer.Deserialize<BaseResponse<NotionExportResponse>>(responseJson);
                
                return baseResponse?.Result ?? throw new Exception("NotionExportResponse가 null입니다.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"내보내기 오류: {ex.Message}", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        /// <summary>
        /// OAuth 콜백 처리
        /// </summary>
        public async Task<string> HandleCallback(string code, string state)
        {
            try
            {
                var query = $"?code={Uri.EscapeDataString(code)}&state={Uri.EscapeDataString(state)}";
                var url = $"{_baseUrl}/api/notion/callback{query}";

                var response = await _httpClient.GetAsync(url);
                
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"콜백 처리 실패: {response.StatusCode} - {errorContent}");
                }

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"콜백 처리 오류: {ex.Message}", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }
    }
}
