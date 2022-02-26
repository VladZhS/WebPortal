using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using WebPortalServer.Services;

namespace WebPortalServer.Middleware
{
    public class DefaultDataMiddleware
    {
        private readonly RequestDelegate next;

        public DefaultDataMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, IDefaultDataService service)
        {
            service.EnsureDefaultData();
            await next.Invoke(context);
        }
    }
}
