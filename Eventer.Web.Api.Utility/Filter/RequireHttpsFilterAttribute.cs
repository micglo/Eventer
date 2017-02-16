using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Eventer.Web.Api.Utility.Filter
{
    public class RequireHttpsFilterAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.RequestUri.Scheme != Uri.UriSchemeHttps)
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden)
                {
                    ReasonPhrase = "HTTPS Required"
                };
            }
            else
            {
                base.OnAuthorization(actionContext);
            }
        }
    }
}