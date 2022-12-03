using Microservices.Monitoring.Products.API.Infrastructure.Data.Contexts;
using Microservices.Products.API.Infrastructure.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Monitoring.Products.API.Infrastructure.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MicroservicesMonitoringContext _microservicesMonitoringContext;
        public ProductRepository(MicroservicesMonitoringContext microservicesMonitoringContext)
        {
            _microservicesMonitoringContext = microservicesMonitoringContext;
        }
        public List<ProductDto> List()
        {
            var query = from p in _microservicesMonitoringContext.Products.Include(p => p.Stocks)
                        join c in _microservicesMonitoringContext.Categories on p.CategoryId equals c.CategoryId
                        join b in _microservicesMonitoringContext.Brands on p.BrandId equals b.BrandId
                        select new ProductDto
                        {
                            ProductId = p.ProductId,
                            ListPrice = p.ListPrice,
                            ModelYear = p.ModelYear,
                            ProductName = p.ProductName,

                            CategoryId = p.CategoryId,
                            Category = new CategoryDto
                            {
                                CategoryId = c.CategoryId,
                                CategoryName = c.CategoryName
                            },

                            BrandId = p.BrandId,
                            Brand = new BrandDto
                            {
                                BrandId = b.BrandId,
                                BrandName = b.BrandName
                            },

                            Stocks = p.Stocks.Select(s => new StockDto { ProductId = s.ProductId, Quantity = s.Quantity, StoreId = s.StoreId }).ToList()
                        };

            List<ProductDto> orders = query.ToList();

            return orders;
        }
    }
}
