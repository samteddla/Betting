namespace SportBet.Contracts.Cards;

public record MyBets(int BetCardId, int MatchSelectionId, int MatchTypeId, string MatchSelection, string MatchType);

public record MyBet(int BetCardId, int MatchId, string HomeTeam, string Home, string AwayTeam, string Away, string MatchType, int OutcomeId, string OutcomeName, DateTime CreatedAt, int MatchSelectionId, string MatchSelectionName, string MatchSelectionDescription);

public record MyBetExtende(int BetCardId,  DateTime CreatedAt, int MatchSelectionId, string MatchSelectionName, string MatchSelectionDescription,decimal BetAmount, decimal WonAmount, int TotalWinCount ,string MatchType, List<MyBetMatchExtend> Matches); 
public record MyBetMatchExtend(int MatchId, string HomeTeam, string Home, string AwayTeam, string Away,  int OutcomeId, string OutcomeName, int MatchResultId, string MatchResult);

public record BetOnGame(int SelectionId, int MatchTypeId, List<MatchRequest> Matches, decimal Amount);
public record BetOnGameResponse(string StatusName, bool IsSaved);

public record BetResultResponse(List<MatchResponse> Matches, decimal BetAmount, decimal WonAmount, int TotalWinCount, int CardId, int MatchSelectionId, int MatchTypeId);

public record MatchResponse(int MatchId, int OutcomeId, int MatchResultId, string MatchResult);

public record GetActiveMatchs(int MatchSelectionId, string Name, string Description, bool IsEnabled);
public record GetActiveMatch(int MatchSelectionId, string Name, string Description, DateTime ActiveUntil, List<SelectionMatchResponse> Matches);
public record SelectionMatchResponse(int MatchSelectionId, int MatchId, int HomeId, string Home, string HomeTeam, int AwayId, string AwayTeam, string Away, DateTime MatchDate);
public record GetMatchTypes(int MatchTypeId, string Name);
public record GetOutcomes(int Id, int OutcomeId, string Name, bool IsEnabled);
public record UpdateBetResult(string Message);
public record UpdateBetResultRequest(int MatchTypeId, int MatchSelectionId, int MatchId, int OutcomeId);
public record MatchRequest(int MatchId, int OutcomeId);

