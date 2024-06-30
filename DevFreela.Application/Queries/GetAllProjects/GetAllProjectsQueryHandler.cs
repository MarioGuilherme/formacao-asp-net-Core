using DevFreela.Application.ViewModels;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetAllProjects;

public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, List<ProjectViewModel>> {
    private readonly DevFreelaDbContext _dbContext;

    public GetAllProjectsQueryHandler(DevFreelaDbContext dbContext) {
        this._dbContext = dbContext;
    }

    public async Task<List<ProjectViewModel>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken) {
        var projects = this._dbContext.Projects;
        List<ProjectViewModel> projectViewModels = await projects
            .Select(p => new ProjectViewModel(p.Id, p.Title, p.CreatedAt)).ToListAsync();
        return projectViewModels;
    }
}