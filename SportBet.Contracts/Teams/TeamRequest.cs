namespace SportBet.Contracts.Teams;
public record TeamRequest();
public record TeamResponse(int Id, string Name, string ShortName);

public record SaveResponse(string StatusName, bool IsSaved);