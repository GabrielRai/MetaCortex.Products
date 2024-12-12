using MetaCortex.Products.DataAccess.RabbitMq;

public class MessageConsumerHostedService : BackgroundService
{
    private readonly ILogger<MessageConsumerHostedService> _logger;
    private readonly IMessageConsumerService _messageConsumerService;

    public MessageConsumerHostedService(
        ILogger<MessageConsumerHostedService> logger,
        IMessageConsumerService messageConsumerService)
    {
        _logger = logger;
        _messageConsumerService = messageConsumerService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("MessageConsumerHostedService is starting.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await _messageConsumerService.ReadFinalOrderAsync();
                _logger.LogInformation("Message consumed successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in MessageConsumerHostedService.");
            }

            // Lägg till en liten fördröjning för att undvika tight loop vid fel
            await Task.Delay(1000, stoppingToken);
        }

        _logger.LogInformation("MessageConsumerHostedService is stopping.");
    }
}
