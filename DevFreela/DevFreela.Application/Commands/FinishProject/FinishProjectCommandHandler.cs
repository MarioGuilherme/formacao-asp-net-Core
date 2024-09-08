using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Services;
using MediatR;

namespace DevFreela.Application.Commands.FinishProject;

public class FinishProjectCommandHandler(IProjectRepository projectRepository, IPaymentService paymentService) : IRequestHandler<FinishProjectCommand, bool> {
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly IPaymentService _paymentService = paymentService;

    public async Task<bool> Handle(FinishProjectCommand request, CancellationToken cancellationToken) {
        Project project = await this._projectRepository.GetByIdAsync(request.Id);

        PaymentInfoDTO paymentInfoDto = new(request.Id, request.CreditCardNumber, request.Cvv, request.ExpiresAt, request.FullName, project.TotalCost);

        this._paymentService.ProcessPayment(paymentInfoDto);

        project.SetPaymentPending();

        await this._projectRepository.UpdateAsync(project);

        return true;
    }
}
