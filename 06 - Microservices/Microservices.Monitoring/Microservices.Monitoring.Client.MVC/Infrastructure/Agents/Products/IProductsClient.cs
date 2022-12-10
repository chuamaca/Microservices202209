using Microservices.Monitoring.Client.MVC.Infrastructure.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microservices.Monitoring.Client.MVC.Infrastructure.Agents.Products
{
    public interface IProductsClient
    {
        Task<List<ProductDto>> List();
        Task<List<ProductDto>> ListFallback();
    }
}