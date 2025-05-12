using System.Text.Json.Serialization;

namespace helpme.Models
{
    public class AuthCheckResponse
    {
        [JsonPropertyName("authenticated")]
        public bool Authenticated { get; set; }
        
        [JsonPropertyName("authUrl")]
        public string AuthUrl { get; set; }
    }
}
