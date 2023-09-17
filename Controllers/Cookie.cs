using System.Runtime.InteropServices;
using IsuctSchedule_Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace IsuctSchedule_Core.Controllers;

public class Cookie : Controller
{
    private HolderChecker _ch;
    
    public Cookie(HolderChecker ch)
    {
        _ch = ch;
    }
    
    [HttpPost]
    [Route("/cookie")]
    public async Task Set()
    {
        try
        {
            var holder = await HttpContext.Request.ReadFromJsonAsync<CookieValue>();
                
            if (holder is not null && await _ch.CheckHolder(holder.Holder))
            {
                HttpContext.Response.Cookies.Delete("value");
                HttpContext.Response.StatusCode = 200;
                var options = new CookieOptions()
                {
                    Expires = DateTimeOffset.MaxValue
                };
                HttpContext.Response.Cookies.Append("holder", holder.Holder, options);
            }
            else
            {
                HttpContext.Response.StatusCode = 401;
            }
        }
        catch (Exception _)
        {
            HttpContext.Response.StatusCode = 401;
        }
    }
    
}

public record CookieValue (string Holder);
