using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microservice.Events.MessageSubscriber.Implementations;
using Microservice.Events.MessageSubscriber.Interfaces;
using Microservice.Events.OrderTracking.Application;

namespace Microservice.Events.OrderTracking.Worker
{
    public static class DIContainer
    {
        public static IContainer Setup()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<RabbitMQSubscriber>().As<ISubscriber>();
            builder.RegisterType<OrderTrackingProcessor>().As<IMessageProcessor>();

            builder.RegisterType<OrderTrackingServiceRunner>();
            var container = builder.Build();
            return container;
        }
    }
}
