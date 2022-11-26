using Microservices.Resilient.Hystrix.Client.MVC.Infrastructure.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microservices.Resilient.Hystrix.Client.MVC.Infrastructure.Agents.Orders
{
    public interface IOrderClient
    {
        public Task<List<Order>> List();
        public Task<List<Order>> ListFallback();
        public Task<Order> Get(int orderId);
        public Task<Order> GetFallback();
    }
}