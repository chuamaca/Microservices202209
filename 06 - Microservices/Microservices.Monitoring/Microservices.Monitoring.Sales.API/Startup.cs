using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using HealthChecks.UI.Client;
using Microservices.Monitoring.Sales.API.Application;
using Microservices.Monitoring.Sales.API.Infrastructure.Data.Contexts;
using Microservices.Monitoring.Sales.API.Infrastructure.Data.Repository;
using Microservices.Monitoring.Sales.API.Infrastructure.Health;
using Microservices.Monitoring.Sales.API.Infrastructure.Health.TIGStatus;
using Microservices.Monitoring.Sales.API.Infrastructure.Log;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Steeltoe.Common.HealthChecks;
using Steeltoe.Discovery.Client;
using Steeltoe.Management.Endpoint;
using Steeltoe.Management.Endpoint.Health;
using Steeltoe.Management.Endpoint.Metrics;
using Elastic.Apm.NetCoreAll;
using Elastic.Apm.DiagnosticSource;
using Elastic.Apm.EntityFrameworkCore;
using Elastic.Apm.AspNetCore;

namespace Microservices.Monitoring.Sales.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDiscoveryClient(Configuration);
            string conStr = Configuration["ConnectionStrings:MicroservicesMonitoringConnection"];
            services.AddDbContext<MicroservicesMonitoringContext>(opt => opt.UseSqlServer(conStr));
                        
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<ISaleApplicationService, SaleApplicationService>();

            services.AddMetricsActuator(Configuration);
            services.AddHealthActuator(Configuration);

            services
                .AddHealthChecks()
                .AddMemoryHealthCheck("Memory")
                .AddSqlServer(conStr)
                .AddDbContextCheck<MicroservicesMonitoringContext>();

            services.AddHttpContextAccessor();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Microservices.Monitoring.Sales.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            CustomLogEnricher.ServiceProvider = app.ApplicationServices;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseElasticApm(Configuration,
                new HttpDiagnosticsSubscriber(),  /* Enable tracing of outgoing HTTP requests */
                new EfCoreDiagnosticsSubscriber()); /* Enable tracing of database calls through EF Core*/

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Microservices.Monitoring.Sales.API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.Map<HealthEndpoint>();

                endpoints.MapHealthChecks("/healthchecks-data-ui", new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });

            //var options = new HealthCheckOptions();
            //options.ResponseWriter = async (c, r) => {
            //    c.Response.ContentType = "application/json";
            //    var result = new List<ServiceStatus>();
            //    result.Add(new ServiceStatus { Service = "OverAll", Status = (int)r.Status });
            //    result.AddRange(r.Entries.Select(e => new ServiceStatus { Service = e.Key, Status = (int)e.Value.Status }));
            //    var json = JsonConvert.SerializeObject(result);
            //    await c.Response.WriteAsync(json);
            //};

            //app.UseHealthChecks("/hc", options);

            app.UseDiscoveryClient();
        }
    }
}
