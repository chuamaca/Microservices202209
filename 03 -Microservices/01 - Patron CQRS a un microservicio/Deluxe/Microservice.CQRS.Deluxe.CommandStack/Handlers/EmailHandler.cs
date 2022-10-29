using System;
using Microservice.CQRS.Deluxe.CommandStack.Events;
using Microservice.CQRS.Deluxe.Infrastructure.Extras;
using Microservice.CQRS.Deluxe.Infrastructure.Framework;
using Microservice.CQRS.Deluxe.Infrastructure.Framework.EventStore;
using Microsoft.AspNetCore.Http;

namespace Microservice.CQRS.Deluxe.CommandStack.Handlers
{
    public class EmailHandler : Handler,
        IHandleMessage<BookingRequestRejectedEvent>,
        IHandleMessage<BookingCreatedEvent> 
    {
        private readonly EmailService _emailService;
        public EmailHandler(IEventStore eventStore, IHttpContextAccessor httpContextAccessor) 
            : base(eventStore)
        {
            _emailService = (EmailService)httpContextAccessor.HttpContext.RequestServices.GetService(typeof(EmailService));
        }
        public void Handle(BookingRequestRejectedEvent message)
        {
            var body = String.Format("Your request {0} could not be satisfied.",
                message.RequestId);
            _emailService.Send("user@company.com", body);
        }

        public void Handle(BookingCreatedEvent message)
        {
            var body = String.Format("Congratulations! Your booking is confirmed. Your confirmation number is {0}.",
                message.Id);
            _emailService.Send("user@company.com", body);
        }
    }
}