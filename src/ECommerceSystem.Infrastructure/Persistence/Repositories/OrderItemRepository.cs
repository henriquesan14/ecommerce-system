using ECommerceSystem.Domain.Entities;
using ECommerceSystem.Domain.Interfaces.Repositories;

namespace ECommerceSystem.Infrastructure.Persistence.Repositories
{
    public class OrderItemRepository : RepositoryBase<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(ECommerceSystemContext dbContext) : base(dbContext)
        {
        }
    }
}
