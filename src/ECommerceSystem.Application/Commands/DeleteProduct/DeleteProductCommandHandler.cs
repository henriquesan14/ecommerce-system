using ECommerceSystem.Application.Exceptions;
using ECommerceSystem.Application.Interfaces.Repositories;
using ECommerceSystem.Domain.ValueObjects;
using ECommerceSystem.Shared.CQRS;
using MediatR;

namespace ECommerceSystem.Application.Commands.DeleteProduct
{
    internal class DeleteProductCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<DeleteProductCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await unitOfWork.Products.GetByIdAsync(ProductId.Of(request.Id));

            if (product == null) throw new ProductNotFoundException(request.Id);

            await unitOfWork.Products.RemoveAsync(product);
            await unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}
