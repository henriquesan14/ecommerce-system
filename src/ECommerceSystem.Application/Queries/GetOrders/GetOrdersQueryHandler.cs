using AutoMapper;
using ECommerceSystem.Application.ViewModels;
using ECommerceSystem.Domain.Interfaces.Repositories;
using ECommerceSystem.Shared.Base;
using ECommerceSystem.Shared.CQRS;

namespace ECommerceSystem.Application.Queries.GetOrders
{
    internal class GetOrdersQueryHandler(IUnitOfWork _unitOfWork, IMapper _mapper) : IQueryHandler<GetOrdersQuery, Result<List<OrderViewModel>>>
    {
        public async Task<Result<List<OrderViewModel>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var list = await _unitOfWork.Orders.GetAllAsync();

            var viewModel = _mapper.Map<List<OrderViewModel>>(list);
            return Result<List<OrderViewModel>>.Success(viewModel);
        }
    }
}
