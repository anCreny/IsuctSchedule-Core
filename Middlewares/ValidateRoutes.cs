using IsuctSchedule_Core.Services;

namespace IsuctSchedule_Core.Middlewares;

public class ValidateRoutes
{
    private readonly RequestDelegate _next;
    
    public ValidateRoutes(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, WhiteList whiteList)
    {
        if (!whiteList.IsPassValidation(context.Request.Path))
        {
            context.Response.Redirect("/");
            return;
        }
        
        await _next.Invoke(context);
    }
}