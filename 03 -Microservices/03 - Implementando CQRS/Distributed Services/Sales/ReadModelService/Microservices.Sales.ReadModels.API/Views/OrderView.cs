using Microservices.Infrastructure.Crosscutting;
using Microservices.Infrastructure.Crosscutting.Exceptions;
using Microservices.Infrastructure.Repository;
using Microservices.Products.ReadModels.Client;
using Microservices.Sales.Infrastructure.Dto;
using Microservices.Sales.Infrastructure.Events;
using System;
using System.Collections.Generic;

namespace Microservices.Sales.ReadModels.API.Views
{
    public class OrderView : ReadModelAggregate,
        IHandle<OrderPlaced>,
        IHandle<OrderPaidFor>
    {
        private readonly IReadModelRepository<OrderDto> repository;

        public OrderView(IReadModelRepository<OrderDto> repository)
        {
            this.repository = repository;
        }

        public OrderDto GetById(Guid id)
        {
            try
            {
                return repository.Get(id);
            }
            catch
            {
                throw new ReadModelNotFoundException(id, typeof(OrderDto));
            }
        }

        public IEnumerable<OrderDto> GetAll()
        {
            return repository.GetAll();
        }

        public void Apply(OrderPlaced e)
        {
            var productView = new ProductsView();
            var product = productView.GetById(e.ProductId);
            var dto = new OrderDto
            {
                Id = e.Id,
                Quantity = e.Quantity,
                ProductName = product.Description,
                Version = e.Version,
                IsPaidFor = false
            };
            repository.Insert(dto);
        }

        public void Apply(OrderPaidFor e)
        {
            var order = GetById(e.Id);
            order.Version = e.Version;
            order.IsPaidFor = true;
            repository.Update(order);
        }
    }
}