using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace SportBet.Application.Authentication.User;
public sealed class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public int UserId => User?.UserId() ?? 0;
    public string Email => User?.Email() ?? string.Empty;
    public string UserName => User?.UserName() ?? string.Empty;
    public string Role => User?.Role() ?? string.Empty;
    public string Phone => User?.Phone() ?? string.Empty;
    private ClaimsPrincipal? User => !(_httpContextAccessor?.HttpContext?.User?.Identity?.IsAuthenticated).GetValueOrDefault()
            ? null : _httpContextAccessor?.HttpContext?.User;

}
