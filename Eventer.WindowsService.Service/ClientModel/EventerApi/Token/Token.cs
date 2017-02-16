using Newtonsoft.Json;

namespace Eventer.WindowsService.Service.ClientModel.EventerApi.Token
{
    public class Token
    {
        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public string Expires { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}