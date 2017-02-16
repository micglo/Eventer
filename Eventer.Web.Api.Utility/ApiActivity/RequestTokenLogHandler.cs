using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Eventer.Model.Dto.ApiActivity;
using Eventer.Service.ApiActivity;
using Eventer.Service.ApiActivity.Interface;
using Eventer.Web.Api.Utility.Infrastructure.SerializeHelper;
using Microsoft.Owin;

namespace Eventer.Web.Api.Utility.ApiActivity
{
    public class RequestTokenLogHandler : OwinMiddleware
    {
        private readonly IApiActivityLogService _apiActivityLogService;

        public RequestTokenLogHandler(OwinMiddleware next) :
            base(next)
        {
            _apiActivityLogService = new ApiActivityLogService();
        }

        public override async Task Invoke(IOwinContext context)
        {
            await Task.Run(() => {
                string body = new StreamReader(context.Request.Body).ReadToEnd();

                if (!string.IsNullOrEmpty(body))
                {
                    var apiLogEntry = GetApiLogEntry(context);
                    apiLogEntry.RequestContentBody = body;

                    byte[] requestData = Encoding.UTF8.GetBytes(body);
                    context.Request.Body = new MemoryStream(requestData);

                    _apiActivityLogService.Add(apiLogEntry);
                }
            });
            await Next.Invoke(context);
        }

        private ApiActivityLogDto GetApiLogEntry(IOwinContext context)
        {
            return new ApiActivityLogDto
            {
                RequestContentType = context.Request.ContentType,
                UserHostAddress = context.Request.Host.Value,
                RequestMethod = context.Request.Method,
                RequestHeaders = SerializeHelper.SerializeHeaders(context.Request.Headers),
                RequestTimestamp = DateTime.Now,
                RequestUri = context.Request.Uri.ToString()
            };
        }
    }
}