using Domain.Constants;
using FitCircleAPI.Middlewares;
using Microsoft.AspNetCore.Authentication;

namespace FitCircleAPI.Exsensions;


public static class AuthenticationServiceExtensions
{
    public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication("Paseto")
            .AddScheme<AuthenticationSchemeOptions, PasetoAuthenticationHandler>("Paseto", options => { });
    }

    public static void ConfigureAuthorization(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy("SuperAdmin", policy =>
                policy.RequireRole(Roles.SuperAdmin.ToString()))
            .AddPolicy("Admin", policy =>
                policy.RequireRole(Roles.Admin.ToString()))
            .AddPolicy("Manager", policy =>
                policy.RequireRole(Roles.Manager.ToString()))
            .AddPolicy("Trainer", policy =>
                policy.RequireRole(Roles.Trainer.ToString()))
            .AddPolicy("Member", policy =>
                policy.RequireRole(Roles.Member.ToString()))
            .AddPolicy("Guest", policy =>
                policy.RequireRole(Roles.Manager.ToString()));
    }
}
