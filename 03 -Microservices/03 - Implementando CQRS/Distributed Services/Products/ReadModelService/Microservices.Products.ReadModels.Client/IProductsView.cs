using Microservices.Products.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservices.Products.ReadModels.Client
{
    public interface IProductsView
    {
        ProductDto GetById(Guid id);
        IEnumerable<ProductDto> GetProducts();
        void Initialize();
        void Reset();
        void UpdateLocalCache(ProductDto newValue);
    }
}
