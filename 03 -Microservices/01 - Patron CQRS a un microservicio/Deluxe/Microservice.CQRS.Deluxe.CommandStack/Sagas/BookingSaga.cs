using Microservice.CQRS.Deluxe.CommandStack.Commands;
using Microservice.CQRS.Deluxe.CommandStack.Domain.Model;
using Microservice.CQRS.Deluxe.CommandStack.Events;
using Microservice.CQRS.Deluxe.Infrastructure.Framework;
using Microservice.CQRS.Deluxe.Infrastructure.Framework.EventStore;
using Microservice.CQRS.Deluxe.Infrastructure.Framework.Repositories;
using Microservice.CQRS.Deluxe.Infrastructure.Persistence.SqlServer.Repositories;
using Microsoft.AspNetCore.Http;

namespace Microservice.CQRS.Deluxe.CommandStack.Sagas
{
    public class BookingSaga : Saga,
            IStartWithMessage<RequestBookingCommand>
    {
        private readonly IRepository _repository;        

        public BookingSaga(IBus bus, IEventStore eventStore, IHttpContextAccessor httpContextAccessor)
            : base(bus, eventStore)
        {
            _repository = (IRepository)httpContextAccessor.HttpContext.RequestServices.GetService(typeof(IRepository));
        }

        public BookingSaga(IBus bus, IEventStore eventStore, IRepository repository)
            : base(bus, eventStore)
        {
            _repository = repository;
        }

        public void Handle(RequestBookingCommand message)
        {
            var request = BookingRequest.Factory.Create(message.CourtId, message.Hour, message.Length, message.UserName);
            var response = _repository.CreateBookingFromRequest(request);
            if (!response.Success)
            {
                var rejected = new BookingRequestRejectedEvent(request.Id, response.Description);
                Bus.RaiseEvent(rejected);
                return;
            }

            var created = new BookingCreatedEvent(request.Id, response.AggregateId);
            Bus.RaiseEvent(created);
        }
    }
}