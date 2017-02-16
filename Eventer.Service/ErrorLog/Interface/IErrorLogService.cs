using System.Threading.Tasks;
using Eventer.Model.ApiPagination.Common;
using Eventer.Model.Dto.ErrorLog;
using Eventer.Service.Common;

namespace Eventer.Service.ErrorLog.Interface
{
    public interface IErrorLogService : IServiceBase<ErrorLogDto, ErrorLogDto, ErrorLogDto>
    {
        Task<long> GetCountByUserAsync(string userName);
        Task<PagedItems<ErrorLogDto>> GetAllByUserAsync(string userName, string skip, string take);
        Task RemoveAllAsync();
    }
}