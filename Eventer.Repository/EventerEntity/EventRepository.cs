using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Eventer.Dal.Context;
using Eventer.Domain.Entity.EventerEntity;
using Eventer.Repository.Common;
using Eventer.Repository.EventerEntity.Interface;

namespace Eventer.Repository.EventerEntity
{
    public class EventRepository : RepositoryBase<Event>, IEventRepository
    {
        public EventRepository(EventerDbContext context) : base(context)
        {
        }

        public override async Task Update(Event entity)
        {
            var actualEvent = Find(entity.Id);
            var actualCategories = actualEvent.Categories.ToList();
            var deletedCategories = actualCategories.Except(entity.Categories).ToList();
            var addedCategories = entity.Categories.Except(actualCategories).ToList();

            foreach (var deletedCategory in deletedCategories)
            {
                actualEvent.Categories.Remove(deletedCategory);
            }

            foreach (var addedCategory in addedCategories)
            {
                if (Context.Entry(addedCategory).State == EntityState.Detached)
                    Context.Category.Attach(addedCategory);
                actualEvent.Categories.Add(addedCategory);
            }

            Context.SaveChanges();
            var local = Context.Set<Event>()
                .Local
                .FirstOrDefault(x => x.Id == entity.Id);

            if (local != null)
            {
                Context.Entry(local).State = EntityState.Detached;
            }
            await base.Update(entity);
        }

        public override async Task RemoveAsync(Event entity)
        {
            var local = Context.Set<Event>()
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