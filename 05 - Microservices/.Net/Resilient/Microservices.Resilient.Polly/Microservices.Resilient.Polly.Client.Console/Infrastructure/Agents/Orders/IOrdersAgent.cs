using Microservices.Resilient.Polly.Client.Console.Infrastructure.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microservices.Resilient.Polly.Client.Console.Infrastructure.Agents.Orders
{
    public interface IOrdersAgent
    {
        Order Get(int orderId);
        List<Order> List();
    }
}