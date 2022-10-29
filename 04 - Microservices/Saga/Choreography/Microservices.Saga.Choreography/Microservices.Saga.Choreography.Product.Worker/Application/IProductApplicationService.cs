using Microservices.Saga.Choreography.Product.Worker.Models;

namespace Microservices.Saga.Choreography.Product.Worker.Application
{
    public interface IProductApplicationService
    {
        void ProcessProduct(ProductModel productModel);
    }
}