using Application.Abstracts.AutoMapper;
using Application.Abstracts.Tokens;
using Application.Abstracts.UoW;
using Application.Bases;
using Application.Features.Auth.Rules;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace Application.Features.Auth.Command.Login
{
    public class LoginCommandHandler : BaseHandler, IRequestHandler<LoginCommandRequest, LoginCommandResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly AuthRules _authRules;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public LoginCommandHandler(UserManager<User> userManager, AuthRules authRules, ITokenService tokenService, IConfiguration configuration, IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
            _userManager = userManager;
            _authRules = authRules;
            _tokenService = tokenService;
            _.configuration = configuration;
        }

        public async Task<LoginCommandResponse> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
            User user = await _userManager.FindByEmailAsync(request.Email);
            bool checkPassword = await _userManager.CheckPasswordAsync(user, request.Password);

            await _authRules.EmailOrPasswordNotValid(user, checkPassword);

            IList<string> roles = await _userManager.GetRolesAsync(user);
            JwtSecurityToken token = await _tokenService.CreateToken(user, roles);
            string refreshToken = _tokenService.GenerateRefreshToken();

            _ = int.TryParse(_configuration["JWT:RefreshTokenValidatyInDays"], out int refreshTokenValidatyInDays);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidatyInDays);

            await _userManager.UpdateAsync(user);
            await _userManager.UpdateSecurityStampAsync(user);

            string convertedToken = new JwtSecurityTokenHandler().WriteToken(token);

            await _userManager.SetAuthenticationTokenAsync(user, "Default", "AccesToken", convertedToken);

            return new()
            {
                Token = convertedToken,
                RefreshToken = refreshToken,
                TokenExpiration = token.ValidTo
            };
        }
    }
}
