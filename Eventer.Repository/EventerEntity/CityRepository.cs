using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Eventer.Dal.Context;
using Eventer.Domain.Entity.EventerEntity;
using Eventer.Repository.Common;
using Eventer.Repository.EventerEntity.Interface;

namespace Eventer.Repository.EventerEntity
{
    public class CityRepository : RepositoryBase<City>, ICityRepository
    {
        public CityRepository(EventerDbContext context) : base(context)
        {
        }

        public override async Task Update(City entity)
        {
            var local = Context.Set<City>()
                .Local
                .FirstOrDefault(x => x.Id == entity.Id);

            if (local != null)
            {
                Context.Entry(local).State = EntityState.Detached;
            }
            await base.Update(entity);
        }

        public override async Task RemoveAsync(City entity)
        {
            var local = Context.Set<City>()
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