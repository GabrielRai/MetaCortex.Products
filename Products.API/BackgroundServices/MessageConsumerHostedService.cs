using MetaCortex.Products.DataAccess.RabbitMq;

namespace MetaCortex.Products.API.BackgroundServices
{
    public class MessageConsumerHostedService : BackgroundService
    {
        private readonly IMessageConsumerService _messageConsumerService;
        public MessageConsumerHostedService(IMessageConsumerService messageConsumerService)
        {
            _messageConsumerService = messageConsumerService;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("MessageConsumerHostedService is starting.");
            await _messageConsumerService.ReadFinalOrderAsync();
        }
    }
}
