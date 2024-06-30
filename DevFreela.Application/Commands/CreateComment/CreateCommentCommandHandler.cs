using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.CreateComment;

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Unit> {
    private readonly DevFreelaDbContext _dbContext;

    public CreateCommentCommandHandler(DevFreelaDbContext dbContext) {
        this._dbContext = dbContext;
    }

    public async Task<Unit> Handle(CreateCommentCommand request, CancellationToken cancellationToken) {
        ProjectComment projectComment = new(request.Content, request.IdProject, request.IdUser);
        await this._dbContext.ProjectComments.AddAsync(projectComment);
        await this._dbContext.SaveChangesAsync();
        return Unit.Value;
    }
} 