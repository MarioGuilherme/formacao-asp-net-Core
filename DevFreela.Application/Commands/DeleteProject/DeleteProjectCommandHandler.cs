using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.DeleteProject;

public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, Unit> {
    private readonly DevFreelaDbContext _dbContext;

    public DeleteProjectCommandHandler(DevFreelaDbContext dbContext) {
        this._dbContext = dbContext;
    }

    public async Task<Unit> Handle(DeleteProjectCommand request, CancellationToken cancellationToken) {
        Project project = this._dbContext.Projects.SingleOrDefault(p => p.Id == request.Id);
        project.Cancel();
        await this._dbContext.SaveChangesAsync();
        return Unit.Value;
    }
}