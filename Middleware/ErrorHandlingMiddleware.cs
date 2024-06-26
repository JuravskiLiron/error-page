using Newtonsoft.Json;
using System.Net;

namespace Errors.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpcontex)
        {
            try
            {
                await _next(httpcontex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpcontex, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new {message = ex.Message, details = ex.StackTrace};

            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }

    }
}
