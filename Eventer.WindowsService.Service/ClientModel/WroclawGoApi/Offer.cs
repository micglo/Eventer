using System.Collections.Generic;
using Newtonsoft.Json;

namespace Eventer.WindowsService.Service.ClientModel.WroclawGoApi
{
    public class Offer
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("longDescription")]
        public string LongDescription { get; set; }

        [JsonProperty("pageLink")]
        public string PageLink { get; set; }

        [JsonProperty("type")]
        public Type Type { get; set; }

        [JsonProperty("categories")]
        public List<Category> Categories { get; set; }

        [JsonProperty("mainImage")]
        public MainImage MainImage { get; set; }
    }
}