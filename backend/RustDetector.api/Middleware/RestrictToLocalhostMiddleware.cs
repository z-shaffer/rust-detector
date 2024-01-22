using System.Net;

namespace RustDetector.api.Middleware;

public class RestrictToLocalhostMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var remoteIp = context.Connection.RemoteIpAddress;
        if (remoteIp == null || !IPAddress.IsLoopback(remoteIp))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }

        await next(context);
    }
}