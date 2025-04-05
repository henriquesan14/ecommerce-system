using ECommerceSystem.Application.Extensions;
using ECommerceSystem.Application.Interfaces.Repositories;
using ECommerceSystem.Application.ViewModels;
using ECommerceSystem.Domain.Entities;
using ECommerceSystem.Domain.ValueObjects;
using ECommerceSystem.Shared.CQRS;

namespace ECommerceSystem.Application.Commands.CreateProduct
{
    internal class CreateProductCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<CreateProductCommand, ProductViewModel>
    {
        public async Task<ProductViewModel> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = Product.Create(
                ProductId.Of(Guid.NewGuid()),
                request.Name,
                request.Price
                );

            await unitOfWork.Products.AddAsync( entity );
            await unitOfWork.CompleteAsync();

            return entity.ToProductDto();
        }
    }
}
