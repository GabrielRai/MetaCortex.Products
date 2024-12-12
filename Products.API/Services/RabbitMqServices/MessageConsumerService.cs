﻿using MetaCortex.Products.API.Services.Interfaces;
using MetaCortex.Products.DataAccess.RabbitMq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using MetaCortex.Products.API.Services.ProductServices;
using MetaCortex.Products.API.BackgroundServices;

namespace MetaCortex.Products.API.Services.RabbitMqServices
{
    public class MessageConsumerService : IMessageConsumerService
    {
        private const string _queueName = "order-to-products";
        private readonly IConnection _connection;
        private readonly ILogger<MessageConsumerHostedService> _logger;
        private readonly IChannel _channel;
        private readonly ProductService _productServices;

        public MessageConsumerService(IRabbitMqService rabbitMqService, ILogger<MessageConsumerHostedService> logger)
        {
            _connection = rabbitMqService.CreateConnection().Result;
            _channel = _connection.CreateChannelAsync().Result;
            _productServices = new ProductService();
            _logger = logger;

        }

        public async Task ReadFinalOrderAsync()
        {
        
           var consumer = new AsyncEventingBasicConsumer(_channel);
            _logger.LogInformation("Testing.");
            await _channel.QueueDeclareAsync(queue: _queueName,
                               durable: false,
                               exclusive: false,
                               autoDelete: false,
                               arguments: null);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = System.Text.Encoding.UTF8.GetString(body);
                await _productServices.UpdateProductOrderStock(message);
                Console.WriteLine(" [x] Consumed {0}", message, "Consumed");
            };
            await _channel.BasicConsumeAsync(queue: "order-to-products",
                                 autoAck: true,
                                 consumer: consumer);

            await Task.CompletedTask;
        }

        public async Task ReadMessagesAsync(string queueName, Func<string,Task> messageProcessor)
        {
            await _channel.QueueDeclareAsync(queue: _queueName,
             durable: false,
             exclusive: false,
             autoDelete: false
             );

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = System.Text.Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Consumed {0}", message, "Consumed");
            };

            await _channel.BasicConsumeAsync(queue: "product-added",
                                 autoAck: true,
                                 consumer: consumer);

            await Task.CompletedTask;
        }
    }
}
