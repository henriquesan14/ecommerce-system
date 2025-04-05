using ECommerceSystem.Application.Extensions;
using ECommerceSystem.Application.Interfaces.Repositories;
using ECommerceSystem.Application.ViewModels;
using ECommerceSystem.Domain.Entities;
using ECommerceSystem.Domain.ValueObjects;
using Microsoft.Extensions.Caching.Distributed;
using System.Linq.Expressions;
using System.Text.Json;

namespace ECommerceSystem.Infrastructure.Persistence.Repositories
{
    public class CachedProductRepository(IProductRepository inner, IDistributedCache cache) : IProductRepository
    {
        private const string cacheKey = "products:all";

        public async Task<IReadOnlyList<Product>> GetAllAsync(
            Func<IQueryable<Product>, IOrderedQueryable<Product>> orderBy = null,
            bool disableTracking = true)
        {
            var cached = await cache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cached))
            {
                var dtos = JsonSerializer.Deserialize<List<ProductViewModel>>(cached)!;
                return dtos.Select(dto =>
                    Product.Create(ProductId.Of(dto.Id), dto.Name, dto.Price)
                ).ToList();
            }

            var products = await inner.GetAllAsync(orderBy, disableTracking);

            if (products is not null && products.Count > 0)
            {
                var toCache = products.Select(p => p.ToProductDto()).ToList();
                var serialized = JsonSerializer.Serialize(toCache);
                await cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(toCache), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                });
            }

            return products;
        }

        public async Task<Product> AddAsync(Product entity)
        {
            await cache.RemoveAsync(cacheKey);
            return await inner.AddAsync(entity);
        }


        public async Task RemoveAsync(Product entity)
        {
            await cache.RemoveAsync(cacheKey);
            await inner.RemoveAsync(entity);
        }

        public async Task UpdateAsync(Product entity)
        {
            await cache.RemoveAsync(cacheKey);
            await inner.UpdateAsync(entity);
        }

        public Task<IReadOnlyList<Product>> GetAsync(Expression<Func<Product, bool>> predicate)
            => inner.GetAsync(predicate);

        public Task<IReadOnlyList<Product>> GetAsync(Expression<Func<Product, bool>> predicate = null, Func<IQueryable<Product>, IOrderedQueryable<Product>> orderBy = null, string includeString = null, bool disableTracking = true)
            => inner.GetAsync(predicate, orderBy, includeString, disableTracking);

        public Task<IReadOnlyList<Product>> GetAsync(Expression<Func<Product, bool>> predicate = null, Func<IQueryable<Product>, IOrderedQueryable<Product>> orderBy = null, List<Expression<Func<Product, object>>> includes = null, bool disableTracking = true, int? pageNumber = null, int? pageSize = 20)
            => inner.GetAsync(predicate, orderBy, includes, disableTracking, pageNumber, pageSize);

        public Task<Product> GetByIdAsync(ProductId id, bool disableTracking = false, List<Expression<Func<Product, object>>> includes = null)
            => inner.GetByIdAsync(id, disableTracking, includes);

        public Task<int> GetCountAsync(Expression<Func<Product, bool>> predicate = null)
            => inner.GetCountAsync(predicate);

        public Task<Product> GetSingleAsync(Expression<Func<Product, bool>> predicate, bool disableTracking = false, List<Expression<Func<Product, object>>> includes = null)
            => inner.GetSingleAsync(predicate, disableTracking, includes);
    }
}
