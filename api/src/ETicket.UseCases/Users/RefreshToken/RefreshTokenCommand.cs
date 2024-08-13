using ETicket.SharedKernel;

namespace ETicket.UseCases.Users.RefreshToken;

public sealed record RefreshTokenCommand(string RefreshToken)
    : ICommand<RefreshTokenResponse> { }
    public sealed record RefreshTokenResponse(string AccessToken, string RefreshToken);