using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Eventer.Dal.Context;
using Eventer.Domain.Entity.ApiActivity;
using Eventer.Repository.ApiActivity.Interface;
using Eventer.Repository.Common;

namespace Eventer.Repository.ApiActivity
{
    public class ApiActivityLogRepository : RepositoryBase<ApiActivityLog>, IApiActivityLogRepository
    {
        public ApiActivityLogRepository(EventerDbContext context) : base(context)
        {
        }

        public ApiActivityLog Add(ApiActivityLog entity)
        {
            var newEntity = DbSet.Add(entity);
            Context.SaveChanges();
            return newEntity;
        }

        public override async Task Update(ApiActivityLog entity)
        {
            var local = Context.Set<ApiActivityLog>()
                .Local
                .FirstOrDefault(x => x.Id == entity.Id);

            if (local != null)
            {
                Context.Entry(local).State = EntityState.Detached;
            }
            await base.Update(entity);
        }

        public override async Task RemoveAsync(ApiActivityLog entity)
        {
            var local = Context.Set<ApiActivityLog>()
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