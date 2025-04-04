using ECommerceSystem.Application.ViewModels;
using ECommerceSystem.Domain.Entities;

namespace ECommerceSystem.Application.Extensions
{
    public static class ProductExtensions
    {
        public static IEnumerable<ProductViewModel> ToProductDtoList(this IEnumerable<Product> products)
        {
            return products.Select(product => DtoFromProduct(product!));
        }

        public static ProductViewModel ToProductDto(this Product order)
        {
            return DtoFromProduct(order);
        }

        private static ProductViewModel DtoFromProduct(Product product)
        {
            return new ProductViewModel(
                Id: product.Id.Value,
                Name: product.Name,
                Price: product.Price
            );
        }
    }
}
