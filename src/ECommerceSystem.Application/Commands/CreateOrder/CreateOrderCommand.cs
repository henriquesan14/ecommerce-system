using ECommerceSystem.Application.InputModels;
using ECommerceSystem.Application.ViewModels;
using ECommerceSystem.Shared.Base;
using ECommerceSystem.Shared.CQRS;

namespace ECommerceSystem.Application.Commands.CreateOrder
{
    public record CreateOrderCommand(List<OrderItemInputModel> Itens, int CustomerId) : ICommand<Result<OrderViewModel>>
    {
    }
}
