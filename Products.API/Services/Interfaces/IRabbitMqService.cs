using RabbitMQ.Client;

namespace MetaCortex.Products.API.Services.Interfaces
{
    public interface IRabbitMqService
    {
        Task<IConnection> CreateConnection();
    }
}
