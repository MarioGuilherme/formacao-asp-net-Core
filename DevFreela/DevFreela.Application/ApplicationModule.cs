using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.InsertProject;
using DevFreela.Application.Consumers;
using DevFreela.Application.Validators;
using DevFreela.Application.ViewModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Application;

public static class ApplicationModule {
    public static IServiceCollection AddApplication(this IServiceCollection services) {
        services
            .AddHandlers()
            .AddValidation()
            .AddConsumers();

        return services;
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services) {
        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<CreateProjectCommand>());

        services.AddTransient<IPipelineBehavior<CreateProjectCommand, ResultViewModel<int>>, ValidateCreateProjectCommandBehavior>();

        return services;
    }

    private static IServiceCollection AddValidation(this IServiceCollection services) {
        services
            .AddFluentValidationAutoValidation()
            .AddValidatorsFromAssemblyContaining<CreateCommentCommandValidator>();

        return services;
    }

    private static IServiceCollection AddConsumers(this IServiceCollection services) {
        services.AddHostedService<PaymentApprovedConsumer>();

        return services;
    }
}