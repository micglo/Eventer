using System.ComponentModel.DataAnnotations;
using Eventer.Model.Dto.Common;

namespace Eventer.Model.Dto.IdentityRole
{
    public class UserRoleModel : DtoBase
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}