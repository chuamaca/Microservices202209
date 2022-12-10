using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Microservices.APIGateway.Product.API.Controllers
{
    using Microservices.APIGateway.Product.API.Models;

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private List<Product> _products;
        public ProductsController()
        {
            GenerateProducts();
        }
        
        // GET: api/<ProductsController>
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _products;
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public Product Get(int id)
        {
            return _products.Where(p=>p.ProductId == id).FirstOrDefault();
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

        private void GenerateProducts()
        {
            _products = new List<Product>();

            _products.Add(new Product
            {
                ProductId = 1,
                Description = "Xbox Series X, Console",
                Price = (decimal)499.99,
                Created = DateTime.Now,
                Updated = DateTime.Now
            });
            _products.Add(new Product
            {
                ProductId = 2,
                Description = "NBA 2K21 - Xbox Series X",
                Price = (decimal)69.99,
                Created = DateTime.Now,
                Updated = DateTime.Now
            });
            _products.Add(new Product
            {
                ProductId = 3,
                Description = "Nintendo Switch with Neon Blue and Neon Red Joy‑Con",
                Price = (decimal)458.99,
                Created = DateTime.Now,
                Updated = DateTime.Now
            });
            _products.Add(new Product
            {
                ProductId = 4,
                Description = "The Legend of Zelda: Breath of the Wild - Nintendo Switch",
                Price = (decimal)43.99,
                Created = DateTime.Now,
                Updated = DateTime.Now
            });
            _products.Add(new Product
            {
                ProductId = 5,
                Description = "Marvel's Spider-Man: Miles Morales Launch Edition - PlayStation 4",
                Price = (decimal)52.99,
                Created = DateTime.Now,
                Updated = DateTime.Now
            });
        }
    }
}
