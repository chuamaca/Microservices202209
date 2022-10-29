using System.Collections.Generic;

namespace Microservices.Saga.Choreography.Product.Worker.Infrastructure.Data.Repositories
{
    public interface IProductRepository
    {
        void Add(Entities.Product product);
        Entities.Product FindByNameAndPrice(string name, decimal? price);
        List<Entities.Product> List();
    }
}