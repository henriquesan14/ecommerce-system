using ECommerceSystem.Application.Extensions;
using ECommerceSystem.Application.Interfaces.Repositories;
using ECommerceSystem.Application.ViewModels;
using ECommerceSystem.Shared.CQRS;

namespace ECommerceSystem.Application.Queries.GetOrders
{
    internal class GetOrdersQueryHandler(IUnitOfWork _unitOfWork) : IQueryHandler<GetOrdersQuery, IEnumerable<OrderViewModel>>
    {
        public async Task<IEnumerable<OrderViewModel>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var list = await _unitOfWork.Orders.GetAsync(includeString: "Items");

            return list.ToOrderDtoList();
        }
    }
}
