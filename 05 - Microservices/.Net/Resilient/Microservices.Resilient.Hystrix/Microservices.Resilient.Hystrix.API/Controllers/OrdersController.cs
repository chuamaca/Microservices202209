using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Resilient.Hystrix.API.Infrastructure.Data.Entities;
using Microservices.Resilient.Hystrix.API.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Microservices.Resilient.Hystrix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderRepository _orderRepository;

        public OrdersController(OrderRepository orderRepository)
        {         
            _orderRepository = orderRepository;
        }

        // GET: api/<OrdersController>
        [HttpGet]
        public IEnumerable<Order> Get()
        {
            RandomException();
            return _orderRepository.List();
        }
        [HttpGet("{OrderId}")]
        public Order Get(int orderId)
        {
            RandomException();
            return _orderRepository.Get(orderId);
        }

        private void RandomException()
        {
            var randomNumber = new Random().Next(1, 10);
            if(randomNumber % 2 == 1)
            {
                throw new Exception("Something went wrong");
            }
        }
    }
}
