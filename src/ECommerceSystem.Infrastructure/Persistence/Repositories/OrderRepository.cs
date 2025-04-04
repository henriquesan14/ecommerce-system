using ECommerceSystem.Domain.Entities;
using ECommerceSystem.Application.Interfaces.Repositories;
using ECommerceSystem.Domain.ValueObjects;

namespace ECommerceSystem.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : RepositoryBase<Order, OrderId>, IOrderRepository
    {
        public OrderRepository(ECommerceSystemDbContext dbContext) : base(dbContext)
        {
        }
    }
}
