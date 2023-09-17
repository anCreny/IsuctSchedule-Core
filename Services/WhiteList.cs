namespace IsuctSchedule_Core.Services;

public class WhiteList
{
    private List<string> _authPassRoutes = new() {"/cookie", "/Static/favicon.ico"};
    private List<string> _validPassRoutes = new () {"/day", "/timetable", "/test", "/"};
    private List<string> _authPassPrefixes = new() {"/share"};

    public bool IsPassAuth(PathString path)
    {
        var res = _authPassRoutes.Contains(path);
        
        if (!res)
        {
            foreach (var prefix in _authPassPrefixes)
            {
                res = path.StartsWithSegments(prefix) || res;
            }
        }

        return res;
    }

    public bool IsPassValidation(PathString path)
    {
        var res = _validPassRoutes.Contains(path) || _authPassRoutes.Contains(path);
        
        if (!res)
        {
            foreach (var prefix in _authPassPrefixes)
            {
                res = path.StartsWithSegments(prefix) || res;
            }
        }

        return res;
    }
}