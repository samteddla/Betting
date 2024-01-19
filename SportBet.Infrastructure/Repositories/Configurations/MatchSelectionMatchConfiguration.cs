using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportBet.Domain.Model;
namespace SportBet.Infrastructure.Repositories.Configurations;

// MatchSelectionMatch
public class MatchSelectionMatchConfiguration : IEntityTypeConfiguration<MatchSelectionMatch>
{
    public void Configure(EntityTypeBuilder<MatchSelectionMatch> entity)
    {
        entity.ToTable("MatchSelectionMatch");
        entity.HasKey(e => e.Id).HasName("PK_MatchSelectionMatch");
        entity.HasIndex(e => e.MatchId, "IX_MatchSelectionMatch_MatchId");
        entity.HasOne(d => d.Match)
            .WithMany(p => p.MatchSelectionMatches)
            .HasForeignKey(d => d.MatchId)
            .HasConstraintName("FK_MSM_Match");
        entity.HasOne(d => d.Selection)
            .WithMany(p => p.MatchSelectionMatches)
            .HasForeignKey(d => d.SelectionId)
            .HasConstraintName("FK_MSM_Selection");
    }
}
