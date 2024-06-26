using ETicket.Infrastructure.Persistence.Data;
using ETicket.Infrastructure.Persistence.Data.Repositories;
using ETicket.Infrastructure.Persistence.Interceptors;
using ETicket.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ETicket.Infrastructure.Setup;

public static class PersistenceDiConfiguration
{
    public static IServiceCollection AddPersistenceConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("AAP_DB"), config =>
            {
                config.EnableRetryOnFailure(3, (TimeSpan.FromSeconds(1) * 2), null);
            });
        });
        
        services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
        
        return services;
    }

    public static IServiceCollection AddPersistenceInterceptorConfig(this IServiceCollection services)
    {
        services.AddScoped<PublishDomainEventsInterceptor>();
        services.AddScoped<AuditiableInterceptor>();
        return services;
    }
}