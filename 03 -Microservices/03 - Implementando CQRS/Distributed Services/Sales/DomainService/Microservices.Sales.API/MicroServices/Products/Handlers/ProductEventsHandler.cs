
using Microservices.Infrastructure.Crosscutting;
using Microservices.Sales.API.MicroServices.Products.Events;

namespace Microservices.Sales.API.Products.Handlers
{
    public class ProductEventsHandler : ReadModelAggregate,
        IHandle<ProductCreated>,
        IHandle<ProductPriceChanged>
    {
        public void Apply(ProductCreated @event)
        {
            var view = ServiceLocator.ProductView;
            view.Add(@event.Id, @event.Price);
        }

        public void Apply(ProductPriceChanged @event)
        {
            var view = ServiceLocator.ProductView;
            var product = view.GetById(@event.Id);
            product.Price = @event.NewPrice;
        }
    }
}
