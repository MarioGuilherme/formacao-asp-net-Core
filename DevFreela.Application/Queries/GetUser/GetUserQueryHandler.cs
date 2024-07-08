using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.GetUser;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserViewModel> {
    private readonly IUserRepository _userRepository;

    public GetUserQueryHandler(IUserRepository userRepository) {
        this._userRepository = userRepository;
    }

    public async Task<UserViewModel> Handle(GetUserQuery request, CancellationToken cancellationToken) {
        var user = await this._userRepository.GetByIdAsync(request.Id);

        if (user == null) {
            return null;
        }

        return new UserViewModel(user.FullName, user.Email);
    }
}