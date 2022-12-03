using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elastic.Apm.AspNetCore;
using Elastic.Apm.DiagnosticSource;
using Elastic.Apm.EntityFrameworkCore;
using HealthChecks.UI.Client;
using Microservices.Monitoring.Products.API.Application;
using Microservices.Monitoring.Products.API.Infrastructure.Data.Contexts;
using Microservices.Monitoring.Products.API.Infrastructure.Data.Repository;
using Microservices.Monitoring.Products.API.Infrastructure.Health;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Steeltoe.Discovery.Client;
using Steeltoe.Management.Endpoint.Health;
using Steeltoe.Management.Endpoint.Metrics;

namespace Microservices.Monitoring.Products.API
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
                        
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductApplicationService, ProductApplicationService>();

            services.AddMetricsActuator(Configuration);
            services.AddHealthActuator(Configuration);

            services
                .AddHealthChecks()
                .AddMemoryHealthCheck("Memory")
                .AddCheck("Azure Service Bus", () => HealthCheckResult.Healthy("Azure Service Bus is OK!"), tags: new[] { "azure_service_bus_tag" })
                .AddCheck("CosmoDB", () => HealthCheckResult.Unhealthy("CosmoDB is unhealthy!"), tags: new[] { "cosmodb_tag" })
                .AddCheck("Azure AD", () => HealthCheckResult.Healthy("Azure AD is OK!"), tags: new[] { "azure_ad_tag" })
                .AddSqlServer(conStr)
                .AddDbContextCheck<MicroservicesMonitoringContext>();

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                // Use the default property (Pascal) casing
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Microservices.Monitoring.Product.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Microservices.Monitoring.Products.API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapHealthChecks("/healthchecks-data-ui", new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });

            app.UseDiscoveryClient();
        }
    }
}
