using ErrorOr;
using Microsoft.EntityFrameworkCore;
using SportBet.Contracts.Authentication;
using SportBet.Infrastructure;

namespace SportBet.Application;

public class AuthenticationService : IAuthenticationService
{
    private readonly BetContext _context;
    private readonly ITokenService _tokenService;

    public AuthenticationService(BetContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }
    public async Task<ErrorOr<AuthenticationResult>> AuthenticateAsync(string username, string password)
    {
        var user = await _context.People.Where(u => u.UserName == username).FirstOrDefaultAsync();

        if (user == null)
        {
            return Error.Unauthorized(code: "USER_NOT_FOUND", description: "Username or password is incorrect");
        }

        var token = _tokenService.CreateToken(user);

        return ErrorOr.ErrorOr.From(new AuthenticationResult(token, user.PersonId));
    }
}
