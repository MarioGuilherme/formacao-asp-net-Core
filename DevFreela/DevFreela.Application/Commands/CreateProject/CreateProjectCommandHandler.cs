using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.CreateProject;

public class CreateProjectCommandHandler(IProjectRepository projectRepository) : IRequestHandler<CreateProjectCommand, int> {
    private readonly IProjectRepository _projectRepository = projectRepository;

    public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken) {
        Project project = new(request.Title, request.Description, request.IdClient, request.IdFreelancer, request.TotalCost);

        await this._projectRepository.AddAsync(project);

        return project.Id;
    }
}