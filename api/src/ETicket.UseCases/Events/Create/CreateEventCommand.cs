using ETicket.SharedKernel;

namespace ETicket.UseCases.Events.Create;

public sealed record CreateEventCommand(string Name, string Information, DateTime StartDate, DateTime? EndDate)
    : ICommand<int>;