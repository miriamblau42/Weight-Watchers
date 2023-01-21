using System.Net;


namespace CoronaApp.Api.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class HandlerErrorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HandlerErrorMiddleware> _ilogger;
        public HandlerErrorMiddleware(RequestDelegate next, ILogger<HandlerErrorMiddleware> ilogger)
        {
            _ilogger = ilogger;
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
                if (httpContext.Response.StatusCode >= 400 && httpContext.Response.StatusCode < 500)
                {
                //    throw new KeyNotFoundException();
                }
            }
            catch (Exception ex)
            {
                var response = httpContext.Response;
                response.ContentType = "application/json";
                switch (ex)
                {
                    case ArgumentNullException e:
                      //  await response.WriteAsync(e.Message+" contains null");
                        response.StatusCode = 404;
                       
                        break;

                    case KeyNotFoundException e:
                        await response.WriteAsync(" page not found");
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                _ilogger.Log(LogLevel.Error, ex.Message);
                //httpContext.Response.StatusCode = 500   ;
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class HandleErrorMiddlewareExtensions
    {
        public static IApplicationBuilder UseHandleErrorMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HandlerErrorMiddleware>();
        }
    }
}
