using Application.Bases;

namespace Application.Features.Auth.Exceptions
{
    public class UserAlreadyExistException : BaseException
    {
        public UserAlreadyExistException() : base(BaseUiMessages.EXCEPTION_USER_ALREADY_EXISTS) { }
    }
}
