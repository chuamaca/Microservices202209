using Microservice.CQRS.Premium.Infrastructure.Context;
using Microservice.CQRS.Premium.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Microservice.CQRS.Premium.Infrastructure.Repositories
{
    public class EventRepository
    {
        private readonly MicroserviceCQRSContext _microserviceCQRSContext;

        public EventRepository(MicroserviceCQRSContext microserviceCQRSContext)
        {
            _microserviceCQRSContext = microserviceCQRSContext;
        }
        public void Store(MatchEvent eventData)
        {
            eventData.TimeStamp = DateTime.Now;
            _microserviceCQRSContext.MatchEvents.Add(eventData);
            _microserviceCQRSContext.SaveChanges();
        }

        public void RemoveMostRecent(string matchId)
        {
            var last = (from e in _microserviceCQRSContext.MatchEvents
                where e.MatchId == matchId
                orderby e.Id descending 
                select e).FirstOrDefault();
            if (last == null)
                return;

            _microserviceCQRSContext.MatchEvents.Remove(last);
            _microserviceCQRSContext.SaveChanges();
        }

        public IList<MatchEvent> All(string matchId)
        {
            var events = (from e in _microserviceCQRSContext.MatchEvents 
                          where e.MatchId == matchId 
                          select e).ToList();
            return events;
        }
    }
}