namespace SportBet.Contracts.Selection;

public record CreateMatchSelectionsRequest(string Name, string Description, DateTime ActiveUntil, IEnumerable<int> Matches, IEnumerable<int> MatchesTypes);

public record CreateMatchSelectionsResponse(int MatchSelectionId, string StatusName, bool IsSaved);