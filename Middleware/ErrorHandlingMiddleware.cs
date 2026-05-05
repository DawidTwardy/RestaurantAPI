
using RestaurantAPI.Exceptions;

namespace RestaurantAPI.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            Logger = logger;
        }

        public  readonly ILogger<ErrorHandlingMiddleware> Logger;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (NotFoundException notFoundExpencion)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFoundExpencion.Message);
            }
             catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                 context.Response.StatusCode = 500;
                context.Response.WriteAsync("Something went wrong");
            }
        }
    }
}
