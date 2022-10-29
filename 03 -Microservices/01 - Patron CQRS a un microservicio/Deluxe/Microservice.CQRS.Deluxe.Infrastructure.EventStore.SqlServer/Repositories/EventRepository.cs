using Microservice.CQRS.Deluxe.Infrastructure.EventStore.SqlServer.Data.Context;
using Microservice.CQRS.Deluxe.Infrastructure.EventStore.SqlServer.Data.Entities;
using System;

namespace Microservice.CQRS.Deluxe.Infrastructure.EventStore.SqlServer.Repositories
{
    public class EventRepository
    {
        private readonly MerloEventStoreContext _merloEventStoreContext;
        public EventRepository(MerloEventStoreContext merloEventStoreContext)
        {
            _merloEventStoreContext = merloEventStoreContext;
        }

        public void Store(LoggedEvent eventToLog)
        {
            eventToLog.TimeStamp = DateTime.Now;
            _merloEventStoreContext.LoggedEvents.Add(eventToLog);
            _merloEventStoreContext.SaveChanges();
        }
    }
}