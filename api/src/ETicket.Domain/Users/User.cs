using Microsoft.AspNetCore.Identity;

namespace ETicket.Domain.Users;


public class User : IdentityUser<long>
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string? RefreshToken {get; set;}
    public DateTime? RefreshTokenLifeTime {get ; set;} = DateTime.UtcNow;
}