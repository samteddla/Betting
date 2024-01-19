using System;
using System.Collections.Generic;

namespace SportBet.Razar.Model;

public partial class Outcome
{
    public int Id { get; set; }

    public int OutcomeId { get; set; }

    public string Name { get; set; } = null!;

    public bool IsEnabled { get; set; }
}
