using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using DevFreela.Core.Repositories;
using DevFreela.Core.Entities;
using DevFreela.Application.IntegrationEvents;

namespace DevFreela.Application.Consumers;

public class PaymentApprovedConsumer : BackgroundService {
    private const string PAYMENTS_APROVED_QUEUE = "PaymentsApproved";
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly IServiceProvider _serviceProvider;

    // Quando executar a aplicação, a fila será criada
    public PaymentApprovedConsumer(IServiceProvider serviceProvider) {
        this._serviceProvider = serviceProvider;
        ConnectionFactory factory = new() { HostName = "localhost", Port = 5672 };
        this._connection = factory.CreateConnection();
        this._channel = this._connection.CreateModel();
        this._channel.QueueDeclare(queue: PAYMENTS_APROVED_QUEUE, durable: false, exclusive: false, autoDelete: false, arguments: null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken) {
        EventingBasicConsumer consumer = new(this._channel);

        consumer.Received += async (sender, eventArgs) => {
            byte[] paymentApprovedBytes = eventArgs.Body.ToArray();
            string paymentApprovedJson = Encoding.UTF8.GetString(paymentApprovedBytes);

            PaymentApprovedIntegrationEvent paymentInfo = JsonSerializer.Deserialize<PaymentApprovedIntegrationEvent>(paymentApprovedJson)!;

            await this.FinishProject(paymentInfo.IdProject);
            
            this._channel.BasicAck(eventArgs.DeliveryTag, false);
        };

        this._channel.BasicConsume(PAYMENTS_APROVED_QUEUE, false, consumer);

        return Task.CompletedTask;
    }

    public async Task FinishProject(int id) {
        using IServiceScope scope = this._serviceProvider.CreateScope();
        IProjectRepository projectRepository = scope.ServiceProvider.GetRequiredService<IProjectRepository>();
        Project project = await projectRepository.GetDetailsByIdAsync(id);
        project.Finish();
        await projectRepository.SaveChangesAsync();
    }
}