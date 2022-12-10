using Microservices.Monitoring.Client.MVC.Infrastructure.Dto;
using Microsoft.Extensions.Configuration;
using Steeltoe.Common.Discovery;
using Steeltoe.Discovery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Microservices.Monitoring.Client.MVC.Infrastructure.Agents.Products
{
    public class ProductsClient : IProductsClient
    {
        private readonly IDiscoveryClient _discoveryClient;
        private readonly IConfiguration _configuration;

        public ProductsClient(IConfiguration configuration, IDiscoveryClient discoveryClient)
        {
            _discoveryClient = discoveryClient;
            _configuration = configuration;
        }
        public async Task<List<ProductDto>> List()
        {
            var Client = GetClient();
            var result = await Client.GetStringAsync("");                
            var products = JsonSerializer.Deserialize<List<ProductDto>>(result);
            return products;            
        }

        public async Task<List<ProductDto>> ListFallback()
        {
            List<ProductDto> products = new List<ProductDto> { new ProductDto { ProductId = 0, ProductName = "From Fallback" } };
            return await Task.FromResult(products);
        }

        private HttpClient GetClient()
        {
            var handler = new DiscoveryHttpClientHandler(_discoveryClient);
            //Certificado no valido
            handler.ServerCertificateCustomValidationCallback = delegate { return true; };
            var httpClient = new HttpClient(handler, false)
            {
                BaseAddress = new Uri(_configuration.GetValue<string>("ProductsServiceURL"))
            };

            return httpClient;
        }
    }
}
