using Autofac;
using Microservice.Events.EmailService.Application;
using Microservice.Events.MessageSubscriber.Implementations;
using Microservice.Events.MessageSubscriber.Interfaces;

namespace Microservice.Events.EmailService.Worker
{
    public static class DIContainer
    {
        public static IContainer Setup()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<RabbitMQSubscriber>().As<ISubscriber>();
            builder.RegisterType<EmailProcessor>().As<IMessageProcessor>();

            builder.RegisterType<WorkerRunner>();
            var container = builder.Build();
            return container;
        }
    }
}
