using ETicket.Domain.Shared.ValueObjects;

namespace ETicket.Domain.Tickets;

public class Ticket
{
    public string Name { get; private set; }
    public string Information { get; private set; }
    public Address Address { get; private set; }
    public int Capacity { get; private set; }
    public int Batch { get; private set; }
    public bool Available { get; private set; }
}