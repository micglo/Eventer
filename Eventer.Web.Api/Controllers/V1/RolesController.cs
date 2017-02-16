using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Eventer.Model.ApiPagination.Common;
using Eventer.Model.Dto.IdentityRole;
using Eventer.Model.QueryString.Pagination;
using Eventer.Web.Api.Utility.Filter;
using Eventer.Web.Api.Utility.Versioning;
using Eventer.Service.RoleService;
using Eventer.Utility.CustomException;
using Microsoft.AspNet.Identity;

namespace Eventer.Web.Api.Controllers.V1
{
    /// <summary>
    /// Controller with actions to manage roles.
    /// </summary>
    [ApiVersion1RoutePrefix("Roles")]
    public class RolesController : ApiControllerBase
    {
        /// <summary>
        /// Roles Controller
        /// </summary>
        /// <param name="customException"></param>
        /// <param name="roleService"></param>
        /// <param name="userManager"></param>
        public RolesController(IRoleService roleService, ApplicationUserManager userManager, ICustomException customException)
        {
            RoleService = roleService;
            UserManager = userManager;
            CustomException = customException;
        }


        /// <summary>
        /// Get list of roles. Administrators only.
        /// </summary>
        /// <remarks>
        /// Get list of roles. Administrators only.
        /// </remarks>
        /// <param name="paginationModel">Model representing query string</param>
        /// <returns>Paged list of roles.</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [MyAuthorize(Roles = "Administrators")]
        [Route("", Name = "GetRoles")]
        [HttpGet]
        [ResponseType(typeof(PagedItems<IdentityRoleDto>))]
        public async Task<IHttpActionResult> GetRoles([FromUri] Pagination paginationModel)
            => Ok(await RoleService.GetAllAsync(paginationModel.Page, paginationModel.PageSize));


        /// <summary>
        /// Get single role specified by id. Administrators only.
        /// </summary>
        /// <remarks>
        /// Get single role specified by id. Administrators only.
        /// </remarks>
        /// <param name="id">Id of role.</param>
        /// <returns>Single role.</returns>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Role not found</response>
        /// <response code="500">Internal Server Error</response>
        [MyAuthorize(Roles = "Administrators")]
        [Route("{id:guid}", Name = "GetRole")]
        [HttpGet]
        [ResponseType(typeof(IdentityRoleDto))]
        public async Task<IHttpActionResult> GetRole(string id)
            => Ok(await RoleService.GetByIdAsync(id));


        /// <summary>
        /// Get single role specified by name. Administrators only.
        /// </summary>
        /// <remarks>
        /// Get single role specified by name. Administrators only.
        /// </remarks>
        /// <param name="roleName">Name of the role.</param>
        /// <returns>Single role.</returns>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Role not found</response>
        /// <response code="500">Internal Server Error</response>
        [MyAuthorize(Roles = "Administrators")]
        [Route("{roleName}", Name = "GetRoleByName")]
        [HttpGet]
        [ResponseType(typeof(IdentityRoleDto))]
        public async Task<IHttpActionResult> GetRoleByName(string roleName)
            => Ok(await RoleService.GetByName(roleName));


        /// <summary>
        /// Edit role. Administrators only.
        /// </summary>
        /// <remarks>
        /// Edit role. Administrators only.
        /// </remarks>
        /// <param name="id">Id of role.</param>
        /// <param name="role">Data role model.</param>
        /// <returns>Edited role.</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Role not found</response>
        /// <response code="500">Internal Server Error</response>
        [MyAuthorize(Roles = "Administrators")]
        [Route("{id:guid}", Name = "PutRole")]
        [HttpPut]
        [ResponseType(typeof(IdentityRoleDto))]
        public async Task<IHttpActionResult> PutRole(string id, IdentityRoleDto role)
        {
            if (id != role.Id)
                CustomException.ThrowBadRequestException($"Id: {id} doesn't match.");

            return Ok(await RoleService.Update(role));
        }


