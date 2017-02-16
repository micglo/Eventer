using System.Collections.Generic;
using Newtonsoft.Json;

namespace Eventer.WindowsService.Service.ClientModel.EventerApi
{
    public class Category
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
        public List<CategoryItem> CategoryItems { get; set; }
    }

    public class CategoryItem
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("categoryName")]
        public string CategoryName { get; set; }
    }

    public class CategoryPostItem
    {
        [JsonProperty("categoryName")]
        public string CategoryName { get; set; }
    }
}