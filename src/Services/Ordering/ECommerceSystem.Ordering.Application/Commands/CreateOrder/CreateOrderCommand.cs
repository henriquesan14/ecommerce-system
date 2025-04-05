using ECommerceSystem.Application.InputModels;
using ECommerceSystem.Application.ViewModels;
using ECommerceSystem.Shared.CQRS;

namespace ECommerceSystem.Application.Commands.CreateOrder
{
    public record CreateOrderCommand(Guid CustomerId, AddressInputModel ShippingAddress,
        PaymentInputModel Payment,
        List<OrderItemInputModel> Items) : ICommand<OrderViewModel>
    {
    }
}
