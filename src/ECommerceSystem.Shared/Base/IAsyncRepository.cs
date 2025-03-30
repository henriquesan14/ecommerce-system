using System.Linq.Expressions;

namespace ECommerceSystem.Shared.Base
{
    public interface IAsyncRepository<T> where T : Entity
    {
        Task<IReadOnlyList<T>> GetAllAsync(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool disableTracking = true);

        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);

        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
          string includeString = null,
          bool disableTracking = true);

        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
          List<Expression<Func<T, object>>> includes = null,
          bool disableTracking = true,
          int? pageNumber = null, int? pageSize = 20);

        Task<T> GetByIdAsync(int id, bool disableTracking = false, List<Expression<Func<T, object>>> includes = null);

        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, bool disableTracking = false, List<Expression<Func<T, object>>> includes = null);

        Task<T> AddAsync(T entity);

        Task AddRangeAsync(List<T> entity);

        void UpdateAsync(T entity);

        void DeleteAsync(T entity);
        Task<int> GetCountAsync(Expression<Func<T, bool>> predicate = null);
    }
}
