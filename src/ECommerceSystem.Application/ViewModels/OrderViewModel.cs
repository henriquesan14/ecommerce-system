using ECommerceSystem.Domain.Enums;

namespace ECommerceSystem.Application.ViewModels
{
    public record OrderViewModel(int Id, OrderStatusEnum Status, decimal Total, List<OrderItemViewModel> Itens, int CustomerId)
    {
    }
}
