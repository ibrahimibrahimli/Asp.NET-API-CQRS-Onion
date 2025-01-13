using Application.Bases;

namespace Application.Features.Auth.Exceptions
{
    public class RefreshTokenExpiryTimeException : BaseException
    {
        public RefreshTokenExpiryTimeException()  : base(BaseUiMessages.EXCEPTION_REFRESH_TOKEN_EXPIRED){ }
    }
}
