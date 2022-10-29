using Microservice.Events.MessageSubscriber.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.Events.OrderPayment.Worker
{
    public class OrderPaymentServiceRunner
    {
        private ISubscriber _subscriber;

        public OrderPaymentServiceRunner(ISubscriber subscriber)
        {
            _subscriber = subscriber;
        }

        public void Start() {
            Logger.LogInfo($"Order Payment Service Started");
            _subscriber.Subscribe();
        }

        public void Stop() {
            Logger.LogInfo($"Order Payment Service Stopped");
        }
    }
}
