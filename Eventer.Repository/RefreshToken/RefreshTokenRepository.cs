using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Eventer.Dal.Context;
using Eventer.Repository.Common;
using Eventer.Repository.RefreshToken.Interface;

namespace Eventer.Repository.RefreshToken
{
    public class RefreshTokenRepository : RepositoryBase<Domain.Entity.RefreshToken.RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(EventerDbContext context) : base(context)
        {
        }

        public override Domain.Entity.RefreshToken.RefreshToken Find(object id)
        {
            return DbSet.AsNoTracking().Single(x => x.Id.Equals((string)id));
        }

        public override async Task Update(Domain.Entity.RefreshToken.RefreshToken entity)
        {
            var local = Context.Set<Domain.Entity.RefreshToken.RefreshToken>()
                .Local
                .FirstOrDefault(x => x.Id == entity.Id);

            if (local != null)
            {
                Context.Entry(local).State = EntityState.Detached;
            }
            await base.Update(entity);
        }

        public override async Task RemoveAsync(Domain.Entity.RefreshToken.RefreshToken entity)
        {
            var local = Context.Set<Domain.Entity.RefreshToken.RefreshToken>()
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