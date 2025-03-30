using ECommerceSystem.Domain.Enums;
using ECommerceSystem.Shared.Base;

namespace ECommerceSystem.Domain.Entities
{
    public class Order : Entity
    {
        public OrderStatusEnum Status { get; private set; }
        public decimal Total { get; private set; }
        public List<OrderItem> Itens { get; private set; }
        public int CustomerId { get; private set; }

        private Order()
        {
            Itens = new List<OrderItem>();
        }

        public Order(List<OrderItem> itens, int customerId)
        {
            Itens = itens;
            CustomerId = customerId;
            CreatedAt = DateTime.UtcNow;
            Status = OrderStatusEnum.PENDING;
            Total = CalculateTotal();
        }

        // Métodos de domínio
        public void Confirm()
        {
            if (Status == OrderStatusEnum.CANCELED)
                throw new InvalidOperationException("Não é possível confirmar um pedido cancelado.");

            Status = OrderStatusEnum.CONFIRMED;
        }

        public void Cancel()
        {
            if (Status == OrderStatusEnum.CONFIRMED)
                throw new InvalidOperationException("Não é possível cancelar um pedido confirmado.");

            Status = OrderStatusEnum.CANCELED;
        }

        public void AdicionarItem(OrderItem item)
        {
            Itens.Add(item);
            Total = CalculateTotal();
        }

        public void RemoverItem(OrderItem item)
        {
            Itens.Remove(item);
            Total = CalculateTotal();
        }

        private decimal CalculateTotal()
        {
            return Itens.Sum(i => i.Price * i.Quantity);
        }
    }
}
