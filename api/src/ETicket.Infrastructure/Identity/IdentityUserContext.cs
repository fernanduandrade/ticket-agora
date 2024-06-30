using ETicket.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ETicket.Infrastructure.Identity;

public class AppIdentityContext: IdentityDbContext<User, IdentityRole<long>, long>
{
    public AppIdentityContext(DbContextOptions<AppIdentityContext> identityContext)
        : base (identityContext) {}
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("identity");
        
    }
}