using Application.Bases;
using FluentValidation;

namespace Application.Features.Auth.Command.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommandRequest>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(BaseUiMessages.NOT_EMPTY_MESSAGE)
                .MinimumLength(3)
                .WithMessage(BaseUiMessages.MINIMUM_3_SYMBOL_MESSAGE)
                .MaximumLength(50)
                .WithMessage(BaseUiMessages.MAXIMUM_50_SYMBOL_MESSAGE);

            RuleFor(x => x.Surname)
                .NotEmpty()
                .WithMessage(BaseUiMessages.NOT_EMPTY_MESSAGE)
                .MinimumLength(3)
                .WithMessage(BaseUiMessages.MINIMUM_3_SYMBOL_MESSAGE)
                .MaximumLength(50)
                .WithMessage(BaseUiMessages.MAXIMUM_50_SYMBOL_MESSAGE);

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(BaseUiMessages.NOT_EMPTY_MESSAGE)
                .MaximumLength(100)
                .WithMessage(BaseUiMessages.MAXIMUM_100_SYMBOL_MESSAGE)
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.Net4xRegex)
                .WithMessage(BaseUiMessages.NOT_VALID_EMAIL);

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(BaseUiMessages.NOT_EMPTY_MESSAGE)
                .MinimumLength(6);

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .WithMessage(BaseUiMessages.NOT_EMPTY_MESSAGE)
                .Equal(x => x.Password);
        }
    }
}
