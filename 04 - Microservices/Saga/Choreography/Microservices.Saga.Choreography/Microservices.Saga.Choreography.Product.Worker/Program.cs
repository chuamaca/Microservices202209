using Microservices.Saga.Choreography.Product.Worker.Application;
using Microservices.Saga.Choreography.Product.Worker.Infrastructure.Connection;
using Microservices.Saga.Choreography.Product.Worker.Infrastructure.Connection.RabbitMQ;
using Microservices.Saga.Choreography.Product.Worker.Models;
using System;
using System.Text.Json;

namespace Microservices.Saga.Choreography.Product.Worker
{
    class Program
    {        
        static void Main(string[] args)
        {            
            IConnectionBus connectionBus = new RabbitMQConnection();


            connectionBus.Received("Microservices.Saga.Choreography.Product", (message) =>
            {
                ProductApplicationService _productApplicationService = new ProductApplicationService();
                ProductModel productModel = JsonSerializer.Deserialize<ProductModel>(message);
                _productApplicationService.ProcessProduct(productModel);
            });

            Console.WriteLine(" Press [enter] to exit Microservices.Saga.Choreography.Product.Worker.");
            Console.ReadLine();
        }        
    }
}
