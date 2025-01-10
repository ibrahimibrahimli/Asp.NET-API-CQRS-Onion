using Application.Bases;
using FluentValidation;

namespace Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommandRequest>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty()
                .WithMessage(BaseUiMessages.NOT_EMPTY_MESSAGE)
                .MinimumLength(3)
                .WithMessage(BaseUiMessages.MINIMUM_3_SYMBOL_MESSAGE)
                .MaximumLength(100)
                .WithMessage(BaseUiMessages.MAXIMUM_100_SYMBOL_MESSAGE);

            RuleFor(p => p.Description)
                .NotEmpty()
                .WithMessage(BaseUiMessages.NOT_EMPTY_MESSAGE)
                .MinimumLength(3)
                .WithMessage(BaseUiMessages.MINIMUM_3_SYMBOL_MESSAGE)
                .MaximumLength(1000)
                .WithMessage(BaseUiMessages.MAXIMUM_1000_SYMBOL_MESSAGE);

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
