using MediatR;

namespace Application.Features.Auth.Command.Register
{
    public class RegisterCommandRequest : IRequest<Unit>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
