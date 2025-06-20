using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrustructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private HttpContext httpContext => _httpContextAccessor?.HttpContext;
        private IHeaderDictionary Headers => httpContext?.Request?.Headers;

        public string GetCurrentUserId() => httpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        public string GetCurrentUsername() =>
              httpContext?.User.FindFirst(ClaimTypes.Name)?.Value;

        public string GetCurrentUserRole() =>
        httpContext?.User.FindFirst(ClaimTypes.Role)?.Value;

        public string GetCurrentUserFullName() =>
            httpContext?.User.FindFirst(ClaimTypes.GivenName)?.Value;
        public string GetUserAgent() =>
            Headers?["User-Agent"].ToString();

        public string GetFingerprint() =>
            Headers?["X-Fingerprint"].ToString();

        public string GetRemoteIpAddress() =>
            Headers?["X-Real-IP"].FirstOrDefault()?? httpContext?.Connection?.RemoteIpAddress?.ToString();

        public string GetRefreshToken() =>
            httpContext.Request.Cookies["refresh_token"];

        public string GetAccessToken() =>
            httpContext.Request.Cookies["access_token"];

    }
}
