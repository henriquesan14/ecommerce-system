using ECommerceSystem.Domain.Entities;
using ECommerceSystem.Domain.Interfaces.Repositories;

namespace ECommerceSystem.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(ECommerceSystemContext dbContext) : base(dbContext)
        {
        }
    }
}
