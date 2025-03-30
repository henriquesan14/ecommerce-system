using AutoMapper;
using ECommerceSystem.Application.ViewModels;
using ECommerceSystem.Domain.Entities;
using ECommerceSystem.Domain.Interfaces.Repositories;
using ECommerceSystem.EventBus;
using ECommerceSystem.EventBus.Abstractions;
using ECommerceSystem.EventBus.Events;
using ECommerceSystem.Shared.Base;
using ECommerceSystem.Shared.CQRS;

namespace ECommerceSystem.Application.Commands.CreateOrder
{
    internal class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, Result<OrderViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventBus _eventBus;
        private readonly IMapper _mapper;

        public CreateOrderCommandHandler(IUnitOfWork unitOfWork, IEventBus eventBus, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _eventBus = eventBus;
            _mapper = mapper;
        }

        public async Task<Result<OrderViewModel>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Order>(request);

            // 2️⃣ Inicia transação
            await _unitOfWork.BeginTransaction();
            
            await _unitOfWork.Orders.AddAsync(entity);
            await _unitOfWork.CompleteAsync();

            // 3️⃣ Publicar evento APÓS salvar no banco
            var orderCreatedEvent = new OrderCreatedEvent(entity.Id, entity.Total);
            await _eventBus.PublishAsync(orderCreatedEvent, EventBusConstants.ORDER_CREATED_QUEUE);

            // 4️⃣ Commit na transação
            await _unitOfWork.CommitAsync();

            var viewModel =  _mapper.Map<OrderViewModel>(entity);
            return Result<OrderViewModel>.Success(viewModel);
        }
    }
}
