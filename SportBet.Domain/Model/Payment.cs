namespace SportBet.Domain.Model;

public class Payment
{
    public int PaymentId { get; set; }

    public int BetCardId { get; set; }

    public decimal Amount { get; set; }

    public DateTime PaymentDate { get; set; }
    public virtual BetCard BetCard { get; set; } = null!;
}