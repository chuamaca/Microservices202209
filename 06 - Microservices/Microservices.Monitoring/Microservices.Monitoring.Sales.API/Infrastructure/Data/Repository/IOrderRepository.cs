using Microservices.Sales.API.Infrastructure.Dto;
using System.Collections.Generic;

namespace Microservices.Monitoring.Sales.API.Infrastructure.Data.Repository
{
    public interface IOrderRepository
    {
        List<OrderDto> List();
    }
}