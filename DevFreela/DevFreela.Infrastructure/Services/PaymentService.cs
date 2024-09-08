using DevFreela.Infrastructure.MessageBus;
using System.Text;
using System.Text.Json;

namespace DevFreela.Infrastructure.Services {
    public class PaymentService(IMessageBusService messageBusService) : IPaymentService {
        private readonly IMessageBusService _messageBusService = messageBusService;
        private const string QUEUE_NAME = "Payments";

        public void ProcessPayment(PaymentInfoDTO paymentInfoDTO) {
            string paymentInfoJson = JsonSerializer.Serialize(paymentInfoDTO);

            byte[] paymentInfoBytes = Encoding.UTF8.GetBytes(paymentInfoJson);

            this._messageBusService.Publish(QUEUE_NAME, paymentInfoBytes);
        }
    }

    public class PaymentInfoDTO(int idProject, string creditCardNumber, string cvv, string expiresAt, string fullName, decimal amount) {
        public int IdProject { get; private set; } = idProject;
        public string CreditCardNumber { get; private set; } = creditCardNumber;
        public string Cvv { get; private set; } = cvv;
        public string ExpiresAt { get; private set; } = expiresAt;
        public string FullName { get; private set; } = fullName;
        public decimal Amount { get; private set; } = amount;
    }
}