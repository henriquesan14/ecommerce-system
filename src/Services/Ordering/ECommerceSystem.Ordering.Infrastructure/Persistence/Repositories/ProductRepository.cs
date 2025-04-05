using ECommerceSystem.Domain.Entities;
using ECommerceSystem.Application.Interfaces.Repositories;
using ECommerceSystem.Domain.ValueObjects;

namespace ECommerceSystem.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : RepositoryBase<Product, ProductId>, IProductRepository
    {
        public ProductRepository(ECommerceSystemDbContext dbContext) : base(dbContext)
        {
        }
    }
}
