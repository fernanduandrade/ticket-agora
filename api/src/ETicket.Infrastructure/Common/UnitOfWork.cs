using ETicket.Application.Interfaces.Common;
using ETicket.Infrastructure.Persistence.Data;

namespace ETicket.Infrastructure.Common;

public class UnitOfWork(ApplicationDbContext applicationDbContext) : IUnitOfWork, IDisposable
{
    public async Task<bool> Commit(CancellationToken cancellationToken = default)
     => await applicationDbContext.SaveChangesAsync(cancellationToken) > 0;

    public void Dispose()
    {
        // Handled by EF CORE
    }
}