using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config
        .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
        .AddJsonFile("appsettings.json", true, true)
        .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true,true)
        .AddJsonFile("ocelot.json", false, true)
        .AddEnvironmentVariables();
});

builder.Services.AddCors(opt => opt.AddPolicy("CorsPolicy",builder => {
    builder
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();
}));

builder.Services.AddOcelot();        

var app = builder.Build();


app.UseCors("CorsPolicy");
app.UseOcelot().Wait();

app.Run();
