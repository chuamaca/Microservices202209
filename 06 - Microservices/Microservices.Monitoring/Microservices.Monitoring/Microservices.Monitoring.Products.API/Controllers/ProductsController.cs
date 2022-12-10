using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Monitoring.Products.API.Application;
using Microservices.Products.API.Infrastructure.Dto;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Microservices.Monitoring.Products.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductApplicationService _productApplicationService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductApplicationService productApplicationService, ILogger<ProductsController> logger)
        {
            _productApplicationService = productApplicationService;
            _logger = logger;
        }
        // GET: api/<ProductsController>
        [HttpGet]
        public IEnumerable<ProductDto> Get()
        {
            _logger.LogInformation("Start calling List Products");
            var correlationId = Request.Headers["CorrelationId"].ToString();
            var products = _productApplicationService.ListProducts();
            _logger.LogInformation($"Count Products : {products.Count}");
            _logger.LogInformation("End calling List Products");

            return products;
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProductsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
