using Microservices.Sales.API.Infrastructure.Dto;
using System.Collections.Generic;

namespace Microservices.Monitoring.Sales.API.Application
{
    public interface ISaleApplicationService
    {
        List<OrderDto> ListOrders();
    }
}