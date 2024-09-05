using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.GetAllProjects;

public class GetAllProjectsQueryHandler(IProjectRepository projectRepository) : IRequestHandler<GetAllProjectsQuery, List<ProjectViewModel>> {
    private readonly IProjectRepository _projectRepository = projectRepository;

    public async Task<List<ProjectViewModel>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken) {
        List<Project> projects = await this._projectRepository.GetAllAsync(request.Query);
        List<ProjectViewModel> projectViewModels = projects
            .Select(p => new ProjectViewModel(p.Id, p.Title, p.CreatedAt))
            .ToList();
        return projectViewModels;
    }
}