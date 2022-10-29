using Messages.Commands;
using Messages.Events;
using NServiceBus;
using NServiceBus.Logging;
using NServiceBus.Persistence.Sql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shipping
{
    //[SqlSaga(correlationProperty: nameof(ShippingPolicyData.OrderId))]
//    [SqlSaga(
//    transitionalCorrelationProperty: nameof(ShippingPolicyData.OrderId)
//)]
    public class ShippingPolicy :
        Saga<ShippingPolicyData>,
        IAmStartedByMessages<OrderPlaced>,
        IAmStartedByMessages<OrderBilled>
    {
        static ILog log = LogManager.GetLogger<ShippingPolicy>();

        public Task Handle(OrderPlaced message, IMessageHandlerContext context)
        {
            // DON'T NEED THIS! NServiceBus does this for us.
            //Data.OrderId = message.OrderId;

            log.Info($"OrderPlaced message received.");
            Data.IsOrderPlaced = true;
            return ProcessOrder(context);
        }

        public Task Handle(OrderBilled message, IMessageHandlerContext context)
        {
            log.Info($"OrderBilled message received.");
            Data.IsOrderBilled = true;
            return ProcessOrder(context);
        }

        private async Task ProcessOrder(IMessageHandlerContext context)
        {
            if (Data.IsOrderPlaced && Data.IsOrderBilled)
            {
                await context.SendLocal(new ShipOrder() { OrderId = Data.OrderId });
                MarkAsComplete();
            }
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<ShippingPolicyData> mapper)
        {
            mapper.ConfigureMapping<OrderPlaced>(message => message.OrderId)
            .ToSaga(sagaData => sagaData.OrderId);

            mapper.ConfigureMapping<OrderBilled>(message => message.OrderId)
            .ToSaga(sagaData => sagaData.OrderId);
        }
    }
}
