using ECommerceSystem.Application.Extensions;
using ECommerceSystem.Application.ViewModels;
using ECommerceSystem.Domain.Entities;
using ECommerceSystem.Domain.Interfaces.Repositories;
using ECommerceSystem.Domain.ValueObjects;
using ECommerceSystem.EventBus;
using ECommerceSystem.EventBus.Abstractions;
using ECommerceSystem.EventBus.Events;
using ECommerceSystem.Shared.Base;
using ECommerceSystem.Shared.CQRS;

namespace ECommerceSystem.Application.Commands.CreateOrder
{
    internal class CreateOrderCommandHandler(IUnitOfWork _unitOfWork, IEventBus _eventBus) : ICommandHandler<CreateOrderCommand, Result<OrderViewModel>>
    {
        public async Task<Result<OrderViewModel>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {

            var entity = Order.Create(
                id: OrderId.Of(Guid.NewGuid()),
                customerId: request.CustomerId
            );
            foreach (var orderItem in request.Itens)
            {
                entity.AddItem(orderItem.ProductId, orderItem.Quantity, orderItem.Price);
            }

            await _unitOfWork.BeginTransaction();
            
            await _unitOfWork.Orders.AddAsync(entity);
            await _unitOfWork.CompleteAsync();

            var orderCreatedEvent = new OrderCreatedEvent(entity.Id.Value, entity.Total);
            await _eventBus.PublishAsync(orderCreatedEvent, EventBusConstants.ORDER_CREATED_QUEUE);

            await _unitOfWork.CommitAsync();

            var viewModel = entity.ToOrderDto();
            return Result<OrderViewModel>.Success(viewModel);
        }
    }
}
