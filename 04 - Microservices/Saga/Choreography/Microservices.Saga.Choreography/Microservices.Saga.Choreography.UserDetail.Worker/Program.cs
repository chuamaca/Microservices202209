using Microservices.Saga.Choreography.UserDetail.Worker.Application;
using Microservices.Saga.Choreography.UserDetail.Worker.Infrastructure.Connection;
using Microservices.Saga.Choreography.UserDetail.Worker.Infrastructure.Connection.RabbitMQ;
using Microservices.Saga.Choreography.UserDetail.Worker.Models;
using System;
using System.Text.Json;

namespace Microservices.Saga.Choreography.UserDetail.Worker
{
    class Program
    {        
        static void Main(string[] args)
        {            
            IConnectionBus connectionBus = new RabbitMQConnection();


            connectionBus.Received("Microservices.Saga.Choreography.UserDetail", (message) =>
            {
                UserDetailApplicationService _userDetailApplicationService = new UserDetailApplicationService();
                UserDetailQueue userDetailQueue = JsonSerializer.Deserialize<UserDetailQueue>(message);
                _userDetailApplicationService.ProcessUserDetail(userDetailQueue);
            });

            Console.WriteLine(" Press [enter] to exit Microservices.Saga.Choreography.UserDetail.Worker.");
            Console.ReadLine();
        }        
    }
}
