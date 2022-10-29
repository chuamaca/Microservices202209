using System;
using Microservice.CQRS.Deluxe.Infrastructure.Framework.EventStore;

namespace Microservice.CQRS.Deluxe.Infrastructure.Framework
{
    public abstract class Handler
    {
        public IEventStore EventStore { get; private set; }


        public Handler(IEventStore eventStore)
        {
            if (eventStore == null)
            {
                throw new ArgumentNullException("eventStore");
            }

            EventStore = eventStore;
        }
    }

}