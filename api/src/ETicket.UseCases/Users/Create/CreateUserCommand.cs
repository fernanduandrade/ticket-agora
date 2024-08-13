using ETicket.SharedKernel;

namespace ETicket.UseCases.Users.Create;

public sealed record CreateUserCommand(string Name, string LastName, string Email, string Password, string UserName)
    : ICommand<bool> {};