using ECommerceSystem.Shared.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerceSystem.Infrastructure.Persistence.Repositories
{
    public class RepositoryBase<T> : IAsyncRepository<T> where T : Entity
    {
        protected readonly ECommerceSystemContext DbContext;

        public RepositoryBase(ECommerceSystemContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool disableTracking = true)
        {
            IQueryable<T> query = DbContext.Set<T>();
            if (disableTracking) query = query.AsNoTracking();
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await DbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
            IQueryable<T> query = DbContext.Set<T>();
            if (disableTracking) query = query.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null,
          bool disableTracking = true, int? pageNumber = null, int? pageSize = 20)
        {
            IQueryable<T> query = DbContext.Set<T>();
            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (pageNumber != null)
            {
                query = query.Skip((pageNumber.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }

            return await query.ToListAsync();
        }


        public async Task<T> GetByIdAsync(int id, bool disableTracking = false, List<Expression<Func<T, object>>> includes = null)
        {
            IQueryable<T> query = DbContext.Set<T>();

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, bool disableTracking = false, List<Expression<Func<T, object>>> includes = null)
        {
            IQueryable<T> query = DbContext.Set<T>();

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<T> AddAsync(T entity)
        {
            await DbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task AddRangeAsync(List<T> entity)
        {
            await DbContext.Set<T>().AddRangeAsync(entity);
        }

        public void UpdateAsync(T entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        public void DeleteAsync(T entity)
        {
            DbContext.Set<T>().Remove(entity);
        }

        public virtual async Task<int> GetCountAsync(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null
            ? await DbContext.Set<T>().CountAsync()
            : await DbContext.Set<T>().CountAsync(predicate);
        }
    }
}
