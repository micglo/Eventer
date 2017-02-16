using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Eventer.Model.Dto.ApiActivity;
using Eventer.Service.ApiActivity;
using Eventer.Service.ApiActivity.Interface;
using Eventer.Web.Api.Utility.Infrastructure.SerializeHelper;

namespace Eventer.Web.Api.Utility.ApiActivity
{
    public class ApiActivityLogHandler : DelegatingHandler
    {
        private readonly IApiActivityLogService _activityLogService;
        public ApiActivityLogHandler()
        {
            _activityLogService = new ApiActivityLogService();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var apiLogEntry = CreateApiLogEntryWithRequestData(request);

            if (request.Content != null)
            {
                await request.Content.ReadAsStringAsync()
                    .ContinueWith(task =>
                    {
                        apiLogEntry.RequestContentBody = task.Result;
                    }, cancellationToken);
            }

            return await base.SendAsync(request, cancellationToken)
            .ContinueWith(task =>
            {
                var response = task.Result;
                apiLogEntry.User = request.GetRequestContext().Principal.Identity.Name;

                apiLogEntry.ResponseStatusCode = (int)response.StatusCode;
                apiLogEntry.ResponseTimestamp = DateTime.Now;

                if (response.Content != null)
                {
                    apiLogEntry.ResponseContentType = response.Content.Headers.ContentType.MediaType;
                    apiLogEntry.ResponseContentBody = response.Content.ReadAsStringAsync().Result;
                    apiLogEntry.ResponseHeaders = SerializeHelper.SerializeHeaders(response.Content.Headers);
                }

                _activityLogService.Add(apiLogEntry);

                return response;
            }, cancellationToken);
        }

        private static ApiActivityLogDto CreateApiLogEntryWithRequestData(HttpRequestMessage request)
        {
            var iOwinContext = HttpContext.Current.GetOwinContext();
            var context = iOwinContext.Get<HttpContextBase>(typeof(HttpContextBase).FullName);
            var routeData = request.GetConfiguration()?.Routes.GetRouteData(request);

            return new ApiActivityLogDto
            {
                RequestContentType = context.Request.ContentType,   
                UserHostAddress = context.Request.UserHostAddress,
                RequestMethod = request.Method.Method,
                RequestHeaders = SerializeHelper.SerializeHeaders(request.Headers),
                RequestTimestamp = DateTime.Now,
                RequestUri = request.RequestUri.ToString(),
                RequestRouteTemplate = routeData?.Route.RouteTemplate
            };
        }
    }
}