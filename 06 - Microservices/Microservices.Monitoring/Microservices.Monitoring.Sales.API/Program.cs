using Steeltoe.Extensions.Configuration.ConfigServer;
using Steeltoe.Discovery.Client;
using Microservices.Monitoring.Sales.API.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microservices.Monitoring.Sales.API.Application;
using Microservices.Monitoring.Sales.API.Infrastructure.Data.Repository;
using Microsoft.Extensions.Configuration;
using Steeltoe.Extensions.Configuration;
using Steeltoe.Management.Endpoint.Metrics;
using Steeltoe.Management.Endpoint.Health;
using Microservices.Monitoring.Sales.API.Infrastructure.Health;
using Microservices.Monitoring.Sales.API.Infrastructure.Log;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Builder;
using Steeltoe.Management.Endpoint;
using Serilog;
using Elastic.Apm.NetCoreAll;

var builder = WebApplication.CreateBuilder(args)
.AddConfigServer();

builder.Host.UseSerilog();

//Log.Logger = new LoggerConfiguration()
//    .WriteTo.File("d:\\Logs\\Microservices.Monitoring.Sales.API.txt")
//    .CreateLogger();

Log.Logger = new LoggerConfiguration()
               .WriteTo.Http("http://ls01-test:28080", null)
               .Enrich.With(new CustomLogEnricher())
               .CreateLogger();

builder.Services.AddDiscoveryClient(builder.Configuration);
string conStr = builder.Configuration["ConnectionStrings:MicroservicesMonitoringConnection"];
builder.Services.AddDbContext<MicroservicesMonitoringContext>(opt => opt.UseSqlServer(conStr));

builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<ISaleApplicationService, SaleApplicationService>();

builder.Services.AddMetricsActuator(builder.Configuration);
builder.Services.AddHealthActuator(builder.Configuration);

builder.Services
    .AddHealthChecks()
    .AddMemoryHealthCheck("Memory")
    .AddSqlServer(conStr)
    .AddDbContextCheck<MicroservicesMonitoringContext>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

var app = builder.Build();

CustomLogEnricher.ServiceProvider = app.Services;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
}

app.UseAllElasticApm(builder.Configuration);

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.Map<HealthEndpoint>();

app.MapHealthChecks("/healthchecks-data-ui", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapControllers();

app.Run();
