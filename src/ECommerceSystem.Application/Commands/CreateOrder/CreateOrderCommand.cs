using ECommerceSystem.Application.InputModels;
using ECommerceSystem.Application.ViewModels;
using ECommerceSystem.Domain.Enums;
using ECommerceSystem.Shared.CQRS;

namespace ECommerceSystem.Application.Commands.CreateOrder
{
    public record CreateOrderCommand(Guid CustomerId, AddressInputModel ShippingAddress,
        PaymentInputModel Payment,
        OrderStatusEnum Status,
        List<OrderItemInputModel> Items) : ICommand<OrderViewModel>
    {
    }
}
