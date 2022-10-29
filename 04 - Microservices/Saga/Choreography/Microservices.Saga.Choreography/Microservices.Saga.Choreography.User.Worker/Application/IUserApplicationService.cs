using Microservices.Saga.Choreography.User.Worker.Models;

namespace Microservices.Saga.Choreography.User.Worker.Application
{
    public interface IUserApplicationService
    {
        void ProcessUser(UserShop userShop);
    }
}