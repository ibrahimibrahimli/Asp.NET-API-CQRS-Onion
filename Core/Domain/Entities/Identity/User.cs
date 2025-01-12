using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity
{
    public class User : IdentityUser<Guid>
    {
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
