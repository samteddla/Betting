namespace SportBet.Domain.Model;

public partial class Person
{
    public int PersonId { get; set; }

    public string UserName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string MiddleName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Roles { get; set; } = null!;

    public decimal Balance { get; set; }

    public bool IsEnabled { get; set; }

    public DateTime RegistrationDate { get; set; }

    public virtual ICollection<BetCard> BetCards { get; set; } = new List<BetCard>();
}
