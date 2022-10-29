using Commands;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HandleMessages
{
    public class DoSomethingHandler :
       IHandleMessages<DoSomethingCommand>,
       IHandleMessages<DoSomethingComplexCommand>
    {
        static ILog log = LogManager.GetLogger<PlaceOrderHandler>();
        public Task Handle(DoSomethingCommand message, IMessageHandlerContext context)
        {
            log.Info($"Received Simple Command, SomeProperty : {message.SomeProperty}");
            return Task.CompletedTask;
        }

        public Task Handle(DoSomethingComplexCommand message, IMessageHandlerContext context)
        {
            log.Info($"Received Complex Commnad, ChildStuff.SomeProperty : {message.ChildStuff.SomeProperty}");
            return Task.CompletedTask;
        }
    }
}
