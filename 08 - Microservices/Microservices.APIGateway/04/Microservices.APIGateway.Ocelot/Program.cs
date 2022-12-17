using Microservices.APIGateway.Ocelot.Security;
using Microsoft.Extensions.Configuration;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Values;

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
builder.Services.AddSecurity(builder.Configuration);

var app = builder.Build();

app.UseCors("CorsPolicy");
app.UseOcelot().Wait();

app.Run();
