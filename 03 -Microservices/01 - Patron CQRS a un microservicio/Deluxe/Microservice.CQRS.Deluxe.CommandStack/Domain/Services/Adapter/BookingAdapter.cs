using Microservice.CQRS.Deluxe.CommandStack.Domain.Model;
using Microservice.CQRS.Deluxe.Infrastructure.Persistence.SqlServer.Data;
using Microservice.CQRS.Deluxe.Infrastructure.Persistence.SqlServer.Data.Entities;

namespace Microservice.CQRS.Deluxe.CommandStack.Domain.Services.Adapter
{
    public class Adapter 
    {
        public static Booking ToDataModel(BookingRequest entity)
        {
            var booking = new Booking
            {
                CourtId = entity.CourtId,
                Length = entity.Length,
                Name = entity.Name,
                StartingAt = entity.Hour,
                RequestId = entity.Id.ToString()
            };
            return booking;
        }
    }
}