using System;
using System.ComponentModel.DataAnnotations;
using Eventer.Model.Dto.Common;

namespace Eventer.Model.Dto.RefreshToken
{
    public class RefreshTokenDto : CommonDto<string>
    {
        [Required]
        public string Subject { get; set; }

        [Required]
        public string ClientId { get; set; }
        public DateTime IssuedUtc { get; set; }
        public DateTime ExpiresUtc { get; set; }

        [Required]
        public string ProtectedTicket { get; set; }
    }
}