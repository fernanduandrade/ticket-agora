using ETicket.Application.Behaviors;
using ETicket.SharedKernel;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ETicket.Application;

public static class DependecyInjectionConfiguration
{
    public static IServiceCollection AddMediatorConfig(this IServiceCollection services)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        return services;
    }
}