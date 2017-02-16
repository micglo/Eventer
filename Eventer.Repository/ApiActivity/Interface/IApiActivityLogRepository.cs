using Eventer.Domain.Entity.ApiActivity;
using Eventer.Repository.Common;

namespace Eventer.Repository.ApiActivity.Interface
{
    public interface IApiActivityLogRepository : IRepositoryBase<ApiActivityLog>
    {
        ApiActivityLog Add(ApiActivityLog entity);
    }
}