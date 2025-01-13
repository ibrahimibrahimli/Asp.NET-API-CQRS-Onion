using Application.Bases;

namespace Application.Features.Auth.Exceptions
{
    public class EmailAdressNotValidException : BaseException
    {
        public EmailAdressNotValidException()  : base(BaseUiMessages.EXCEPTION_EMAIL_NOT_VALID){ }
    }
}
