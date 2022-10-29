using Microservice.Events.MessageSubscriber.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.Events.OrderService.Worker
{
    public class WorkerRunner
    {
        private ISubscriber _subscriber;

        public WorkerRunner(ISubscriber subscriber)
        {
            _subscriber = subscriber;
        }

        public void Start() {
            Logger.LogInfo($"Order Service Started");
            _subscriber.Subscribe();
        }

        public void Stop() {
            Logger.LogInfo($"Order Service Stopped");
        }
    }
}
