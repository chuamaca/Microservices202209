using Microservices.Monitoring.Sales.API.Infrastructure.Data.Repository;
using Microservices.Sales.API.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Monitoring.Sales.API.Application
{
    public class SaleApplicationService : ISaleApplicationService
    {
        private readonly IOrderRepository _orderRepository;

        public SaleApplicationService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public List<OrderDto> ListOrders()
        {
            return _orderRepository.List();
        }
    }
}
