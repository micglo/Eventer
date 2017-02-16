using System.ComponentModel.DataAnnotations;
using Eventer.Model.Dto.Common;

namespace Eventer.Model.Dto.Client
{
    public class AddClientDto : DtoBase
    {
        [Required]
        public bool IsJavaScriptClient { get; set; }
        
        public string AllowedOrigin { get; set; }
    }
}