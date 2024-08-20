using DevFreela.Core.Services;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace DevFreela.Infrastructure.MessageBus;

public class MessageBusService : IMessageBusService {
    private readonly ConnectionFactory _factory;

    public MessageBusService(IConfiguration configuration) {
        this._factory = new() {
            HostName = "localhost"
        };
    }

    public void Publish(string queue, byte[] message) {
        using var connection = _factory.CreateConnection();
        using var channel = connection.CreateModel();
        // Garantir que a fila esteja criada
        channel.QueueDeclare(queue: queue, durable: false, exclusive: false, autoDelete: false, arguments: null);
        channel.BasicPublish(exchange: string.Empty, routingKey: queue, basicProperties: null, body: message);
    }
}