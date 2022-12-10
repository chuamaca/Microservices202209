using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Monitoring.Sales.API.Application;
using Microservices.Sales.API.Infrastructure.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Microservices.Monitoring.Sales.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISaleApplicationService _saleApplicationService;
        private readonly ILogger<SalesController> _logger;

        public SalesController(ISaleApplicationService saleApplicationService, ILogger<SalesController> logger)
        {
            _saleApplicationService = saleApplicationService;
            _logger = logger;
        }
        // GET: api/<SalesController>
        [HttpGet]
        public IEnumerable<OrderDto> Get()
        {
            _logger.LogInformation("Start calling List Sales");
            var correlationId = Request.Headers["CorrelationId"].ToString();
            var orders = _saleApplicationService.ListOrders();
            _logger.LogInformation($"Count Orders : {orders.Count}");
            _logger.LogInformation("End calling List Sales");
            return orders;
        }

        // GET api/<SalesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
    }
}
