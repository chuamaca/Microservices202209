using Microservice.CQRS.Deluxe.CommandStack.Domain.Model;
using Microservice.CQRS.Deluxe.Infrastructure.Persistence.SqlServer.Data.Entities;

namespace Microservice.CQRS.Deluxe.Infrastructure.Persistence.SqlServer.Repositories.Adapters
{
    public class Adapter 
    {
        public static Booking RequestToBooking(BookingRequest entity)
        {
            var booking = new Booking
            {
                CourtId = entity.CourtId,
                Length = entity.Length,
                Name = entity.Name,
                StartingAt = entity.Hour,
                RequestId = entity.Id.ToString(),
            };
            return booking;
        }
    }
}