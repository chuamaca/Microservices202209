using Microservice.Events.MessageSubscriber.Interfaces;
using Microservice.Events.MessageSubscriber.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.Events.OrderTracking.Application
{
    public class OrderTrackingProcessor : IMessageProcessor
    {
        public void Process(Message message)
        {
            var order = (Order)message.Data.DeSerializeFromJSON(typeof(Order));
            Logger.LogInfoWithColor($"Processing Order Tracking for order: {order.OrderNo} for {order.CustomerUserName}", ((order.OrderNo % 2 == 0)? ConsoleColor.DarkMagenta:ConsoleColor.DarkRed));
            System.Threading.Thread.Sleep(1000);
        }
    }
}
