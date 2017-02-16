using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Eventer.Dal.Context;
using Eventer.Domain.Entity.Common;

namespace Eventer.Repository.Common
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        protected readonly DbSet<TEntity> DbSet;
        protected readonly EventerDbContext Context;

        public RepositoryBase(EventerDbContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null, int? take = null) => await GetQueryable(null, orderBy, skip, take).ToListAsync();

        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null, int? take = null) => await GetQueryable(filter, orderBy, skip, take).ToListAsync();

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> filter) => await DbSet.SingleOrDefaultAsync(filter);

        public virtual TEntity Find(object id) => DbSet.Find(id);

        public async Task<TEntity> FindAsync(object id) => await DbSet.FindAsync(id);

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter) => await DbSet.AnyAsync(filter);

        public bool Any(Expression<Func<TEntity, bool>> filter) => DbSet.Any(filter);

        public async Task<long> Count(Expression<Func<TEntity, bool>> filter = null)
        {
            if(filter != null)
                return await DbSet.LongCountAsync(filter);
            return DbSet.Count();
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            var newEntity = DbSet.Add(entity);
            await Context.SaveChangesAsync();
            return newEntity;
        }

        public virtual async Task Update(TEntity entity)
        {
            var entry = Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            entry.State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        public virtual async Task RemoveAsync(TEntity entity)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }

            DbSet.Remove(entity);
            await Context.SaveChangesAsync();
        }

        private IQueryable<TEntity> GetQueryable(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        int? skip = null,
        int? take = null)
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue && take.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query;
        }
    }
}