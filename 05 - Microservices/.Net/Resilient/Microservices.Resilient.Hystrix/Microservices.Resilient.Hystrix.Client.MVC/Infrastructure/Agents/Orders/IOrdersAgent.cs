using Microservices.Resilient.Hystrix.Client.MVC.Infrastructure.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microservices.Resilient.Hystrix.Client.MVC.Infrastructure.Agents.Orders
{
    public interface IOrdersAgent
    {
        Task<Order> GetAsync(int orderId);
        Task<List<Order>> ListAsync();
    }
}