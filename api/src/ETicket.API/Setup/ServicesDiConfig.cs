using System.Text;
using ETicket.Application;
using ETicket.Application.Interfaces.Services;
using ETicket.Domain.Users;
using ETicket.Infrastructure.Identity;
using ETicket.Infrastructure.Services;
using ETicket.Infrastructure.Setup;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
        services.AddCors(options =>
            options.AddDefaultPolicy(builder => builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()));
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

    public static WebApplication InjectAppDepencies(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        return app;
    }

    public static IServiceCollection AddIdentityAuthConfig(this IServiceCollection services,
        IConfiguration configuration)
    {
        var securityKey = configuration["authKey"];
        services.AddAuthentication(options => {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options => {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = "https://localhost:7266",
                    ValidAudience = "https://localhost:7266",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey))
                };
            });
        services.AddAuthorization();
        services.AddIdentityApiEndpoints<User>().AddEntityFrameworkStores<AppIdentityContext>();
        
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