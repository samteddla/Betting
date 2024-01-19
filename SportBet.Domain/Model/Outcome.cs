namespace SportBet.Domain.Model;

public partial class Outcome
{
    public int Id { get; set; }

    public int OutcomeId { get; set; }

    public string Name { get; set; } = null!;

    public bool IsEnabled { get; set; }
}
