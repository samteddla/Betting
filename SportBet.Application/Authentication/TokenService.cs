using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SportBet.Application.Authentication.User;
using SportBet.Domain.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SportBet.Application;

public class TokenService : ITokenService
{
    private readonly JwtSettings _jwtSettings;

    public TokenService(IOptions<JwtSettings> jwtOptions)
    {
        _jwtSettings = jwtOptions.Value;
    }

    public string CreateToken(Person person)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
            SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, person.PersonId.ToString()),
            new (ClaimTypes.Name, person.UserName),
            new (ClaimTypes.Email, person.Email),
            new (BetClaimTypes.UserId, person.PersonId.ToString()),
            new (BetClaimTypes.Phone, person.PhoneNumber),
            new (BetClaimTypes.UserName, person.UserName)
        };
        var roles = person.Roles.Split(',');
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        claims.Add(new Claim(CustomClaimTypes.Scope, Scopes.CanWrite));

        var securityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryIn),
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }

}