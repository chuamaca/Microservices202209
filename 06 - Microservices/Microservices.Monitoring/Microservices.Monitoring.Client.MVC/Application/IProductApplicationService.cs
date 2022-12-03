using Microservices.Monitoring.Client.MVC.Infrastructure.Dto;
using System.Collections.Generic;

namespace Microservices.Monitoring.Client.MVC.Application
{
    public interface IProductApplicationService
    {
        List<ProductDto> ListProducts();
    }
}