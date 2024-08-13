using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ETicket.Domain.Users;
using ETicket.SharedKernel;
using ETicket.UseCases.Users.Login;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ETicket.UseCases.Users.RefreshToken;

public class RefreshTokenHandler(UserManager<User> userManager, IConfiguration configuration) : ICommandHandler<RefreshTokenCommand, RefreshTokenResponse>
{
    public async Task<RefreshTokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var claimsPrincipal = ReadRefreshToken(request.RefreshToken, configuration);

        if (claimsPrincipal is null)
            return null;

        var user = await userManager.FindByEmailAsync(claimsPrincipal.Identity.Name);

        if (user is null)
            return null;

        if (user.RefreshToken != request.RefreshToken || user.RefreshTokenLifeTime <= DateTime.Now)
            return null;

        var (accessToken, refreshToken) = LoginHandler.GetToken(user, configuration);
        
        user.SetRefresh(refreshToken);
        await userManager.UpdateAsync(user);

        return new RefreshTokenResponse(accessToken, refreshToken);

    }

    private ClaimsPrincipal ReadRefreshToken(string refreshToken, IConfiguration configuration)
    {
        var validateParameters = new TokenValidationParameters()
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["identityKey"])),
            ValidateLifetime = true
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var principal = tokenHandler
            .ValidateToken(refreshToken, validateParameters, out SecurityToken securityToken);
        
        return principal;
    }
}