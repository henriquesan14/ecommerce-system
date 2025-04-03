namespace ECommerceSystem.Application.InputModels
{
    public record OrderItemInputModel(Guid ProductId, int Quantity, decimal Price);
}
