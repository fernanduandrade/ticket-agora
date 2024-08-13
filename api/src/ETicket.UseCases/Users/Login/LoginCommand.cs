using ETicket.SharedKernel;
using ETicket.UseCases.Shared.Responses;
using OneOf;

namespace ETicket.UseCases.Users.Login;

public sealed record LoginCommand(string Email, string Password)
    : ICommand<OneOf<LoginResponseDto, LoginResponses>> { }