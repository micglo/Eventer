using System.Collections.Generic;
using Newtonsoft.Json;

namespace Eventer.WindowsService.Service.ClientModel.EventerApi
{
    public class EventItemPostList
    {
        [JsonProperty("events")]
        public List<EventPostItem> Events { get; set; }
    }
}