using ECommerceSystem.Domain.Entities;
using ECommerceSystem.Domain.Interfaces.Repositories;
using ECommerceSystem.Domain.ValueObjects;

namespace ECommerceSystem.Infrastructure.Persistence.Repositories
{
    public class OrderItemRepository : RepositoryBase<OrderItem, OrderItemId>, IOrderItemRepository
    {
        public OrderItemRepository(ECommerceSystemContext dbContext) : base(dbContext)
        {
        }
    }
}
