using System.Web.Http;
using Eventer.Web.Api.Utility.Versioning;
using Eventer.Service.RoleManager;
using Eventer.Utility.CustomException;

namespace Eventer.Web.Api.Controllers.V1
{
    /// <summary>
    /// Test
    /// </summary>
    [AllowAnonymous]
    [ApiVersion1RoutePrefix("test")]
    public class TestController : ApiControllerBase
    {
        /// <summary>
        /// Test
        /// </summary>
        /// <param name="customException"></param>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        public TestController(ICustomException customException, ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
            CustomException = customException;
        }
    }
}