using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.StartProject;

public class StartProjectCommandHandler : IRequestHandler<StartProjectCommand, Unit> {
    private readonly IProjectRepository _projectRepository;

    public StartProjectCommandHandler(IProjectRepository projectRepository) {
        this._projectRepository = projectRepository;
    }

    // Versão em que é tirado a responsabilidade de negócio (start no proj) da camada repositório (Versão do Command).
    // Mas não é estritamente errado fazer o 'project.Start()' no repository. Obs.: Não usa-se 'this._projectRepository.SaveChangesAsync()' em vez do 'this._projectRepository.StartAsync(project)' pois aqui usamos Dapper
    public async Task<Unit> Handle(StartProjectCommand request, CancellationToken cancellationToken) {
        var project = await this._projectRepository.GetDetailsByIdAsync(request.Id);

        project.Start();

        await this._projectRepository.StartAsync(project);

        return Unit.Value;
    }
}
