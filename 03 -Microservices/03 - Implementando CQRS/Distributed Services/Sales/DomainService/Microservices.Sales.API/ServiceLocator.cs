using Microservices.Infrastructure.MessageBus;
using Microservices.Sales.API.MicroServices.Orders.Handlers;
using Microservices.Sales.API.MicroServices.Products.View;

namespace Microservices.Sales.API
{
    public static class ServiceLocator
    {
        public static IMessageBus Bus { get; set; }
        public static OrderCommandHandlers OrderCommands { get; set; }
        public static ProductView ProductView { get; set; }
    }
}