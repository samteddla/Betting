using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportBet.Domain.Model;

namespace SportBet.Infrastructure.Repositories.Configurations;


// BetCard
public class BetCardConfiguration : IEntityTypeConfiguration<BetCard>
{
    public void Configure(EntityTypeBuilder<BetCard> entity)
    {
        entity.ToTable("BetCard");
        entity.HasKey(e => e.BetCardId).HasName("PK_BetCard");
        entity.HasIndex(e => e.MatchSelectionId, "IX_BetCard_MatchSelectionId");
        entity.HasIndex(e => e.MatchTypeId, "IX_BetCard_MatchType");
        entity.HasIndex(e => e.PersonId, "IX_BetCard_PersonId");
        entity.Property(e => e.BetAmount).HasColumnType("decimal(10, 2)");
        entity.Property(e => e.WonAmount).HasColumnType("decimal(10, 2)");
        entity.Property(e => e.BetDate).HasColumnType("datetime");
        entity.HasOne(d => d.MatchSelection).WithMany(p => p.BetCards)
            .HasForeignKey(d => d.MatchSelectionId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_BetCard_MatchSelection");
        entity.HasOne(d => d.BetMatchType).WithMany(p => p.BetCards)
            .HasForeignKey(d => d.MatchTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_BetCard_MatchType");
        entity.HasOne(d => d.Person).WithMany(p => p.BetCards)
            .HasForeignKey(d => d.PersonId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_BetCard_Person");
    }
}
