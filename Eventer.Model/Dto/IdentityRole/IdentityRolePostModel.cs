using System.ComponentModel.DataAnnotations;
using Eventer.Model.Dto.Common;

namespace Eventer.Model.Dto.IdentityRole
{
    public class IdentityRolePostModel : DtoBase
    {
        [Required]
        public string RoleName { get; set; }
    }
}