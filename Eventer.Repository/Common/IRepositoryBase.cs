using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Eventer.Domain.Entity.Common;

namespace Eventer.Repository.Common
{
    public interface IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        Task<IEnumerable<TEntity>> GetAllAsync(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null, int? take = null);

        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null, int? take = null);

        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> filter);

        TEntity Find(object id);

        Task<TEntity> FindAsync(object id);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter);

        bool Any(Expression<Func<TEntity, bool>> filter);

        Task<long> Count(Expression<Func<TEntity, bool>> filter = null);

        Task<TEntity> AddAsync(TEntity entity);

        Task Update(TEntity entity);

        Task RemoveAsync(TEntity entity);
    }
}