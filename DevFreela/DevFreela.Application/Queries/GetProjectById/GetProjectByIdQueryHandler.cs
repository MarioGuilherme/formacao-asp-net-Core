using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.GetProjectById;

public class GetProjectByIdQueryHandler(IProjectRepository projectRepository) : IRequestHandler<GetProjectByIdQuery, ProjectDetailsViewModel> {
    private readonly IProjectRepository _projectRepository = projectRepository;

    public async Task<ProjectDetailsViewModel> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken) {
        Project project = await this._projectRepository.GetDetailsByIdAsync(request.Id);

        if (project is null) return null;

        ProjectDetailsViewModel projectDetailsViewModel = new(
            project.Id,
            project.Title,
            project.Description,
            project.TotalCost,
            project.StartedAt,
            project.FinishedAt,
            project.Client.FullName,
            project.Freelancer.FullName
        );

        return projectDetailsViewModel;
    }
}
