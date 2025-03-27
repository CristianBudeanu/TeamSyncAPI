using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace TeamSync.Application.Common.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> loger)
        {
            _next = next;
            _logger = loger;
        }

        public async Task Invoke(HttpContext context)
        {
            var httpMethod = context.Request.Method;
            var requestPath = context.Request.Path;

            string username = context.User?.Identity.Name;

            _logger.LogInformation("{Username} => {HttpMethod} {RequestPath} initiated.", username, httpMethod, requestPath);
            await _next(context);
        }
    }
}
