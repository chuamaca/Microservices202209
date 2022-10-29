using Microservice.CQRS.Deluxe.Infrastructure.EventStore.SqlServer.Data.Entities;
using Microservice.CQRS.Deluxe.Infrastructure.EventStore.SqlServer.Repositories;
using System.Collections.Generic;
using System.Text.Json;

namespace Microservice.CQRS.Deluxe.Infrastructure.Framework.EventStore
{
    public class SqlEventStore : IEventStore
    {
        private readonly EventRepository _eventRepository;
        public SqlEventStore(EventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public IEnumerable<Event> All(string matchId)
        {
            return null; //EventRepository.All(matchId);
        }

        public void Save<T>(T theEvent) where T : Event
        {
            var loggedEvent = new LoggedEvent()
            {
                Action = theEvent.Name,
                Cargo = JsonSerializer.Serialize(theEvent)
            };

            _eventRepository.Store(loggedEvent);
        }
    }
}