using Newtonsoft.Json;

namespace Eventer.WindowsService.Service.ClientModel.EventerApi
{
    public class State
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("stateName")]
        public string StateName { get; set; }
    }
}