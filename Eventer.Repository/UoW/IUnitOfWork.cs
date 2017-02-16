using System.Threading.Tasks;
using Eventer.Repository.ApiActivity.Interface;
using Eventer.Repository.Client.Interface;
using Eventer.Repository.ErrorLog.Interface;
using Eventer.Repository.EventerEntity.Interface;
using Eventer.Repository.RefreshToken.Interface;

namespace Eventer.Repository.UoW
{
    public interface IUnitOfWork
    {
        IApiActivityLogRepository ActivityLogRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        ICityRepository CityRepository { get; }
        IClientRepository ClientRepository { get; }
        IErrorLogRepository ErrorLogRepository { get; }
        IEventRepository EventRepository { get; }
        IRefreshTokenRepository RefreshTokenRepository { get; }
        IStateRepository StateRepository { get; }

        Task<int> SaveChangesAsync();
    }
}