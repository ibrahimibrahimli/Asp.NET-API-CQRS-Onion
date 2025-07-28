using Domain.Identity;

namespace Application.Common.Interfaces
{
    public interface IAuthentificationService
    {
        Task<(string accessToken, string refreshToken)> LoginAsync(string email, string password);
        Task<(string accessToken, string refreshToken)> RegisterAsync(ApplicationUser newUser, string password);
        Task<(string accessToken, string refreshToken)> RefreshTokenAsync(string accessToken, string refreshToken); 
    }
}
