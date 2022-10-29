using System.Collections.Generic;

namespace Microservices.Saga.Choreography.User.Worker.Infrastructure.Data.Repositories
{
    using Microservices.Saga.Choreography.User.Worker.Infrastructure.Data.Entities;
    public interface IUserRepository
    {
        List<User> List();
        User FindByNameAndSurname(string name, string surname);
        void Add(User user);
    }
}