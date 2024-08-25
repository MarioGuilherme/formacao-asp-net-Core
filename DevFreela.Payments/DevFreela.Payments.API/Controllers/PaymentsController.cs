using DevFreela.Payments.API.Models;
using DevFreela.Payments.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.Payments.API.Controllers;

[Route("api/payments")]
public class PaymentsController(IPaymentService paymentService) : ControllerBase {
    private readonly IPaymentService _paymentService = paymentService;

    // Podemos usar o Command, CQRS e todo o conceito de arquitetura limpa
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PaymentInfoInputModel paymentInfoInputModel) { 
        bool result = await this._paymentService.Process(paymentInfoInputModel);

        if (!result) return this.BadRequest();

        return this.NoContent();
    }
}