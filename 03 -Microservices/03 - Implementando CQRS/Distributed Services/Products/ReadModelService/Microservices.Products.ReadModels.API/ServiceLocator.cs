using Microservices.Infrastructure.MessageBus;
using Microservices.Products.ReadModels.API.Views;

namespace Microservices.Products.ReadModels.API
{
    public static class ServiceLocator
    {
        public static IMessageBus Bus { get; set; }
        public static ProductView ProductView { get; set; }
    }
}