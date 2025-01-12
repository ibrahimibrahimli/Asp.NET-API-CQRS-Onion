using Domain.Entities.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Application.Abstracts.Tokens
{
    public interface ITokenService
    {
        Task<JwtSecurityToken> CreateToken(User user, IList<string> roles);
        string GenerateRefreshToken();
        ClaimsPrincipal? GetPrincipialFromExpiredToken(string? token);
    }
}
