namespace SportBet.Contracts.Authentication;
public record LoginRequest(string Username, string Password);

public record AuthenticationResult(string Token, int UserId);

