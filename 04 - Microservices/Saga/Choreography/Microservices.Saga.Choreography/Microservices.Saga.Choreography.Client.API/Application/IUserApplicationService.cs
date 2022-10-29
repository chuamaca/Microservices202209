using Microservices.Saga.Choreography.Client.API.Infrastructure.Data.Entities;
using Microservices.Saga.Choreography.Client.API.Models;
using System.Collections.Generic;

namespace Microservices.Saga.Choreography.Client.API.Application
{
    public interface IUserApplicationService
    {
        string InsertUser(UserShop userShop);
        List<User> ListUsers();
    }
}