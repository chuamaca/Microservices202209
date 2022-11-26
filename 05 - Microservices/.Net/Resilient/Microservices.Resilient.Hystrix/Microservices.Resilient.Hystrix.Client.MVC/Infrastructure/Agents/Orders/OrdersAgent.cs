using Microservices.Resilient.Hystrix.Client.MVC.Infrastructure.Agents.Orders.Commands;
using Microservices.Resilient.Hystrix.Client.MVC.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Resilient.Hystrix.Client.MVC.Infrastructure.Agents.Orders
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

        public async Task<List<Order>> ListAsync()
        {
            return await _listCommand.ExecuteAsync();
        }
        public async Task<Order> GetAsync(int orderId)
        {
            return await _getCommand.ExecuteAsync(orderId);
        }
    }
}
