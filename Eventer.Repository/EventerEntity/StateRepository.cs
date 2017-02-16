using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Eventer.Dal.Context;
using Eventer.Domain.Entity.EventerEntity;
using Eventer.Repository.Common;
using Eventer.Repository.EventerEntity.Interface;

namespace Eventer.Repository.EventerEntity
{
    public class StateRepository : RepositoryBase<State>, IStateRepository
    {
        public StateRepository(EventerDbContext context) : base(context)
        {
        }

        public override async Task Update(State entity)
        {
            var local = Context.Set<State>()
                .Local
                .FirstOrDefault(x => x.Id == entity.Id);

            if (local != null)
            {
                Context.Entry(local).State = EntityState.Detached;
            }
            await base.Update(entity);
        }

        public override async Task RemoveAsync(State entity)
        {
            var local = Context.Set<State>()
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