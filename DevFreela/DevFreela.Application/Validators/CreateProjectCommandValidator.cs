using DevFreela.Application.Commands.CreateProject;
using FluentValidation;

namespace DevFreela.Application.Validators;

public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand> {
    public CreateProjectCommandValidator() {
        this.RuleFor(p => p.Description)
            .MaximumLength(255)
            .WithMessage("Tamanho máximo de Descrição é de 255 caracteres.");

        this.RuleFor(p => p.Title)
            .MaximumLength(30)
            .WithMessage("Tamanho máximo de Título é de 30 caracteres.");
    }
}