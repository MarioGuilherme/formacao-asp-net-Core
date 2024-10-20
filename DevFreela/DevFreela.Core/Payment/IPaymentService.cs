using DevFreela.Core.Models;

namespace DevFreela.Core.Payment;

public interface IPaymentService {
    void ProcessPayment(PaymentInfo paymentInfoDTO);
}