using Newtonsoft.Json;

namespace Eventer.WindowsService.Service.ClientModel.WroclawGoApi
{
    public class WroclawGoApiItem
    {
        [JsonProperty("startDate")]
        public string StartDate { get; set; }

        [JsonProperty("offer")]
        public Offer Offer { get; set; }

        [JsonProperty("placeName")]
        public string PlaceName { get; set; }
    }
}