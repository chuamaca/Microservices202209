using Microservices.Saga.Choreography.User.Worker.Infrastructure.Data.Context;
using Microservices.Saga.Choreography.User.Worker.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservices.Saga.Choreography.User.Worker.Application
{
    using Microservices.Saga.Choreography.User.Worker.Infrastructure.Connection;
    using Microservices.Saga.Choreography.User.Worker.Infrastructure.Connection.RabbitMQ;
    using Microservices.Saga.Choreography.User.Worker.Infrastructure.Data.Entities;
    using Microservices.Saga.Choreography.User.Worker.Infrastructure.Enums;
    using Microservices.Saga.Choreography.User.Worker.Models;
    using System.Text.Json;

    public class UserApplicationService : IUserApplicationService
    {

        public void ProcessUser(UserShop userShop)
        {
            Console.WriteLine("\n");
            Console.WriteLine($"Received User : {userShop.name} {userShop.surname}");

            IUserRepository userRepository = new UserRepository(new SagaChoreographyContext());
            User existingUser = userRepository.FindByNameAndSurname(userShop.name, userShop.surname);
            ProductModel product = new ProductModel();

            if (existingUser == null)
            {
                User newUser = AddUser(userShop);

                product.Name = userShop.productName;
                product.Price = userShop.productPrice;
                product.UserId = newUser.Id;
                product.User = newUser;
                product.IsActive = false;

                List<TransactionHistory> listTransaction = new List<TransactionHistory>();
                listTransaction.Add(new TransactionHistory()
                {
                    TableName = "[User]",
                    ID = newUser.Id,
                    State = TransactionState.Pending,
                    Step = TransactionStep.User,
                    Type = TransactionType.SqlDB
                });
                product.TransactionList = listTransaction;
            }
            else
            {                
                product.UserId = existingUser.Id;
                product.User = existingUser;
                product.Name = userShop.productName;
                product.Price = userShop.productPrice;                
                product.IsActive = false;                                
                product.TransactionList = new List<TransactionHistory>();
            }

            PublishMessage(product);
        }

        private User AddUser(UserShop user)
        {
            IUserRepository userRepository = new UserRepository(new SagaChoreographyContext());

            User newUser = new User();
            newUser.Name = user.name;
            newUser.Surname = user.surname;
            newUser.No = user.no;
            newUser.IsActive = false;
            userRepository.Add(newUser);

            Console.WriteLine($"Save User : {newUser.Name} {newUser.Surname}");

            return newUser;
        }

        private void PublishMessage(ProductModel product)
        {
            var stocData = JsonSerializer.Serialize(product);
            var message = Encoding.UTF8.GetBytes(stocData);

            IConnectionBus connectionBus = new RabbitMQConnection();
            connectionBus.PublishMessage("Microservices.Saga.Choreography.Product", message);

            Console.WriteLine($"Send Product : {product.Name}, price : {product.Price}");
        }


    }
}
