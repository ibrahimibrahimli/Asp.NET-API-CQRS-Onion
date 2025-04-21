using Application.Abstractions.Services.Authentification;

namespace Application.Abstractions.Services
{
    public interface IAuthService : IExternalAuthentification, IInternalAuthentification
    {
        Task ResetPasswordAsync(string email);
        Task<bool > VerifyResetTokenAsync(string resetToken, string userId);
    }
}
