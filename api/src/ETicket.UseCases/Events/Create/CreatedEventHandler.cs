using ETicket.SharedKernel;

namespace ETicket.UseCases.Events.Create;

public class CreatedEventHandler : ICommandHandler<CreateEventCommand, int>
{
    public Task<int> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}