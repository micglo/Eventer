using System.Web.Http;
using Eventer.Service.ApiActivity.Interface;
using Eventer.Service.Client.Interface;
using Eventer.Service.ErrorLog.Interface;
using Eventer.Service.EventerService.Interface;
using Eventer.Service.RefreshToken.Interface;
using Microsoft.AspNet.Identity;
using Eventer.Service.RoleManager;
using Eventer.Service.RoleService;
using Eventer.Utility.CustomException;

namespace Eventer.Web.Api.Controllers
{
    public abstract class ApiControllerBase : ApiController
    {
        protected ApplicationUserManager UserManager;
        protected ApplicationRoleManager RoleManager;
        protected IApiActivityLogService ApiActivityLogService;
        protected ICategoryService CategoryService;
        protected ICityService CityService;
        protected IClientService ClientService;
        protected IErrorLogService ErrorLogService;
        protected IEventService EventService;
        protected IRefreshTokenService RefreshTokenService;
        protected IRoleService RoleService;
        protected IStateService StateService;
        protected ICustomException CustomException;

        protected IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
                return InternalServerError();

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}