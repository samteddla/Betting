using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportBet.Domain.Model;
namespace SportBet.Infrastructure.Repositories.Configurations;

// BetResult
public class BetResultConfiguration : IEntityTypeConfiguration<BetResult>
{
        public void Configure(EntityTypeBuilder<BetResult> entity)
        {
                entity.ToTable("BetResult");
                entity.HasKey(e => e.BetResultId).HasName("PK_BetResult");
                entity.Property(e => e.Outcome).IsRequired();
                // entity.Property(e => e.ResultDate).HasColumnType("datetime");
                entity.HasIndex(e => e.MatchId, "IX_BetResult_MatchId");
                entity.HasIndex(e => e.MatchTypeId, "IX_BetResult_MatchType");
                entity.HasIndex(e => e.MatchSelectionId, "IX_BetResult_MatchSelectionId");
                entity.HasOne(d => d.MatchSelection).WithMany(p => p.BetResults)
                        .HasForeignKey(d => d.MatchSelectionId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_BetResult_MatchSelection");
                entity.HasOne(d => d.Match).WithMany(p => p.BetResults)
                        .HasForeignKey(d => d.MatchId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_BetResult_Match");
                entity.HasOne(d => d.BetMatchType).WithMany(p => p.BetResults)
                        .HasForeignKey(d => d.MatchTypeId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_BetResult_MatchType");
        }
}
