using DevFreela.Core.Auth;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Auth;
using MediatR;

namespace DevFreela.Application.Commands.CreateUser;

public class CreateUserCommandHandler(IUserRepository userRepository, IAuthService authService) : IRequestHandler<CreateUserCommand, int> {
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IAuthService _authService = authService;

    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken) {
        string passwordHash = this._authService.ComputeSha256Hash(request.Password);

        User user = new(request.FullName, request.Email, request.BirthDate, passwordHash, request.Role);

        int id = await this._userRepository.CreateUserAsync(user);

        return id;
    }
}