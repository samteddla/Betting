using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportBet.Domain.Model;
namespace SportBet.Infrastructure.Repositories.Configurations;

// Match
public class MatchConfiguration : IEntityTypeConfiguration<Match>
{
    public void Configure(EntityTypeBuilder<Match> entity)
    {
        entity.ToTable("Match");
        entity.HasKey(e => e.MatchId).HasName("PK_Match");
        entity.HasIndex(e => e.AwayTeamId, "IX_Match_AwayTeamId");
        entity.HasIndex(e => e.HomeTeamId, "IX_Match_HomeTeamId");
        entity.Property(e => e.MatchDate).HasColumnType("datetime");

        entity.HasOne(d => d.AwayTeam).WithMany(p => p.MatchAwayTeams)
            .HasForeignKey(d => d.AwayTeamId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Match_AwayTeam");

        entity.HasOne(d => d.HomeTeam).WithMany(p => p.MatchHomeTeams)
            .HasForeignKey(d => d.HomeTeamId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Match_HomeTeam");
    }
}
