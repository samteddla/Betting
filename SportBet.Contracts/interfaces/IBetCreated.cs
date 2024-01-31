namespace SportBet.Contracts.Interfaces;

public interface IBetCreated
{
    int MatchTypeId { get; }
    int MatchSelectionId { get; }
    int MatchId { get; }
    int OutcomeId { get; }
    DateTime ResultDate { get; }
}
