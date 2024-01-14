using CashFlow.Application.DataTransferObjects;
using CashFlow.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.WebApi.Controllers;

/// <summary>
///     Identity management endpoint
/// </summary>
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

    /// <summary>
    ///     Enables a user to authenticate in the app
    /// </summary>
    /// <param name="user">The user data: username and password</param>
    /// <returns>JWT Token if the user data was matched in database</returns>
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<dynamic>> Authenticate([FromBody] UserDto user)
    {
        var token = _tokenService.GenerateToken(_userService.Get(user));
        return new {idToken = token};
    }
}