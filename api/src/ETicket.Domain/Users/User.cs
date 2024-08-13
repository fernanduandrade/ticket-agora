using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ETicket.Domain.Users;

[Table(("AspNetUsers"))]
public class User : IdentityUser<long>
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string? RefreshToken {get; set;}
    public DateTime? RefreshTokenLifeTime {get ; set;}


    public void SetRefresh(string refreshToken)
    {
        RefreshToken = refreshToken;
        RefreshTokenLifeTime = DateTime.Now.AddMinutes(50);
    }
}