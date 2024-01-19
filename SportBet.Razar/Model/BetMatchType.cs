using System;
using System.Collections.Generic;

namespace SportBet.Razar.Model;

public partial class BetMatchType
{
    public int MatchTypeId { get; set; }

    public string Name { get; set; } = null!;

    public bool IsEnabled { get; set; }

    public virtual ICollection<BetCard> BetCards { get; set; } = new List<BetCard>();

    public virtual ICollection<BetResult> BetResults { get; set; } = new List<BetResult>();
}
