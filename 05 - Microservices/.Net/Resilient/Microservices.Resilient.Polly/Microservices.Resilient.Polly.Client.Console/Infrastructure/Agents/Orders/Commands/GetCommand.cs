using Microservices.Resilient.Polly.Client.Console.Infrastructure.Data.Entities;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Resilient.Polly.Client.Console.Infrastructure.Agents.Orders.Commands
{
    public class GetCommand: PollyCommand<Order>
    {
        private IOrderClient _orderClient;        
        private int _orderId;

        public GetCommand(IOrderClient orderClient)
        {
            _orderClient = orderClient;
        }

        public Order Execute(int orderId)
        {
            _orderId = orderId;
            return base.Execute();
        }
        protected override Order Run()
        {
            return _orderClient.Get(_orderId).Result;
        }

        protected override Order RunFallback(Context context)
        {            
            return _orderClient.GetFallback().Result;
        }
    }
}
