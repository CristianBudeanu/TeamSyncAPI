using Microsoft.AspNetCore.Http;

namespace TeamSync.Helpers.HttpContextHelper
{
    public class HttpContextService(IHttpContextAccessor _httpContextAccessor) : IHttpContextService
    {
        public string GetUsernameFromToken()
        {
            return _httpContextAccessor.HttpContext.User.Identity?.IsAuthenticated == true
                        ? _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "Username").Value
                        : "Anonymous";
        }
    }
}
