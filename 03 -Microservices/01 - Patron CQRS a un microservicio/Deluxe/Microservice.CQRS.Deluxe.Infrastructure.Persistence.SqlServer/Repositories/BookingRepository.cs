using System;
using Microservice.CQRS.Deluxe.CommandStack.Domain.Common;
using Microservice.CQRS.Deluxe.CommandStack.Domain.Model;
using Microservice.CQRS.Deluxe.Infrastructure.Framework;
using Microservice.CQRS.Deluxe.Infrastructure.Framework.Repositories;
using Microservice.CQRS.Deluxe.Infrastructure.Persistence.SqlServer.Data;
using Microservice.CQRS.Deluxe.Infrastructure.Persistence.SqlServer.Data.Context;
using Microservice.CQRS.Deluxe.Infrastructure.Persistence.SqlServer.Repositories.Adapters;

namespace Microservice.CQRS.Deluxe.Infrastructure.Persistence.SqlServer.Repositories
{
    public class BookingRepository : IRepository
    {
        private readonly MerloContext _merloContext;
        public BookingRepository(MerloContext merloContext)
        {
            _merloContext = merloContext;
        }

        public T GetById<T>(int id) where T : IAggregate
        {
            throw new NotImplementedException();
        }

        public CommandResponse CreateBookingFromRequest<T>(T item) where T : class, IAggregate
        {
            // Gets a BookingRequest
            var request = item as BookingRequest;
            var booking = Adapter.RequestToBooking(request);

            _merloContext.Bookings.Add(booking); //.Set<T>().Add(booking);
            var count = _merloContext.SaveChanges();

            var response = new CommandResponse(count >0, booking.Id) {RequestId = new Guid(booking.RequestId)};
            return response;
        }
    }
}