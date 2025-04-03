namespace ECommerceSystem.Application.InputModels
{
    public class OrderItemInputModel
    {
        public Guid ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
