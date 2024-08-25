using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.UpdateProject;

public class UpdateProjectCommandHandler(IProjectRepository projectRepository) : IRequestHandler<UpdateProjectCommand, Unit> {
    private readonly IProjectRepository _projectRepository = projectRepository;

    public async Task<Unit> Handle(UpdateProjectCommand request, CancellationToken cancellationToken) {
        Project project = await this._projectRepository.GetDetailsByIdAsync(request.Id);
        project.Update(request.Title, request.Description, request.TotalCost);
        await this._projectRepository.SaveChangesAsync();
        return Unit.Value;
    }
}