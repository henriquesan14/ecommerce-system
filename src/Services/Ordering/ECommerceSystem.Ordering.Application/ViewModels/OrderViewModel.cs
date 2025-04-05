using ECommerceSystem.Domain.Enums;

namespace ECommerceSystem.Application.ViewModels
{
    public record OrderViewModel(Guid Id,
        Guid CustomerId,
        AddressViewModel ShippingAddress,
        PaymentViewModel Payment,
        OrderStatusEnum Status,
        decimal Total,
        List<OrderItemViewModel> Itens)
    {
    }
}
