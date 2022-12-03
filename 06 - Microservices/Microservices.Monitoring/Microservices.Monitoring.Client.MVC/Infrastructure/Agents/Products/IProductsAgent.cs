using Microservices.Monitoring.Client.MVC.Infrastructure.Dto;
using System.Collections.Generic;

namespace Microservices.Monitoring.Client.MVC.Infrastructure.Agents.Products
{
    public interface IProductsAgent
    {
        List<ProductDto> List();
    }
}