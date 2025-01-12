using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity
{
    public class User : IdentityUser<Guid>
    {
        public string FullName { get; set; }
    }
}
