using Application.Bases;
using FluentValidation;

namespace Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommandRequest>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty()
                .WithMessage(BaseUiMessages.NOT_EMPTY_MESSAGE)
                .GreaterThan(0)
                .WithMessage(BaseUiMessages.GREATER_THAN_0);
        }
    }
}
