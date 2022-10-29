using Microservice.CQRS.Deluxe.CommandStack.Handlers;
using Microservice.CQRS.Deluxe.CommandStack.Sagas;
using Microservice.CQRS.Deluxe.Infrastructure.Framework;
using Microservice.CQRS.Deluxe.Infrastructure.Framework.EventStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservice.CQRS.Deluxe.Client.Application
{
    public class BookingApplication
    {
        private readonly IBus _inMemoryBus;
        public BookingApplication(IBus inMemoryBus)
        {
            _inMemoryBus = inMemoryBus;
            Bus = _inMemoryBus;
            Bus.RegisterSaga<BookingSaga>();
            Bus.RegisterHandler<BookingRejectedHandler>();
            Bus.RegisterHandler<EmailHandler>();
        }
        public IBus Bus { get; set; }
    }
}