        /// <summary>
        /// Create new role. Administrators only.
        /// </summary>
        /// <remarks>
        /// Create new role. Administrators only.
        /// </remarks>
        /// <param name="role">Data role model.</param>
        /// <returns>Created role.</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [MyAuthorize(Roles = "Administrators")]
        [Route("", Name = "PostRole")]
        [HttpPost]
        [ResponseType(typeof(IdentityRoleDto))]
        public async Task<IHttpActionResult> PostRole(IdentityRolePostModel role)
        {
            var newRole = await RoleService.AddAsync(role);

            return CreatedAtRoute("RoleRoute", new { id = newRole.Id }, newRole);
        }


        /// <summary>
        /// Delete role. Administrators only.
        /// </summary>
        /// <remarks>
        /// Delete role. Administrators only.
        /// </remarks>
        /// <param name="id">Id of role.</param>
        /// <returns>Http status code.</returns>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Role not found</response>
        /// <response code="500">Internal Server Error</response>
        [MyAuthorize(Roles = "Administrators")]
        [Route("{id:guid}", Name = "DeleteRole")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteRole(string id)
        {
            await RoleService.RemoveAsync(id);
            return Ok();
        }


        /// <summary>
        /// Add user to role. Administrators only.
        /// </summary>
        /// <remarks>
        /// Add user to role. Administrators only.
        /// </remarks>
        /// <param name="model">UserRole data model.</param>
        /// <returns>Http status code.</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Role/User not found</response>
        /// <response code="500">Internal Server Error</response>
        [MyAuthorize(Roles = "Administrators")]
        [Route("AddUserToRole", Name = "AddUserToRole")]
        [HttpPost]
        public async Task<IHttpActionResult> AddUserToRole(UserRoleModel model)
        {
            var user = await UserManager.FindByEmailAsync(model.Email);

            if (user == null)
                CustomException.ThrowNotFoundException($"There is no user {model.Email}.");

            if (await UserManager.IsInRoleAsync(user?.Id, model.RoleName))
                CustomException.ThrowBadRequestException($"User {model.Email} is already in role {model.RoleName}.");

            IdentityResult result = await UserManager.AddToRoleAsync(user?.Id, model.RoleName);

            return result.Succeeded ? Ok() : GetErrorResult(result);
        }


        /// <summary>
        /// Remove user from role. Administrators only.
        /// </summary>
        /// <remarks>
        /// Remove user from role. Administrators only.
        /// </remarks>
        /// <param name="model">UserRole data model.</param>
        /// <returns>Http status code.</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Role/User found</response>
        /// <response code="500">Internal Server Error</response>
        [MyAuthorize(Roles = "Administrators")]
        [Route("RemoveUserRole", Name = "RemoveUserRole")]
        [HttpPost]
        public async Task<IHttpActionResult> RemoveUserRole(UserRoleModel model)
        {
            var user = await UserManager.FindByEmailAsync(model.Email);

            if (user == null)
                CustomException.ThrowNotFoundException($"There is no user {model.Email}.");

            if (!await UserManager.IsInRoleAsync(user?.Id, model.RoleName))
                CustomException.ThrowBadRequestException($"User {model.Email} is not in role {model.RoleName}.");

            IdentityResult result = await UserManager.RemoveFromRoleAsync(user?.Id, model.RoleName);

            return result.Succeeded ? Ok() : GetErrorResult(result);
        }


        /// <summary>
        /// Get my roles
        /// </summary>
        /// <remarks>
        /// Get my roles
        /// </remarks>
        /// <returns>List of role names</returns>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [Route("GetMyRoles", Name = "GetMyRoles")]
        [HttpGet]
        [ResponseType(typeof(List<string>))]
        public async Task<IHttpActionResult> GetMyRoles()
        {
            var userId = User.Identity.GetUserId();
            var roleNames = await UserManager.GetRolesAsync(userId);

            return Ok(roleNames);
        }
    }
}