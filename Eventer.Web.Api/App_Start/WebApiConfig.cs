using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Routing;
using Eventer.Web.Api.Utility.ApiActivity;
using Eventer.Web.Api.Utility.ErrorHandler;
using Eventer.Web.Api.Utility.Filter;
using Eventer.Web.Api.Utility.Formatter;
using Eventer.Web.Api.Utility.Versioning;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;

namespace Eventer.Web.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            var constraintsResolver = new DefaultInlineConstraintResolver();
            constraintsResolver.ConstraintMap.Add("apiVersionConstraint", typeof
                (ApiVersionConstraint));
            config.MapHttpAttributeRoutes(constraintsResolver);
            config.Services.Replace(typeof(IHttpControllerSelector),
                new NamespaceHttpControllerSelector(config));

            config.Formatters.Add(new BrowserJsonFormatter());
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.Filters.Add(new RequireHttpsFilterAttribute());
            config.Filters.Add(new MyAuthorizeAttribute());
            config.Filters.Add(new ValidateModelFilterAttribute());
            config.Filters.Add(new CheckPaginationModelAttribute());

            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
            config.Services.Add(typeof(IExceptionLogger), new GlobalExceptionLogger());
            config.MessageHandlers.Add(new ApiActivityLogHandler());

            ApiRouteConfig.Config(config);
        }
    }
}
