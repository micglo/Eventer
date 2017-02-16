using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Eventer.WindowsService.Service.ClientModel.EventerApi
{
    public class EventPostItem
    {
        [JsonProperty("eventName")]
        public string EventName { get; set; }

        [JsonProperty("eventDate")]
        public DateTime? EventDate { get; set; }

        [JsonProperty("eventLocalization")]
        public string EventLocalization { get; set; }

        [JsonProperty("eventImage")]
        public string EventImage { get; set; }

        [JsonProperty("eventUrl")]
        public string EventUrl { get; set; }

        [JsonProperty("eventDescription")]
        public string EventDescription { get; set; }

        [JsonProperty("cityId")]
        public int CityId { get; set; }

        [JsonProperty("categories")]
        public List<string> Categories { get; set; }
    }
}