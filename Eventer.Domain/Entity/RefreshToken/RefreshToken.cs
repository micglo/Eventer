using System;
using Eventer.Domain.Entity.Common;

namespace Eventer.Domain.Entity.RefreshToken
{
    /// <summary>
    /// Encja przedstawiająca zależność clien-token, każdy klient może otrzymac token za pomocą swojego refresh tokena
    /// </summary>
    public class RefreshToken : CommonEntity<string>
    {
        /// <summary>
        /// Podmiot uzywajacy refresh tokena (login, email)
        /// </summary>
        public string Subject { get; set; }
        public string ClientId { get; set; }
        public DateTime IssuedUtc { get; set; }
        public DateTime ExpiresUtc { get; set; }

        /// <summary>
        /// Ticket zawierajacy claimy uzytkownika, oraz inne jego wlasciwosci, potrzebny do utworzenia nowego access tokena
        /// </summary>
        public string ProtectedTicket { get; set; }
    }
}