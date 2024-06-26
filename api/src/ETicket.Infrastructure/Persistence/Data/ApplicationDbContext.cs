using System.Reflection;
using ETicket.Infrastructure.Persistence.Interceptors;
using ETicket.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace ETicket.Infrastructure.Persistence.Data;

public class ApplicationDbContext : DbContext
{
    private readonly PublishDomainEventsInterceptor _publishDomainEventsInterceptor;
    private readonly AuditiableInterceptor _auditiableInterceptor;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
        PublishDomainEventsInterceptor publishDomainEventsInterceptor,
        AuditiableInterceptor auditiableInterceptor)
        : base(options)
    {
        _publishDomainEventsInterceptor = publishDomainEventsInterceptor;
        _auditiableInterceptor = auditiableInterceptor;
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.Ignore<List<DomainEventBase>>().ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.HasDefaultSchema("public");
        base.OnModelCreating(builder);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_publishDomainEventsInterceptor);
        optionsBuilder.AddInterceptors(_auditiableInterceptor);
    }
}