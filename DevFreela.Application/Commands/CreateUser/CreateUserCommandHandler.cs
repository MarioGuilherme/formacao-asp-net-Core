using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int> {
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository) {
        this._userRepository = userRepository;
    }

    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken) {
        var user = new User(request.FullName, request.Email, request.BirthDate);

        int id = await this._userRepository.CreateUserAsync(user);

        return id;
    }
}