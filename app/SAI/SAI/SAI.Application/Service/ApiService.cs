using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Http.Headers;
using SAI.SAI.App.Models;

namespace SAI.SAI.Application.Service
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ApiService(string baseUrl)
        {
            _baseUrl = baseUrl;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(baseUrl);
            
            // 학습 프로세스를 위한 긴 타임아웃 설정 (10분)
            _httpClient.Timeout = TimeSpan.FromMinutes(10);
        }
        // ApiService.cs에 다음 메서드 추가
        public async Task<HttpResponseMessage> PostAsync(string endpoint, HttpContent content)
        {
            return await _httpClient.PostAsync(endpoint, content);
        }

        public async Task<HttpResponseMessage> GetAsync(string endpoint)
        {
            return await _httpClient.GetAsync(endpoint);
        }

        // 서버 학습 취소 메서드 추가
        public async Task<bool> CancelTraining(string taskId)
        {
            try
            {
                Console.WriteLine($"[INFO] 서버 학습 취소 요청: taskId={taskId}");
                
                var response = await _httpClient.PostAsync(
                    $"/api/training/cancel/{taskId}",
                    new StringContent(string.Empty, Encoding.UTF8, "application/json")
                );

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"[INFO] 서버 학습 취소 성공: taskId={taskId}");
                    return true;
                }
                else
                {
                    Console.WriteLine($"[WARNING] 서버 학습 취소 실패: taskId={taskId}, StatusCode={response.StatusCode}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] 서버 학습 취소 중 오류 발생: taskId={taskId}, Error={ex.Message}");
                return false;
            }
        }

        public async Task<InferenceResult> RunInference(string imagePath, double conf)
        {
            try
            {
                // 이미지를 Base64로 인코딩
                string imageBase64 = Convert.ToBase64String(File.ReadAllBytes(imagePath));
                string imageName = Path.GetFileName(imagePath);

                // API 요청 데이터 구성
                var requestData = new
                {
                    image_base64 = imageBase64,
                    confidence = conf,
                    image_name = imageName
                };

                // API 호출
                var response = await _httpClient.PostAsync(
                    "/api/inference",
                    new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json")
                );

                if (!response.IsSuccessStatusCode)
                {
                    return new InferenceResult
                    {
                        Success = false,
                        Error = $"API 호출 실패: {response.StatusCode}"
                    };
                }

                // 응답 파싱
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ApiInferenceResponse>(responseContent);

                if (!result.success)
                {
                    return new InferenceResult
                    {
                        Success = false,
                        Error = result.error
                    };
                }

                // Base64 이미지를 파일로 저장
                string resultImagePath = Path.Combine(
                    Path.GetDirectoryName(imagePath),
                    $"{Path.GetFileNameWithoutExtension(imagePath)}_result{Path.GetExtension(imagePath)}"
                );

                File.WriteAllBytes(resultImagePath, Convert.FromBase64String(result.result_image_base64));

                return new InferenceResult
                {
                    Success = true,
                    ResultImage = resultImagePath,
                    OriginalName = result.original_name
                };
            }
            catch (Exception ex)
            {
                return new InferenceResult
                {
                    Success = false,
                    Error = $"API 호출 중 오류 발생: {ex.Message}"
                };
            }
        }

        private class ApiInferenceResponse
        {
            public bool success { get; set; }
            public string result_image_base64 { get; set; }
            public string original_name { get; set; }
            public string error { get; set; }
        }
        public class InferenceResult
        {
            public bool Success { get; set; }
            public string ResultImage { get; set; }
            public double InferenceTime { get; set; }
            public string Error { get; set; }
            public string OriginalName { get; set; } // 추가된 속성
        }
    }
} 