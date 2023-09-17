using IsuctSchedule_Core.Services;

namespace IsuctSchedule_Core.Middlewares;

public class CookieAuthMiddleware
{
    private RequestDelegate _next;

    public CookieAuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, HolderChecker ch, WhiteList whiteList)
    {
        if (whiteList.IsPassAuth(context.Request.Path))
        {
            await _next.Invoke(context);
            return;
        }
        
        if (!context.Request.Cookies.ContainsKey("holder") && context.Request.Path == "/welcome")
        {
            await context.Response.SendFileAsync("wwwroot/html/welcome.html");
            return;
        }
        
        var holder = context.Request.Cookies["holder"];
        
        if (holder is not null && await ch.CheckHolder(holder))
        {
            await _next.Invoke(context);
            return;
        }

        context.Response.Cookies.Delete("holder");
        context.Response.Redirect("/welcome");
    }
}