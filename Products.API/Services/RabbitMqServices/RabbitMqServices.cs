using MetaCortex.Products.API.Services.Interfaces;
using MetaCortex.Products.DataAccess.RabbitMq;
using RabbitMQ.Client;

namespace MetaCortex.Products.API.Services.RabbitMqServices
{
    public class RabbitMqService(RabbitMqConfiguration config) : IRabbitMqService
    {
        public Task<IConnection> CreateConnection()
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = config.HostName,
                UserName = config.UserName,
                Password = config.Password
            };
            var connection = connectionFactory.CreateConnectionAsync();
            return connection;
        }
    }
}
