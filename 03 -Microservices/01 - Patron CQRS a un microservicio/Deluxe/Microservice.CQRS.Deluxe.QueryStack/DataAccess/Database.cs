using System.Linq;
using Microservice.CQRS.Deluxe.Infrastructure.Persistence.SqlServer.Data;
using Microservice.CQRS.Deluxe.Infrastructure.Persistence.SqlServer.Data.Context;
using Microservice.CQRS.Deluxe.Infrastructure.Persistence.SqlServer.Data.Entities;

namespace Microservice.CQRS.Deluxe.QueryStack.DataAccess
{
    public class Database : IDatabase
    {
        private readonly MerloContext _merloContext;
        public Database(MerloContext merloContext)
        {
            _merloContext = merloContext;
        }

        public IQueryable<Booking> Bookings
        {
            get
            {
                return _merloContext.Bookings; 
            }
        }

        public IQueryable<Court> Courts
        {
            get
            {
                return _merloContext.Courts;
            }
        }
    }
}