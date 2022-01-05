using Microsoft.AspNetCore.Http;

namespace SignalR.Middleware;

public class WebSocketsAuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public WebSocketsAuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        var request = httpContext.Request;
        
        if (request.Path.StartsWithSegments("/hub", StringComparison.OrdinalIgnoreCase) &&
            request.Query.TryGetValue("access_token", out var accessToken))
        {
            request.Headers.Add("Authorization", $"Bearer {accessToken}");
        }
        
        await _next(httpContext);
    }
}