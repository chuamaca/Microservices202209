using Microservices.Products.ReadModels.Client;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Microservices.Sales.API.MicroServices.Products.View
{
    public class ProductView
    {
        private readonly IProductsView ProductsProducts;
        private readonly ConcurrentDictionary<Guid, Domain.Product> products = new ConcurrentDictionary<Guid, Domain.Product>();

        public ProductView(IProductsView ProductsProductView)
        {
            ProductsProducts = ProductsProductView;
            if (products.Count == 0)
            {
                InitialiseProducts();
            }
        }

        public ProductView() : this(new ProductsView()) { }

        public IEnumerable<Domain.Product> GetAll()
        {
            return products.Values.AsEnumerable();
        }

        private void InitialiseProducts()
        {
            var transformedDtos = ProductsProducts.GetProducts().Select(p => new Domain.Product
            {
                Id = p.Id,
                Price = p.Price
            });
            foreach (var p in transformedDtos)
            {
                products.TryAdd(p.Id, p);
            }
        }

        internal void Add(Guid id, decimal price)
        {
            var success = products.TryAdd(id, new Domain.Product() { Id = id, Price = price });
            if (!success)
            {
                //Assume product is already present from a previous event (not sure if it's possible)
                //So we'll just update the price instead.
                var p = GetById(id);
                p.Price = price;
            }
        }

        public Domain.Product GetById(Guid id)
        {
            Domain.Product product;
            if (products.TryGetValue(id, out product)) return product;

            throw new ArgumentOutOfRangeException("id","A product with the id of " + id.ToString() + "couldn't be found");
        }
    }
}
