using ETicket.Domain.Shared.ValueObjects;
using ETicket.SharedKernel;

namespace ETicket.Domain.Events;

public class Event : Entity, IAggregateRoot
{
    public string? Name { get; private set; }
    public string? Information { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public Address Address { get; private set ; }
}