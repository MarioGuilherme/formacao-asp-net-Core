using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.FinishProject;

public class FinishProjectCommandHandler : IRequestHandler<FinishProjectCommand, Unit> {
    private readonly IProjectRepository _projectRepository;

    public FinishProjectCommandHandler(IProjectRepository projectRepository) {
        this._projectRepository = projectRepository;
    }

    public async Task<Unit> Handle(FinishProjectCommand request, CancellationToken cancellationToken) {
        Project project = await this._projectRepository.GetDetailsByIdAsync(request.Id);
        project.Finish();
        await this._projectRepository.SaveChangesAsync();
        return Unit.Value;
    }
}
