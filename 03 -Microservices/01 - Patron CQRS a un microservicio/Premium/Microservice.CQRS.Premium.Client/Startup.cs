using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservice.CQRS.Premium.Client.Application;
using Microservice.CQRS.Premium.Client.Application.SignalR;
using Microservice.CQRS.Premium.Command;
using Microservice.CQRS.Premium.Command.Services;
using Microservice.CQRS.Premium.Infrastructure.Context;
using Microservice.CQRS.Premium.Infrastructure.Repositories;
using Microservice.CQRS.Premium.Read.Facade;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Microservice.CQRS.Premium.Client
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
            services.AddControllersWithViews();
            services.AddSignalR();

            services.AddTransient<AdminApplicationService>();
            services.AddTransient<HomeApplicationService>();
            services.AddTransient<LiveScoreApplicationService>();
            services.AddTransient<MatchApplicationService>();

            services.AddTransient<EventRepository>();
            services.AddTransient<MatchRepository>();
            services.AddTransient<MiscRepository>();

            services.AddTransient<MatchFacade>();
            services.AddTransient<EventSourceManager>();
            services.AddTransient<MatchSynchronizer>();

            string conStr = Configuration["connectionStrings:CQRSConnectionString"];
            services.AddDbContext<MicroserviceCQRSContext>(opt => opt.UseSqlServer(conStr));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
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

                endpoints.MapHub<LiveScoreHub>("/LiveScoreHub");
            });
        }
    }
}
