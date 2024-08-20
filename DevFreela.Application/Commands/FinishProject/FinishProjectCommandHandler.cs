using DevFreela.Core.DTOs;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using MediatR;

namespace DevFreela.Application.Commands.FinishProject;

public class FinishProjectCommandHandler : IRequestHandler<FinishProjectCommand, bool> {
    private readonly IProjectRepository _projectRepository;
    private readonly IPaymentService _paymentService;

    public FinishProjectCommandHandler(IProjectRepository projectRepository, IPaymentService paymentService) {
        this._projectRepository = projectRepository;
        this._paymentService = paymentService;
    }

    public async Task<bool> Handle(FinishProjectCommand request, CancellationToken cancellationToken) {
        Project project = await this._projectRepository.GetDetailsByIdAsync(request.Id);
        project.Finish();

        PaymentInfoDTO paymentInfoDto = new(request.Id, request.CreditCardNumber, request.Cvv, request.ExpiresAt, request.FullName, project.TotalCost);

        bool result = await this._paymentService.ProcessPayment(paymentInfoDto);

        if (!result)
            project.SetPaymentPending();

        await this._projectRepository.SaveChangesAsync();
        return result;
    }
}
