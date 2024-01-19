using Microsoft.EntityFrameworkCore;
using SportBet.Domain.Model;

namespace SportBet.Infrastructure;

public class BetContext : DbContext
{
    public BetContext(DbContextOptions<BetContext> options) : base(options)
    {
    }
    public virtual DbSet<BetCard> BetCards { get; set; }
    public virtual DbSet<BetResult> BetResults { get; set; }
    public virtual DbSet<BetSelection> BetSelections { get; set; }
    public virtual DbSet<Match> Matches { get; set; }
    public virtual DbSet<MatchSelection> MatchSelections { get; set; }
    public virtual DbSet<MatchSelectionMatch> MatchSelectionMatches { get; set; }
    public virtual DbSet<BetMatchType> MatchTypes { get; set; }
    public virtual DbSet<Outcome> Outcomes { get; set; }
    public virtual DbSet<Person> People { get; set; }
    public virtual DbSet<Team> Teams { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            throw new Exception("DbContext is not configured");
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BetContext).Assembly);
    }
}
