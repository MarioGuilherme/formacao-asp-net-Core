using DevFreela.Application.ViewModels;
using MediatR;

namespace DevFreela.Application.Queries.GetUser;

public class GetUserQuery(int id) : IRequest<UserViewModel> {
    public int Id { get; private set; } = id;
}