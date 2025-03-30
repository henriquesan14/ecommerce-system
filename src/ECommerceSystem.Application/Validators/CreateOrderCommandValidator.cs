using ECommerceSystem.Application.Commands.CreateOrder;
using FluentValidation;

namespace ECommerceSystem.Application.Validators
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(c => c.CustomerId).NotEmpty().WithMessage("{PropertyName} é obrigatório");
            RuleFor(c => c.Itens).NotEmpty().WithMessage("{PropertyName} é obrigatório");
        }
    }
}
