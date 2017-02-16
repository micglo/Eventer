using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Eventer.WindowsService.Service.ClientModel.EventerApi
{
    public class Event
    {
        [JsonProperty("pageNumber")]
        public int PageNumber { get; set; }

        [JsonProperty("pageSize")]
        public int PageSize { get; set; }

        [JsonProperty("totalNumberOfPages")]
        public int TotalNumberOfPages { get; set; }

        [JsonProperty("totalNumberOfRecords")]
        public int TotalNumberOfRecords { get; set; }

        [JsonProperty("items")]
        public List<EventItem> EventItems { get; set; }
    }

    public class EventItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }

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

        [JsonProperty("city")]
        public City City { get; set; }

        [JsonProperty("categories")]
        public List<CategoryItem> Categories { get; set; }

        public int CityId { get; set; }
        public ICollection<int> CategoriesId { get; set; }
    }
}