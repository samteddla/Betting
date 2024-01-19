namespace SportBet.Domain.Model;

public partial class MatchSelectionMatch
{
    public int Id { get; set; }

    public int SelectionId { get; set; }

    public int MatchId { get; set; }

    public virtual Match Match { get; set; } = null!;

    public virtual MatchSelection Selection { get; set; } = null!;
}
