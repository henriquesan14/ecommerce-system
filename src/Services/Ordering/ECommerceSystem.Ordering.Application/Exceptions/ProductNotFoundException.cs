using ECommerceSystem.Shared.Exceptions;

namespace ECommerceSystem.Application.Exceptions
{
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(Guid id) : base("Product", id)
        {
        }
    }
}
