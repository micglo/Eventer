using Newtonsoft.Json;

namespace Eventer.WindowsService.Service.ClientModel.EventerApi
{
    public class City
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("cityName")]
        public string CityName { get; set; }

        [JsonProperty("state")]
        public State State { get; set; }
    }
}