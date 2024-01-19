

using ErrorOr;
using SportBet.Contracts.Authentication;

namespace SportBet.Application;

public interface IAuthenticationService
{
    Task<ErrorOr<AuthenticationResult>> AuthenticateAsync(string username, string password);
}
