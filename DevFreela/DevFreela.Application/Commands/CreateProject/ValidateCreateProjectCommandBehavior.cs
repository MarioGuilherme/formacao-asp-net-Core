using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.ViewModels;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.InsertProject;

public class ValidateCreateProjectCommandBehavior(IUnitOfWork unitOfWork) : IPipelineBehavior<CreateProjectCommand, ResultViewModel<int>> {
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ResultViewModel<int>> Handle(CreateProjectCommand request, RequestHandlerDelegate<ResultViewModel<int>> next, CancellationToken cancellationToken) {
        // var res = await next(); O processamento do Handler vai ser no instante da invocação do método next(),
        bool clientExists = (await this._unitOfWork.Users.GetByIdAsync(request.IdClient)) is not null;
        bool freelancerExists = (await this._unitOfWork.Users.GetByIdAsync(request.IdFreelancer)) is not null;

        if (!clientExists || !freelancerExists)
            return ResultViewModel<int>.Error("Cliente ou Freelancer inválidos.");

        return await next();
    }
}
