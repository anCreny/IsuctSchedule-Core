using Microsoft.AspNetCore.Mvc;

namespace IsuctSchedule_Core.Controllers;

public class User : Controller
{
    
    [HttpGet]
    public async Task Timetable()
    {
        await HttpContext.Response.SendFileAsync("wwwroot/html/timetable.html");
    }

    [HttpGet]
    public async Task Day()
    {
        await HttpContext.Response.SendFileAsync("wwwroot/html/today.html");
    }

    [HttpGet]
    public async Task Index()
    {
        var isMobile = false;
        foreach (var value in HttpContext.Request.Headers["User-Agent"])
        {
            isMobile = value.Contains("Mobile");
        }

        if (isMobile)
        {
            await HttpContext.Response.SendFileAsync("wwwroot/html/MobileMain.html");
        }
        else
        {
            await HttpContext.Response.SendFileAsync("wwwroot/html/index.html");
        }
    }
}