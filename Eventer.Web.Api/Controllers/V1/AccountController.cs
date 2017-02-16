using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Eventer.Model.ApiPagination.Common;
using Eventer.Domain.Entity;
using Eventer.Model.BindingModel.Account;
using Eventer.Model.Dto.IdentityRole;
using Eventer.Model.QueryString.Pagination;
using Eventer.Web.Api.Utility.CustomMessage;
using Eventer.Web.Api.Utility.Versioning;
using Microsoft.AspNet.Identity;
using Eventer.Service.RoleManager;
using Eventer.Service.RoleService;
using Eventer.Utility.CustomException;
using Eventer.Web.Api.Utility.Filter;

namespace Eventer.Web.Api.Controllers.V1
{
    /// <summary>
    /// Account management
    /// </summary>
    [ApiVersion1RoutePrefix("Account")]
    public class AccountController : ApiControllerBase
    {
        private readonly ICustomException _customException;
        /// <summary>
        /// Account controller
        /// </summary>
        /// <param name="customException"></param>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        /// <param name="roleService"></param>
        public AccountController(ApplicationUserManager userManager, ApplicationRoleManager roleManager, IRoleService roleService, ICustomException customException) 
            //: base(customException)
        {
            UserManager = userManager;
            RoleManager = roleManager;
            RoleService = roleService;
            _customException = customException;
        }

        /// <summary>
        /// Get single user info
        /// </summary>
        /// <remarks>
        /// Get single user info
        /// </remarks>
        /// <param name="id">Id of user</param>
        /// <returns>Single user</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">User not found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [MyAuthorize(Roles = "Administrators")]
        [Route("GetUserInfo/{id:guid}", Name = "GetUserInfo")]
        public async Task<IHttpActionResult> GetUserInfo(string id)
        {
            var user = await UserManager.FindByIdAsync(id);

            if (user == null)
                _customException.ThrowNotFoundException($"User with id: {id} doesn't exist.");

            var model = new
            {
                Id = user?.Id,
                Email = user?.Email
            };

            return Ok(model);
        }

        /// <summary>
        /// Get single user roles
        /// </summary>
        /// <remarks>
        /// Get single user roles
        /// </remarks>
        /// <param name="id">Id of user</param>
        /// <param name="paginationModel">page, pageSize variables</param>
        /// <returns>Paged list of user roles</returns>
        /// /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">User not found</response>
        /// <response code="500">Internal Server Error</response>
        [MyAuthorize(Roles = "Administrators")]
        [Route("GetUserInfo/{id:guid}/Roles", Name = "GetUserRoles")]
        [HttpGet]
        [ResponseType(typeof(PagedItems<IdentityRoleDto>))]
        public async Task<IHttpActionResult> GetUserRoles(string id, [FromUri] Pagination paginationModel)
        {
            var user = await UserManager.FindByIdAsync(id);

            if (user == null)
                _customException.ThrowNotFoundException($"User with id: {id} doesn't exist.");

            return Ok(await RoleService.GetUserRoles(id, paginationModel.Page, paginationModel.PageSize));
        }

        /// <summary>
        /// Register new user
        /// </summary>
        /// <remarks>
        /// Register new user
        /// </remarks>
        /// <param name="model">Model with data to register new user</param>
        /// <returns>Http status code</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [AllowAnonymous]
        [HttpPost]
        [Route("Register", Name = "Register")]
        public async Task<IHttpActionResult> Register(RegisterBindingModel model)
        {
            var user = new User
            {
                UserName = model.Email,
                Email = model.Email
            };

            IdentityResult result = await UserManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return GetErrorResult(result);

            UserManager.AddToRoles(user.Id, "Users");

            await SendEmailConfirmationTokenAsync(user.Id, "Confirm your account");

            return Ok();
        }

        /// <summary>
        /// Resend activation link to confirm registered account
        /// </summary>
        /// <remarks>
        /// Resend activation link to confirm registered account
        /// </remarks>
        /// <param name="model">Model with data to resend activation link</param>
        /// <returns>Http status code</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [AllowAnonymous]
        [Route("ResendActivationLink", Name = "ResendActivationLink")]
        [HttpPost]
        public async Task<IHttpActionResult> ResendActivationLink([FromBody]AuthorizationBindingModel model)
        {
            var user = await UserManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                if (user.EmailConfirmed)
                    _customException.ThrowBadRequestException($"Email: {model.Email} is already confirmed");

                var result = await UserManager.CheckPasswordAsync(user, model.Password);

                if (result)
                {
                    await SendEmailConfirmationTokenAsync(user.Id, "Confirm your account");
                    return Ok();
                }

                ModelState.AddModelError("", "Invalid email or password.");
            }
            else
                _customException.ThrowNotFoundException($"Email: {model.Email} doesn't exist.");

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Change user password
        /// </summary>
        /// <remarks>
        /// Change user password
        /// </remarks>
        /// <param name="model">Model with data to change password</param>
        /// <returns>Http status code</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [Route("ChangePassword", Name = "ChangePassword")]
        [HttpPost]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
                model.NewPassword);
            
            if (!result.Succeeded)
                return GetErrorResult(result);

            return Ok();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        [AllowAnonymous]
        [Route("ConfirmEmail", Name = "ConfirmEmail")]
        public async Task<IHttpActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
                _customException.ThrowBadRequestException("User Id and Code are required");

            var user = await UserManager.FindByIdAsync(userId);

            if (user.EmailConfirmed)
                _customException.ThrowBadRequestException($"Email: {user.Email} is already confirmed.");

            var result = await UserManager.ConfirmEmailAsync(userId, code);

            return !result.Succeeded ? GetErrorResult(result) : new TextPlainMessage("Email confirmed.", Request);
        }

        #region Helpers

        private async Task SendEmailConfirmationTokenAsync(string userId, string subject)
        {
            var code = await UserManager.GenerateEmailConfirmationTokenAsync(userId);
            var callbackUrl = new Uri(Url.Link("ConfirmEmail", new { userId, code }));
            await UserManager.SendEmailAsync(userId, subject,
               "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
        }

        #endregion
    }
}
