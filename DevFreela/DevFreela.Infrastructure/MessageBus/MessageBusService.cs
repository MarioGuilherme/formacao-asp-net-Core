using DevFreela.Core.Services;
using RabbitMQ.Client;

namespace DevFreela.Infrastructure.MessageBus;

public class MessageBusService : IMessageBusService {
    private readonly ConnectionFactory _factory;

    public MessageBusService() => this._factory = new() { HostName = "localhost", Port = 5672 };

    public void Publish(string queue, byte[] message) {
        using IConnection connection = _factory.CreateConnection();
        using IModel channel = connection.CreateModel();
        // Garantir que a fila esteja criada
        channel.QueueDeclare(queue: queue, durable: false, exclusive: false, autoDelete: false, arguments: null);
        channel.BasicPublish(exchange: string.Empty, routingKey: queue, basicProperties: null, body: message);
    }
}