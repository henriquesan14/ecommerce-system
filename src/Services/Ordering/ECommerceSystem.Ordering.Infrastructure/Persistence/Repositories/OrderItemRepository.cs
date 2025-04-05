using ECommerceSystem.Domain.Entities;
using ECommerceSystem.Application.Interfaces.Repositories;
using ECommerceSystem.Domain.ValueObjects;

namespace ECommerceSystem.Infrastructure.Persistence.Repositories
{
    public class OrderItemRepository : RepositoryBase<OrderItem, OrderItemId>, IOrderItemRepository
    {
        public OrderItemRepository(ECommerceSystemDbContext dbContext) : base(dbContext)
        {
        }
    }
}
