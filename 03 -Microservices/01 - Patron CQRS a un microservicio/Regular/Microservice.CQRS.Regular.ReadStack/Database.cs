using Microservice.CQRS.Regular.ReadStack.Context;
using Microservice.CQRS.Regular.ReadStack.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microservice.CQRS.Regular.ReadStack
{
    public class Database : IDisposable
    {
        private readonly QueryDbContext _context;
        public Database(QueryDbContext context)
        {
            _context = context;
        }

        public IQueryable<Match> Matches
        {
            get
            {
                return _context.Matches;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
