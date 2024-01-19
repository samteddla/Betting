using System;
using System.Collections.Generic;

namespace SportBet.Razar.Model;

public partial class Reward
{
    public int RewardId { get; set; }

    public int BetCardId { get; set; }

    public decimal Amount { get; set; }

    public DateTime RewardDate { get; set; }

    public virtual BetCard BetCard { get; set; } = null!;
}
