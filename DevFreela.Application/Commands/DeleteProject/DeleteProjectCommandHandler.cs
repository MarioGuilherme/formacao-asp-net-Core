using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.DeleteProject;

public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, Unit> {
    private readonly IProjectRepository _projectRepository;

    public DeleteProjectCommandHandler(IProjectRepository projectRepository) {
        this._projectRepository = projectRepository;
    }

    public async Task<Unit> Handle(DeleteProjectCommand request, CancellationToken cancellationToken) {
        Project project = await this._projectRepository.GetDetailsByIdAsync(request.Id);
        project.Cancel();
        await this._projectRepository.SaveChangesAsync();
        return Unit.Value;
    }
}