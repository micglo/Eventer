using System.Threading.Tasks;
using Eventer.Dal.Context;
using Eventer.Repository.ApiActivity;
using Eventer.Repository.ApiActivity.Interface;
using Eventer.Repository.Client;
using Eventer.Repository.Client.Interface;
using Eventer.Repository.ErrorLog;
using Eventer.Repository.ErrorLog.Interface;
using Eventer.Repository.EventerEntity;
using Eventer.Repository.EventerEntity.Interface;
using Eventer.Repository.RefreshToken;
using Eventer.Repository.RefreshToken.Interface;

namespace Eventer.Repository.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EventerDbContext _context;

        public UnitOfWork()
        {
            _context = new EventerDbContext();
            ActivityLogRepository = new ApiActivityLogRepository(_context);
            CategoryRepository = new CategoryRepository(_context);
            CityRepository = new CityRepository(_context);
            ClientRepository = new ClientRepository(_context);
            ErrorLogRepository = new ErrorLogRepository(_context);
            EventRepository = new EventRepository(_context);
            RefreshTokenRepository = new RefreshTokenRepository(_context);
            StateRepository = new StateRepository(_context);
        }

        public IApiActivityLogRepository ActivityLogRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public ICityRepository CityRepository { get; }
        public IClientRepository ClientRepository { get; }
        public IErrorLogRepository ErrorLogRepository { get; }
        public IEventRepository EventRepository { get; }
        public IRefreshTokenRepository RefreshTokenRepository { get; }
        public IStateRepository StateRepository { get; }

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}