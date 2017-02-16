using System;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json.Serialization;
using Formatting = Newtonsoft.Json.Formatting;

namespace Eventer.Web.Api.Utility.Formatter
{
    public class BrowserJsonFormatter : JsonMediaTypeFormatter
    {
        public BrowserJsonFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            SupportedEncodings.Add(new UTF8Encoding(false));
            SerializerSettings.Formatting = Formatting.Indented;
            SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers,
            MediaTypeHeaderValue mediaType)
        {
            base.SetDefaultContentHeaders(type, headers, mediaType);
            headers.ContentType = new MediaTypeHeaderValue("application/json");
        }
    }
}