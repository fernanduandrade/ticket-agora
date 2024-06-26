using System.ComponentModel.DataAnnotations.Schema;

namespace ETicket.SharedKernel;

public abstract class HasDomainEvents
{
    private readonly List<DomainEventBase> _domainEvents = new();
    [NotMapped]
    public IEnumerable<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();

    protected void RegisterDomainEvent(DomainEventBase domainEvent) => _domainEvents.Add(domainEvent);
    public void ClearDomainEvents() => _domainEvents.Clear();
}