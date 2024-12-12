using MetaCortex.Products.DataAccess.RabbitMq;

namespace MetaCortex.Products.API.BackgroundServices
{
    public class MessageConsumerHostedService : BackgroundService
    {
        private readonly IMessageConsumerService _messageConsumerService;
        private readonly ILogger<MessageConsumerHostedService> _logger;
        public MessageConsumerHostedService(IMessageConsumerService messageConsumerService, ILogger<MessageConsumerHostedService> logger)
        {
            _messageConsumerService = messageConsumerService;
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var queue = "order-to-products";
            _logger.LogInformation("MessageConsumerHostedService is starting.");
            await _messageConsumerService.ReadFinalOrderAsync(queue);
        }
    }
}
