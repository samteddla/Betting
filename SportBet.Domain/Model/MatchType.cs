namespace SportBet.Domain.Model;

public partial class BetMatchType
{
    public int MatchTypeId { get; set; }

    public string Name { get; set; } = null!;

    // public int Outcome { get; set; }

    public bool IsEnabled { get; set; }

    public virtual ICollection<BetCard> BetCards { get; set; } = new List<BetCard>();

    public virtual ICollection<BetResult> BetResults { get; set; } = new List<BetResult>();
}
