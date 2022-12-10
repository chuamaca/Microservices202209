using Microservices.Monitoring.Client.MVC.Infrastructure.Agents.Products.Commands;
using Microservices.Monitoring.Client.MVC.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Monitoring.Client.MVC.Infrastructure.Agents.Products
{
    public class ProductsAgent : IProductsAgent
    {
        private ListCommand _listCommand;

        public ProductsAgent(ListCommand listCommand)
        {
            _listCommand = listCommand;
        }

        public List<ProductDto> List()
        {
            return _listCommand.Execute();
        }
    }
}
