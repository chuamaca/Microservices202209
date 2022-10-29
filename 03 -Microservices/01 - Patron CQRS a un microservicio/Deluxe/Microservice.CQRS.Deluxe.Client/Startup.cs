using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservice.CQRS.Deluxe.Client.Application;
using Microservice.CQRS.Deluxe.Client.Application.Services;
using Microservice.CQRS.Deluxe.Infrastructure.EventStore.SqlServer.Data.Context;
using Microservice.CQRS.Deluxe.Infrastructure.EventStore.SqlServer.Repositories;
using Microservice.CQRS.Deluxe.Infrastructure.Extras;
using Microservice.CQRS.Deluxe.Infrastructure.Extras.Backend.Context;
using Microservice.CQRS.Deluxe.Infrastructure.Framework;
using Microservice.CQRS.Deluxe.Infrastructure.Framework.EventStore;
using Microservice.CQRS.Deluxe.Infrastructure.Framework.Repositories;
using Microservice.CQRS.Deluxe.Infrastructure.Persistence.SqlServer.Data.Context;
using Microservice.CQRS.Deluxe.Infrastructure.Persistence.SqlServer.Repositories;
using Microservice.CQRS.Deluxe.QueryStack.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Microservice.CQRS.Deluxe.Client
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
            services.AddTransient<HomeService>();
            services.AddTransient<EmailService>();
            services.AddTransient<BookingService>();

            services.AddTransient(typeof(IRepository),typeof(BookingRepository));
            services.AddTransient<EventRepository>();

            services.AddTransient<BookingApplication>();
            services.AddTransient<Database>();
            services.AddTransient(typeof(IEventStore), typeof(SqlEventStore));
            services.AddTransient(typeof(IBus), typeof(InMemoryBus));

            services.AddHttpContextAccessor();

            string conStr = Configuration.GetConnectionString("CQRSConnectionString");
            services.AddDbContext<MerloXtraContext>(opt => { opt.UseSqlServer(conStr); });
            services.AddDbContext<MerloEventStoreContext>(opt => { opt.UseSqlServer(conStr); });
            services.AddDbContext<MerloContext>(opt => { opt.UseSqlServer(conStr); });

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
