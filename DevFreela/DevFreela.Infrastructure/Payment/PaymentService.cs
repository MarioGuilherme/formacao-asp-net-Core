using DevFreela.Core.MessageBus;
using DevFreela.Core.Models;
using DevFreela.Core.Payment;
using System.Text;
using System.Text.Json;

namespace DevFreela.Infrastructure.Payment;

public class PaymentService(IMessageBusService messageBusService) : IPaymentService {
    private readonly IMessageBusService _messageBusService = messageBusService;
    private const string QUEUE_NAME = "Payments";

    public void ProcessPayment(PaymentInfo paymentInfoDTO) {
        string paymentInfoJson = JsonSerializer.Serialize(paymentInfoDTO);

        byte[] paymentInfoBytes = Encoding.UTF8.GetBytes(paymentInfoJson);

        this._messageBusService.Publish(QUEUE_NAME, paymentInfoBytes);
    }
}