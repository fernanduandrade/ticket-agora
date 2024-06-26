namespace ETicket.Application.Interfaces.Common;

public interface IUnitOfWork
{
    Task<bool> Commit(CancellationToken cancellationToken = default);
}