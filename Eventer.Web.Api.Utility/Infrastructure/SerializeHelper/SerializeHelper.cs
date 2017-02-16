using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using Microsoft.Owin;
using Newtonsoft.Json;

namespace Eventer.Web.Api.Utility.Infrastructure.SerializeHelper
{
    public static class SerializeHelper
    {
        public static string SerializeHeaders(HttpHeaders headers)
        {
            var dict = new Dictionary<string, string>();

            foreach (var item in headers.ToList())
            {
                if (item.Value != null)
                {
                    var header = String.Empty;
                    foreach (var value in item.Value)
                    {
                        header += value + " ";
                    }

                    header = header.TrimEnd(" ".ToCharArray());
                    dict.Add(item.Key, header);
                }
            }

            return JsonConvert.SerializeObject(dict, Formatting.Indented);
        }

        public static string SerializeHeaders(IHeaderDictionary headers)
        {
            var dict = new Dictionary<string, string>();

            foreach (var item in headers.ToList())
            {
                if (item.Value != null)
                {
                    var header = String.Empty;
                    foreach (var value in item.Value)
                    {
                        header += value + " ";
                    }

                    header = header.TrimEnd(" ".ToCharArray());
                    dict.Add(item.Key, header);
                }
            }

            return JsonConvert.SerializeObject(dict, Formatting.Indented);
        }
    }
}