namespace SportBet.Domain.Model;

public partial class BetResult
{
    public int BetResultId { get; set; }
    public int MatchId { get; set; }
    public int MatchSelectionId { get; set; }
    public int MatchTypeId { get; set; }
    public int Outcome { get; set; }
    public DateTime ResultDate { get; set; }
    public virtual MatchSelection MatchSelection { get; set; } = null!;
    public virtual Match Match { get; set; } = null!;
    public virtual BetMatchType BetMatchType { get; set; } = null!;
}
