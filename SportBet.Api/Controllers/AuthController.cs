using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportBet.Application;
using SportBet.Application.Users;
using SportBet.Contracts.Authentication;
using SportBet.Contracts.Users;

namespace SportBet.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ApiController
{
    private readonly IAuthenticationService _authenticationService;

    public AuthController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [AllowAnonymous]
    [HttpPost(Name = "Login")]
    public async Task<ActionResult<AuthenticationResult>> Login([FromBody] LoginRequest request)
    {
        var result = await _authenticationService.AuthenticateAsync(request.Username, request.Password);

        return result.Match(
            base.Ok,
            Problem
        );
    }

    [Authorize]
    [HttpPost("add-user")]
    public async Task<ActionResult<AddUserResponse>> AddUser([FromBody] AddUserRequest request)
    {
        var user = await Sender.Send(new AddUserCommand(request.Username, request.Password, request.FirstName, request.MiddleName, request.LastName, request.PhoneNumber, request.Email));

        return user.Match(
           base.Ok,
           Problem);
    }

    [Authorize]
    [HttpPut("update-user")]
    public async Task<ActionResult<UpdateUserResponse>> UpdateUser([FromBody] UpdateUserRequest request)
    {
        var user = await Sender.Send(new UpdateUserCommand(request.Username, request.Password, request.FirstName, request.MiddleName, request.LastName, request.PhoneNumber, request.Email));

        return user.Match(
           base.Ok,
           Problem);
    }

    // change user's password
    [Authorize]
    [HttpPut("change-password")]
    public async Task<ActionResult<ChangePasswordResponse>> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        var user = await Sender.Send(new ChangePasswordCommand(request.Password, request.NewPassword));

        return user.Match(
           base.Ok,
           Problem);
    }
}
