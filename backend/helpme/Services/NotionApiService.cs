using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using helpme.Models;
using System.Windows.Forms;

namespace helpme.Services
{
    public class NotionApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public NotionApiService(string baseUrl)
        {
            _baseUrl = baseUrl;
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(30);  // 타임아웃 설정
        }

        /// <summary>
        /// 인증 상태 확인
        /// </summary>
        public async Task<AuthCheckResponse> CheckAuthStatus(string uuid)
        {
            try
            {
                var url = $"{_baseUrl}/api/notion/auth/check?uuid={Uri.EscapeDataString(uuid)}";
                SaveDebugLog($"Check Auth URL: {url}");

                var response = await _httpClient.GetAsync(url);
                
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    SaveDebugLog($"Auth Check Error: {errorContent}");
                    throw new Exception($"HTTP {response.StatusCode}: {errorContent}");
                }

                var responseJson = await response.Content.ReadAsStringAsync();
                SaveDebugLog($"Auth Check Response: {responseJson}");
                
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                
                var baseResponse = JsonSerializer.Deserialize<BaseResponse<AuthCheckResponse>>(responseJson, options);
                
                if (baseResponse != null && baseResponse.Result != null)
                {
                    return baseResponse.Result;
                }
                else
                {
                    throw new Exception("AuthCheckResponse가 null입니다.");
                }
            }
            catch (Exception ex)
            {
                SaveDebugLog($"Auth Check Exception: {ex.Message}");
                MessageBox.Show($"인증 상태 확인 오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                var request = new NotionExportRequest { Uuid = uuid };
                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_baseUrl}/api/notion/export", content);
                
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    SaveDebugLog($"Export Error: {errorContent}");
                    throw new Exception($"HTTP {response.StatusCode}: {errorContent}");
                }

                var responseJson = await response.Content.ReadAsStringAsync();
                SaveDebugLog($"Export Response: {responseJson}");
                
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                
                var baseResponse = JsonSerializer.Deserialize<BaseResponse<NotionExportResponse>>(responseJson, options);
                
                if (baseResponse != null && baseResponse.Result != null)
                {
                    return baseResponse.Result;
                }
                else
                {
                    throw new Exception("NotionExportResponse가 null입니다.");
                }
            }
            catch (JsonException ex)
            {
                SaveDebugLog($"JSON Parse Error: {ex.Message}");
                MessageBox.Show($"JSON 파싱 오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            catch (Exception ex)
            {
                SaveDebugLog($"Export Exception: {ex.Message}");
                MessageBox.Show($"내보내기 오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                // 디버깅: 콜백 파라미터 로그
                SaveDebugLog($"Callback - Code: {code}, State: {state}");
                
                var query = $"?code={Uri.EscapeDataString(code)}&state={Uri.EscapeDataString(state)}";
                var url = $"{_baseUrl}/api/notion/callback{query}";

                SaveDebugLog($"Callback URL: {url}");

                var response = await _httpClient.GetAsync(url);
                
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    SaveDebugLog($"Callback Error: {errorContent}");
                    throw new Exception($"HTTP {response.StatusCode}: {errorContent}");
                }

                var result = await response.Content.ReadAsStringAsync();
                SaveDebugLog($"Callback Success: {result}");
                return result;
            }
            catch (Exception ex)
            {
                SaveDebugLog($"Callback Exception: {ex.Message}");
                throw new Exception($"콜백 처리 실패: {ex.Message}", ex);
            }
        }

        private void SaveDebugLog(string message)
        {
            try
            {
                var logPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "notion_debug.log");
                var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                System.IO.File.AppendAllText(logPath, $"[{timestamp}] {message}\n");
            }
            catch
            {
                // 로그 저장 실패해도 무시
            }
        }
    }
}
