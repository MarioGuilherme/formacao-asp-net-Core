﻿using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.UpdateProject;

public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Unit> {
    private readonly DevFreelaDbContext _dbContext;

    public UpdateProjectCommandHandler(DevFreelaDbContext dbContext) {
        this._dbContext = dbContext;
    }

    public async Task<Unit> Handle(UpdateProjectCommand request, CancellationToken cancellationToken) {
        Project project = this._dbContext.Projects.SingleOrDefault(p => p.Id == request.Id);
        project.Update(request.Title, request.Description, request.TotalCost);
        await this._dbContext.SaveChangesAsync();
        return Unit.Value;
    }
}