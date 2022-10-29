using Microservices.Saga.Choreography.Product.Worker.Infrastructure.Data.Context;
using Microservices.Saga.Choreography.Product.Worker.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservices.Saga.Choreography.Product.Worker.Application
{
    using Microservices.Saga.Choreography.Product.Worker.Infrastructure.Connection;
    using Microservices.Saga.Choreography.Product.Worker.Infrastructure.Connection.RabbitMQ;
    using Microservices.Saga.Choreography.Product.Worker.Infrastructure.Data.Entities;
    using Microservices.Saga.Choreography.Product.Worker.Infrastructure.Enums;
    using Microservices.Saga.Choreography.Product.Worker.Models;
    using System.Text.Json;

    public class ProductApplicationService : IProductApplicationService
    {

        public void ProcessProduct(ProductModel productModel)
        {
            Console.WriteLine("\n");
            Console.WriteLine($"Received Product : {productModel.Name}, Price : {productModel.Price}");

            IProductRepository productRepository = new ProductRepository(new SagaChoreographyContext());
            Product existingProduct = productRepository.FindByNameAndPrice(productModel.Name, productModel.Price);
            UserDetailQueue userDetailQueue = new UserDetailQueue();

            if (existingProduct == null)
            {
                Product newProduct = AddProduct(productModel);
                
                userDetailQueue.ProductId = newProduct.Id;
                userDetailQueue.Product = newProduct;
                userDetailQueue.UserId = productModel.UserId;
                userDetailQueue.User = productModel.User;
                userDetailQueue.IsActive = false;

                List<TransactionHistory> listTransaction = productModel.TransactionList;
                listTransaction.Add(new TransactionHistory()
                {
                    TableName = "[Product]",
                    ID = newProduct.Id,
                    State = TransactionState.Pending,
                    Step = TransactionStep.Product,
                    Type = TransactionType.SqlDB
                });
                userDetailQueue.TransactionList = listTransaction;
            }
            else
            {                
                userDetailQueue.ProductId = existingProduct.Id;
                userDetailQueue.Product = existingProduct;
                userDetailQueue.UserId = productModel.UserId;
                userDetailQueue.User = productModel.User;
                userDetailQueue.IsActive = false;
                userDetailQueue.TransactionList = productModel.TransactionList;
            }

            PublishMessage(userDetailQueue);
        }

        private Product AddProduct(ProductModel product)
        {
            IProductRepository productRepository = new ProductRepository(new SagaChoreographyContext());

            Product newProduct = new Product();
            newProduct.Name = product.Name;
            newProduct.Price = product.Price;
            newProduct.IsActive = false;
            productRepository.Add(newProduct);

            Console.WriteLine($"Save Product : {product.Name}, Price : {product.Price}");

            return newProduct;
        }

        private void PublishMessage(UserDetailQueue userDetailQueue)
        {
            var stocData = JsonSerializer.Serialize(userDetailQueue);
            var message = Encoding.UTF8.GetBytes(stocData);

            IConnectionBus connectionBus = new RabbitMQConnection();
            connectionBus.PublishMessage("Microservices.Saga.Choreography.UserDetail", message);

            Console.WriteLine($"Send UserDetail; User : {userDetailQueue.User.Name} {userDetailQueue.User.Surname}; Product : {userDetailQueue.Product.Name}, Price : {userDetailQueue.Product.Price}");
        }


    }
}
