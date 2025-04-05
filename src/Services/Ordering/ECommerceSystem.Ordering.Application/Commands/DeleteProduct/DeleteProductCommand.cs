using ECommerceSystem.Shared.CQRS;
using MediatR;

namespace ECommerceSystem.Application.Commands.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : ICommand<Unit>
    {
    }
}
