using Microservices.Infrastructure.Repository;
using Microservices.Products.ReadModels.Client;
using Microservices.Sales.API.MicroServices.Orders.Commands;
using Microservices.Sales.API.MicroServices.Products.View;
using System;

namespace Microservices.Sales.API.MicroServices.Orders.Handlers
{
    public class OrderCommandHandlers
    {
        private readonly ProductView productView;
        private readonly IRepository repository;

        public OrderCommandHandlers(IRepository repository)
            : this(repository, new ProductsView())
        {
        }

        public OrderCommandHandlers(IRepository repository, IProductsView ProductsProductsView)
        {
            this.repository = repository;
            this.productView = new ProductView(ProductsProductsView);
        }

        public void Handle(StartNewOrder message)
        {
            ValidateProduct(message.ProductId);
            var order = new Domain.Order(message.Id, message.ProductId, message.Quantity);
            repository.Save(order);
        }

        public void Handle(PayForOrder message)
        {
            var order = repository.GetById<Domain.Order>(message.Id);
            int committableVersion = message.Version;
            order.PayForOrder(committableVersion);
            repository.Save(order);
        }

        void ValidateProduct(Guid productId)
        {
            if (productId != Guid.Empty)
            {
                try
                {
                    productView.GetById(productId);
                }
                catch (Exception)
                {
                    throw new ArgumentOutOfRangeException("productId", "Invalid product identifier specified: the product cannot be found.");
                }
            }
        }

    }
}