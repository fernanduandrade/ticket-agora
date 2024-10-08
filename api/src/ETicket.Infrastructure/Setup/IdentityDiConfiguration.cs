using ETicket.Infrastructure.Identity;
using ETicket.Infrastructure.Persistence;
using ETicket.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ETicket.Infrastructure.Setup;

public static class IdentityDiConfiguration
{
    public static IServiceCollection AddIdentityConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppIdentityContext>(options =>
        {
            options.UseNpgsql("Host=localhost;Port=5432;User Id=postgres;Password=postgres;Database=postgres", config =>
            {
                config.EnableRetryOnFailure(3, (TimeSpan.FromSeconds(1) * 2), null);
            });
        });
        
        return services;
    }
}