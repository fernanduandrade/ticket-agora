using System.Reflection;
using ETicket.Application;
using ETicket.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ETicket.UseCases;

public static class DependecyInjection
{
    public static IServiceCollection AddUserCasesConfig(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        });
        return services;
    }
}