using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Eventer.Web.Api.Utility.CustomMessage
{
    public class TextPlainMessage : IHttpActionResult
    {
        private readonly string _value;
        private readonly HttpRequestMessage _request;

        public TextPlainMessage(string value, HttpRequestMessage request)
        {
            _value = value;
            _request = request;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(_value),
                RequestMessage = _request
            };
            return Task.FromResult(response);
        }
    }
}