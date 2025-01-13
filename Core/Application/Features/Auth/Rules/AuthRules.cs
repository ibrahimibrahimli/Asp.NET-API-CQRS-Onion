using Application.Bases;
using Domain.Entities.Identity;
using Application.Features.Auth.Exceptions;

namespace Application.Features.Auth.Rules
{
    public class AuthRules : BaseRules
    {
        internal Task UserShouldNotBeExist(User? user)
        {
            if (user is not null) throw new UserAlreadyExistException();
            return Task.CompletedTask;
        }

        internal Task EmailOrPasswordNotValid(User? user, bool checkPassword)
        {
            if (user is null || checkPassword) throw new EmailOrPasswordNotValidException();
            return Task.CompletedTask;
        }

        internal Task RefreshTokenExpiryTime(DateTime? expiredTime)
        {
            if (expiredTime <= DateTime.Now) throw new RefreshTokenExpiryTimeException();
                return Task.CompletedTask;
        }

        internal Task EmailAdressNotValid(User? user)
        {
            if (user is null) throw new EmailAdressNotValidException();
            return Task.CompletedTask;
        }
    }
}
