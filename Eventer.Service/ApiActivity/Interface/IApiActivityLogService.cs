using System.Threading.Tasks;
using Eventer.Model.ApiPagination.Common;
using Eventer.Model.Dto.ApiActivity;
using Eventer.Service.Common;

namespace Eventer.Service.ApiActivity.Interface
{
    public interface IApiActivityLogService : IServiceBase<ApiActivityLogDto, ApiActivityLogDto, ApiActivityLogDto>
    {
        //ApiActivityLogDto Add(ApiActivityLogDto dtoModel);
        void Add(ApiActivityLogDto dtoModel);
        Task<PagedItems<ApiActivityLogDto>> GetAllByUserAsync(string userName, string skip, string take);
        Task<PagedItems<ApiActivityLogDto>> GetAllByHostAddressAsync(string hostAddress, string skip, string take);
        Task<long> GetCountByUserAsync(string userName);
        Task<long> GetCountByHostAddressAsync(string hostAddress);
        Task RemoveAllAsync();
    }
}