using MetaCortex.Products.API.Services.Interfaces;
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
        private string message;

        public MessageConsumerService(IRabbitMqService rabbitMqService, ILogger<MessageConsumerHostedService> logger, ProductService productService)
        {
            _connection = rabbitMqService.CreateConnection().Result;
            _channel = _connection.CreateChannelAsync().Result;
            _productServices = productService;
            _logger = logger;

        }

        public async Task ReadFinalOrderAsync()
        {
        
           var consumer = new AsyncEventingBasicConsumer(_channel);
            await _channel.QueueDeclareAsync(queue: _queueName,
                               durable: false,
                               exclusive: false,
                               autoDelete: false,
                               arguments: null);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                message = System.Text.Encoding.UTF8.GetString(body);

                if (message != null)
                {
                    _logger.LogInformation("[x] Consumed Order, updating stock");
                    await _productServices.UpdateProductOrderStock(message);
                }
            };

            await _channel.BasicConsumeAsync(queue: "order-to-products",
                                 autoAck: true,
                                 consumer: consumer);

            await Task.CompletedTask;
        }
    }
}
