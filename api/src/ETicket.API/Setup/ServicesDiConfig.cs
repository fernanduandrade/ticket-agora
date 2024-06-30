using ETicket.Application;
using ETicket.Application.Interfaces.Services;
using ETicket.Infrastructure.Services;
using ETicket.Infrastructure.Setup;
using Microsoft.AspNetCore.Mvc;

namespace ETicket.API.Setup;

public static class ServicesDiConfig
{
    public static IServiceCollection AddAppServicesConfig(this IServiceCollection services)
    {
        services.AddScoped<IUserManagerService, UserManagerService>();
        return services;
    }

    public static IServiceCollection AddApiBehaviors(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.ConfigureHttpClientDefaults(http =>
        {
            http.AddStandardResilienceHandler();
            http.AddServiceDiscovery();
        });
        
        services.AddControllers();
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
        return services;
    }


    public static IServiceCollection InjectDependecies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatorConfig()
            .AddAppServicesConfig()
            .AddIdentityConfig(configuration)
            .AddPersistenceInterceptorConfig()
            .AddPersistenceConfig(configuration)
            .AddApiBehaviors();
        
        return services;
    }
}