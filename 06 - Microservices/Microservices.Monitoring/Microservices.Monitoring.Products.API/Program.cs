using Elastic.Apm.Config;
using Elastic.Apm.NetCoreAll;
using HealthChecks.UI.Client;
using Microservices.Monitoring.Products.API.Application;
using Microservices.Monitoring.Products.API.Infrastructure.Data.Contexts;
using Microservices.Monitoring.Products.API.Infrastructure.Data.Repository;
using Microservices.Monitoring.Products.API.Infrastructure.Health;
using Microservices.Monitoring.Products.API.Infrastructure.Log;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json.Serialization;
using Serilog;
using Steeltoe.Discovery.Client;
using Steeltoe.Extensions.Configuration;
using Steeltoe.Extensions.Configuration.ConfigServer;
using Steeltoe.Extensions.Logging;
using Steeltoe.Management.Endpoint.Health;
using Steeltoe.Management.Endpoint.Metrics;

var builder = WebApplication.CreateBuilder(args)
.AddConfigServer();

builder.Host.UseSerilog();

Log.Logger = new LoggerConfiguration()
               .WriteTo.Http("http://ls01-test:28080", null)
               .Enrich.With(new CustomLogEnricher())
               .CreateLogger();

builder.Services.AddDiscoveryClient(builder.Configuration);
string conStr = builder.Configuration["ConnectionStrings:MicroservicesMonitoringConnection"];
builder.Services.AddDbContext<MicroservicesMonitoringContext>(opt => opt.UseSqlServer(conStr));

builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IProductApplicationService, ProductApplicationService>();

builder.Services.AddMetricsActuator(builder.Configuration);
builder.Services.AddHealthActuator(builder.Configuration);

builder.Services
    .AddHealthChecks()
    .AddMemoryHealthCheck("Memory")
    .AddCheck("Azure Service Bus", () => HealthCheckResult.Healthy("Azure Service Bus is OK!"), tags: new[] { "azure_service_bus_tag" })
    .AddCheck("CosmoDB", () => HealthCheckResult.Unhealthy("CosmoDB is unhealthy!"), tags: new[] { "cosmodb_tag" })
    .AddCheck("Azure AD", () => HealthCheckResult.Healthy("Azure AD is OK!"), tags: new[] { "azure_ad_tag" })
    .AddSqlServer(conStr)
    .AddDbContextCheck<MicroservicesMonitoringContext>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    // Use the default property (Pascal) casing
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
}); ;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   
}

app.UseAllElasticApm(builder.Configuration);

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapHealthChecks("/healthchecks-data-ui", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapControllers();

app.Run();
