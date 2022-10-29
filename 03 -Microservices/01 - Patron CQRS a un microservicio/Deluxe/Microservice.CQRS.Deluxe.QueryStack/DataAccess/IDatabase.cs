using System.Linq;
using Microservice.CQRS.Deluxe.Infrastructure.Persistence.SqlServer.Data;
using Microservice.CQRS.Deluxe.Infrastructure.Persistence.SqlServer.Data.Entities;

namespace Microservice.CQRS.Deluxe.QueryStack.DataAccess
{
    public interface IDatabase
    {
        IQueryable<Booking> Bookings { get; }
        IQueryable<Court> Courts { get; }
    }
}