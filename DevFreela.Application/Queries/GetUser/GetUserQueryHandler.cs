using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.GetUser;

public class GetUserQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserQuery, UserViewModel> {
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<UserViewModel> Handle(GetUserQuery request, CancellationToken cancellationToken) {
        User user = await this._userRepository.GetByIdAsync(request.Id);

        if (user is null) return null;

        return new(user.FullName, user.Email);
    }
}