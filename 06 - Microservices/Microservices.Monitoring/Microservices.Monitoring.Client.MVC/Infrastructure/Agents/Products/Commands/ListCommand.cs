using Microservices.Monitoring.Client.MVC.Infrastructure.Agents.Orders.Commands;
using Microservices.Monitoring.Client.MVC.Infrastructure.Dto;
using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservices.Monitoring.Client.MVC.Infrastructure.Agents.Products.Commands
{
    public class ListCommand:PollyCommand<List<ProductDto>>
    {
        private IProductsClient _productsClient;

        public ListCommand(IProductsClient productsClient)
        {
            _productsClient = productsClient;
        }
        protected override List<ProductDto> Run()
        {
            return _productsClient.List().Result;
        }

        protected override List<ProductDto> RunFallback(Context context)
        {   
            return _productsClient.ListFallback().Result;
        }
    }
}
