using Microservices.Resilient.Hystrix.API.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
    using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Resilient.Hystrix.API.Infrastructure.Data.Repositories
{
    public class OrderRepository
    {
        private List<Order> _orders;
        public OrderRepository()
        {
            _orders = new List<Order>();

            for (int i = 1; i <= 20; i++)
            {
                _orders.Add(new Order
                {
                    OrderId = i,
                    Description = $"Order {i}"
                });
            }
        }

        public List<Order> List()
        {
            return _orders;
        }
        public Order Get(int orderId)
        {
            return _orders.Find(o=>o.OrderId==orderId);
        }
    }
}
