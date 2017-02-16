namespace Eventer.WindowsService.Service.ClientModel.EventerApi.Token
{
    public class TokenCredential
    {
        public TokenCredential(string grantType, string userName, string password, string clientId, string clientSecret)
        {
            GrantType = grantType;
            Username = userName;
            Password = password;
            ClientId = clientId;
            ClientSecret = clientSecret;
        }

        public TokenCredential(string grantType, string clientId, string clientSecret)
        {
            GrantType = grantType;
            ClientId = clientId;
            ClientSecret = clientSecret;
        }

        public string GrantType { get; set; }

        public string Username { get; set; } 

        public string Password { get; set; } 

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }
    }
}