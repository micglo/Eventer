using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Eventer.Web.Api.Utility.Filter
{
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            if (actionContext.RequestContext.Principal.Identity.IsAuthenticated)
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
            }
            else
            {
                base.HandleUnauthorizedRequest(actionContext);
            }
        }
    }
}