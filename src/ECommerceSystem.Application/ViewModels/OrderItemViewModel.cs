namespace ECommerceSystem.Application.ViewModels
{
    public record OrderItemViewModel(int Id, int ProdutoId, decimal Price, int Quantity)
    {
    }
}
