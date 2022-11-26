using Microservices.Resilient.Polly.Client.Console.Infrastructure.Data.Entities;
using Polly;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservices.Resilient.Polly.Client.Console.Infrastructure.Agents.Orders.Commands
{
    public class ListCommand:PollyCommand<List<Order>>
    {
        private IOrderClient _orderClient;        

        public ListCommand(IOrderClient orderClient)
        {
            _orderClient = orderClient;            
        }
        protected override List<Order> Run()
        {
            return _orderClient.List().Result;
        }

        protected override List<Order> RunFallback(Context context)
        {   
            return _orderClient.ListFallback().Result;
        }
    }
}
