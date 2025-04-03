using ECommerceSystem.Domain.Enums;

namespace ECommerceSystem.Application.ViewModels
{
    public record OrderViewModel(Guid Id, OrderStatusEnum Status, decimal Total, List<OrderItemViewModel> Itens, Guid CustomerId)
    {
    }
}
