using System;
using System.Collections.Generic;

namespace SportBet.Razar.Model;

public partial class BetSelection
{
    public int BetSelectionId { get; set; }

    public int BetCardId { get; set; }

    public int MatchId { get; set; }

    public int Outcome { get; set; }

    public virtual BetCard BetCard { get; set; } = null!;

    public virtual Match Match { get; set; } = null!;
}
