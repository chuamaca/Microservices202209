using System;
using Microservices.Infrastructure.Crosscutting;
using Newtonsoft.Json;

namespace Microservices.Sales.Infrastructure.Events
{
    public class OrderPlaced : Event
    {
        public OrderPlaced(Guid id, Guid productId, int quantity)
        {
            Id = id;
            ProductId = productId;
            Quantity = quantity;
        }

        [JsonConstructor]
        private OrderPlaced(Guid id, Guid productId, int quantity, int version) : this(id, productId, quantity)
        {
            Version = version;
        }

        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
    }

    public class OrderPaidFor : Event
    {
        public OrderPaidFor(Guid id)
        {
            Id = id;
        }

        [JsonConstructor]
        private OrderPaidFor(Guid id, int version) : this(id)
        {
            Version = version;
        }
    }
}