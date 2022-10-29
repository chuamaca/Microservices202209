using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Saga.Choreography.UserDetail.Worker.Infrastructure.Data.Repositories
{
    using Microservices.Saga.Choreography.UserDetail.Worker.Infrastructure.Data.Context;
    using Microservices.Saga.Choreography.UserDetail.Worker.Infrastructure.Data.Entities;
    public class UserDetailRepository : IUserDetailRepository
    {
        private readonly SagaChoreographyContext _context;

        public UserDetailRepository(SagaChoreographyContext context)
        {
            _context = context;
        }

        public List<UserDetail> List()
        {
            return _context.UserDetails.ToList();
        }

        public void Add(UserDetail UserDetail)
        {
            _context.UserDetails.Add(UserDetail);
            _context.SaveChanges();
        }
    }
}
