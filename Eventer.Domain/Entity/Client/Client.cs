using Eventer.Domain.Entity.Common;

namespace Eventer.Domain.Entity.Client
{
    /// <summary>
    /// Encja przedstawiająca klienta api, kazdy klient za pomoca swojego id (i secretu) otrzymuje dostep do api
    /// </summary>
    public class Client : CommonEntity<string>
    {
        public string ClientSecret { get; set; }
        public string Username { get; set; }
        public ApplicationTypes ApplicationType { get; set; }
        public bool Active { get; set; }
        public int RefreshTokenLifeTime { get; set; }


        /// <summary>
        /// Jaki adres URL obowiazuje dla danego klienta(cors)
        /// </summary>
        public string AllowedOrigin { get; set; }
    }
}