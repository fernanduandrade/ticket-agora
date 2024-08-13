using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ETicket.Domain.Users;
using ETicket.SharedKernel;
using ETicket.UseCases.Shared.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OneOf;

namespace ETicket.UseCases.Users.Login;

public class LoginHandler(UserManager<User> userManager, IConfiguration configuration)
    : ICommandHandler<LoginCommand, OneOf<LoginResponseDto, LoginResponses>>
{
    public async Task<OneOf<LoginResponseDto, LoginResponses>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);

        if (user is null)
            return LoginResponses.NotFound;

        bool checkPassword = await userManager.CheckPasswordAsync(user, request.Password);

        if (!checkPassword)
            return LoginResponses.WrongPassword;
        
        var (token, refreshToken) = GetToken(user, configuration);

        user.SetRefresh(refreshToken);
        await userManager.UpdateAsync(user);
        return new LoginResponseDto(token, refreshToken, "OK", true, user);
    }

    public static Tuple<string, string> GetToken(User user, IConfiguration configuration)
    {
        string appHost = configuration["appHost"];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("a13e18ab8c8607457f7d8f8a818e731fac47a6099fbeb5e868440666bed9e76a"));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.UserName)
        };

        var token = new JwtSecurityToken(
            issuer: appHost,
            audience: appHost,
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials
        );
        
        var refresh = new JwtSecurityToken(
            issuer: appHost,
            audience: appHost,
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: credentials
        );

        var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
        var refreshToken = new JwtSecurityTokenHandler().WriteToken(refresh);
        return new Tuple<string, string>(accessToken, refreshToken);
    }
}