using ECommerceSystem.Shared.Base;

namespace ECommerceSystem.Domain.Entities
{
    public class OrderItem : Entity
    {
        public int ProdutoId { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }

        public OrderItem(int produtoId, string name, decimal price, int quantity)
        {
            ProdutoId = produtoId;
            Price = price;
            Quantity = quantity;
        }

        private OrderItem()
        {
        }

        public void SetQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("A quantidade deve ser maior que zero.");

            Quantity = quantity;
        }

    }
}
