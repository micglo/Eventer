using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Eventer.Dal.Context;
using Eventer.Domain.Entity.EventerEntity;
using Eventer.Repository.Common;
using Eventer.Repository.EventerEntity.Interface;

namespace Eventer.Repository.EventerEntity
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(EventerDbContext context) : base(context)
        {
        }

        public override async Task Update(Category entity)
        {
            var local = Context.Set<Category>()
                .Local
                .FirstOrDefault(x => x.Id == entity.Id);

            if (local != null)
            {
                Context.Entry(local).State = EntityState.Detached;
            }
            await base.Update(entity);
        }

        public override async Task RemoveAsync(Category entity)
        {
            var local = Context.Set<Category>()
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