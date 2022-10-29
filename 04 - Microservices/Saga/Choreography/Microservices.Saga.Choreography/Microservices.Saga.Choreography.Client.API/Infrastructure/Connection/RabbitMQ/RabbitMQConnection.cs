using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Saga.Choreography.Client.API.Infrastructure.Connection.RabbitMQ
{
    public class RabbitMQConnection:IConnectionBus
    {
        private readonly IConnectionFactory _connectionFactory;
        private object userShop;

        public RabbitMQConnection()
        {
            _connectionFactory = new ConnectionFactory() { HostName = "localhost" };
        }

        public void PublishMessage(string queue,byte[] message)
        {           
            using (var connection = _connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queue,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                channel.BasicPublish(exchange: "",
                                     routingKey: queue,
                                     basicProperties: null,
                                     body: message);
            }
        }
    }
}
