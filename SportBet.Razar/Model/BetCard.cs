using System;
using System.Collections.Generic;

namespace SportBet.Razar.Model;

public partial class BetCard
{
    public int BetCardId { get; set; }

    public int PersonId { get; set; }

    public int MatchSelectionId { get; set; }

    public int MatchTypeId { get; set; }

    public decimal BetAmount { get; set; }

    public decimal WonAmount { get; set; }

    public DateTime BetDate { get; set; }

    public virtual ICollection<BetSelection> BetSelections { get; set; } = new List<BetSelection>();

    public virtual MatchSelection MatchSelection { get; set; } = null!;

    public virtual BetMatchType MatchType { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Person Person { get; set; } = null!;

    public virtual ICollection<Reward> Rewards { get; set; } = new List<Reward>();
}
