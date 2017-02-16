using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Eventer.Model.Dto.Common;
using Eventer.Model.Dto.User;

namespace Eventer.Model.Dto.IdentityRole
{
    public class IdentityRoleDto : CommonDto<string>
    {
        [Required]
        public string RoleName { get; set; }
        public IEnumerable<UserDto> Users { get; set; }
    }
}