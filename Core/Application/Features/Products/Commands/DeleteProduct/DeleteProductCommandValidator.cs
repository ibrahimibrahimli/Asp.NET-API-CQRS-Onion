using Application.BaseMessages;
using FluentValidation;

namespace Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommandRequest>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty()
                .WithMessage(UiMessages.NOT_EMPTY_MESSAGE)
                .GreaterThan(0)
                .WithMessage(UiMessages.GREATER_THAN_0);
        }
    }
}
