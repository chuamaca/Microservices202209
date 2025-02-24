﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microservice.Events.MessageSubscriber.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.MessagePatterns;
using Microservice.Events.MessageSubscriber.Models;

namespace Microservice.Events.MessageSubscriber.Implementations
{
    public class RabbitMQSubscriber : ISubscriber
    {
        private bool _subscribing;
        private static ConnectionFactory _factory;
        private static IConnection _connection;

        private const string Exchange = "OrderExchange";
        private const string Queue = "OrderQueue";
        private IMessageProcessor _messageProcessor;

        public bool Subscribing
        {
            get { return _subscribing; }
            set { _subscribing = value; }
        }

        public RabbitMQSubscriber(IMessageProcessor messageProcessor)
        {
            _subscribing = true;
            _messageProcessor = messageProcessor;
        }

        public void Subscribe()
        {
            ConnectToMessageBroker();

            using (_connection = _factory.CreateConnection())
            {
                using (var channel = _connection.CreateModel())
                {
                    SetupChannel(channel);

                    var consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume(Queue, false, consumer);

                    Logger.LogInfo("Connected and listening...");
                    while (_subscribing)
                    {
                        GetAMessageFromQueue(consumer, channel);
                    }
                }
            }
        }

        private void GetAMessageFromQueue(QueueingBasicConsumer consumer, IModel channel)
        {
            var m = consumer.Queue.Dequeue();
            var message = (Message)m.Body.DeSerializeFromByteArray(typeof(Message));

            try
            {
                _messageProcessor.Process(message);
                channel.BasicAck(m.DeliveryTag, false);
            } catch (Exception e)
            {
                Logger.LogError(e);
                channel.BasicNack(m.DeliveryTag, false, true);
            }
        }

        private void SetupChannel(IModel channel)
        {
            channel.ExchangeDeclare(Exchange, "direct");
            channel.QueueDeclare(Queue, true, false, false, null);
            channel.QueueBind(Queue, Exchange, "");

            channel.BasicQos(0, 1, false);
        }

        private void ConnectToMessageBroker()
        {
            Logger.LogInfo("Connecting to Message Broker...");
            _factory = new ConnectionFactory { HostName = "localhost", UserName = "test", Password = "test" };
        }

        public void UnSubscribe()
        {
            _subscribing = false;
        }
    }
}
