namespace ECommerceSystem.Application.InputModels
{
    public class OrderItemInputModel
    {
        public int ProdutoId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
