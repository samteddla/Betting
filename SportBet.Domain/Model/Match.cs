namespace SportBet.Domain.Model;

public partial class Match
{
    public int MatchId { get; set; }

    public int HomeTeamId { get; set; }

    public int AwayTeamId { get; set; }

    public int HomeScore { get; set; }

    public int AwayScore { get; set; }

    public DateTime MatchDate { get; set; }

    public virtual Team AwayTeam { get; set; } = null!;

    public virtual ICollection<BetResult> BetResults { get; set; } = new List<BetResult>();

    public virtual ICollection<BetSelection> BetSelections { get; set; } = new List<BetSelection>();

    public virtual Team HomeTeam { get; set; } = null!;

    public virtual ICollection<MatchSelectionMatch> MatchSelectionMatches { get; set; } = new List<MatchSelectionMatch>();
}
