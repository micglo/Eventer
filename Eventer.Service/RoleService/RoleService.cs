using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Eventer.Mapper.RoleModelFactory;
using Eventer.Model.ApiPagination.Common;
using Eventer.Model.Dto.IdentityRole;
using Eventer.Service.Common;
using Eventer.Service.RoleManager;
using Eventer.Utility.CustomException;

namespace Eventer.Service.RoleService
{
    public class RoleService : ServiceBase<IdentityRoleDto, IdentityRolePostModel, IdentityRoleDto>, IRoleService
    {
        private readonly ApplicationRoleManager _roleManager;
        private readonly IRoleModelFactory _roleModelFactory;

        public RoleService(IRoleModelFactory roleModelFactory, HttpRequestMessage request, ApplicationRoleManager roleManager, ICustomException customException)
            : base(customException, request)
        {
            _roleModelFactory = roleModelFactory;
            _roleManager = roleManager;
            CustomException = customException;
        }

        public override async Task<PagedItems<IdentityRoleDto>> GetAllAsync(string skip, string take)
        {
            var intSkip = int.Parse(skip);
            var intTake = int.Parse(take);
            var skipAmount = intTake * (intSkip - 1);

            var roles = await _roleManager.Roles.OrderBy(r=>r.Name).Skip(skipAmount).Take(intTake).ToListAsync();
            var rolesCount = _roleManager.Roles.Count();
            var rolesDto = roles.Select(_roleModelFactory.GetModel);

            return CreatePagedItems(rolesDto, "RoleRoute", intSkip, intTake, rolesCount);
        }

        public override async Task<IdentityRoleDto> GetByIdAsync(object id)
        {
            if (!await ExistsAsync(id))
                CustomException.ThrowNotFoundException($"There is no role with id = {id}.");

            var role = await _roleManager.FindByIdAsync((string) id);
            return _roleModelFactory.GetModel(role);
        }

        public override async Task<IdentityRoleDto> AddAsync(IdentityRolePostModel dtoModel)
        {
            if (await RoleExistsAsync(dtoModel.RoleName))
                CustomException.ThrowBadRequestException($"Role: {dtoModel.RoleName} already exists.");

            var role = _roleModelFactory.GetModel(dtoModel);
            await _roleManager.CreateAsync(role);
            var newRole = await _roleManager.FindByNameAsync(dtoModel.RoleName);
            return _roleModelFactory.GetModel(newRole);
        }

        public override async Task<IdentityRoleDto>  Update(IdentityRoleDto dtoModel)
        {
            var role = _roleModelFactory.GetModel(dtoModel);
            await _roleManager.UpdateAsync(role);

            return await GetByIdAsync(dtoModel.Id);
        }

        public override async Task RemoveAsync(object id)
        {
            if (!await ExistsAsync(id))
                CustomException.ThrowNotFoundException($"There is no role with id = {id}.");

            var role = await _roleManager.FindByIdAsync((string) id);
            await _roleManager.DeleteAsync(role);
        }

        public async Task<IdentityRoleDto> GetByName(string name)
        {
            if (!await RoleExistsAsync(name))
                CustomException.ThrowNotFoundException($"There is no role: {name}.");

            var role = await _roleManager.FindByNameAsync(name);
            return _roleModelFactory.GetModel(role);
        }

        public async Task<PagedItems<IdentityRoleDto>> GetUserRoles(string userId, string skip, string take)
        {
            var intSkip = int.Parse(skip);
            var intTake = int.Parse(take);
            var skipAmount = intTake * (intSkip - 1);

            var roles = await _roleManager.Roles.Where(r => r.Users.Any(u => u.UserId.Equals(userId))).OrderBy(r => r.Name).Skip(skipAmount).Take(intTake).ToListAsync();
            var rolesCount = _roleManager.Roles.Count(r => r.Users.Any(u => u.UserId.Equals(userId)));
            var rolesDto = roles.Select(_roleModelFactory.GetModel);

            return CreatePagedItems(rolesDto, "GetUserRolesRoute", intSkip, intTake, rolesCount);
        }

        #region Helpers

        protected override async Task<bool> ExistsAsync(object id)
            => await _roleManager.FindByIdAsync((string)id) != null;

        private async Task<bool> RoleExistsAsync(string name)
            => await _roleManager.RoleExistsAsync(name);

        #endregion
    }
}