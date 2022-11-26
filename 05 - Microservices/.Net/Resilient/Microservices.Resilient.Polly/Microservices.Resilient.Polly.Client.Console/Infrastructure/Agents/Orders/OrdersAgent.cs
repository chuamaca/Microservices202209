using Microservices.Resilient.Polly.Client.Console.Infrastructure.Agents.Orders.Commands;
using Microservices.Resilient.Polly.Client.Console.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Resilient.Polly.Client.Console.Infrastructure.Agents.Orders
{
    public class OrdersAgent : IOrdersAgent
    {
        private ListCommand _listCommand;
        private GetCommand _getCommand;

        public OrdersAgent(ListCommand listCommand, GetCommand getCommand)
        {
            _listCommand = listCommand;
            _getCommand = getCommand;
        }

        public List<Order> List()
        {
            return _listCommand.Execute();
        }
        public Order Get(int orderId)
        {
            return _getCommand.Execute(orderId);
        }
    }
}
