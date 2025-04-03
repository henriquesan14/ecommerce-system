using ECommerceSystem.Domain.Entities;
using ECommerceSystem.Domain.ValueObjects;
using ECommerceSystem.Shared.Base;

namespace ECommerceSystem.Domain.Interfaces.Repositories
{
    public interface IOrderItemRepository : IAsyncRepository<OrderItem, OrderItemId>
    {
    }
}
