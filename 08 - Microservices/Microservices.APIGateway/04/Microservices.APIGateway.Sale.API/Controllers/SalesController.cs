using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.APIGateway.Sale.API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Microservices.APIGateway.Sale.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private List<Order> _orders;

        public SalesController()
        {
            GenerateOrders();
        }
        // GET: api/<SalesController>
        [HttpGet]
        public IEnumerable<Order> Get()
        {
            return _orders;
        }

        // GET api/<SalesController>/5
        [HttpGet("{id}")]
        public Order Get(int id)
        {
            return _orders.Where(o => o.OrderId == id).FirstOrDefault();
        }

        // POST api/<SalesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SalesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SalesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private void GenerateOrders()
        {
            _orders = new List<Order>();

            _orders.Add(new Order
            {
                OrderId = 1,
                Description = "Buying Xbox Products",
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Items = new List<OrderItem>
                {
                    new OrderItem{ OrderId = 1,ProductId=1,Created=DateTime.Now,Updated=DateTime.Now },
                    new OrderItem{ OrderId = 1,ProductId=2,Created=DateTime.Now,Updated=DateTime.Now }
                }
            });

            _orders.Add(new Order
            {
                OrderId = 2,
                Description = "Buying Nintendo Switch Products",
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Items = new List<OrderItem>
                {
                    new OrderItem{ OrderId = 2,ProductId=3,Created=DateTime.Now,Updated=DateTime.Now },
                    new OrderItem{ OrderId = 2,ProductId=4,Created=DateTime.Now,Updated=DateTime.Now }
                }
            });

            _orders.Add(new Order
            {
                OrderId = 3,
                Description = "Buying Play Station Products",
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Items = new List<OrderItem>
                {
                    new OrderItem{ OrderId = 3,ProductId=3,Created=DateTime.Now,Updated=DateTime.Now }                    
                }
            });
        }
    }
}
