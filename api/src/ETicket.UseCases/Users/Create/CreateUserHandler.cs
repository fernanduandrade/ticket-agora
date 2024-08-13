using ETicket.Domain.Users;
using ETicket.SharedKernel;
using Microsoft.AspNetCore.Identity;

namespace ETicket.UseCases.Users.Create;

public sealed class CreateUserHandler(UserManager<User> userManager) : ICommandHandler<CreateUserCommand, bool>
{
    public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is not null)
            return false;
 
        var newUser = new User()
        {
            UserName = request.UserName,
            Name = request.Name,
            LastName = request.LastName,
            PasswordHash = request.Password,
            Email = request.Email,
        };

        var create = await userManager.CreateAsync(newUser, request.Password);
        if (!create.Succeeded)
            return false;

        return true;
    }
}