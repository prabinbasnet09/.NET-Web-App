using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ASP.NET_CORE.CustomMiddleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class NewCustomMiddleware
    {
        private readonly RequestDelegate _next;

        public NewCustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public  async Task Invoke(HttpContext httpContext)
        {
            // before logic
            if(httpContext.Request.Query.ContainsKey("firstName"))
            {
                await httpContext.Response.WriteAsync(httpContext.Request.Query["firstName"]);
            }
            await _next(httpContext);

        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseNewCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<NewCustomMiddleware>();
        }
    }
}
