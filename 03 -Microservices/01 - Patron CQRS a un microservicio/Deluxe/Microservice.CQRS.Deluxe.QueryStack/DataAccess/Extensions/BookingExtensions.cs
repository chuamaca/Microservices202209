using System.Linq;
using Microservice.CQRS.Deluxe.Infrastructure.Persistence.SqlServer.Data;
using Microservice.CQRS.Deluxe.Infrastructure.Persistence.SqlServer.Data.Entities;

namespace Microservice.CQRS.Deluxe.QueryStack.DataAccess.Extensions 
{
    public static class BookingExtensions
    {
        public static IQueryable<Booking> ForCourts(this IQueryable<Booking> bookings, params int[] courtIds)
        {
            return bookings.Where(b => courtIds.Contains(b.CourtId));
        }
    }
}