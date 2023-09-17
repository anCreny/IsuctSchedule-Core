using IsuctSchedule_Core.Addons;
using IsuctSchedule_Core.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<HolderChecker>();
builder.Services.AddSingleton<WhiteList>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCookieAuth();

app.UseRouteValidator();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "/{action}",
    defaults: new {controller = "User", action = "Index"}
    );

app.Run();