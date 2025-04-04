using ECommerceSystem.Application.Extensions;
using ECommerceSystem.Application.ViewModels;
using ECommerceSystem.Domain.Entities;
using ECommerceSystem.Domain.Interfaces.Repositories;
using ECommerceSystem.Domain.ValueObjects;
using ECommerceSystem.EventBus.Events;
using ECommerceSystem.Shared.Base;
using ECommerceSystem.Shared.CQRS;
using MassTransit;

namespace ECommerceSystem.Application.Commands.CreateOrder
{
    internal class CreateOrderCommandHandler(IUnitOfWork _unitOfWork, IPublishEndpoint publishEndpoint) : ICommandHandler<CreateOrderCommand, Result<OrderViewModel>>
    {
        public async Task<Result<OrderViewModel>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var shippingAddress = Address.Of(
                firstName: request.ShippingAddress.FirstName,
                lastName: request.ShippingAddress.LastName,
                emailAddress: request.ShippingAddress.EmailAddress,
                addressLine: request.ShippingAddress.AddressLine,
                country: request.ShippingAddress.Country,
                state: request.ShippingAddress.State,
                zipCode: request.ShippingAddress.ZipCode
            );

            var payment = Payment.Of(
                cardName: request.Payment.CardName,
                cardNumber: request.Payment.CardNumber,
                expiration: request.Payment.Expiration,
                cvv: request.Payment.Cvv,
                paymentMethod: request.Payment.PaymentMethod
            );

            var entity = Order.Create(
                id: OrderId.Of(Guid.NewGuid()),
                customerId: CustomerId.Of(request.CustomerId),
                shippingAddress: shippingAddress,
                payment: payment
            );
            foreach (var orderItem in request.Items)
            {
                entity.AddItem(ProductId.Of(orderItem.ProductId), orderItem.Quantity, orderItem.Price);
            }

            await _unitOfWork.BeginTransaction();
            
            await _unitOfWork.Orders.AddAsync(entity);
            await _unitOfWork.CompleteAsync();

            var orderCreatedEvent = new OrderCreatedIntegrationEvent(entity.Id.Value, entity.Total);

            await publishEndpoint.Publish(orderCreatedEvent, cancellationToken);

            await _unitOfWork.CommitAsync();

            var viewModel = entity.ToOrderDto();
            return Result<OrderViewModel>.Success(viewModel);
        }
    }
}
