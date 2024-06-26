using ETicket.Application.Interfaces.Services;
using ETicket.Infrastructure.Services;

namespace ETicket.API.Setup;

public static class ServicesDiConfig
{
    public static IServiceCollection AddAppServicesConfig(this IServiceCollection services)
    {
        services.AddScoped<IUserManagerService, UserManagerService>();
        return services;
    }
}