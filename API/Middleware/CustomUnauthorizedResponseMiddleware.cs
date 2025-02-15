using API.Domain;

namespace API.Middleware
{
    public class CustomUnauthorizedResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomUnauthorizedResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
            {
                SystemMessageBase message = new(new("Acesso não autorizado! Faça login"));
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(message);
            }
        }
    }
}
