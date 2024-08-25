using DevFreela.Application.ViewModels;
using MediatR;

namespace DevFreela.Application.Queries.GetProjectById;

public class GetProjectByIdQuery(int id) : IRequest<ProjectDetailsViewModel> {
    public int Id { get; private set; } = id;
}