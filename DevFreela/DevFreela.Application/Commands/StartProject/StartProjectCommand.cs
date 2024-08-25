using MediatR;

namespace DevFreela.Application.Commands.StartProject;

public class StartProjectCommand(int id) : IRequest<Unit> {
    public int Id { get; private set; } = id;
}