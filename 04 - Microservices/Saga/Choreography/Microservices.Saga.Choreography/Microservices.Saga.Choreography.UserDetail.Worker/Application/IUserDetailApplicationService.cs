using Microservices.Saga.Choreography.UserDetail.Worker.Models;

namespace Microservices.Saga.Choreography.UserDetail.Worker.Application
{
    public interface IUserDetailApplicationService
    {
        void ProcessUserDetail(UserDetailQueue userDetailQueue);
    }
}