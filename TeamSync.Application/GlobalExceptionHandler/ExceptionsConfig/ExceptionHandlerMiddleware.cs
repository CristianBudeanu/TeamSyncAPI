using TeamSync.Application.GlobalExceptionHandler.CustomExceptions;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using Microsoft.Data.SqlClient;

namespace TeamSync.Application.GlobalExceptionHandler.ExceptionsConfig
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public ExceptionHandlerMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (SqlException e)
            {
                await HandleExceptionAsync(context, new Exception(e.ToString()));
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }

        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode statusCode;
            string message = exception.Message;

            var typeException = exception.GetType();

            switch (typeException.Name)
            {
                case nameof(BadRequestException):
                    statusCode = HttpStatusCode.BadRequest;
                    break;
                case nameof(CustomExceptions.NotImplementedException):
                    statusCode = HttpStatusCode.NotImplemented;
                    break;
                case nameof(NotFoundException):
                    statusCode = HttpStatusCode.NotFound;
                    break;
                case nameof(CustomExceptions.UnauthorizedAccessException):
                    statusCode = HttpStatusCode.Unauthorized;
                    break;
                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            //Log.Information("TEST => {@message}", message);
            return context.Response.WriteAsync(JsonSerializer.Serialize(new { message, statusCode }));
        }
    }
}
