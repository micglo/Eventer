using System.Collections.Generic;
using Newtonsoft.Json;

namespace Eventer.WindowsService.Service.ClientModel.WroclawGoApi
{
    public class WroclawGoApiModel
    {
        [JsonProperty("listSize")]
        public int ListSize { get; set; }

        [JsonProperty("pageSize")]
        public int PageSize { get; set; }

        [JsonProperty("items")]
        public List<WroclawGoApiItem> WroclawGoApiItems { get; set; }
    }
}