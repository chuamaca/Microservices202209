using Microservices.Monitoring.Products.API.Infrastructure.Data.Repository;
using Microservices.Products.API.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Monitoring.Products.API.Application
{
    public class ProductApplicationService : IProductApplicationService
    {
        private readonly IProductRepository _orderRepository;

        public ProductApplicationService(IProductRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public List<ProductDto> ListProducts()
        {
            return _orderRepository.List();
        }
    }
}
