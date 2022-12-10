using Microservices.Monitoring.Client.MVC.Infrastructure.Agents.Products;
using Microservices.Monitoring.Client.MVC.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Monitoring.Client.MVC.Application
{
    public class ProductApplicationService : IProductApplicationService
    {
        private readonly IProductsAgent _productsAgent;
        public ProductApplicationService(IProductsAgent productsAgent)
        {
            _productsAgent = productsAgent;
        }

        public List<ProductDto> ListProducts()
        {
            return _productsAgent.List();
        }
    }
}
