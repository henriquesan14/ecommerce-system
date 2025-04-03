using ECommerceSystem.Application.Extensions;
using ECommerceSystem.Application.ViewModels;
using ECommerceSystem.Domain.Interfaces.Repositories;
using ECommerceSystem.Shared.Base;
using ECommerceSystem.Shared.CQRS;

namespace ECommerceSystem.Application.Queries.GetOrders
{
    internal class GetOrdersQueryHandler(IUnitOfWork _unitOfWork) : IQueryHandler<GetOrdersQuery, Result<IEnumerable<OrderViewModel>>>
    {
        public async Task<Result<IEnumerable<OrderViewModel>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var list = await _unitOfWork.Orders.GetAsync(includeString: "Items");

            var viewModel = list.ToOrderDtoList();
            return Result<IEnumerable<OrderViewModel>>.Success(viewModel);
        }
    }
}
