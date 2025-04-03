using ECommerceSystem.Domain.Entities;
using ECommerceSystem.Domain.Interfaces.Repositories;
using ECommerceSystem.Domain.ValueObjects;

namespace ECommerceSystem.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : RepositoryBase<Order, OrderId>, IOrderRepository
    {
        public OrderRepository(ECommerceSystemContext dbContext) : base(dbContext)
        {
        }
    }
}
