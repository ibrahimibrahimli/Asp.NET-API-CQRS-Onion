using FitCircleAPI.Middlewares;

namespace FitCircleAPI.Exsensions;

public static class MiddlewareExtensions
{
    public static void UseApplicationMiddlewares(this WebApplication app)
    {
        app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
    }
}
