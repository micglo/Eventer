using System.Collections.Generic;
using Newtonsoft.Json;

namespace Eventer.WindowsService.Service.ClientModel.EventerApi
{
    public class CategoryPostList
    {
        [JsonProperty("categoryNames")]
        public List<string> Categories { get; set; }
    }
}