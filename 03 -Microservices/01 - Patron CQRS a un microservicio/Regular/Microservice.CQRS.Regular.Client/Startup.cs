using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservice.CQRS.Regular.Client.Application;
using Microservice.CQRS.Regular.CommandStack.Context;
using Microservice.CQRS.Regular.ReadStack;
using Microservice.CQRS.Regular.ReadStack.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Microservice.CQRS.Regular.Client
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
            string conStr = Configuration["connectionStrings:CQRSConnectionString"];
            services.AddDbContext<QueryDbContext>(opt => opt.UseSqlServer(conStr));
            services.AddDbContext<CommandDbContext>(opt => opt.UseSqlServer(conStr));

            services.AddTransient<IAdminApplicationService, AdminApplicationService>();
            services.AddTransient<Database>();
            services.AddControllersWithViews();
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
        }
    }
}
