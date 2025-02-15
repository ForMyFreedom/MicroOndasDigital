using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Threading.Tasks;
using API.Domain;

namespace API.Middleware
{
    public class CustomBadRequestMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomBadRequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);
            
            if (context.Response.StatusCode == StatusCodes.Status400BadRequest)
            {
                if (!context.Response.HasStarted)
                {
                    SystemMessageBase message = new(new SystemError("Requisição mal formada!"));
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsJsonAsync(message);
                }
            }
        }
    }
}