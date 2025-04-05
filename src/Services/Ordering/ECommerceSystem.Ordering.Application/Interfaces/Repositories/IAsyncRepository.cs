using ECommerceSystem.Shared.Base;
using System.Linq.Expressions;

namespace ECommerceSystem.Application.Interfaces
{
    public interface IAsyncRepository<TEntity, TId> where TEntity : IAggregate<TId>
    {
        Task<IReadOnlyList<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, bool disableTracking = true);

        Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);

        Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          string includeString = null,
          bool disableTracking = true);

        Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          List<Expression<Func<TEntity, object>>> includes = null,
          bool disableTracking = true,
          int? pageNumber = null, int? pageSize = 20);

        Task<TEntity> GetByIdAsync(TId id, bool disableTracking = false, List<Expression<Func<TEntity, object>>> includes = null);

        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, bool disableTracking = false, List<Expression<Func<TEntity, object>>> includes = null);

        Task<TEntity> AddAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task RemoveAsync(TEntity entity);
        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> predicate = null);
    }
}
