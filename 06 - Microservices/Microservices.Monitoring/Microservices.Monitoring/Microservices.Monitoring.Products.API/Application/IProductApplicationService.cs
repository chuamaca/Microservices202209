using Microservices.Products.API.Infrastructure.Dto;
using System.Collections.Generic;

namespace Microservices.Monitoring.Products.API.Application
{
    public interface IProductApplicationService
    {
        List<ProductDto> ListProducts();
    }
}