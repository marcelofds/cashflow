using CashFlow.Application.Services;
using CashFlow.Domain.Aggregates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

[Produces("application/json")]
[Route("api/home")]
public class HomeController
{

    private readonly ITokenService _tokenService;
    private readonly IUserService _userService;

    public HomeController(IUserService userSerice, ITokenService tokenService)
    {
        _userService = userSerice;
        _tokenService = tokenService;
    }
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<dynamic>> Authenticate([FromBody] User user)
    {
        var token = _tokenService.GenerateToken(_userService.Get(user));
        return new {idToken = token};
    }
}