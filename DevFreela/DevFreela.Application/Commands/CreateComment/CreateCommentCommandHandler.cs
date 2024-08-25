using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.CreateComment;

public class CreateCommentCommandHandler(IProjectRepository projectRepository) : IRequestHandler<CreateCommentCommand, Unit> {
    private readonly IProjectRepository _projectRepository = projectRepository;

    public async Task<Unit> Handle(CreateCommentCommand request, CancellationToken cancellationToken) {
        ProjectComment projectComment = new(request.Content, request.IdProject, request.IdUser);
        await this._projectRepository.AddCommentAsync(projectComment);
        return Unit.Value;
    }
} 