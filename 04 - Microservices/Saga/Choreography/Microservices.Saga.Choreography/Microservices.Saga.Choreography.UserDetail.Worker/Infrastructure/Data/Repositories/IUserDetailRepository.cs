using System.Collections.Generic;

namespace Microservices.Saga.Choreography.UserDetail.Worker.Infrastructure.Data.Repositories
{
    public interface IUserDetailRepository
    {
        void Add(Entities.UserDetail UserDetail);
        List<Entities.UserDetail> List();
    }
}