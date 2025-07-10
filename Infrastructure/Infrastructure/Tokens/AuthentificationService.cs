using Application.Common.Interfaces;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistance.Context;
using System.Security.Claims;

namespace Infrustructure.Services
{
    public class AuthentificationService : IAuthentificationService
    {
        readonly UserManager<ApplicationUser> _userManager;
        readonly RoleManager<ApplicationRole> _roleManager;
        readonly ITokenService _tokenService;
        readonly FitCircleDbContext _dbContext;

        public AuthentificationService(UserManager<ApplicationUser> userManager,
                                       RoleManager<ApplicationRole> roleManager,
                                       ITokenService tokenService,
                                       FitCircleDbContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _dbContext = dbContext; 
        }
        public async Task<(string accessToken, string refreshToken)> RegisterAsync(ApplicationUser newUser, string password)
        {
            var existingUser = await _userManager.FindByEmailAsync(newUser.Email);
            if (existingUser is not null)
                throw new Exception("This email is already registered");

            var result = await _userManager.CreateAsync(newUser, password);
            if (!result.Succeeded)
                throw new Exception($"Failed to create user: {string.Join(",",result.Errors.Select(e => e.Description))}");

            await _userManager.AddToRoleAsync(newUser, "User");
            return await GenerateTokenAsync(newUser);
        }

        public async Task<(string accessToken, string refreshToken)> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null || !await _userManager.CheckPasswordAsync(user, password))
                throw new UnauthorizedAccessException("Invalid Credentials");

            return await GenerateTokenAsync(user);
        }

        public async Task<(string accessToken, string refreshToken)> RefreshTokenAsync(string accessToken, string refreshToken)
        {
            var principial = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            if (principial is null)
                throw new SecurityTokenException("Invalid access token");

            var userId = principial.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
                throw new SecurityTokenException("User ID not found in token.");

            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                throw new SecurityTokenException("User not found");

            var storedToken = await _dbContext.RefreshTokens
                .Where(x => x.UserId == userId && x.Token == refreshToken && x.RevokedAt == null)
                .FirstOrDefaultAsync();
            if (storedToken is null || storedToken.ExpiresAt < DateTime.UtcNow)
                throw new SecurityTokenException("RefreshToken is invalid or expired");

            storedToken.RevokedAt = DateTime.UtcNow;

            return await GenerateTokenAsync(user);

        }

        async Task<(string accessToken, string refreshToken)> GenerateTokenAsync(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var accessToken = _tokenService.GenerateAccessToken(user, roles);
            var refreshToken = _tokenService.GenerateRefreshToken();


            await _dbContext.RefreshTokens.AddAsync(new RefreshToken
            {
                UserId = user.Id,
                Token = refreshToken,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
            });

            await _dbContext.SaveChangesAsync();

            return (accessToken, refreshToken);
        }
    }
}
