using MediatR;

namespace ECommerceSystem.Shared.CQRS
{
    public interface ICommand : IRequest<Unit>
    {

    }
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
