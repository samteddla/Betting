using SportBet.Domain.Model;

namespace SportBet.Application;

public interface ITokenService
{
    string CreateToken(Person person);
}

public static class CustomClaimTypes
{
    public const string Scope = "my-scope";
}

public static class Scopes
{
    public const string CanRead = "canRead";
    public const string CanWrite = "canWrite";
    public const string CanDelete = "canDelete";
}
