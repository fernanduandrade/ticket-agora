using ETicket.Domain.Users;

namespace ETicket.UseCases.Users.Login;

public sealed record LoginResponseDto(
    string Token, string RefreshToken, string Message, bool Status, User user);