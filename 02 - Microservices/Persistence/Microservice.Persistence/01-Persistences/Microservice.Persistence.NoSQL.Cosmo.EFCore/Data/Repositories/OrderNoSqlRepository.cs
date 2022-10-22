using Microservice.Persistence.NoSQL.Cosmo.EFCore.Data.Contexts;
using Microservice.Persistence.NoSQL.Cosmo.EFCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microservice.Persistence.NoSQL.Cosmo.EFCore.Data.Repositories
{
    public class OrderNoSqlRepository : IOrderNoSqlRepository
    {
        private readonly OrderNoSqlContext _context;
        public OrderNoSqlRepository(OrderNoSqlContext context)
        {
            _context = context;
        }

        public IList<Order> List()
        {
            return _context.Orders.ToList();
        }
    }
}
