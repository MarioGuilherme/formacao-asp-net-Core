using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Core.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.GetAllProjects;

public class GetAllProjectsQueryHandler(IProjectRepository projectRepository) : IRequestHandler<GetAllProjectsQuery, PaginationResult<ProjectViewModel>> {
    private readonly IProjectRepository _projectRepository = projectRepository;

    public async Task<PaginationResult<ProjectViewModel>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken) {
        PaginationResult<Project> paginationProjects = await this._projectRepository.GetAllAsync(request.Query, request.Page);
        List<ProjectViewModel> projectViewModels = paginationProjects
            .Data
            .Select(p => new ProjectViewModel(p.Id, p.Title, p.CreatedAt))
            .ToList();

        PaginationResult<ProjectViewModel> paginationProjectViewModel = new(
            paginationProjects.CurrentPage,
            paginationProjects.TotalPages,
            paginationProjects.PageSize,
            paginationProjects.ItemsCount,
            projectViewModels
        );

        return paginationProjectViewModel;
    }
}