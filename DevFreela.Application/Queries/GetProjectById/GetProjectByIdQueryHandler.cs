using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetProjectById;

public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDetailsViewModel> {
    private readonly DevFreelaDbContext _dbContext;

    public GetProjectByIdQueryHandler(DevFreelaDbContext dbContext) {
        _dbContext = dbContext;
    }

    public async Task<ProjectDetailsViewModel> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken) {
        Project project = await this._dbContext.Projects
            .Include(p => p.Client)
            .Include(p => p.Freelancer)
            .SingleOrDefaultAsync(p => p.Id == request.Id);

        if (project == null) return null;

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
