using IsuctSchedule_Core.Middlewares;

namespace IsuctSchedule_Core.Addons;

public static class Extender
{
    public static IApplicationBuilder UseRouteValidator(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ValidateRoutes>();
    }

    public static IApplicationBuilder UseCookieAuth(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CookieAuthMiddleware>();
    }
}