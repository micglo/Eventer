using System.Threading.Tasks;
using Eventer.Model.ApiPagination.Common;
using Eventer.Model.Dto.IdentityRole;
using Eventer.Service.Common;

namespace Eventer.Service.RoleService
{
    public interface IRoleService : IServiceBase<IdentityRoleDto, IdentityRolePostModel, IdentityRoleDto>
    {
        Task<IdentityRoleDto> GetByName(string name);
        Task<PagedItems<IdentityRoleDto>> GetUserRoles(string userId, string skip, string take);
    }
}