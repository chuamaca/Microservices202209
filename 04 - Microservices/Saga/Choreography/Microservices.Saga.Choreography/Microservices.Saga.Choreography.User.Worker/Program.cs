using Microservices.Saga.Choreography.User.Worker.Application;
using Microservices.Saga.Choreography.User.Worker.Infrastructure.Connection;
using Microservices.Saga.Choreography.User.Worker.Infrastructure.Connection.RabbitMQ;
using Microservices.Saga.Choreography.User.Worker.Infrastructure.Data.Context;
using Microservices.Saga.Choreography.User.Worker.Models;
using System;
using System.Text.Json;

namespace Microservices.Saga.Choreography.User.Worker
{
    class Program
    {        
        static void Main(string[] args)
        {            
            IConnectionBus connectionBus = new RabbitMQConnection();


            connectionBus.Received("Microservices.Saga.Choreography.User", (message) =>
            {
                UserApplicationService _userApplicationService = new UserApplicationService();
                UserShop userShop = JsonSerializer.Deserialize<UserShop>(message);
                _userApplicationService.ProcessUser(userShop);
            });

            Console.WriteLine(" Press [enter] to exit Microservices.Saga.Choreography.User.Worker.");
            Console.ReadLine();
        }        
    }
}
