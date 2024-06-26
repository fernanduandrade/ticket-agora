using ETicket.SharedKernel;

namespace ETicket.Domain.Shared.ValueObjects;

public class Address : ValueObject
{
    public string Street { get; private set; }
    public int Number { get; private set; }
    public string? Complement { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string District { get; private set; }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}