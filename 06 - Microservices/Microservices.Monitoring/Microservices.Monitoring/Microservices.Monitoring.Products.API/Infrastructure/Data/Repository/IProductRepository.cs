using Microservices.Products.API.Infrastructure.Dto;
using System.Collections.Generic;

namespace Microservices.Monitoring.Products.API.Infrastructure.Data.Repository
{
    public interface IProductRepository
    {
        List<ProductDto> List();
    }
}