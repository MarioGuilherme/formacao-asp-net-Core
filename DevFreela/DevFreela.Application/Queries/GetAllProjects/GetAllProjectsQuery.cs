using DevFreela.Application.ViewModels;
using MediatR;

namespace DevFreela.Application.Queries.GetAllProjects;

public class GetAllProjectsQuery(string query) : IRequest<List<ProjectViewModel>> {
    public string Query { get; private set; } = query;
}