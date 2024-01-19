using System.Security.Claims;

namespace SportBet.Application.Authentication.User;

public static class ClaimsPrincipalExtensions
{
    public static int UserId(this ClaimsPrincipal principal) => SafeCast(principal.First(BetClaimTypes.UserId));
    public static string UserName(this ClaimsPrincipal principal) => principal.First(BetClaimTypes.UserName);
    public static string Phone(this ClaimsPrincipal principal) => principal.First(BetClaimTypes.Phone);
    public static string Email(this ClaimsPrincipal principal) => principal.First(ClaimTypes.Email);
    public static string Role(this ClaimsPrincipal principal) => principal.First(ClaimTypes.Role);
    private static string First(this ClaimsPrincipal self, string key) => self.FindFirst(key)?.Value!;

    private static int SafeCast(string? value)
    {
        if (int.TryParse(value, out var result))
        {
            return result;
        }
        return 0;
    }
}