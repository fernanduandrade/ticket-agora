using System.Reflection;
using ETicket.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ETicket.Application;

public static class DependecyInjectionConfiguration
{
    public static IServiceCollection AddMediatorConfig(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        });
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        return services;
    }
}