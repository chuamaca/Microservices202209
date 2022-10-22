using Microservice.Persistence.NoSQL.Cosmo.EFCore.Data.Entities;
using System.Collections.Generic;

namespace Microservice.Persistence.NoSQL.Cosmo.EFCore.Data.Repositories
{
    public interface IOrderNoSqlRepository
    {
        IList<Order> List();
    }
}