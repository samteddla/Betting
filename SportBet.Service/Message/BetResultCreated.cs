namespace SportBet.Service.Message;
public class BetResultCreated : BetResult
{
    public int MatchTypeId { get; set;}
    public int MatchSelectionId { get; set;}
    public int MatchId { get; set;}
    public int MatchResultId { get; set;}
}