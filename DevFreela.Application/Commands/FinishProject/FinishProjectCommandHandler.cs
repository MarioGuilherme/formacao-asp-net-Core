using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.FinishProject;

public class FinishProjectCommandHandler : IRequestHandler<FinishProjectCommand, Unit> {
    private readonly DevFreelaDbContext _dbContext;

    public FinishProjectCommandHandler(DevFreelaDbContext dbContext) {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(FinishProjectCommand request, CancellationToken cancellationToken) {
        Project project = await this._dbContext.Projects.SingleOrDefaultAsync(p => p.Id == request.Id);
        project.Finish();
        this._dbContext.SaveChanges();
        return Unit.Value;
    }
}
