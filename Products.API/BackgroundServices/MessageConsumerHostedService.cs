using MetaCortex.Products.API.Services.Interfaces;
using MetaCortex.Products.API.Services.ProductServices;
using MetaCortex.Products.DataAccess.RabbitMq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

public class MessageConsumerHostedService : BackgroundService
{
    private readonly ILogger<MessageConsumerHostedService> _logger;
    private readonly IMessageConsumerService _messageConsumerService;
    private const string _queueName = "order-to-products";
    private readonly IConnection _connection;
    private readonly IChannel _channel;
    private readonly ProductService _productServices;
    public MessageConsumerHostedService(
        ILogger<MessageConsumerHostedService> logger,
        IMessageConsumerService messageConsumerService,
        IRabbitMqService rabbitMqService)
    {
        _logger = logger;
        _messageConsumerService = messageConsumerService;
        _connection = rabbitMqService.CreateConnection().Result;
        _channel = _connection.CreateChannelAsync().Result;
        _productServices = new ProductService();
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("MessageConsumerHostedService is starting.");

           try
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
                    var message = System.Text.Encoding.UTF8.GetString(body);
                    await _productServices.UpdateProductOrderStock(message);
                    _logger.LogInformation($"{message}");

                };
                await _channel.BasicConsumeAsync(queue: "order-to-products",
                                     autoAck: true,
                                     consumer: consumer);

                
                await Task.CompletedTask;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in MessageConsumerHostedService.");
            }

        await Task.Delay(1000, stoppingToken);

        _logger.LogInformation("MessageConsumerHostedService is stopping.");
    }
}
