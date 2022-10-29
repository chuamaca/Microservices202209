using Microservices.Infrastructure.MessageBus;
using Microservices.Products.API.MicroServices.Products.Handlers;

namespace Microservices.Products.API
{
    public static class ServiceLocator
    {
        public static IMessageBus Bus { get; set; }
        public static ProductCommandHandlers ProductCommands { get; set; }
    }
}