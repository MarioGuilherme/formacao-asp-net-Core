using DevFreela.Core.DTOs;
using DevFreela.Core.Services;
using System.Text;
using System.Text.Json;

namespace DevFreela.Infrastructure.Payments;

public class PaymentService(IMessageBusService messageBusService) : IPaymentService {
    private readonly IMessageBusService _messageBusService = messageBusService;
    private const string QUEUE_NAME = "Payments";

    public void ProcessPayment(PaymentInfoDTO paymentInfoDTO) {
        string paymentInfoJson = JsonSerializer.Serialize(paymentInfoDTO);

        byte[] paymentInfoBytes = Encoding.UTF8.GetBytes(paymentInfoJson);

        this._messageBusService.Publish(QUEUE_NAME, paymentInfoBytes);
    }
}