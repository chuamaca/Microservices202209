using Microservices.Saga.Choreography.User.Worker.Infrastructure.Data.Context;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Microservices.Saga.Choreography.User.Worker.Infrastructure.Connection.RabbitMQ
{
    using Microservices.Saga.Choreography.User.Worker.Infrastructure.Data.Entities;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;

    public class RabbitMQConnection:IConnectionBus
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMQConnection()
        {
            _connectionFactory = new ConnectionFactory() { HostName = "localhost" };
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void Received(string queue, Action<string> action)
        {
           
            
            _channel.QueueDeclare(queue: queue,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.Span;
                var message = Encoding.UTF8.GetString(body);
                action(message);
            };

            _channel.BasicConsume(queue: queue,
                                 autoAck: true,
                                 consumer: consumer);            
        }

        private Byte[] SerializeMessage<T>(T data)
        {
            string stocData = JsonSerializer.Serialize(data);            
            byte[] message = Encoding.UTF8.GetBytes(stocData);

            return message;
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
