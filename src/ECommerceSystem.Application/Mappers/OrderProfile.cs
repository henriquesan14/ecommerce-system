using AutoMapper;
using ECommerceSystem.Application.Commands.CreateOrder;
using ECommerceSystem.Application.InputModels;
using ECommerceSystem.Application.ViewModels;
using ECommerceSystem.Domain.Entities;

namespace ECommerceSystem.Application.Mappers
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<CreateOrderCommand, Order>();
            CreateMap<OrderItemInputModel, OrderItem>();

            CreateMap<Order, OrderViewModel>();
            CreateMap<OrderItem, OrderItemViewModel>();
        }
    }
}
