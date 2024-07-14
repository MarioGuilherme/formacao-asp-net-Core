using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using MediatR;

namespace DevFreela.Application.Commands.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel> {
    private readonly IAuthService _authService;
    private readonly IUserRepository _userRepository;

    public LoginUserCommandHandler(IAuthService authService, IUserRepository userRepository) {
        this._authService = authService;
        this._userRepository = userRepository;
    }

    public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken) {
        // Utilizar o mesmo algoritmo para criar o hash dessa senha
        var passwordHash = this._authService.ComputeSha256Hash(request.Password);

        // Buscar no banco de dados um User que tenha meu e-mail e senha em formato hash
        var user = await this._userRepository.GetUserByEmailAndPasswordAsync(request.Email, passwordHash);

        // Se não existir, erro no login
        if (user is null)
            return null;

        // Se existir, gero o token usando os dados do usuário
        var token = this._authService.GenerateJwtToken(user.Email, user.Role);
        return new LoginUserViewModel(request.Email, token);
    }
}