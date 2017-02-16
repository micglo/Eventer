using System.Web.Http;

namespace Eventer.Web.Api.Utility.Versioning
{
    public class ApiVersion2RoutePrefixAttribute : RoutePrefixAttribute
    {
        private const string RouteBase = "api/{apiVersion:apiVersionConstraint(v2)}";
        private const string PrefixRouteBase = RouteBase + "/";

        public ApiVersion2RoutePrefixAttribute(string routePrefix)
            : base(string.IsNullOrWhiteSpace(routePrefix) ? RouteBase : PrefixRouteBase + routePrefix)
        {
        }
    }
}