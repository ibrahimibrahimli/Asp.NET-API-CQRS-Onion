using Application.Bases;

namespace Application.Features.Auth.Exceptions
{
    public class EmailOrPasswordNotValidException : BaseException
    {
        public EmailOrPasswordNotValidException() : base(BaseUiMessages.EXCEPTION_EMAIL_OR_PASSWORD_NOT_VALID) { }
    }
}
