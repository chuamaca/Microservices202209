using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microservice.Events.MessageSubscriber.Implementations;
using Microservice.Events.MessageSubscriber.Interfaces;
using Microservice.Events.OrderPayment.Domain;

namespace Microservice.Events.OrderPayment.Worker
{
    public static class DIContainer
    {
        public static IContainer Setup()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<RabbitMQSubscriber>().As<ISubscriber>();
            builder.RegisterType<OrderPaymentProcessor>().As<IMessageProcessor>();

            builder.RegisterType<OrderPaymentServiceRunner>();
            var container = builder.Build();
            return container;
        }
    }
}
