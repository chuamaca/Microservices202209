using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Saga.Choreography.Client.API.Application;
using Microservices.Saga.Choreography.Client.API.Infrastructure.Connection;
using Microservices.Saga.Choreography.Client.API.Infrastructure.Connection.RabbitMQ;
using Microservices.Saga.Choreography.Client.API.Infrastructure.Data.Context;
using Microservices.Saga.Choreography.Client.API.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Microservices.Saga.Choreography.Client.API
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
            services.AddDbContext<SagaChoreographyContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SagaChoreographyConnection")));
            services.AddTransient<IUserApplicationService,UserApplicationService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IConnectionBus, RabbitMQConnection>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
