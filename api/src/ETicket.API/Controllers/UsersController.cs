using ETicket.API.Controllers.Base;
using ETicket.UseCases.Users.Create;
using ETicket.UseCases.Users.Login;
using ETicket.UseCases.Users.RefreshToken;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ETicket.API.Controllers;

public class UsersController : BaseController
{
    [HttpPost("register")]
    public async Task<IActionResult> Login([FromBody] CreateUserCommand command)
    {
        var result = await Mediator.Send(command);
        if(!result)
            return BadRequest(result);
        
        // TODO disparar evento para confirmar email

        return Ok(new { message = "Criado com sucesso", success = true });
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var result = await Mediator.Send(command);

        return result.Match<IActionResult>(
            user => Ok(new { data = user, hasError = false, message = "OK" }),
            error => BadRequest(new { data = Unit.Value, hasError = true, message = nameof(error) }));
        
    }
    
    [HttpPost("refresh")]
    public async Task<IActionResult> Login([FromBody] RefreshTokenCommand command)
    {
        var result = await Mediator.Send(command);
        if (result is null)
            return BadRequest();

        return Ok(result); 
    }
}

// inativar - conta inativada é deletada após 1 mês 
// deletar a conta

// refreshtoken