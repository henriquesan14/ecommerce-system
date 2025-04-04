using ECommerceSystem.Application.ViewModels;
using ECommerceSystem.Shared.CQRS;

namespace ECommerceSystem.Application.Commands.CreateProduct
{
    public record CreateProductCommand(string Name, decimal Price) : ICommand<ProductViewModel>
    {
    }
}
