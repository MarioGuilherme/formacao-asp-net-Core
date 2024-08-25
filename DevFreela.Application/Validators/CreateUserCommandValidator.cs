using DevFreela.Application.Commands.CreateUser;
using FluentValidation;
using System.Text.RegularExpressions;

namespace DevFreela.Application.Validators;

public partial class CreateUserCommandValidator : AbstractValidator<CreateUserCommand> {
    public CreateUserCommandValidator() {
        this.RuleFor(u => u.Email)
            .EmailAddress()
            .WithMessage("E-mail inválido.");

        this.RuleFor(u => u.Password)
            .Must(ValidPassword)
            .WithMessage("A senha deve conter pelo menos 8 caracteres, um número, uma letra maiúscula, uma minúscula e um caractere especial.");

        this.RuleFor(u => u.FullName)
            .NotEmpty()
            .NotNull()
            .WithMessage("Nome é obrigatório.");
    }

    public static bool ValidPassword(string password) {
        Regex regex = RegexEmail();
        return regex.IsMatch(password);
    }

    [GeneratedRegex(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=].*$)")]
    private static partial Regex RegexEmail();
}