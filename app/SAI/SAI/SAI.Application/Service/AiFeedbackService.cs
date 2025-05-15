using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SAI.SAI.Application.Dto;
using System.IO;
using System.Web.Script.Serialization;

namespace SAI.SAI.Application.Service
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
                form.Add(new StringContent(dto.code, Encoding.UTF8), nameof(dto.code));
                form.Add(new StringContent(dto.log, Encoding.UTF8), nameof(dto.log));

                if (!string.IsNullOrWhiteSpace(dto.image) && File.Exists(dto.image))
                {
                    var fileStream = File.OpenRead(dto.image);
                    var streamPart = new StreamContent(fileStream);

                    // 파일 확장자 기반으로 Content-Type 설정
                    var extension = Path.GetExtension(dto.image)?.ToLower();
                    string contentType = "application/octet-stream"; // 기본값

                    if (extension == ".jpg" || extension == ".jpeg")
                        contentType = "image/jpeg";
                    else if (extension == ".png")
                        contentType = "image/png";
                    else if (extension == ".gif")
                        contentType = "image/gif";

                    streamPart.Headers.ContentType = new MediaTypeHeaderValue(contentType);

                    form.Add(streamPart, nameof(dto.image), Path.GetFileName(dto.image));
                }

                var response = await _http.PostAsync("/api/ai/feedback", form)
                                          .ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync()
                                             .ConfigureAwait(false);

                var serializer = new JavaScriptSerializer();
                return serializer.Deserialize<BaseResponse<AiFeedbackResponseDto>>(json);
            }
        }

        public void Dispose() => _http.Dispose();
    }
}
