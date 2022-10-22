using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservice.Persistence.EFCore.Data.Repositories;
using Microservice.Persistence.Infrastructure.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Persistence.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IOrderRepository _orderRepository;
        public OrdersController(IOrderRepository ordenRepository)
        {
            _orderRepository = ordenRepository;
        }

        [HttpGet]
        public IList<OrderDto> List()
        {
            return _orderRepository.List();
        }
    }
}
