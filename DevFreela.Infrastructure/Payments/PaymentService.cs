using DevFreela.Core.DTOs;
using DevFreela.Core.Services;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace DevFreela.Infrastructure.Payments;
public class PaymentService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : IPaymentService {
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly string _paymentsBaseUrl = configuration.GetSection("Services:Payments").Value;

    public async Task<bool> ProcessPayment(PaymentInfoDTO paymentInfoDTO) {
        string url = $"{this._paymentsBaseUrl}/api/payments";
        string paymentInfoJson = JsonSerializer.Serialize(paymentInfoDTO);
        StringContent paymentInfoContent = new(paymentInfoJson, Encoding.UTF8, "application/json");
        HttpClient httpClient = this._httpClientFactory.CreateClient("Payments");
        HttpResponseMessage response = await httpClient.PostAsync(url, paymentInfoContent);

        return response.IsSuccessStatusCode;
    }
}