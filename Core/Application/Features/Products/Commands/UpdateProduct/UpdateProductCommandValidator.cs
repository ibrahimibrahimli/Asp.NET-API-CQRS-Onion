using Application.BaseMessages;
using FluentValidation;

namespace Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommandRequest>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty()
                .WithMessage(UiMessages.NOT_EMPTY_MESSAGE)
                .MinimumLength(3)
                .WithMessage(UiMessages.MINIMUM_3_SYMBOL_MESSAGE)
                .MaximumLength(100)
                .WithMessage(UiMessages.MAXIMUM_100_SYMBOL_MESSAGE);

            RuleFor(p => p.Description)
                .NotEmpty()
                .WithMessage(UiMessages.NOT_EMPTY_MESSAGE)
                .MinimumLength(3)
                .WithMessage(UiMessages.MINIMUM_3_SYMBOL_MESSAGE)
                .MaximumLength(1000)
                .WithMessage(UiMessages.MAXIMUM_1000_SYMBOL_MESSAGE);

            RuleFor(p => p.BrandId)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(p => p.Price)
                .NotEmpty()
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.Discount)
                .NotEmpty()
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.CategoryIds)
                .NotEmpty()
                .Must(categories => categories.Any());

        }
    }
}
