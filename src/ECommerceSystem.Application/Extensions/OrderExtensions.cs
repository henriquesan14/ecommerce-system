using ECommerceSystem.Application.ViewModels;
using ECommerceSystem.Domain.Entities;

namespace ECommerceSystem.Application.Extensions
{
    public static class OrderExtensions
    {
        public static IEnumerable<OrderViewModel> ToOrderDtoList(this IEnumerable<Order> orders)
        {
            return orders.Select(order => new OrderViewModel(
                Id: order.Id.Value,
                CustomerId: order.CustomerId,
                Status: order.Status,
                Total: order.Total,
                Itens: order.Items.Select(oi => new OrderItemViewModel(oi.OrderId.Value, oi.ProductId, oi.Price, oi.Quantity)).ToList()
            ));
        }

        public static OrderViewModel ToOrderDto(this Order order)
        {
            return DtoFromOrder(order);
        }

        private static OrderViewModel DtoFromOrder(Order order)
        {
            return new OrderViewModel(
                Id: order.Id.Value,
                CustomerId: order.CustomerId,
                Status: order.Status,
                Total: order.Total,
                Itens: order.Items.Select(oi => new OrderItemViewModel(oi.OrderId.Value, oi.ProductId, oi.Price, oi.Quantity)).ToList()
            );
        }
    }

}
