using ECommerceSystem.Shared.Exceptions;

namespace ECommerceSystem.Domain.Exceptions
{
    public class OrderNotFoundException : NotFoundException
    {
        public OrderNotFoundException(int id) : base("Order", id) { }
    }
}
