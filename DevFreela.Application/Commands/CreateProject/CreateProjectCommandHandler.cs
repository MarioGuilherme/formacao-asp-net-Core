using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.CreateProject;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, int> {
    private readonly DevFreelaDbContext _dbContext;

    public CreateProjectCommandHandler(DevFreelaDbContext dbContext) {
        this._dbContext = dbContext;
    }

    public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken) {
        Project project = new(request.Title, request.Description, request.IdClient, request.IdFreelancer, request.TotalCost);

        await this._dbContext.Projects.AddAsync(project);
        await this._dbContext.SaveChangesAsync();

        return project.Id;
    }
}