using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.CreateProject;

public class CreateProjectCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateProjectCommand, int> {
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken) {
        Project project = new(request.Title, request.Description, request.IdClient, request.IdFreelancer, request.TotalCost);
        project.Comments.Add(new("Project was created.", project.Id, request.IdClient));

        await this._unitOfWork.BeginTransactionAsync();

        await this._unitOfWork.Projects.AddAsync(project);
        await this._unitOfWork.CompleteAsync();

        await this._unitOfWork.Skills.AddSkillFromProject(project);
        await this._unitOfWork.CompleteAsync();

        await this._unitOfWork.CommitAsync();

        return project.Id;
    }
}