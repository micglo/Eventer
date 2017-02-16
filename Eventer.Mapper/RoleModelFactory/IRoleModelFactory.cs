using Eventer.Model.Dto.IdentityRole;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Eventer.Mapper.RoleModelFactory
{
    public interface IRoleModelFactory
    {
        IdentityRoleDto GetModel(IdentityRole role);
        IdentityRole GetModel(IdentityRoleDto role);
        IdentityRole GetModel(IdentityRolePostModel role);
    }
}