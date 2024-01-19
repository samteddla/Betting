using System;
using System.Collections.Generic;

namespace SportBet.Razar.Model;

public partial class MatchSelection
{
    public int MatchSelectionId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool IsEnabled { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime ActiveUntil { get; set; }

    public virtual ICollection<BetCard> BetCards { get; set; } = new List<BetCard>();

    public virtual ICollection<BetResult> BetResults { get; set; } = new List<BetResult>();

    public virtual ICollection<MatchSelectionMatch> MatchSelectionMatches { get; set; } = new List<MatchSelectionMatch>();
}
