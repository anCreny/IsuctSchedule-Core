using IsuctSchedule_Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace IsuctSchedule_Core.Controllers;


public class Share : Controller
{
    private HolderChecker _checker;

    public Share(HolderChecker checker)
    {
        _checker = checker;
    }
    
    
    [HttpGet]
    [Route("/share/group/{number}")]
    public async Task Group(string number)
    {
        if (await _checker.CheckGroup(number))
        {
            await HttpContext.Response.SendFileAsync("wwwroot/html/share/timetable_share.html");
            return;
        }

        HttpContext.Response.StatusCode = 400;
    }

    [HttpGet]
    [Route("/share/group/{number}/day")]
    public async Task GroupDay(string number)
    {
        if (await _checker.CheckGroup(number))
        {
            await HttpContext.Response.SendFileAsync("wwwroot/html/share/today_share.html");
            return;
        }

        HttpContext.Response.StatusCode = 400;
    }
    
    [HttpGet]
    [Route("/share/teacher/{name}")]
    public async Task Teacher(string name)
    {
        if (await _checker.CheckTeacher(name))
        {
            await HttpContext.Response.SendFileAsync("wwwroot/html/share/timetable_share.html");
            return;
        }

        HttpContext.Response.StatusCode = 400;
    }

    [HttpGet]
    [Route("/share/teacher/{name}/day")]
    public async Task TeacherDay(string name)
    {
        if (await _checker.CheckTeacher(name))
        {
            await HttpContext.Response.SendFileAsync("wwwroot/html/share/today_share.html");
            return;
        }

        HttpContext.Response.StatusCode = 400;
    }
}