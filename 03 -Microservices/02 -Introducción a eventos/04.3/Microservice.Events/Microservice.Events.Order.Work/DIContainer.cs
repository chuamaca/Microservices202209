using Autofac;
using Microservice.Events.MessageSubscriber.Implementations;
using Microservice.Events.MessageSubscriber.Interfaces;
using Microservice.Events.Order.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.Events.Order.Worker
{
    public static class DIContainer
    {
        public static IContainer Setup()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<RabbitMQSubscriber>().As<ISubscriber>();
            builder.RegisterType<OrderProcessor>().As<IMessageProcessor>();

            builder.RegisterType<OrderServiceRunner>();
            var container = builder.Build();
            return container;
        }
    }
}
