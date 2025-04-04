using ECommerceSystem.Domain.Entities;
using ECommerceSystem.Domain.ValueObjects;
using ECommerceSystem.Shared.Base;

namespace ECommerceSystem.Application.Interfaces.Repositories
{
    public interface IOrderItemRepository : IAsyncRepository<OrderItem, OrderItemId>
    {
    }
}
