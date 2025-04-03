using ECommerceSystem.Application.ViewModels;
using ECommerceSystem.Shared.Base;
using ECommerceSystem.Shared.CQRS;

namespace ECommerceSystem.Application.Queries.GetOrders
{
    public class GetOrdersQuery : IQuery<Result<IEnumerable<OrderViewModel>>>
    {
    }
}
