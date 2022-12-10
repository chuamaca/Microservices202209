using Microservices.Monitoring.Dashboard.Infrastructure;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using Steeltoe.Discovery.Client;
using Steeltoe.Extensions.Configuration;
using Steeltoe.Extensions.Configuration.ConfigServer;
using System.Net.Mime;

var builder = WebApplication.CreateBuilder(args)
.AddConfigServer();

builder.Services.AddDiscoveryClient(builder.Configuration);
string conStr = builder.Configuration["ConnectionStrings:MicroservicesMonitoringDashboardConnection"];

builder.Services.AddHealthChecks();
builder.Services.AddHealthChecksUI()
    //.AddInMemoryStorage();
    .AddSqlServerStorage(conStr);

builder.Services.AddControllersWithViews();

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHealthChecksUI();

app.MapHealthChecks("/healthchecks-ui", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        var result = JsonConvert.SerializeObject(
            new
            {
                statusApplication = report.Status.ToString(),
                healthChecks = report.Entries.Select(e => new
                {
                    check = e.Key,
                    ErrorMessage = e.Value.Exception?.Message,
                    status = Enum.GetName(typeof(Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus), e.Value.Status)
                })
            });
        context.Response.ContentType = MediaTypeNames.Application.Json;
        await context.Response.WriteAsync(result);
    }
});

app.Run();
