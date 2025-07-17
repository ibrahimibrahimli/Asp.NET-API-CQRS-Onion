using Application.Common.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;

namespace FitCircleAPI.Middlewares;

public class PasetoAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    IPasetoService pasetoService) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var accessToken = Request.Cookies["access_token"] ?? Response.Headers["PASETO-TOKEN"];
        Response.Headers.Remove("PASETO-TOKEN");

        if(string.IsNullOrWhiteSpace(accessToken))
            return  Task.FromResult(AuthenticateResult.Fail("Invalid token"));

        var principal = pasetoService.ValidateToken(accessToken);

        if(principal == null)
            return Task.FromResult(AuthenticateResult.Fail("Invalid token"));

        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}
