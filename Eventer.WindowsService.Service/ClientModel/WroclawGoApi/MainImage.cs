using Newtonsoft.Json;

namespace Eventer.WindowsService.Service.ClientModel.WroclawGoApi
{
    public class MainImage
    {
        [JsonProperty("standard")]
        public string Standard { get; set; }
    }
}