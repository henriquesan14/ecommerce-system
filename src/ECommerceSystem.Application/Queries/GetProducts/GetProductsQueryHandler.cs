using ECommerceSystem.Application.Extensions;
using ECommerceSystem.Application.ViewModels;
using ECommerceSystem.Application.Interfaces.Repositories;
using ECommerceSystem.Shared.CQRS;

namespace ECommerceSystem.Application.Queries.GetProducts
{
    internal class GetProductsQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetProductsQuery, IEnumerable<ProductViewModel>>
    {
        public async Task<IEnumerable<ProductViewModel>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await unitOfWork.Products.GetAllAsync();

            return products.ToProductDtoList();
        }
    }
}
