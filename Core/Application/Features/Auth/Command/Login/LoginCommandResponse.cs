using MediatR;

namespace Application.Features.Auth.Command.Login
{
    public class LoginCommandResponse 
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime TokenExpiration { get; set; }
    }
}
