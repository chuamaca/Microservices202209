using Messages.Commands;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sales
{
    //Para procesar un mensaje, creamos un controlador de mensajes,una clase que implementa , donde hay un tipo de mensaje
    public class PlaceOrderHandler: IHandleMessages<PlaceOrderCommand>
    {
        static ILog log = LogManager.GetLogger<PlaceOrderHandler>();

        public Task Handle(PlaceOrderCommand message, IMessageHandlerContext context)
        {
            log.Info($"Received PlaceOrder, OrderId = {message.OrderId}");
            return Task.CompletedTask;
        }
    }
}
