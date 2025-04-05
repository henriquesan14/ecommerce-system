using ECommerceSystem.Application.ViewModels;
using ECommerceSystem.Domain.Entities;

namespace ECommerceSystem.Application.Extensions
{
    public static class OrderExtensions
    {
        public static IEnumerable<OrderViewModel> ToOrderDtoList(this IEnumerable<Order> orders)
        {
            return orders.Select(order => DtoFromOrder(order!));
        }

        public static OrderViewModel ToOrderDto(this Order order)
        {
            return DtoFromOrder(order);
        }

        private static OrderViewModel DtoFromOrder(Order order)
        {
            return new OrderViewModel(
                Id: order.Id.Value,
                CustomerId: order.CustomerId.Value,
                ShippingAddress: new AddressViewModel(order.ShippingAddress.FirstName, order.ShippingAddress.LastName, order.ShippingAddress.EmailAddress!, order.ShippingAddress.AddressLine, order.ShippingAddress.Country, order.ShippingAddress.State, order.ShippingAddress.ZipCode),
                Payment: new PaymentViewModel(order.Payment.CardName!, order.Payment.CardNumber, order.Payment.Expiration, order.Payment.CVV, order.Payment.PaymentMethod),
                Status: order.Status,
                Total: order.Total,
                Itens: order.Items.Select(oi => new OrderItemViewModel(oi.OrderId.Value, oi.ProductId.Value, oi.Price, oi.Quantity)).ToList()
            );
        }
    }

}
