using Microservices.Saga.Choreography.Client.API.Infrastructure.Data.Entities;
using System.Collections.Generic;

namespace Microservices.Saga.Choreography.Client.API.Infrastructure.Data.Repositories
{
    public interface IUserRepository
    {
        List<User> ListUsers();
    }
}