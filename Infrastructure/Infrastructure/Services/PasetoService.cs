using Application.Common.Interfaces;
using Domain.Models;
using Paseto;
using Paseto.Builder;
using Paseto.Validators;
using System.Security.Claims;

namespace Infrustructure.Services;

public class PasetoService : IPasetoService
{
    readonly PasetoSettings _pasetoSettings;

    public PasetoService(PasetoSettings pasetoSettings)
    {
        _pasetoSettings = pasetoSettings;
    }

    public string GenerateToken(string userCode, string name, string fullName, string userRole)
    {
        return new PasetoBuilder()
            .UseV4(Purpose.Local)
            .WithKey(Convert.FromBase64String(_pasetoSettings.Secret), Encryption.SymmetricKey)
            .AddClaim(ClaimTypes.NameIdentifier, userCode)
            .AddClaim(ClaimTypes.Name, name)
            .AddClaim(ClaimTypes.GivenName, fullName)
            .AddClaim(ClaimTypes.Role, userRole)
            .Issuer(_pasetoSettings.Issuer)
            .Subject(Guid.NewGuid().ToString())
            .Audience(_pasetoSettings.Audience)
            .IssuedAt(DateTime.UtcNow)
            .Expiration(DateTime.UtcNow.AddMinutes(_pasetoSettings.AccessTokenExpirationMinutes))
            .TokenIdentifier(Guid.NewGuid().ToString())
            .AddFooter("env=.;key=v1;type=access")
            .Encode();
    }

    public ClaimsPrincipal ValidateToken(string token)
    {
        var valParams = new PasetoTokenValidationParameters
        {
            ValidateLifetime = true,
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidAudience = _pasetoSettings.Audience,
            ValidIssuer = _pasetoSettings.Issuer,
        };

        var payload = new PasetoBuilder()
        .UseV4(Purpose.Local)
        .WithKey(Convert.FromBase64String(_pasetoSettings.Secret), Encryption.SymmetricKey)
        .Decode(token, valParams);

        return payload.IsValid ? CreatePrincipalFromPasetoPayload(payload.Paseto.Payload) : null;
    }

    private ClaimsPrincipal CreatePrincipalFromPasetoPayload(Dictionary<string, object> payload)
    {
        var claims = new List<Claim>();

        foreach (var item in payload)
            claims.Add(new Claim(item.Key, item.Value.ToString()));

        var identity = new ClaimsIdentity(claims, "Paseto");
        var pricipal = new ClaimsPrincipal(identity);
        return pricipal;
    }
}
