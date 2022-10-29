using Microservices.Saga.Choreography.Client.API.Infrastructure.Data.Context;
using Microservices.Saga.Choreography.Client.API.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Saga.Choreography.Client.API.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SagaChoreographyContext _context;

        public UserRepository(SagaChoreographyContext context)
        {
            _context = context;
        }

        public List<User> ListUsers()
        {
            return _context.Users.ToList();
        }
    }
}
