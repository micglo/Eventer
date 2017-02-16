using Newtonsoft.Json;

namespace Eventer.WindowsService.Service.ClientModel.WroclawGoApi
{
    public class Type
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}