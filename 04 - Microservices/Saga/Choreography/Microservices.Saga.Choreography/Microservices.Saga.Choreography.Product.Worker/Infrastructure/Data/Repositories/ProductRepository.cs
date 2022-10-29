using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Saga.Choreography.Product.Worker.Infrastructure.Data.Repositories
{
    using Microservices.Saga.Choreography.Product.Worker.Infrastructure.Data.Context;
    using Microservices.Saga.Choreography.Product.Worker.Infrastructure.Data.Entities;
    public class ProductRepository : IProductRepository
    {
        private readonly SagaChoreographyContext _context;

        public ProductRepository(SagaChoreographyContext context)
        {
            _context = context;
        }

        public List<Product> List()
        {
            return _context.Products.ToList();
        }

        public Product FindByNameAndPrice(string name, decimal? price)
        {
            return _context.Products.FirstOrDefault(p => p.Name == name && p.Price == price);
        }

        public void Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }
    }
}
