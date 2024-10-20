using DevFreela.Application.Notification.ProjectCreated;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.CreateProject;

public class CreateProjectCommandHandler(IUnitOfWork unitOfWork, IMediator mediator) : IRequestHandler<CreateProjectCommand, int> {
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMediator _mediator;

    public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken) {
        Project project = new(request.Title, request.Description, request.IdClient, request.IdFreelancer, request.TotalCost);
        project.Comments.Add(new("Project was created.", project.Id, request.IdClient));

        await this._unitOfWork.BeginTransactionAsync();

        await this._unitOfWork.Projects.AddAsync(project);
        await this._unitOfWork.CompleteAsync();

        await this._unitOfWork.Skills.AddSkillFromProject(project);
        await this._unitOfWork.CompleteAsync();

        await this._unitOfWork.CommitAsync();

        ProjectCreatedNotification projectCreated = new(project.Id, project.Title, project.TotalCost);
        await this._mediator.Publish(projectCreated, cancellationToken);

        return project.Id;
    }
}