using Application.Bases;

namespace Application.Features.Auth.Exceptions
{
    public class UserAlreadyExistException : BaseException
    {
        public UserAlreadyExistException() : base("This user already registered") { }
    }
}
