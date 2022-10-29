using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Saga.Choreography.User.Worker.Infrastructure.Data.Repositories
{
    using Microservices.Saga.Choreography.User.Worker.Infrastructure.Data.Context;
    using Microservices.Saga.Choreography.User.Worker.Infrastructure.Data.Entities;
    public class UserRepository : IUserRepository
    {
        private readonly SagaChoreographyContext _context;

        public UserRepository(SagaChoreographyContext context)
        {
            _context = context;
        }

        public List<User> List()
        {
            return _context.Users.ToList();
        }

        public User FindByNameAndSurname(string name,string surname)
        {
            return _context.Users.FirstOrDefault(us => us.Name == name && us.Surname == surname);
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}
