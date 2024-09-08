using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Consumers;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Application;

public static class ApplicationModule {
    public static IServiceCollection AddApplication(this IServiceCollection services) {
        services
            .AddMediatR(opt => opt.RegisterServicesFromAssemblyContaining(typeof(CreateProjectCommand))) // Já configura todos os Commands e Queries
            .AddConsumers();

        return services;
    }

    private static IServiceCollection AddConsumers(this IServiceCollection services) {
        services.AddHostedService<PaymentApprovedConsumer>();

        return services;
    }
}