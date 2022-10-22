using Microservice.Persistence.Infrastructure.Dto;
using System.Collections.Generic;

namespace Microservice.Persistence.EFCore.Data.Repositories
{
    public interface IOrderRepository
    {
        IList<OrderDto> List();
    }
}