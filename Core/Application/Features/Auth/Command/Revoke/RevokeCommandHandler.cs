using Application.Abstracts.AutoMapper;
using Application.Abstracts.UoW;
using Application.Bases;
using Application.Features.Auth.Rules;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Auth.Command.Revoke
{
    public class RevokeCommandHandler :BaseHandler, IRequestHandler<RevokeCommandRequest, Unit>
    {
        private readonly UserManager<User> _userManager;
        private readonly AuthRules _authRules;
        public RevokeCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, AuthRules authRules) : base(mapper, unitOfWork, httpContextAccessor)
        {
            _userManager = userManager;
            _authRules = authRules;
        }
        public async Task<Unit> Handle(RevokeCommandRequest request, CancellationToken cancellationToken)
        {
            User user = await _userManager.FindByEmailAsync(request.Email);
             _authRules.EmailAdressNotValid(user);

            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);

            return Unit.Value;
        }
    }
}
