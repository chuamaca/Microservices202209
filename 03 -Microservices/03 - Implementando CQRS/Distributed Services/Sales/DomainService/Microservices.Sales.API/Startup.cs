using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EasyNetQ;
using EventStore.ClientAPI;
using Microservices.Infrastructure.Crosscutting;
using Microservices.Infrastructure.Crosscutting.Util;
using Microservices.Infrastructure.MessageBus;
using Microservices.Infrastructure.Repository;
using Microservices.Sales.API.MicroServices.Orders.Handlers;
using Microservices.Sales.API.MicroServices.Products.View;
using Microservices.Sales.API.Products.Handlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace Microservices.Sales.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConfigureHandlers();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Microservices.Sales.API", Version = "v1" });
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Microservices.Sales.API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureHandlers()
        {
            var b = RabbitHutch.CreateBus("host=localhost");
            var bus = new RabbitMqBus(b);
            ServiceLocator.Bus = bus;

            var messageBusEndPoint = "Microservices_Sales_API";
            var topicFilter = "Microservices.Products.Infrastructure.Events";

            var eventStorePort = 1113;

            var eventStoreConnection = EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, eventStorePort));
            eventStoreConnection.ConnectAsync().Wait();
            var repository = new EventStoreRepository(eventStoreConnection, bus);
            
            ServiceLocator.OrderCommands = new OrderCommandHandlers(repository);
            ServiceLocator.ProductView = new ProductView();

            var eventMappings = new EventHandlerDiscovery()
                .Scan(new ProductEventsHandler())
                .Handlers;

            b.Subscribe<PublishedMessage>(messageBusEndPoint,
            m =>
            {
                EventHandlerData eventHandlerData;
                var messageType = Type.GetType(m.MessageTypeName);
                var handlerFound = eventMappings.TryGetValue(messageType.Name, out eventHandlerData);
                if (handlerFound)
                {
                    var @event = JsonConvert.DeserializeObject(m.SerialisedMessage, eventHandlerData.TypeParameter);
                    eventHandlerData.AggregateHandler.AsDynamic().ApplyEvent(@event, ((Event)@event).Version);
                }
            },
            q => q.WithTopic(topicFilter));

        }
    }
}
