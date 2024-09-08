using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Auth;
using MediatR;

namespace DevFreela.Application.Commands.LoginUser;

public class LoginUserCommandHandler(IAuthService authService, IUserRepository userRepository) : IRequestHandler<LoginUserCommand, LoginUserViewModel> {
    private readonly IAuthService _authService = authService;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken) {
        // Utilizar o mesmo algoritmo para criar o hash dessa senha
        string passwordHash = this._authService.ComputeSha256Hash(request.Password);

        // Buscar no banco de dados um User que tenha meu e-mail e senha em formato hash
        User user = await this._userRepository.GetUserByEmailAndPasswordAsync(request.Email, passwordHash);

        // Se não existir, erro no login
        if (user is null) return null;

        // Se existir, gero o token usando os dados do usuário
        string token = this._authService.GenerateJwtToken(user.Email, user.Role);
        return new(request.Email, token);
    }
}