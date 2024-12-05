using MetaCortex.Products.DataAccess.RabbitMq;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;

namespace MetaCortex.Products.API.Services.RabbitMqServices
{
    public class MessageProducerService(RabbitMqConfiguration config) : IMessageProducerService
    {
        private readonly ConnectionFactory _connectionFactory = new ConnectionFactory
        {
            HostName = config.HostName,
            UserName = config.UserName,
            Password = config.Password
        };
        public async Task SendProductAsync<T>(T message)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "product-to-customer",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);


            var jsonString = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonString);

            await channel.BasicPublishAsync(exchange: "",
                                 routingKey: "product-to-customer",
                                 body: body);
        }
    }
}
