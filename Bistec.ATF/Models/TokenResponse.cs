using System.Text.Json.Serialization;

namespace Bistec.ATF.Models
{
    public class TokenResponse
    {
        [JsonPropertyName("id_token")]
        public string IdToken { get; set; }

        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
    }

}
