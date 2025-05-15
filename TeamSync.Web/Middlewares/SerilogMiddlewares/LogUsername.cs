using Serilog.Context;

namespace TeamSync.Web.Middlewares.SerilogMiddlewares;

public class LogUsername
{
    private readonly RequestDelegate next;

    public LogUsername(RequestDelegate next)
    {
        this.next = next;
    }

    public Task Invoke(HttpContext context)
    {
        var username = context.User.Identity.Name;

        if (string.IsNullOrEmpty(username))
        {
            LogContext.PushProperty("Username", "Unregistered");
        }
        else
        {
            LogContext.PushProperty("Username", username);
        }
        
        
        return next(context);
    }
}