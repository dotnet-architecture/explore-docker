using Microsoft.Extensions.Configuration;
using WebApp;

var builder = WebApplication.CreateBuilder(args);
var configuration = GetConfiguration();
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpClient<WeatherClient>(client =>
{
    var baseAddress = new Uri(configuration.GetValue<string>("backendUrl"));


    client.BaseAddress = baseAddress;
});

var app = builder.Build();
IConfiguration GetConfiguration()
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();

    return builder.Build();
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
