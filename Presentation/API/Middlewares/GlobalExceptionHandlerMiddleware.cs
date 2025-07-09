using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace FitCircleAPI.Middlewares;

public class GlobalExceptionHandlerMiddleware(RequestDelegate next, 
    ILogger<GlobalExceptionHandlerMiddleware> logger,
    IHostEnvironment env)
{
    public async Task InvokeAsync(HttpContext context)
    {
		try
		{
			await next(context);
		}
		catch (Exception ex)
		{
			var correlationId = context.TraceIdentifier;

			var statusCode = ex switch
			{
				UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
				ArgumentException => (int)HttpStatusCode.BadRequest,
				KeyNotFoundException => (int)HttpStatusCode.NotFound,
				_ => (int)HttpStatusCode.InternalServerError
			};

			var title = statusCode switch
			{
				400 => "Bad Request",
				401 => "Unauthorized",
				403 => "Forbidden",
				404 => "Not Found",
				_ => "Internal Server Error",
			};

			var pd = new ProblemDetails
			{
				Type = $"https://httpstatuses.com/{statusCode}",
				Title = title,
				Status = statusCode,
				Instance = context.Request.Path,
				Detail = env.IsDevelopment() ? ex.ToString() : "An unexpected error occurred. Please contact support."
            };

			pd.Extensions["correlationId"] = correlationId;
			pd.Extensions["timeStamp"] = DateTime.UtcNow;

			using (logger.BeginScope(new Dictionary<string, object>
			{
				["CorrelationId"] = correlationId,
				["User"] = context.User?.Identity?.Name ?? "Anonymous",
				["Method"] = context.Request.Method,
				["Path"] = context.Request.Path,
				["Query"] = context.Request.QueryString.ToString(),
				["StatusCode"] = statusCode
			}))
			{
				logger.LogCritical(ex, "Unhandled exception occurred");
			}

			context.Response.StatusCode = statusCode;	
			context.Response.ContentType = "application/problem+json";

			var options = new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
				WriteIndented = env.IsDevelopment()
			};

			var json = JsonSerializer.Serialize(pd, options);
			await context.Response.WriteAsync(json);
		}
    }
}
