
using OnlineWallet.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace OnlineWallet.WebApi.Middlewares
{
    public sealed class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        public async static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode code;
            switch (exception)
            {
                case KeyNotFoundException
                or EntityNotFoundException
                or FileNotFoundException:
                    code = HttpStatusCode.NotFound;
                    break;
                case EntityAlreadyExistsException:
                    code = HttpStatusCode.Conflict;
                    break;
                case UnauthorizedAccessException:
                    code = HttpStatusCode.Unauthorized;
                    break;
                case ArgumentException
                or InvalidOperationException:
                    code = HttpStatusCode.BadRequest;
                    break;
                default:
                    code = HttpStatusCode.InternalServerError;
                    break;
            }

            var response = new
            {
                Status = code,
                Code = exception.GetType().Name,
                Message = exception.Message,
                Source = exception.Source,
            };

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
