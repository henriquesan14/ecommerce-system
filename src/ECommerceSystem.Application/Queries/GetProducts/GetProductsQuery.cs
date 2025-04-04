using ECommerceSystem.Application.ViewModels;
using ECommerceSystem.Shared.CQRS;

namespace ECommerceSystem.Application.Queries.GetProducts
{
    public record GetProductsQuery : IQuery<IEnumerable<ProductViewModel>>
    {
    }
}
