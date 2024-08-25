namespace DevFreela.Core.DTOs;

public class PaymentInfoDTO(int idProject, string creditCardNumber, string cvv, string expiresAt, string fullName, decimal amount) {
    public int IdProject { get; private set; } = idProject;
    public string CreditCardNumber { get; private set; } = creditCardNumber;
    public string Cvv { get; private set; } = cvv;
    public string ExpiresAt { get; private set; } = expiresAt;
    public string FullName { get; private set; } = fullName;
    public decimal Amount { get; private set; } = amount;
}