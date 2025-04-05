namespace ECommerceSystem.Application.ViewModels
{
    public record OrderItemViewModel(Guid Id, Guid ProductId, decimal Price, int Quantity)
    {
    }
}
