using System.ComponentModel.DataAnnotations;
using Eventer.Domain.Entity.Client;
using Eventer.Model.Dto.Common;

namespace Eventer.Model.Dto.Client
{
    public class ClientNoSecretDto : CommonDto<string>
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public ApplicationTypes ApplicationType { get; set; }
        [Required]
        public bool Active { get; set; }
        [Required]
        public int RefreshTokenLifeTime { get; set; }
        [Required]
        public string AllowedOrigin { get; set; }
    }
}