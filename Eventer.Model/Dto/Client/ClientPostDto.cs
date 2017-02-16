using System.ComponentModel.DataAnnotations;
using Eventer.Model.Dto.Common;

namespace Eventer.Model.Dto.Client
{
    public class ClientPostDto : DtoBase
    {
        public string ClientSecret { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public int ApplicationType { get; set; }
        [Required]
        public bool Active { get; set; }
        [Required]
        public int RefreshTokenLifeTime { get; set; }
        public string AllowedOrigin { get; set; }
    }
}