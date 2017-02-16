using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Eventer.Dal.Context;
using Eventer.Repository.Client.Interface;
using Eventer.Repository.Common;

namespace Eventer.Repository.Client
{
    public class ClientRepository : RepositoryBase<Domain.Entity.Client.Client>, IClientRepository
    {
        public ClientRepository(EventerDbContext context) : base(context)
        {
        }

        public override Domain.Entity.Client.Client Find(object id)
        {
            return DbSet.AsNoTracking().Single(x=>x.Id.Equals((string)id));
        }

        public bool Any(Expression<Func<Domain.Entity.Client.Client, bool>> filter) => DbSet.Any(filter);

        public override async Task Update(Domain.Entity.Client.Client entity)
        {
            var local = Context.Set<Domain.Entity.Client.Client>()
                .Local
                .FirstOrDefault(x => x.Id == entity.Id);

            if (local != null)
            {
                Context.Entry(local).State = EntityState.Detached;
            }
            await base.Update(entity);
        }

        public override async Task RemoveAsync(Domain.Entity.Client.Client entity)
        {
            var local = Context.Set<Domain.Entity.Client.Client>()
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