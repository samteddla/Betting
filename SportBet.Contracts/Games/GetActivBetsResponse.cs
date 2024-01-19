namespace SportBet.Contracts.Games;

public record GetActivBetsResponse(int Id, string Name, string Description, string TypeName, bool IsEnabled);