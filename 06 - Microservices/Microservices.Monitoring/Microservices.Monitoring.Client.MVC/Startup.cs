using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elastic.Apm.AspNetCore;
using Elastic.Apm.DiagnosticSource;
using Elastic.Apm.EntityFrameworkCore;
using Microservices.Monitoring.Client.MVC.Application;
using Microservices.Monitoring.Client.MVC.Infrastructure.Agents.Products;
using Microservices.Monitoring.Client.MVC.Infrastructure.Agents.Products.Commands;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Steeltoe.Discovery.Client;

namespace Microservices.Monitoring.Client.MVC
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
            services.AddControllersWithViews();

            services.AddTransient<ListCommand>();
            services.AddTransient<IProductsClient, ProductsClient>();
            services.AddTransient<IProductsAgent, ProductsAgent>();
            services.AddTransient<IProductApplicationService,ProductApplicationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseElasticApm(Configuration,
                new HttpDiagnosticsSubscriber(),  /* Enable tracing of outgoing HTTP requests */
                new EfCoreDiagnosticsSubscriber()); /* Enable tracing of database calls through EF Core*/
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseDiscoveryClient();
        }
    }
}
