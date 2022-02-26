using Microsoft.AspNetCore.Builder;
using WebPortalServer.Middleware;

namespace WebPortalServer.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseDefaultData(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DefaultDataMiddleware>();
        }
    }
}
