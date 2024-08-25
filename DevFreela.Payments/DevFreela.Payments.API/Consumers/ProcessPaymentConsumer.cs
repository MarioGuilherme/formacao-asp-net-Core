using DevFreela.Payments.API.Models;
using DevFreela.Payments.API.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace DevFreela.Payments.API.Consumers;

public class ProcessPaymentConsumer : BackgroundService {
    private const string QUEUE = "Payments";
    private const string PAYMENTS_APROVED_QUEUE = "PaymentsApproved";
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly IServiceProvider _serviceProvider;

    public ProcessPaymentConsumer(IServiceProvider serviceProvider) {
        this._serviceProvider = serviceProvider;
        ConnectionFactory factory = new() { HostName = "localhost", Port = 5672 };
        this._connection = factory.CreateConnection();
        this._channel = this._connection.CreateModel();
        this._channel.QueueDeclare(queue: QUEUE, durable: false, exclusive: false, autoDelete: false, arguments: null);
        this._channel.QueueDeclare(queue: PAYMENTS_APROVED_QUEUE, durable: false, exclusive: false, autoDelete: false, arguments: null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken) {
        EventingBasicConsumer consumer = new(this._channel);

        consumer.Received += (sender, eventArgs) => {
            byte[] bytes = eventArgs.Body.ToArray();
            string paymentInfoJson = Encoding.UTF8.GetString(bytes);

            PaymentInfoInputModel paymentInfo = JsonSerializer.Deserialize<PaymentInfoInputModel>(paymentInfoJson)!;

            this.ProcessPayment(paymentInfo);

            PaymentApprovedIntegrationEvent paymentApproved = new(paymentInfo.IdProject);
            string paymentApprovedJson = JsonSerializer.Serialize(paymentApproved);
            byte[] paymentApprovedBytes = Encoding.UTF8.GetBytes(paymentApprovedJson);

            this._channel.BasicPublish(
                exchange: string.Empty,
                routingKey: PAYMENTS_APROVED_QUEUE,
                basicProperties: null,
                body: paymentApprovedBytes
            );

            this._channel.BasicAck(eventArgs.DeliveryTag, false);
        };

        this._channel.BasicConsume(QUEUE, false, consumer);

        return Task.CompletedTask;
    }

    public void ProcessPayment(PaymentInfoInputModel paymentInfo) {
        using IServiceScope scope = this._serviceProvider.CreateScope();
        IPaymentService paymentService = scope.ServiceProvider.GetRequiredService<IPaymentService>();
        paymentService.Process(paymentInfo);
    }
}