using Newtonsoft.Json;

namespace Eventer.WindowsService.Service.ClientModel.WroclawGoApi
{
    public class Category
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}