using ECommerceSystem.Domain.Entities;
using ECommerceSystem.Shared.Base;

namespace ECommerceSystem.Domain.Interfaces.Repositories
{
    public interface IOrderItemRepository : IAsyncRepository<OrderItem>
    {
    }
}
