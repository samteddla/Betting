using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportBet.Domain.Model;
namespace SportBet.Infrastructure.Repositories.Configurations;

// BetSelection
public class BetSelectionConfiguration : IEntityTypeConfiguration<BetSelection>
{
    public void Configure(EntityTypeBuilder<BetSelection> entity)
    {
        entity.ToTable("BetSelection");
        entity.HasKey(e => e.BetSelectionId).HasName("PK_BetSelection");
        entity.HasIndex(e => e.BetCardId, "IX_BetSelection_BetCardId");
        entity.HasIndex(e => e.MatchId, "IX_BetSelection_MatchId");
        entity.HasOne(d => d.BetCard).WithMany(p => p.BetSelections)
                .HasForeignKey(d => d.BetCardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BetSelection_BetCard");
        entity.HasOne(d => d.Match).WithMany(p => p.BetSelections)
                .HasForeignKey(d => d.MatchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BetSelection_Match");

    }
}
