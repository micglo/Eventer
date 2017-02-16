using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Eventer.Dal.Context;
using Eventer.Repository.Common;
using Eventer.Repository.ErrorLog.Interface;

namespace Eventer.Repository.ErrorLog
{
    public class ErrorLogRepository : RepositoryBase<Domain.Entity.ErrorLog.ErrorLog>, IErrorLogRepository
    {
        public ErrorLogRepository(EventerDbContext context) : base(context)
        {
        }

        public override async Task RemoveAsync(Domain.Entity.ErrorLog.ErrorLog entity)
        {
            var local = Context.Set<Domain.Entity.ErrorLog.ErrorLog>()
                .Local
                .FirstOrDefault(x => x.Id == entity.Id);

            if (local != null)
            {
                Context.Entry(local).State = EntityState.Detached;
            }
            await base.RemoveAsync(entity);
        }
    }
}