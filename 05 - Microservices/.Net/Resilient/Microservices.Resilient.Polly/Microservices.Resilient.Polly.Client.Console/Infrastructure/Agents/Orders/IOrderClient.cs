using Microservices.Resilient.Polly.Client.Console.Infrastructure.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microservices.Resilient.Polly.Client.Console.Infrastructure.Agents.Orders
{
    public interface IOrderClient
    {
        public Task<List<Order>> List();
        public Task<List<Order>> ListFallback();
        public Task<Order> Get(int orderId);
        public Task<Order> GetFallback();
    }
}