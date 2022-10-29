using Microservices.Infrastructure.Crosscutting;

namespace Microservices.Sales.API.MicroServices.Products.Domain
{
    public class Product : ReadObject
    {
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
