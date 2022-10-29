using Microservice.CQRS.Deluxe.CommandStack.Events;
using Microservice.CQRS.Deluxe.Infrastructure.Framework;
using Microservice.CQRS.Deluxe.Infrastructure.Framework.EventStore;

namespace Microservice.CQRS.Deluxe.CommandStack.Handlers
{
    public class BookingRejectedHandler : Handler,
        IHandleMessage<BookingRequestRejectedEvent>
    {
        public BookingRejectedHandler(IEventStore eventStore) 
            : base(eventStore)
        {
        }

        public void Handle(BookingRequestRejectedEvent message)
        {
            throw new System.NotImplementedException();
        }
    }
}