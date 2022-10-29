using Microservices.Infrastructure.MessageBus;
using Microservices.Sales.ReadModels.API.Views;

namespace Microservices.Sales.ReadModels.API
{
    public static class ServiceLocator
    {
        public static IMessageBus Bus { get; set; }
        public static OrderView BrandView { get; set; }
    }
}