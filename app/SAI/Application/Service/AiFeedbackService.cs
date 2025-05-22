using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SAI.Application.Dto;
using System.IO;
using System.Web.Script.Serialization;
using SAI.App.Models;
using System.Security.Cryptography;
using System.Globalization;

namespace SAI.Application.Service
{
    public class AiFeedbackService
    {
        private readonly HttpClient _http;
        private readonly string _token;

        public AiFeedbackService(string baseAddress, string token)
        {
            _http = new HttpClient { BaseAddress = new Uri(baseAddress) };
            _token = token;
        }

        public async Task<BaseResponse<AiFeedbackResponseDto>> SendAsync(AiFeedbackRequestDto dto)
        {

            _http.DefaultRequestHeaders.Authorization =
                 new AuthenticationHeaderValue("Bearer", _token);

            using (var form = new MultipartFormDataContent())
            {
                var codePath = Path.GetFullPath(dto.code);
                if (File.Exists(codePath))
                {
                    // (A) 텍스트로 읽어서 StringContent
                    var codeText = File.ReadAllText(codePath, Encoding.UTF8);
                    form.Add(new StringContent(codeText, Encoding.UTF8), "code");
                }

                AddImagePart(form, dto.logImage, "logImage");
                AddImagePart(form, dto.resultImage, "resultImage");

                form.Add(new StringContent(dto.memo, Encoding.UTF8), "memo");
                form.Add(new StringContent(dto.threshold.ToString(CultureInfo.InvariantCulture), Encoding.UTF8), "threshold");

                var response = await _http.PostAsync("/api/ai/feedback", form)
                                          .ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync()
                                             .ConfigureAwait(false);

                var serializer = new JavaScriptSerializer();
                return serializer.Deserialize<BaseResponse<AiFeedbackResponseDto>>(json);
            }
        }

        private void AddImagePart(MultipartFormDataContent form, string path, string partName)
        {
            Console.WriteLine($"[DEBUG] Attaching image part '{partName}', relative-path='{path}'");

            if (string.IsNullOrWhiteSpace(path))
                return;

            var fullPath = Path.GetFullPath(path);

            Console.WriteLine($"[DEBUG] → fullPath = '{fullPath}', Exists = {File.Exists(fullPath)}");
            if (!File.Exists(fullPath))
                return;

            var fileStream = File.OpenRead(fullPath);
            var streamContent = new StreamContent(fileStream);

            var extension = Path.GetExtension(fullPath)?.ToLower();
            string contentType = "application/octet-stream"; // 기본값

            if (extension == ".jpg" || extension == ".jpeg")
                contentType = "image/jpeg";
            else if (extension == ".png")
                contentType = "image/png";
            else if (extension == ".gif")
                contentType = "image/gif";

            streamContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);

            // form 필드에 추가 (form.Dispose() 시 streamContent, fileStream 모두 닫힙니다)
            form.Add(streamContent, partName, Path.GetFileName(fullPath));
        }

        public void Dispose() => _http.Dispose();
    }
}
