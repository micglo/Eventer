using System.Collections.Generic;
using Eventer.Model.Dto.Common;

namespace Eventer.Model.ApiPagination.Common
{
    public class PagedItems<T> where T : DtoBase
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public long TotalNumberOfPages { get; set; }
        public long TotalNumberOfRecords { get; set; }
        public string NextPageUrl { get; set; }
        public string PreviousPageUrl { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}