using System.Net;

namespace IsuctSchedule_Core.Services;

public class HolderChecker
{
    private string _cacheAddr;

    public HolderChecker()
    {
        var host = Environment.GetEnvironmentVariable("CACHE_HOST") ?? "188.120.234.21";
        var port = Environment.GetEnvironmentVariable("CACHE_PORT") ?? "9818";

        if (host is not null && port is not null)
        {
            _cacheAddr = $"http://{host}:{port}";
            return;
        }

        throw new Exception("Couldn't init cache address");
    }

    public async Task<bool> CheckHolder(string holder)
    {
        var arr = holder.Split("-");

        if (arr.Length != 2)
        {
            return false;
        }
        
        var leftPart = arr[0];
        var rightPart = arr[1];

        if (rightPart.Contains("."))
        {
            leftPart = Decode(leftPart);
            rightPart = Decode(rightPart);

            return await CheckTeacher($"{leftPart}-{rightPart}");
        }

        return await CheckGroup($"{leftPart}-{rightPart}");
    }
    
    private string Decode(string target)
    {
        var encodedSymbols = target.Split(".");
        var result = string.Empty;
        foreach (var encodedSymbol in encodedSymbols)
        {
            result += Convert.ToChar(Convert.ToInt32(encodedSymbol));
        }

        return result;
    }

    private async Task<bool> CheckTeacher(string teacherName)
    {
        var client = new HttpClient();
        var resp = await client.GetAsync($"{_cacheAddr}/api/check/teacher/{teacherName}");

        return resp.StatusCode == HttpStatusCode.OK;
    }

    private async Task<bool> CheckGroup(string groupNumber)
    {
        var client = new HttpClient();
        var resp = await client.GetAsync($"{_cacheAddr}/api/check/group/{groupNumber}");

        return resp.StatusCode == HttpStatusCode.OK;
    }
}