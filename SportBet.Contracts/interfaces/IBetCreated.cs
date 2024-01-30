namespace SportBet.Contracts.Interfaces;

public interface IBetCreated
{
    DateTime ResultDate { get; }
    int MatchTypeId { get; }
    int MatchSelectionId { get; }
    int MatchId { get; }
}
